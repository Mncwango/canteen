using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Canteen.Startup))]

namespace Canteen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}