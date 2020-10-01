using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SharedLibraries.Services
{
    public class DeviceClientService
    {
        public DeviceClient deviceClient;

        private int _interval = 10;

        private static Random rnd = new Random();

        public DeviceClientService(string connectionstring)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(connectionstring, TransportType.Mqtt);
            deviceClient.SetMethodHandlerAsync("SetInterval", SetTelemetryIntervalAsync, null).GetAwaiter();
        }

        public async Task<MethodResponse> SetTelemetryIntervalAsync(MethodRequest methodRequest, object userContext)
        {            
            SetInterval(Convert.ToInt32(methodRequest.DataAsJson.Replace("\"", "")));

            return await Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(methodRequest.DataAsJson), 200));
            
        }
        public bool SetInterval(int interval)
        {
            try
            {
                _interval = interval;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task SendMessagAsync()
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

                await deviceClient.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");

                await Task.Delay(_interval * 1000);
            }

        }
    }
}
