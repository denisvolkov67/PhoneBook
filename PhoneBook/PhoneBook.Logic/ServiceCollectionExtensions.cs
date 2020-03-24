using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Data;

[assembly: Fody.ConfigureAwait(false)] // to set up ConfigureAwait(false) globally in the assembly
namespace PhoneBook.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPhoneBookServices(this IServiceCollection services)
        {
            services.AddPhoneBookData();
            return services;
        }
    }
}
