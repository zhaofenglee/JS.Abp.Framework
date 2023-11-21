using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JS.Abp.AspNetCore.Extensions
{
    public static class SameSiteCookiesServiceCollectionExtensions
    {
        public static IServiceCollection AddSameSiteCookiePolicy(
          this IServiceCollection services)
        {
            OptionsServiceCollectionExtensions.Configure<CookiePolicyOptions>(services, (Action<CookiePolicyOptions>)(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = (Action<AppendCookieContext>)(cookieContext => SameSiteCookiesServiceCollectionExtensions.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions));
                options.OnDeleteCookie = (Action<DeleteCookieContext>)(cookieContext => SameSiteCookiesServiceCollectionExtensions.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions));
            }));
            return services;
        }

        private static void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite != SameSiteMode.None)
                return;
            string userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            if (!httpContext.Request.IsHttps || SameSiteCookiesServiceCollectionExtensions.DisallowsSameSiteNone(userAgent))
                options.SameSite = SameSiteMode.Unspecified;
        }

        private static bool DisallowsSameSiteNone(string userAgent) => userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12") || userAgent.Contains("Macintosh; Intel Mac OS X 10_14") && userAgent.Contains("Version/") && userAgent.Contains("Safari") || userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6");
    }
}
