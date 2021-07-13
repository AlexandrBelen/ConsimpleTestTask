using ConsimpleTestTask.Interfaces;
using ConsimpleTestTask.Models;
using ConsimpleTestTask.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsimpleTestTask
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = @"http://tester.consimple.pro";
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            IRequestSender httpService = new RequestSender(client);

            bool runing = true;
            while (runing)
            {
                Console.WriteLine("1 - to get info | 2 - to exit | 3 - clear console");
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        Table(await httpService.Get(url));
                        break;
                    case ConsoleKey.D2:
                        runing = false;
                        break;
                    default:
                    case ConsoleKey.D3:
                        Console.Clear();
                        break;
                }
            }
        }

        private static void Table(ResultModel data)
        {
            for (int i = 0; i < data.Products.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"\t\t--------------------------------------------");
                    Console.WriteLine($"\t\t|{"Product name",-20} |{"Category name",-20}|");
                    Console.WriteLine($"\t\t--------------------------------------------");
                }
                Console.WriteLine($"\t\t|{$"{data.Products?[i]?.Name }",-20} |{$"{data.Categories.FirstOrDefault(item => item.Id == data.Products?[i]?.CategoryId)?.Name}",-20}|");
            }
            Console.WriteLine($"\t\t--------------------------------------------");
            Console.WriteLine("\n\n");
        }
    }
}