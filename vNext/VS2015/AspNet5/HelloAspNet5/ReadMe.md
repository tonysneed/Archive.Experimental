# Hello Asp.Net 5

- Started with a class lib (aspnet5)
- Building does produce any output
  + Need to check 'produce outputs on build' (project props, build)
  + Produces HelloAspNet5.ClassLib.1.0.0.nupkg
  + Also produces HelloAspNet5.ClassLib.1.0.0.symbols.nupkg

- Added a console app (aspnet5)
  + Referenced class lib from console app,
    which add dependency to project.json file
  + There is also a Commands section in project.json
  + Ctrl+F5 works, even though there is no exe produced
  + installed kvm per online guide
  + navigated to dir with project.json, executed k run command,
    which ran the console app

- Added an empty web app (aspnet5)
  + Added dependencies in project.json:
    Microsoft.AspNet.Diagnostics
    Microsoft.AspNet.Mvc
  + Added code to Startup.cs

      public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseWelcomePage();
        }
    }

  + Added a Values Controller

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        static readonly List<string> Items = new List<string>
        {
            "value1", "value2", "value3"
        };

        [HttpGet]
        public List<string> GetAll()
        {
            return Items;
        }
    }

  + Pressed Ctrl+F5 to start IIS Express
    http://localhost:4010/api/values

- Ran web app from console
  + Added dependency: Microsoft.AspNet.Server.WebListener
  + Added commands section to project.json:

      "commands": {
        "web": "Microsoft.AspNet.Hosting --server Microsoft.AspNet.Server.WebListener --server.urls http://localhost:5000"
    },

  + Navigated to web folder path, executed k web
  + Changed port to 5000 in brower then refreshed page
  + Selected "web" command as Debug target on project properties,
    then pressed Ctrl+F5 to start in console