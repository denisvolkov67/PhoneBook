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
                opt => opt.UseSqlite(@"Data Source=..\PhoneBook.Data\phonebook.db"));
            return services;
        }
    }
}
