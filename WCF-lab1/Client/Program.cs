using System;
using System.ServiceModel;
using KSR_WCF1;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetNamedPipeBinding binding = new NetNamedPipeBinding();
            EndpointAddress endpoint = new EndpointAddress("net.pipe://localhost/ksr-wcf1-test");

            ChannelFactory<IZadanie1> factory = new ChannelFactory<IZadanie1>(binding, endpoint);
            IZadanie1 proxy = factory.CreateChannel();

            try
            {
                string input = "Przykładowy argument";
                string result = proxy.Test(input);
                Console.WriteLine("Wynik wywołania metody Test: " + result);

                proxy.RzucWyjatek(true);
                Console.WriteLine("Wywołanie RzucWyjatek(true) przebiegło poprawnie.");
            }
            catch (FaultException<Wyjatek> ex)
            {
                Console.WriteLine("Otrzymano wyjątek FaultException:");
                Console.WriteLine("Opis: " + ex.Detail.opis);

                Console.WriteLine("Pole magia: " + ex.Detail.magia);

                // W ciągu sekundy  - OtoMagia
                Console.WriteLine(proxy.OtoMagia(ex.Detail.magia));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił wyjątek: " + ex.Message);
            }
            finally
            {
                try
                {
                    ((IClientChannel)proxy).Close();
                }
                catch { }

                factory.Close();
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}
