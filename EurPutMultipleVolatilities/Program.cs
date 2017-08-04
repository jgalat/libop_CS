using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibOpCS;

/**
 * Opción put europea con precio de strike $20 y tiempo a la expiración de un año.
 * Posee un dividendo continuo del 10% y una tasa libre de riesgo de 9%.
 * Se calcula el precio de la opción para un precio de subyacente de $21
 * y para una volatilidad de 40% y para otra de 25%.
 */

namespace EurPutMultipleVolatilities
{
  class Program
  {
    static void Main(string[] args)
    {
      double underlying = 21,
                    strike = 20;
      IntPtr sigma = LibOp.NewVolatility(0.4);
      IntPtr r = LibOp.NewRiskFreeRate(0.09);
      IntPtr d = LibOp.NewContinuousDividend(0.1);

      IntPtr tp = LibOp.NewTimePeriod365();

      IntPtr opt = LibOp.NewOption(LibOp.OptionType.OPTION_PUT,
                                  LibOp.ExerciseType.EU_EXERCISE,
                                  LibOp.DAYS(tp, 365),
                                  strike);

      IntPtr pm = LibOp.NewPricingMethod(LibOp.PricingMethodId.EU_ANALYTIC, sigma, r, d);

      LibOp.OptionSetPricingMethod(opt, pm);

      IntPtr result = LibOp.NewResult();

      LibOp.OptionPrice(opt, underlying, result);

      double price40 = LibOp.ResultGetPrice(result);

      Console.WriteLine($"Option price (Vol: 40%) = {price40}");

      LibOp.VolatilitySetValue(sigma, 0.25);

      LibOp.OptionPrice(opt, underlying, result);

      double price25 = LibOp.ResultGetPrice(result);

      Console.WriteLine($"Option price (Vol: 25%) = {price25}");

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
