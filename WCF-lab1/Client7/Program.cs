using System;
using System.ServiceModel;
using KSR_WCF1;

namespace Client7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var client = new Zadanie7Client();

            try
            {
                client.RzucWyjatek7("Test", 123);
            }
            catch (FaultException<Wyjatek7> exception)
            {
                Console.WriteLine(exception.Detail.opis);
            }
        }
    }
}