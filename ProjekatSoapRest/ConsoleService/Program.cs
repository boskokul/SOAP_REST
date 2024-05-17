using System;
using System.ServiceModel;

namespace ConsoleService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ConsoleService.CompanyService));
            host.Open();
            Console.WriteLine("Service Hosted!");
            Console.ReadLine();
        }
    }
}
