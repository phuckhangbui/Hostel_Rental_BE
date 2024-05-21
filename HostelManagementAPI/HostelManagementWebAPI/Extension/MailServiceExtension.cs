using BusinessObject.Mail;
using Service.Implement;

namespace HostelManagementWebAPI.Extension
{
    public class MailServiceExtension
    {
        IConfiguration _configuration;
        public MailServiceExtension(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureService(IServiceCollection services)
        {
            services.AddOptions();
            var mailSetting = _configuration.GetSection("MailSetting");
            services.Configure<MailSetting>(mailSetting);

            services.AddTransient<MailService>();
        }
    }
}
