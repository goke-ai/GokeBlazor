## Blazor for various Authentication and Authorization

A sample project showcasing a blazor webassembly app that can switch between various authentication and authorization such as IdentityServer and Aspnet Identity.

The default authentication scheme is Aspnet identity. It can be changed to IdentityServer by changing the value of `Identity` section in the `appsetting.json` file in both the Client and Server project.

### How to run
1. Install [**the latest dotnet sdk**](https://dotnet.microsoft.com/download) and the latest [**Visual Studio**](https://visualstudio.microsoft.com/vs/).
2. Clone or download or [**Install Template**](#InstallTemplate).
3. Keep or change Identity settings to:
   1. `AspnetIdentity`
   2. `IdentityServer`
4. Open the solution in Visual Studio and press F5.
5. Create a user using the `Create Account` button in the login page or login if you have already created a user.
6. A default user in role(s) `Gamers` can assess the Game page. See `SeedData` for roles creation.
   1. Set User Secret using the following keys and values except UserPWD, please adjust appropriately.
      1. "Seed:UserUN": "gamer@ark.com"
      2. "Seed:UserPWD": "xxxxxxxxx"
      3. "Seed:UserRoles": "Gamers,Users"

### Install Template
- `dotnet new --install GokeBlazor.Templates`  

then Create project 
- `dotnet new GokeBlazor [-o [Project Name]]`

### Credit
1. Credit to  https://github.com/stavroskasidis/BlazorWithIdentity
2. Blazor WebAssembly App template (***dotnet new blazorwasm*** ) 
   1. https://dotnet.microsoft.com/learn/aspnet/blazor-tutorial/intro
   2. https://docs.microsoft.com/en-us/aspnet/core/blazor/tooling?view=aspnetcore-5.0&pivots=windows


### Donation
-  

### Contact 
<https://goke.azurewebsites.net>  
<https://goke.me>  
<https://github.com/goke-ai/GokeBlazor>