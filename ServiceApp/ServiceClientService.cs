﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace ServiceApp
{
    public class ServiceClientService
    {
        private static ServiceClient serviceClient;


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
