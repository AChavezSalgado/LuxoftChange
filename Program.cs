using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Change
{
    public class Program
    {

        /// <summary>
        /// The first argument is the price
        /// The next argumentes are the bills and coins values in the format bill-number of bills (500-2, 200-4, ..)
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Transaction trx = new Transaction() {args = args };
            ChangeTransaction ChangeProcess = new ChangeTransaction(trx);

            string resultValidatedArgs = ChangeProcess.ValidateArgs();
            if (resultValidatedArgs.Length > 0)
            {
                Console.WriteLine(resultValidatedArgs);
            }
            else
            {
                trx.changeImport = Math.Abs(trx.price - trx.sumDelivered);
                //Console.WriteLine(string.Format("Delivered: {0} Price {1} Change {2}\n", sumDelivered, price, changeImport));

                trx.valuesBillAndCoinsReturn = ChangeProcess.GetChange(trx.changeImport);

                Console.WriteLine(trx.valuesBillAndCoinsReturn);

            }
            Console.ReadLine();
        }

    }
}
