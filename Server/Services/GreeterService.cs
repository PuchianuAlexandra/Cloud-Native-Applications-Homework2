using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Grpc.Core;

namespace Server
{
    public class GreeterService : Greeter.GreeterBase
    {

        public override Task<SignReply> SendSign(SignRequest request, ServerCallContext context)
        {
            DateTime birthDate;
            birthDate = DateTime.ParseExact(request.Date.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            string sign = "";
            string[] dates = File.ReadAllLines(@"ZodiacSigns.txt");

            for (int index = 0; index < dates.Length; index++)
            {
                string[] dateSplit = dates[index].Split(' ');
                var FirstDate = DateTime.ParseExact(dateSplit[1], "MM/dd", CultureInfo.InvariantCulture);
                var LastDate = DateTime.ParseExact(dateSplit[2], "MM/dd", CultureInfo.InvariantCulture);

                if ((birthDate.Month == FirstDate.Month && birthDate.Day >= FirstDate.Day) || (birthDate.Month == LastDate.Month && birthDate.Day <= LastDate.Day))
                {
                    sign = dateSplit[0];
                    break;
                }
            }

            return Task.FromResult(new SignReply
            {
                Zodiac = sign
            });
        }

    }
}
