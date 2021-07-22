using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Change
{
    public class Transaction
    {
        public string[] args;
        public string defaultCurrency;
        public string valuesCurrency;
        public string[] valuesBillAndCoins;
        public float price;
        public float sumDelivered;
        public float changeImport;
        public string valuesBillAndCoinsReturn;
    }
}
