function initializeIDE() {
    console.log("Checking if Ace is loaded...");

    let checkAceInterval = setInterval(() => {
        if (typeof ace !== "undefined") {
            clearInterval(checkAceInterval);
            console.log("✅ Ace Editor detected. Initializing...");

            document.querySelectorAll(".cpp-ide-container").forEach(function (container, index) {
                let editorElement = container.querySelector(".ace-editor");
                let outputElement = container.querySelector(".output-container");
                let compilerDropdown = container.querySelector(".compiler-selector");
                let wandboxCompilerDropdown = container.querySelector(".wandbox-compiler-selector");
                let wandboxCompilerContainer = container.querySelector(".wandbox-compiler-container");
                let runButton = container.querySelector(".run-code-btn");

                // Ensure we have an editor element
                if (!editorElement) {
                    console.error("🚨 ERROR: <div class='ace-editor'> NOT FOUND!");
                    return;
                }

                // Assign a unique ID based on index
                const uniqueEditorId = `editor-${index}`;
                editorElement.id = uniqueEditorId;
                let editor = ace.edit(uniqueEditorId);

                // Font size controls
                const increaseBtn = container.querySelector(".increaseFont");
                const decreaseBtn = container.querySelector(".decreaseFont");
                const fontSizeDisplay = container.querySelector(".fontSizeDisplay");

                let fontSize = 14;
                editor.setFontSize(fontSize);

                function updateFontSize(newSize) {
                    fontSize = Math.max(10, Math.min(32, newSize)); // Clamp between 10px and 32px
                    editor.setFontSize(fontSize);
                    if (fontSizeDisplay) fontSizeDisplay.textContent = `${fontSize}px`;
                }

                if (increaseBtn) {
                    increaseBtn.addEventListener("click", () => updateFontSize(fontSize + 2));
                }
                if (decreaseBtn) {
                    decreaseBtn.addEventListener("click", () => updateFontSize(fontSize - 2));
                }

                // Ace editor settings
                editor.setTheme("ace/theme/dracula");
                editor.session.setMode("ace/mode/c_cpp");
                editor.setOptions({
                    enableBasicAutocompletion: true,
                    enableLiveAutocompletion: true,
                    enableSnippets: true,
                });

                // Debounced resize handling
                let resizeTimeout;

                const resizeObserver = new ResizeObserver(() => {
                    requestAnimationFrame(() => {
                        editor.resize();
                    });
                });

                // Start observing the container
                resizeObserver.observe(editor.container);

                console.log(`🎸 Ace Editor ${index} initialized successfully.`);

                // Compiler selection handling
                function updateWandboxVisibility() {
                    if (compilerDropdown.value === "wandbox") {
                        wandboxCompilerContainer.style.display = "block";
                    } else {
                        wandboxCompilerContainer.style.display = "none";
                    }
                }

                compilerDropdown.addEventListener("change", updateWandboxVisibility);
                updateWandboxVisibility(); // Trigger on load

                // Run button click
                runButton.addEventListener("click", async function () {
                    let code = editor.getValue().trim();
                    let compiler = compilerDropdown.value;

                    if (!code) {
                        outputElement.textContent = "❌ Error: No code entered!";
                        return;
                    }

                    if (compiler === "wandbox") {
                        let selectedWandboxCompiler = wandboxCompilerDropdown.value;
                        await runWandboxCompiler(code, selectedWandboxCompiler, outputElement);
                    } else {
                        await runServerCompiler(code, compiler, outputElement);
                    }
                });
            });
        }
    }, 100); // Check every 100ms until Ace is available
}


// Ensures script runs when the page is fully loaded
window.addEventListener("load", function () {
    initializeIDE();
});

// Function to Run Code Using Server-Side Compiler
async function runServerCompiler(code, compiler, outputElement) {
    let payload = { code: code, compiler: compiler };

    outputElement.innerHTML = `<div><span class="spinner"></span> Compiling...</div>`;
    try {
        let response = await fetch("/api/ide/run-server", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload)
        });

        let data = await response.json();
        outputElement.innerHTML = `<pre>${data.output}</pre>`;    } catch (error) {
        outputElement.textContent = "❌ Error: " + error.message;
    }
}

// Function to Run Code Using Wandbox API
async function runWandboxCompiler(code, compiler, outputElement) {
    const payload = {
        code: code,
        compiler: compiler || "gcc-head",
        options: "warning,gnu++20",
        stdin: "",
        compiler_option_raw: "",
        runtime_option_raw: "",
        save: false
    };

    outputElement.innerHTML = `<div><span class="spinner"></span> Compiling...</div>`;
    try {
        const response = await fetch("https://wandbox.org/api/compile.json", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload),
        });

        if (!response.ok) {
            throw new Error(`HTTP ${response.status} - ${response.statusText}`);
        }

        const data = await response.json();
        let output = "";

        // Only include compiler errors, skip warnings
        if (data.compiler_error?.trim()) {
            output += `Compiler Error:\n${data.compiler_error.trim()}\n\n`;
        }

        if (data.program_error?.trim()) {
            output += `Program Error:\n${data.program_error.trim()}\n\n`;
        }

        if (data.program_output?.trim()) {
            output += `${data.program_output.trim()}\n`;
        }

        if (!output.trim()) {
            output = "⚠️ No output received.";
        }

        outputElement.innerHTML = `<pre>${escapeHTML(output.trim())}</pre>`;
    } catch (error) {
        outputElement.textContent = "❌ Error: " + error.message;
    }
}

function escapeHTML(text) {
    return text
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");
}



