using SharedLibraries.Services;
using System;
using Xunit;
using Microsoft.Azure.Devices.Client;

namespace DeviceApp.Tests
{
    public class InvokeMethodTests
    {
        [Theory]
        [InlineData("inlamningsuppgift5","SetInterval","5","200")]
        [InlineData("inlamningsuppgift5", "GetInterval", "5", "501")]
        public void InvokeMethod(string targetDevice, string methodName, string payload, string expected)
        {
            var service = new ServiceClientService("HostName=ec-win20-samuelw-iothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=DIJhSlhCzfs92bNMikOddSA5aqvJG06xdPcOLgyRHTA=");
            var response = service.InvokeMethodAsync(targetDevice, methodName, payload);

            Assert.Equal(expected, response.Result.Status.ToString());
        }
    }
}
