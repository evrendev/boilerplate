using System.Threading.Tasks;
using EvrenDev.Application.SharedPreferences;

namespace EvrenDev.Application.Interfaces.Shared
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailParameter parameter);
    }
}
