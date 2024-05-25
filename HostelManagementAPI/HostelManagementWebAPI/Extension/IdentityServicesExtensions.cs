using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HostelManagementWebAPI.Extensions;

public static class IdentityServiceExtension
{
    private const string ADMIN_ID = "1";
    private const string STAFF_ID = "2";
    private const string MEMBER_ID = "3";

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
                ValidateAudience = false
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
                policy.RequireClaim("RoleId", ADMIN_ID));
            options.AddPolicy("Staff", policy =>
                policy.RequireClaim("RoleId", STAFF_ID));
            options.AddPolicy("Member", policy =>
                policy.RequireClaim("RoleId", MEMBER_ID));
            options.AddPolicy("AdminAndStaff", policy =>
                policy.RequireClaim("RoleId", ADMIN_ID, STAFF_ID));
        });
        return services;
    }
}