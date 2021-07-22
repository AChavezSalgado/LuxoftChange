using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Change
{
    public class ChangeTransaction
    {
        public Transaction transaction;

        public ChangeTransaction(Transaction trx)
        {
            transaction = trx;
        }

        /// <summary>
        /// Validate the data
        /// </summary>
        /// <returns></returns>
        public string ValidateArgs()
        {
            StringBuilder ValidateString = new StringBuilder();

            // Check if the number of parameters
            if (transaction.args.Length == 0 || transaction.args.Length == 1)
            {
                ValidateString.Append("Wrong parameters\n");
                return ValidateString.ToString();
            }

            // Load the Price
            if (float.TryParse(transaction.args[0], out float priceLoad))
            {
                transaction.price = priceLoad;
            }

            // Check if there is defined a Default Currency
            transaction.defaultCurrency = ConfigurationManager.AppSettings["DefaultCurrency"];

            if (string.IsNullOrEmpty(transaction.defaultCurrency))
            {
                ValidateString.Append("Default Currency is Not Defined\n");
                return ValidateString.ToString();
            }
            else
            {
                // Check if there is defined the values for the Default Currency
                transaction.valuesCurrency = ConfigurationManager.AppSettings[transaction.defaultCurrency];

                // If it is not defined a default currency will not continue the process
                if (string.IsNullOrEmpty(transaction.valuesCurrency))
                {
                    ValidateString.Append(string.Format("Bills and Coins values for {0} Currency are Not Defined\n", transaction.valuesCurrency));
                    return ValidateString.ToString();
                }

                // Get the Bill and Coins values from configuration
                transaction.valuesBillAndCoins = transaction.valuesCurrency.Split(',');

                transaction.sumDelivered = 0.00F;

                // Check if the value bill delivered exists for the current currency
                for (int i = 1; i <= transaction.args.Length - 1; i++)
                {
                    string[] pairMoney = transaction.args[i].Split('-');
                    var existMoney = transaction.valuesBillAndCoins.Any(x => x == pairMoney[0]);
                    if (!existMoney)
                    {
                        ValidateString.Append(string.Format("Values {0} Not Existed\n", pairMoney[0]));
                    }
                    else
                    {
                        if (Int32.TryParse(pairMoney[0], out int valueBill))
                        {
                            if (pairMoney.Length >= 2)
                            {
                                if (Int32.TryParse(pairMoney[1], out int numberBill))
                                {
                                    transaction.sumDelivered += (valueBill * numberBill);
                                }
                            }
                            else
                            {
                                ValidateString.Append(string.Format("Missing number for Values {0}\n", pairMoney[0]));
                            }
                        }
                    }
                }

                // Check if the sum is greater or equal to the price
                if (transaction.sumDelivered < transaction.price)
                {
                    ValidateString.Append(string.Format("Delivered: {0} less than Price {1} \n", transaction.sumDelivered, transaction.price));
                }

            }

            return ValidateString.ToString();
        }


        /// <summary>
        /// Define the bills and coins return as a change of the transaction
        /// </summary>
        /// <param name="import"></param>
        /// <returns></returns>
        public string GetChange(float import)
        {
            StringBuilder valuesBillAndCoinsReturn = new StringBuilder();

            double sumNewBills = import;

            foreach (var v in transaction.valuesBillAndCoins)
            {
                if (Double.TryParse(v, out double valueBill))
                {
                    if (sumNewBills > 0)
                    {
                        var noBills = Math.Truncate(sumNewBills / valueBill);

                        if (noBills > 0)
                        {
                            double newImport = (valueBill * noBills);

                            if (valuesBillAndCoinsReturn.Length > 0) valuesBillAndCoinsReturn.Append(",");
                            valuesBillAndCoinsReturn.Append(string.Format("{0}-{1}", valueBill, noBills));
                            sumNewBills = Math.Round(sumNewBills - newImport, 2);
                        }
                    }
                }
            }
            return valuesBillAndCoinsReturn.ToString();
        }

    }
}
