using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace Botworx.AgentLib.ServerLib
{
    public class AgencyServer
    {
        public static readonly AgencyServer Instance = new AgencyServer();
        List<ServiceHostFactory> ServiceHostFactories = new List<ServiceHostFactory>();
        //
        AgencyServer()
        {
        }
        public void Run()
        {
            Add(new AgencyService.HostFactory(new Uri("http://localhost:8090/Agency")));
            foreach (var factory in ServiceHostFactories)
            {
                factory.Create();
            }
            //
            Console.WriteLine("Server started at " + DateTime.Now.ToString());
            Console.WriteLine("Server is running... Press <Enter> key to stop");
            Console.ReadLine();
        }
        void Add(ServiceHostFactory factory)
        {
            ServiceHostFactories.Add(factory);
        }
    }
}
