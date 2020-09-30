using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp
{
    class Program
    {
        private static DeviceClient deviceclient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-samuelw-iothub.azure-devices.net;DeviceId=inlamningsuppgift5;SharedAccessKey=hh5F4vDEHpSo1C4hw2ozjGMda61nVWpFgFhAvXx9ThA=", TransportType.Mqtt);
        private static int telemetryInterval = 10;
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            deviceclient.SetMethodHandlerAsync("SetTelemetryInterval", SetTelemetryInterval, null).Wait();
            SendMessagAsync().GetAwaiter();

            Console.ReadKey();
        }

        private static Task<MethodResponse> SetTelemetryInterval(MethodRequest request, object userContext)
        {
            var payload = Encoding.UTF8.GetString(request.Data).Replace("\"","");
        

            if (Int32.TryParse(payload, out telemetryInterval)) 
            {
                Console.WriteLine($"Interval set to: {telemetryInterval} seconds");

                // { "result": "Executed direct method: SetTelemetryInterval" }
                string json = "{\"result\": \"Executed direct method: " + request.Name + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 200));
            }
            else
            {
                string json = "{\"result\": \"Method not implemented\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(json), 501));
            }
        }

        private static async Task SendMessagAsync()
        {
            while (true)
            {
                double temp = 10 + rnd.NextDouble() * 15;
                double hum = 40 + rnd.NextDouble() * 20;

                var data = new
                {
                    temperature = temp,
                    humidity = hum
                };

                var json = JsonConvert.SerializeObject(data);
                var payload = new Message(Encoding.UTF8.GetBytes(json));
                payload.Properties.Add("temperatureAlert", (temp > 30) ? "true" : "false");

                await deviceclient.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");

                await Task.Delay(telemetryInterval * 1000);
            }
            
        }
    }
}
