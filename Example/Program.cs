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
      if (TP.Equals(IntPtr.Zero))
        Console.WriteLine("TP Failed");

      double S = 21.0, K = 20.0;
      double vol = 0.4;
      double r = 0.09;
      //IntPtr div = LibOp.NewContinuousDividend(0.1);
      IntPtr div = LibOp.NewDiscreteDividend();

      if (div.Equals(IntPtr.Zero))
        Console.WriteLine("div failed");

      int[] dates = { 182 };
      double[] ammounts = { 1 };

      LibOp.DiscDivSetDates(div, TP, dates);
      LibOp.DiscDivSetAmmounts(div, ammounts);

      IntPtr EuOption =
        LibOp.NewOption(LibOp.OptionType.OPTION_CALL,
          LibOp.ExerciseType.EU_EXERCISE, LibOp.DAYS(TP, 365), K);

      if (EuOption.Equals(IntPtr.Zero))
        Console.WriteLine("euOption failed");

      IntPtr EuAnalytic =
        LibOp.NewPricingMethod(LibOp.PricingMethodId.EU_ANALYTIC,
          vol, r, div);

      if (EuAnalytic.Equals(IntPtr.Zero))
        Console.WriteLine("euAnalytic failed");

      if (LibOp.OptionSetPricingMethod(EuOption, EuAnalytic) < 0)
        Console.WriteLine("setpm failed");

      IntPtr result = LibOp.NewResult();

      if (result.Equals(IntPtr.Zero))
        Console.WriteLine("result failed");

      if (LibOp.OptionPrice(EuOption, S, result) < 0)
        Console.WriteLine("option price failed");

      Console.WriteLine(LibOp.ResultGetPrice(result));

      Console.ReadLine();
    }
  }
}
