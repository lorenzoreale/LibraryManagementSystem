using System;
using Microsoft.Extensions.DependencyInjection;
using LMS.Domain.Interfaces;
using LMS.Infrastructure.Repositories;
using LMS.Presentation.UI;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.CompilerServices;

namespace LMS.Presentation
{
    public class LMS
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBookRepository, JsonBookRepository>()
                .AddSingleton<IMemberRepository, JsonMemberRepository>()
                .AddSingleton<ITransactionRepository, JsonTransactionRepository>()
                .AddTransient<BookUI>()
                .AddTransient<MemberUI>()
                .AddTransient<MainMenu>()
                .BuildServiceProvider();
            var app = serviceProvider.GetService<MainMenu>();

            if (app == null)
            {
                Console.WriteLine("Initialising Error.");
                return;
            }

            app.Show();
        }

    }

}