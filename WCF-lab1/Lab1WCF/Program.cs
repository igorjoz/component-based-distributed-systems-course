using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using KSR_WCF1;

namespace KSR_WCF1
{
    [ServiceContract]
    public interface IZadanie2
    {
        [OperationContract]
        string Test(string arg);
    }

    [ServiceContract]
    public interface IZadanie7
    {
        [OperationContract]
        [FaultContract(typeof(Wyjatek7))]
        void RzucWyjatek7(string a, int b);
    }

    [DataContract]
    public class Wyjatek7
    {
        [DataMember]
        public string opis;

        [DataMember]
        public string a;

        [DataMember]
        public int b;
    }
}

namespace Server
{
    public class Server : KSR_WCF1.IZadanie2, KSR_WCF1.IZadanie7
    {
        public string Test(string arg)
        {
            Console.WriteLine($"Otrzymano: {arg}");
            return arg.ToUpperInvariant();
        }

        public void RzucWyjatek7(string a, int b)
        {
            throw new FaultException<KSR_WCF1.Wyjatek7>(
                new KSR_WCF1.Wyjatek7
                {
                    a = a,
                    b = b,
                    opis = $"Zadanie 7: {a}, {b}"
                },
                new FaultReason("Otrzymano  Wyjatek7")
            );
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Server), new Uri[]
            {
                new Uri("net.tcp://127.0.0.1:55765")
            });

            var smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(smb);
            }

            host.AddServiceEndpoint(
                ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadane"
            );

            host.AddServiceEndpoint(
                typeof(KSR_WCF1.IZadanie2),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2"
            );

            host.AddServiceEndpoint(
                typeof(KSR_WCF1.IZadanie2),
                new NetTcpBinding(),
                "net.tcp://127.0.0.1:55765/"
            );

            host.AddServiceEndpoint(
                typeof(KSR_WCF1.IZadanie7),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad7"
            );

            host.Open();
            Console.WriteLine("Server started. Press [Enter] to exit.");
            Console.ReadLine();

            host.Close();
        }
    }
}
