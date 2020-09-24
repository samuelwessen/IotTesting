using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace ServiceApp
{
    class Program
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=ec-win20-samuelw-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=DIJhSlhCzfs92bNMikOddSA5aqvJG06xdPcOLgyRHTA=");

        static void Main(string[] args)
        {
            Task.Delay(5000).Wait();

            InvokeMethod("DeviceApp", "SetTelemetryInterval", "10").GetAwaiter();
            Console.ReadKey();
        }

        static async Task InvokeMethod(string deviceId, string methodName, string payload)
        {
            var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson(payload);


            var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine(response.GetPayloadAsJson());

        }

    }
}
