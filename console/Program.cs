using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace console
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("\nYELLOW LAB - HACS (HIGHLY ADVANCED CLIENT SYSTEM)");
            Console.WriteLine("---------------------------------------------------\n");
            Console.WriteLine("Welcome to our 2022 interface!");
            Console.WriteLine("You can now query our web api directly from the command line!");
            Console.WriteLine("To get started, type out the name, or the areacode of the city you would like to know more about");
            Console.WriteLine("To quit, type exit\n");

            bool quit = false;
            while(!quit)
            {
                string input = Console.ReadLine();

                if(input.Trim().ToLower() == "exit")
                {
                    quit = true;
                }
                else
                {
                    await GetNumberOfSeatholders(input);
                }
            }

            Console.WriteLine("Bye bye");
        }

        private static async Task GetNumberOfSeatholders(string city)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            // client.DefaultRequestHeaders.Accept.Add(
            //     new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
         //   client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var response = await client.GetAsync("http://localhost:5000/api/seatholders/" + city);
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Could not find data for {city}. Please try again:");
            }
            else
            {
                string value = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{city} has {value} seatholders!\n");
            }

            //var msg = await stringTask;
            //Console.WriteLine(msg);
        }
    }
}
