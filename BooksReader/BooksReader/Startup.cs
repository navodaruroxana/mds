using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BooksReader.Startup))]
namespace BooksReader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
