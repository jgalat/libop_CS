using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibOpCS;

/**
 * Opción call americana con precio de strike $100 y tiempo a la expiración de 252 días.
 * Utilizando un año de 252 días.
 * Posee una volatilidad de 25% y una tasa libre de riesgo de 10%.
 * Posee dividendos discretos a pagar:
 * Tiempo hasta el pago - Monto
 *   50 días               10
 *   200 días              15
 * Se calcula el precio de la opción para un precio de subyacente de $100.
 */

namespace AmCallDiscDivOption
{
  class Program
  {
    static void Main(string[] args)
    {
      double underlying = 100,
              strike = 100;
      IntPtr sigma = LibOp.NewVolatility(0.25);
      IntPtr r = LibOp.NewRiskFreeRate(0.1);
      IntPtr d = LibOp.NewDiscreteDividend();

      IntPtr tp = LibOp.NewTimePeriod252();

      int[] dates = { 50, 200 };
      double[] amounts = { 10, 15 };

      LibOp.DiscDivSetDates(d, tp, dates);
      LibOp.DiscDivSetamounts(d, amounts);

      IntPtr opt = LibOp.NewOption(LibOp.OptionType.OPTION_CALL,
                              LibOp.ExerciseType.AM_EXERCISE,
                              LibOp.DAYS(tp, 252),
                              strike);

      /* Se usan diferencias finitas con una grilla uniforme (AM_FD_UG) */
      IntPtr pm = LibOp.NewPricingMethod(LibOp.PricingMethodId.AM_FD_UG, sigma, r, d);

      LibOp.OptionSetPricingMethod(opt, pm);

      IntPtr result = LibOp.NewResult();

      LibOp.OptionPrice(opt, underlying, result);

      double price = LibOp.ResultGetPrice(result);

      Console.WriteLine($"Option price = {price}");

      LibOp.DeleteResult(result);
      LibOp.DeleteTimePeriod(tp);
      LibOp.DeletePricingMethod(pm);
      LibOp.DeleteRiskFreeRate(r);
      LibOp.DeleteVolatility(sigma);
      LibOp.DeleteDividend(d);
      LibOp.DeleteOption(opt);

      Console.ReadLine();
    }
  }
}