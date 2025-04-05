using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client2.ServiceReference1;

namespace Client2
{
    class Program
    {
        private class Handler : IZadanie6Callback
        {
            public void Wynik(int wyn)
            {
                Console.WriteLine($"Zad 6: {wyn}");
            }
        }

        static void Main(string[] args)
        {
            var client5 = new Zadanie5Client();
            Console.WriteLine($"Zad 5: {client5.ScalNapisy("abc", "def")}");

            var client6 = new Zadanie6Client(new InstanceContext(new Handler()));
            client6.Dodaj(1, 2);

            Console.WriteLine("Naciśnij Enter, żeby przestać nasłuchiwać");
            Console.Read();
        }
    }
}
