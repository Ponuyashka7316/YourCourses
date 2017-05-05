using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourCourses.Startup))]
namespace YourCourses
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
