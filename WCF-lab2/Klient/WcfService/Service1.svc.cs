using KSR_WCF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfService
{
    public class Service1 : IZadanie5, IZadanie6
    {
        public void Dodaj(int a, int b)
        {
            var channel = OperationContext.Current.GetCallbackChannel<IZadanie6Zwrotny>();
            channel.Wynik(a + b);
        }

        public string ScalNapisy(string a, string b)
        {
            return a + b;
        }
    }
}