/* 4.3 Secure an API - CodeBits.cs
Enable CORS

Add to your web.config (global level)

<system.webServer>
<httpProtocol>
<customHeaders>
<add name="Access-Control-Allow-Origin" value="*"/>
<add name="Access-Control-Allow-Headers" value="Origin, X-Requested-With, Content-Type, Accept" />
</customHeaders>
</httpProtocol>
</system.webServer>

or install from NuGet package manager (more granular)

Install-Package Microsoft.AspNet.WebApi.Cors -pre

then go to WebApi.config and add config.EnableCors();

then you can enable it at an Action level

[EnableCors(origins: "http://localhost:26891", headers: "*", methods: "*")]
public IEnumerable<string> Get()
{
return new[] { "Value1", "Value2" };
}

*/ 
// Black list authorization attribute example 

public class BlackListAuthorizationAttribute : AuthorizeAttribute
{
protected override bool IsAuthorized(HttpActionContext actionContext)
{
IPrincipal user = Thread.CurrentPrincipal;
if (user == null) return true;
var splitUsers = SplitString(Users);
if (splitUsers.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
return false;
var splitRoles = SplitString(Roles);
if (splitRoles.Any(user.IsInRole)) return false;
return true;
}
private static string[] SplitString(string original)
{
if (String.IsNullOrEmpty(original))
{
return new string[0];
}
var split = from piece in original.Split(',')
let trimmed = piece.Trim()
where !String.IsNullOrEmpty(trimmed)
select trimmed;
return split.ToArray();
}
}

// Creating a WebAPI Hosting Server
// first search for and install nuget package MICROSOFT ASP.NET API Self Host
// after configuring it navigate to http://localhost:8080/api/Values/
class Program
{
static void Main(string[] args)
{
Console.WriteLine("Starting Web API Server. Please wait...");
if (typeof(MyWebApi.Controllers.ValuesController) == null)
{
// work-around 
// Required when the controllers exists in a different assembly
return;
}
var hostConfig = new HttpSelfHostConfiguration("http://localhost:8080");
// Create routes (as WebApiConfig.cs would do it)
hostConfig.Routes.MapHttpRoute("API Name",
"api/{controller}/{action}/{id}",
new { id = RouteParameter.Optional });
// Create an instances of the Self Host Server class
using (HttpSelfHostServer server = new HttpSelfHostServer(hostConfig))
{
server.OpenAsync().Wait();
Console.WriteLine("Press [ENTER] to close");
Console.ReadLine();
server.CloseAsync().Wait();
}
}
}

// protect self host agains alrg messages 
var config = new HttpSelfHostConfiguration(baseAddress);
config.MaxReceivedMessageSize = 1024;
config.MaxBufferSize = 1024;

// in iis add to web.config
<httpRuntime maxRequestLength="1024" />