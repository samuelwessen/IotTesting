using ServiceApp;
using System;
using Xunit;

namespace DeviceApp.Tests
{
    public class InvokeMethodTests
    {
        [Theory]
        [InlineData("Inlamningsuppgift5","SetInterval","5","200")]
        [InlineData("Inlamningsuppgift5", "GetInterval", "5", "501")]
        public void InvokeMethod(string targetDevice, string methodName, string payload, string expected)
        {
            var service = new ServiceClientService("HostName=ec-win20-samuelw-iothub.azure-devices.net;DeviceId=inlamningsuppgift5;SharedAccessKey=hh5F4vDEHpSo1C4hw2ozjGMda61nVWpFgFhAvXx9ThA=");
            var response = service.InvokeMethodAsync(targetDevice, methodName, payload);

            Assert.Equal(expected, response.Result.Status.ToString());
        }
    }
}
