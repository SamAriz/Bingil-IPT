# Bingil IPT

A modern Blazor Web Application built with .NET 8.

## How to Fix "Port Already in Use" Error (Main Problem)

This is the most common error you will face in this project. Follow these steps when the error appears:

1. Open the **Properties** folder
2. Open the file **`launchSettings.json`**
3. Find this line:
   ```json
   "applicationUrl": "http://localhost:5208"
4. Change the number between 5200 - 5900
