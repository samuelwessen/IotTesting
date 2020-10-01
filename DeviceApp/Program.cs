using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibraries.Services;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace DeviceApp
{
    class Program
    {     

        static void Main(string[] args)
        {
            var device = new DeviceClientService("HostName=ec-win20-samuelw-iothub.azure-devices.net;DeviceId=inlamningsuppgift5;SharedAccessKey=hh5F4vDEHpSo1C4hw2ozjGMda61nVWpFgFhAvXx9ThA=");

            device.SendMessagAsync().GetAwaiter();            
            Console.ReadKey();
        }

      

       
    }
}
