using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace ServiceApp
{
    public class ServiceClientService
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString("HostName=ec-win20-samuelw-iothub.azure-devices.net;DeviceId=inlamningsuppgift5;SharedAccessKey=hh5F4vDEHpSo1C4hw2ozjGMda61nVWpFgFhAvXx9ThA=");

        static void Main(string[] args)
        {
           
        }
        public ServiceClientService(string connetctionstring)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connetctionstring);
        }

        public async Task<CloudToDeviceMethodResult> InvokeMethodAsync(string targetDevice, string methodName, string payload)
        {
            var methodInvocation = new CloudToDeviceMethod(methodName);
            methodInvocation.SetPayloadJson(payload);


            var response = await serviceClient.InvokeDeviceMethodAsync(targetDevice, methodInvocation);
            return response;            

        }

    }
}
