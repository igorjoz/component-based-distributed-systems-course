using KSR_WCF2;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Serwer
{
    class Program
    {
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
        private class Server : IZadanie3, IZadanie4
        {
            private int counter = 0;

            public void TestujZwrotny()
            {
                var channel = OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>();

                for (var x = 0; x <= 30; x++) {
                    channel.WolanieZwrotne(x, x * x * x - x * x);
                }
            }

            public void Ustaw(int v)
            {
                counter = v;
            }

            public int Dodaj(int v)
            {
                counter += v;

                return counter;
            }
        }

        public static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Server), new Uri("http://localhost:1100"));
            var behaviour = host.Description.Behaviors.Find<ServiceMetadataBehavior>() ?? new ServiceMetadataBehavior();

            host.Description.Behaviors.Add(behaviour);

            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexNamedPipeBinding(), "net.pipe://localhost/metadane");

            host.AddServiceEndpoint(typeof(IZadanie3), new NetNamedPipeBinding(), "net.pipe://localhost/ksr-wcf2-zad3");
            host.AddServiceEndpoint(typeof(IZadanie4), new NetNamedPipeBinding(), "net.pipe://localhost/ksr-wcf2-zad4");

            host.Open();

            Console.WriteLine("Serwer uruchomiony. Naciśnij Enter, żeby przestać nasłuchiwać.");
            Console.ReadLine();

            host.Close();
        }
    }
}
