# Server Setup and Deployment Guide for **Nebula Workbook** App

This guide outlines the steps required to run and test the workbook from the Linux server, and how to pull updates from GitHub.

---

## Prerequisites (Already Installed on Server)
- .NET 9 SDK
- Node.js
- g++
- Emscripten SDK
- ASP.NET Runtime


## Running the App Locally on the Server

### 1. Navigate to the project directory:
```bash
cd ~/2420-workbook
```

### 2. Restore and build the project:
```bash
dotnet restore
dotnet build
```

### 3. Run the app (development mode):
```bash
dotnet run --urls "http://0.0.0.0:5077"
```
> You can then access it in a browser by going to `http://timetracker.users.weber.edu:5077` on your local machine.

### 4. Stop the app
Use `Ctrl + C` in the terminal where it's running.


## Updating the App from GitHub

### 1. Navigate to the project directory:
```bash
cd ~/2420-workbook
```

### 2. Pull the latest changes:
```bash
git pull origin main
```

### 3. Rebuild after pulling:
```bash
dotnet build
```

### 4. Clean up temp files if needed:
```bash
dotnet clean
```


## Setting Up Git (SSH Authentication)
If you're accessing the GitHub repository over SSH, you will likely need to generate and add an SSH key:

### 1. Generate a key:
```bash
ssh-keygen -t ed25519 -C "your_email@example.com"
```
Replace the portion in quotations with your GitHub email. Press Enter to accept the default file location, and optionally set a passphrase.

### 2. Copy your public key:
```bash
cat ~/.ssh/id_ed25519.pub
```
This prints a line that looks like this:
```bash
ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIE... user@example.com
```
Copy the entire line.

### 3. Add the key to GitHub:
Go to GitHub → Settings → SSH and GPG Keys → New SSH key → Paste the copied line.
This allows GitHub to recognize your server as a trusted source, so you can use URLs like:
```bash
git@github.com:your-username/your-repo.git
```
### REMINDER: Never share your private key with anyone. Only the .pub version is safe to upload.

### 4. Test your connection:
```bash
ssh -T git@github.com
```
You should see a success message.

## Notes on Configuration

- If the database connection string changes, update it in `appsettings.json`:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=137.190.19.215;Database=NebulaWorkbook;..."
}
```

- The server compilers (g++, emcc) are invoked by the backend controller `IdeController.cs`.

- The app runs in development mode unless published.
