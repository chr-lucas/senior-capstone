# Ace Editor README

This README explains how the Ace Editor is implemented in the Nebula Workbook and how it connects across the Razor Pages, controller logic, and JavaScript runtime. It also outlines how code is compiled using both local and cloud compilers.

**NOTE:**  
>As of now, code written using the Ace IDE will only compile through **g++** and **Emscripten** when running the project from the **server**.  
>If testing the project **locally**, only compilation with **Wandbox** is currently supported.

---

## What is Ace Editor?
Ace is an embeddable code editor written in JavaScript. It provides syntax highlighting, autocompletion, code folding, and many other IDE-like features, all within the browser. For more information and for more styling options, visit their [GitHub Repository](https://github.com/ajaxorg/ace)

## Files Involved

### 1. **Pages/ide.cshtml**
This Razor Page is a **demonstration/example page** that shows how the Ace Editor can be embedded into any other Razor Page you may wish to create. 
- It provides a hardcoded starter C++ snippet (in `initialCode`) for users to edit.
- It calls the partial view `Shared/_IDEPartial.cshtml` to render the editor.

To reuse the Ace Editor in any other page, pass a C++ code snippet to the `Html.PartialAsync("Shared/_IDEPartial", yourCode)` call.

---

### 2. **Pages/Shared/_IDEPartial.cshtml**
This is the **reusable UI partial view** for the Ace Editor:
- Renders the editor container.
- Allows selection of compiler (g++, Emscripten, or Wandbox).
- Allows font-size adjustment.
- Shows the output in a `<pre>` block.

This partial view can be used on multiple pages, or even multiple times on a single page by passing in a string of starter code.

---

### 3. **wwwroot/js/ide.js**
This JavaScript file handles:
- Initializing **each instance** of the Ace Editor.
- Wiring up UI components (compiler dropdown, font size buttons, Run Code button).
- Sending POST requests to the backend API:
  - `/api/ide/run-server` for local compilation via g++ or emscripten.
  - Wandbox API for online cloud compilation.
- Handling output formatting and live resizing.

It uses `ResizeObserver` to ensure the editor remains responsive to layout changes and resizes correctly.

---

### 4. **Controllers/IdeController.cs**
This controller exposes two endpoints:

#### `POST /api/ide/run-server`
- Accepts code and compiler name.
- Writes code to a temp file.
- Invokes `g++` or `emcc` (Emscripten) depending on user selection.
- Runs the compiled output.
- Deletes the temp file.

#### `POST /api/ide/run-wandbox`
- Sends a JSON payload to the [Wandbox API](https://wandbox.org/).
- Returns compiler/program output.

**Note:** These calls return JSON responses that are rendered to the page by the JavaScript logic.

---

## Developer Notes

- To **add the editor to a new page**, simply create a string for initial C++ code and include:
  ```cshtml
  @await Html.PartialAsync("Shared/_IDEPartial", yourInitialCode)
  ```
- The editor is dynamically enhanced on page load by `ide.js`.
- You can extend the controller to support other compilers or sandboxed execution if needed.
- Be cautious with user-submitted code — it’s executed server-side (or through Wandbox) and should be sandboxed in production.

---

## Example Flow
1. `ide.cshtml` passes a C++ snippet to `_IDEPartial.cshtml`.
2. `_IDEPartial.cshtml` renders the Ace Editor + dropdowns + output window.
3. `ide.js` loads Ace, sets language mode, and binds events.
4. Clicking "Run Code" sends code to `IdeController.cs`.
5. Output is streamed back and rendered under the editor.
