__How to run this application:__

1. Clone the repository and open the solution in Visual Studio.
2. Open App.config file in the LibraryManager.Data project and set your connection string for the DB access.
3. Right-click on the solution in the solution explorer window and open Properties. Select the "Multiple startup projects" option. Set your Web Forms project action to "Start". Set your API project action to "Start without debugging".
Click "OK".
4. Press F5 to run the applicatiion.
5. Wait until the home page pops up in your browser.
6. Now you can manage your library.

__Possible issues and workarounds:__
In case any issues with nuget packages occur follow these steps: 
1. Open Source folder in the root of the local project. Delete everything from the packages folder.
2. In Visual Studio right-click on the solution and then click "Restore NuGet packages".
3. Run this command in the package manager console: "Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r"
4. Rebuild the solution and try to run the app again. 
