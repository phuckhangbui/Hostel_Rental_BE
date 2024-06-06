using Repository.Implement;
using Repository.Interface;
using Service.Cloundinary;
using Service.Implement;
using Service.Interface;
using Service.Mail;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection ApplicationServices(this IServiceCollection services
        , IConfiguration config)
    {
        services.Configure<MailSetting>(config.GetSection("MailSetting"));
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<ITokenService, TokenService>();

        //services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IMemberShipRepository, MemberShipRepository>();
        services.AddScoped<ITypeServiceRepository, TypeServiceRepository>();
        services.AddScoped<IHostelRepository, HostelRepository>();
        //services.AddScoped<IDashboardRepository, DashboardRepository>();

        //services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IMemberShipService, MemberShipService>();

        services.AddScoped<ITypeServiceRepository, TypeServiceRepository>();
        services.AddScoped<ITypeServiceService, TypeServiceService>();

        //services.AddScoped<IServiceRepository, ServiceRepository>();
        //services.AddScoped<IServiceService, ServiceService>();

        services.AddScoped<IMembershipRegisterRepository, MembershipRegisterRepository>();
        services.AddScoped<IMembershipRegisterService, MembershipRegisterService>();

        services.Configure<CloudinarySetting>(config.GetSection("CloudinarySettings"));
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IHostelService, HostelService>();
        //services.AddScoped<IContractService, ContractService>();
        //services.AddScoped<IContractRepository, ContractRepository>();
        //services.AddScoped<IDashboardService, DashboardService>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //the current position of the mapping profile

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });

        return services;
    }
}