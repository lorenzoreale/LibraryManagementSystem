using System;
using Microsoft.Extensions.DependencyInjection;
using LMS.Domain.Interfaces;
using LMS.Infrastructure.Repositories;
using LMS.Presentation.UI;

namespace LMS.Presentation
{
    public class LMS
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddSingleton<IBookRepository, JsonBookRepository>().AddTransient<BookUI>().AddTransient<MainMenu>().BuildServiceProvider();
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