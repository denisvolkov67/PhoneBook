using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data.Context;

namespace PhoneBook.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPhoneBookData(this IServiceCollection services)
        {
            services.AddDbContext<PhoneBookDbContext>(
                opt => opt.UseSqlite(@"Data Source=phonebook.db"));
            return services;
        }
    }
}
