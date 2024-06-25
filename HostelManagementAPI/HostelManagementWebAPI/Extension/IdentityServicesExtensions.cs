using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HostelManagementWebAPI.Extensions;

public static class IdentityServiceExtension
{

    const string ADMIN_ID = "1";
    const string OWNER_ID = "2";
    const string MEMBER_ID = "3";
    public static IServiceCollection IdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                      policy.RequireClaim("RoleId", ADMIN_ID));
            options.AddPolicy("Owner", policy =>
                      policy.RequireClaim("RoleId", OWNER_ID));
            options.AddPolicy("Member", policy =>
                      policy.RequireClaim("RoleId", MEMBER_ID));
            options.AddPolicy("MemberAndOwner", policy =>
                        policy.RequireClaim("RoleId", MEMBER_ID, OWNER_ID));
            options.AddPolicy("OwnerAndAdmin", policy =>
                        policy.RequireClaim("RoleId", ADMIN_ID, OWNER_ID));
        });
        return services;
    }
}