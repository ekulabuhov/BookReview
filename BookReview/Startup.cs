using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookReview.Startup))]
namespace BookReview
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
