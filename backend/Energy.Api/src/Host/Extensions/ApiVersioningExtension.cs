using Asp.Versioning;

namespace Energy.Host.Extensions
{
    public static class ApiVersioningExtension
    {
        public static WebApplicationBuilder AddCustomApiVersioning(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return builder;
        }
    }
}
