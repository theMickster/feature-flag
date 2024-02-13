using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace FeatureFlag.API.libs;

[ExcludeFromCodeCoverage]
internal static class RegisterAuthentication
{
    /// <summary>
    /// Configure authorization for the API.
    /// </summary>
    internal static WebApplicationBuilder RegisterApiAuthentication(this WebApplicationBuilder builder)
    {
        var debuggingEvents = GetAuthorizationDebuggingEvents();
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(jwtOptions => 
                {
                    builder.Configuration.Bind("AzureAd", jwtOptions);
                    // Uncomment for debugging purposes
                    //#if DEBUG
                    //jwtOptions.RequireHttpsMetadata = false;
                    //jwtOptions.Events = debuggingEvents;
                    //jwtOptions.IncludeErrorDetails = true;
                    //#endif
                }, options => 
                {
                    builder.Configuration.Bind("AzureAd", options);
                }, JwtBearerDefaults.AuthenticationScheme, true);

        return builder;
    }

    /// <summary>
    /// Configure the authorization access policies for the API.
    /// </summary>
    internal static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicyConstants.RequireAdministratorPolicy,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AuthPolicyConstants.AdministratorRole);
                });
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicyConstants.RequireContributorPolicy,
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AuthPolicyConstants.AdministratorRole, AuthPolicyConstants.ContributorRole);
                });
        });

        return services;
    }


    private static JwtBearerEvents GetAuthorizationDebuggingEvents()
    {
        return new JwtBearerEvents()
        {
            // If a JWT token gets rejected as invalid, set a breakpoint on the context of 'OnChallenge' event
            OnChallenge = context =>
            {
                
                
                return Task.CompletedTask;
            },
            // Other events are here just in case
            OnForbidden = context => Task.CompletedTask,
            OnAuthenticationFailed = context => Task.CompletedTask,
            OnMessageReceived = context => Task.CompletedTask,
            OnTokenValidated = context => Task.CompletedTask,
        };
    }
}