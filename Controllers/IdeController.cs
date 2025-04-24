using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using NuGet.Protocol.Plugins;

namespace NebulaTextbook.Controllers
{
    [Route("api/ide")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    [AllowAnonymous]
    public class IdeController : ControllerBase
    {
        public class RunCodeRequest
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("compiler")]
            public string Compiler { get; set; }
        }

        public class WandboxResponse
        {
            public string compiler_output { get; set; }
            public string compiler_error { get; set; }
            public string program_output { get; set; }
            public string program_error { get; set; }
            public string signal { get; set; }
            public int? status { get; set; }
        }

        /// <summary>
        /// Endpoint to run code via Wandbox API
        /// </summary>
        [HttpPost("run-wandbox")]
        public async Task<IActionResult> RunWandboxAsync([FromBody] RunCodeRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Code))
            {
                Console.WriteLine("Wandbox Request: Invalid Request");
                return BadRequest(new { output = "Error: Invalid Request" });
            }

            var payload = new
            {
                code = request.Code,
                compiler = request.Compiler ?? "gcc-head",
                options = "warning,gnu++20",
                stdin = "",
                compiler_option_raw = "",
                runtime_option_raw = "",
                save = false
            };

            var jsonPayload = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    Console.WriteLine("Sending request to Wandbox...");
                    HttpResponseMessage response = await httpClient.PostAsync("https://wandbox.org/api/compile.json", jsonPayload);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Wandbox API Error: {response.StatusCode} - {response.ReasonPhrase}");
                        return StatusCode((int)response.StatusCode, new { output = $"Error: HTTP {response.StatusCode}" });
                    }

                    string result = await response.Content.ReadAsStringAsync();
                    var resultObject = JsonSerializer.Deserialize<WandboxResponse>(result);

                    string output = resultObject?.program_output ?? resultObject?.compiler_output ?? "⚠️ No output received.";
                    Console.WriteLine($"Wandbox Compilation Success! Output: {output}");

                    return Ok(new { output });
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Wandbox API Exception: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { output = "Error: " + ex.Message });
            }
        }


        /// <summary>
        /// Endpoint to run code using the local server's compilers (g++ or Emscripten)
        /// </summary>
        [IgnoreAntiforgeryToken]
        [HttpPost("run-server")]
        public IActionResult RunServerCompiler([FromBody] RunCodeRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Code) || string.IsNullOrWhiteSpace(request.Compiler))
            {
                return BadRequest(new { output = "Error: Invalid Request" });
            }

            // Write code to a unique temp file
            string tempCppFile = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.cpp");
            System.IO.File.WriteAllText(tempCppFile, request.Code);

            // Execute Compilation
            string output = ExecuteCppCode(tempCppFile, request.Compiler);

            // Clean up the temp file
            try { System.IO.File.Delete(tempCppFile); } catch { /* silently fail */ }

            return Ok(new { output });
        }

        private string ExecuteCppCode(string filePath, string compiler)
        {
            string output = "";
            if (compiler == "emscripten-server")
            {
                output = CompileAndRunEmscripten(filePath);
            }
            else if (compiler == "g++")
            {
                output = CompileAndRunGPlusPlus(filePath);
            }
            else
            {
                return "Error: Unsupported Compiler Selected.";
            }

            Console.WriteLine("Server Output: " + output); // Debugging log
            return output;
        }

        private string CompileAndRunEmscripten(string filePath)
        {
            Console.WriteLine("Compiling with Emscripten...");

            string wasmFilePath = Path.Combine(Path.GetTempPath(), $"out_{Guid.NewGuid()}.wasm");

            if (System.IO.File.Exists(wasmFilePath))
            {
                System.IO.File.Delete(wasmFilePath);
            }

            // No Python, no .py file, just directly run `emcc`
            string compileCmd = $"{filePath} -o {wasmFilePath} --verbose";
            Console.WriteLine($"Running Command: emcc {compileCmd}");

            string compileResult = RunProcess("emcc", compileCmd, wasmFilePath);

            if (System.IO.File.Exists(wasmFilePath))
            {
                string result = RunWebAssembly(wasmFilePath);

                try { System.IO.File.Delete(wasmFilePath); } catch { }

                return result;
            }

            return "❌ Compilation Failed.\n" + compileResult;
        }

        private string RunWebAssembly(string wasmFilePath)
        {
            string nodePath = "node";  // Just call node globally
            string jsRunner = Path.Combine(Path.GetTempPath(), "run_wasm.js");

            System.IO.File.WriteAllText(jsRunner, @"
                const { readFileSync } = require('fs');
                const { WASI } = require('wasi');
                const { argv, env } = process;

                const wasi = new WASI({
                    version: 'preview1',
                    args: argv,
                    env,
                    preopens: { '/': process.cwd() }
                });

                const wasmPath = process.argv[2];
                const wasmBytes = readFileSync(wasmPath);

                WebAssembly.instantiate(wasmBytes, {
                    wasi_snapshot_preview1: wasi.wasiImport
                }).then(({ instance }) => {
                    wasi.start(instance);
                }).catch(err => console.error(""Execution Error:"", err));
            ");

            if (!System.IO.File.Exists(jsRunner))
            {
                return "❌ Node.js runner script not found.";
            }

            return RunProcess(nodePath, $"--no-warnings \"{jsRunner}\" \"{wasmFilePath}\"", null);
        }


        private string CompileAndRunGPlusPlus(string filePath)
        {
            string gppPath = "g++";
            string outputFile = Path.Combine(Path.GetTempPath(), $"out_{Guid.NewGuid()}");

            if (System.IO.File.Exists(outputFile))
            {
                System.IO.File.Delete(outputFile);
            }

            string compileCmd = $"\"{filePath}\" -o \"{outputFile}\"";
            string compilationResult = RunProcess(gppPath, compileCmd, outputFile);

            // Log the output and errors
            Console.WriteLine("Compilation Output:\n" + compilationResult);

            // Check if the output file was actually created
            if (!System.IO.File.Exists(outputFile))
            {
                return "❌ Compilation Failed.\n" + compilationResult;
            }

            // Run the compiled output and capture execution results
            string result = RunProcess(outputFile, "", null);

            // Clean up the output file after execution
            try { System.IO.File.Delete(outputFile); } catch { }

            return result;
        }


        private string RunProcess(string fileName, string arguments, string wasmFilePath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            var fullOutput = new StringBuilder();
            var filteredOutput = new StringBuilder();

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                {
                    fullOutput.AppendLine(e.Data);
                    if (IsRelevantLine(e.Data))
                        filteredOutput.AppendLine(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                {
                    fullOutput.AppendLine(e.Data);
                    if (IsRelevantLine(e.Data))
                        filteredOutput.AppendLine(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            string full = fullOutput.ToString().Trim();
            string filtered = filteredOutput.ToString().Trim();

            // Show filtered error output if any, otherwise return full output
            return !string.IsNullOrWhiteSpace(filtered) ? filtered : full;
        }

        private bool IsRelevantLine(string line)
        {
            return
                line.Contains(".cpp:") ||                               // Compiler error lines
                line.TrimStart().StartsWith("^") ||                     // Caret indicators
                line.Trim().EndsWith("error generated.") ||
                line.Trim().EndsWith("errors generated.") ||
                line.Trim().StartsWith("|") ||                          // Line number + code marker
                line.TrimStart().StartsWith("error:") ||
                Regex.IsMatch(line, @"^\s*\d+\s*\|");                   // Matches: "   5 | ..."
        }


    }
}
