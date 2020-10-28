using System;
using System.Threading.Tasks;
using dotenv.net;
using RingCentral;

namespace RingCentralForcedAccountCodesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DotEnv.Config(true);

            Task.Run(async () =>
            {
                var rc = new RestClient(
                    Environment.GetEnvironmentVariable("RINGCENTRAL_CLIENT_ID"),
                    Environment.GetEnvironmentVariable("RINGCENTRAL_CLIENT_SECRET"),
                    Environment.GetEnvironmentVariable("RINGCENTRAL_SERVER_URL")
                );

                await rc.Authorize(
                    Environment.GetEnvironmentVariable("RINGCENTRAL_USERNAME"),
                    Environment.GetEnvironmentVariable("RINGCENTRAL_EXTENSION"),
                    Environment.GetEnvironmentVariable("RINGCENTRAL_PASSWORD")
                );
                var eventFilters = new[]
                {
                    "/restapi/v1.0/account/~/extension/~/telephony/sessions"
                };
                Console.WriteLine(rc.token.access_token);
                // await Task.Delay(999999999);
                await rc.Revoke();
            }).GetAwaiter().GetResult();
        }
    }
}