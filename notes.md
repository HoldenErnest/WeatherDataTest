10/15/2025
 - Using ASP.NET framework. `$ dotnet run` to run backend.
 - Default web page is located at `http://localhost:5058/`
 - Seems pretty well modularized. MVC as indicated.
 - .cshtml (Razor files) have some additional non-static prerendered HTML before its passed to the client (like Jinja).
 - I'll probably have to look more into the "Shared" Views, I imagine theyre generic boilerplate for all pages. Maybe: `_ViewStart.cshtml` gets run first which subsequently adds the other boilerplate, then the unique View
 - Controllers take methods specified in Program.cs: `pattern: "{controller=Home}/{action=Index}/{id?}");`. Home is the View folder to look in and controller to use, Index is the default page.