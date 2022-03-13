using Microsoft.AspNetCore.Builder;

namespace EvrenDev.PublicApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EvrenDev.PublicApi");
                options.RoutePrefix = string.Empty;
                options.DisplayRequestDuration();
                options.DefaultModelsExpandDepth(-1);
            });
        }
    }
}