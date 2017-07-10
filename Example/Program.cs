using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibOpCS;

namespace Example
{
  class Program
  {
    static void Main(string[] args)
    {
      IntPtr TP = LibOp.NewTimePeriod365();
      double S = 21.0, K = 20.0;
      double vol = 0.4;
      double r = 0.09;
      //IntPtr div = LibOp.NewContinuousDividend(0.1);

      IntPtr div = LibOp.NewDiscreteDividend();

      int[] dates = { 182 } ;
      double[] ammounts = { 1 };

      LibOp.DiscDivSetDates(div, TP, 1, dates);
      LibOp.DiscDivSetAmmounts(div, 1, ammounts);

      IntPtr EuOption =
        LibOp.NewOption(LibOp.OptionType.OPTION_CALL,
          LibOp.ExerciseType.EU_EXERCISE, LibOp.DAYS(TP, 365), K);

      IntPtr EuAnalytic =
        LibOp.NewPricingMethod(LibOp.PricingMethodId.EU_ANALYTIC,
          vol, r, div);

      LibOp.OptionSetPricingMethod(EuOption, EuAnalytic);

      IntPtr result = LibOp.NewResult();

      LibOp.OptionPrice(EuOption, S, result);

      Console.WriteLine(LibOp.ResultGetPrice(result));

      Console.ReadLine();
    }
  }
}
