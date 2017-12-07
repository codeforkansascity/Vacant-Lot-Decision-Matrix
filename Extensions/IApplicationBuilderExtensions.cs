using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root, bool enableDirectoryBrowsing)
        {
            return app.UseFileServer(new FileServerOptions()
            {

                FileProvider = new PhysicalFileProvider(Path.Combine(root, @"node_modules")),

                RequestPath = new PathString("/node_modules"),

                EnableDirectoryBrowsing = enableDirectoryBrowsing
            });

        }

    }
}
