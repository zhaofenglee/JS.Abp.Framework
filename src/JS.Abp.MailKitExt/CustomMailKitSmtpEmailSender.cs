using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.MailKit;

namespace JS.Abp.MailKitExt
{
    public class CustomMailKitSmtpEmailSender : MailKitSmtpEmailSender
    {
        public CustomMailKitSmtpEmailSender(ISmtpEmailSenderConfiguration smtpConfiguration, IBackgroundJobManager backgroundJobManager, IOptions<AbpMailKitOptions> abpMailKitConfiguration) : base(smtpConfiguration, backgroundJobManager, abpMailKitConfiguration)
        {

        }

        protected override Task ConfigureClient(SmtpClient client)
        {
            //解决发送邮件时邮箱服务器证书问题
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            return base.ConfigureClient(client);
        }
    }
}
