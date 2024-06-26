﻿using ProjekatSoapRest.Data;
using System;
using System.ServiceModel;

namespace ConsoleService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new CustomDbContext();
            var serviceInstance = new CompanyService(dbContext);
            ServiceHost host = new ServiceHost(serviceInstance);
            host.Open();
            Console.WriteLine("Service Hosted!");
            Console.ReadLine();
        }
    }
}
