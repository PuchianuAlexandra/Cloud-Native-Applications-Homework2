using Grpc.Net.Client;
using Server;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            bool validDate = false;
            do
            {
                Console.WriteLine("Enter the date of birth (MM/DD/YYYY): ");
                string input = Console.ReadLine();
                DateTime dateOut;
                if ((DateTime.TryParseExact(input, "MM/dd/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateOut)))
                {
                    var reply = await client.SendSignAsync(new SignRequest { Date = input });
                    Console.WriteLine("The zodiac sign is: " + reply.Zodiac);
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Date string {0} is invalid.\n", input);
                }
            } while (!validDate);
           
            Console.WriteLine("\n" + "Press any key to exit...");
            Console.ReadKey();
        }
    }
}
