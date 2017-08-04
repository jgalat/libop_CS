using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibOpCS;

/**
 * Opción call europea con precio de strike $20 y tiempo a la expiración de un año.
 * Posee un dividendo continuo del 10% y una tasa libre de riesgo de 9%.
 * Se calcula la volatilidad implícita para un precio de subyacente de $21 y
 * un precio de opción de $3.3299 calculado previamente.
 * La volatilidad implícita debería ser de 40%.
 */

namespace EurCallImplVol
{
  class Program
  {
    static void Main(string[] args)
    {
      double  underlying = 21,
              strike = 20;
      /* No es necesario el sigma */
      IntPtr sigma = IntPtr.Zero;

      IntPtr r = LibOp.NewRiskFreeRate(0.09);
      IntPtr d = LibOp.NewContinuousDividend(0.1);

      double price = 3.3299;

      IntPtr tp = LibOp.NewTimePeriod365();

      IntPtr opt = LibOp.NewOption(LibOp.OptionType.OPTION_CALL, 
                                  LibOp.ExerciseType.EU_EXERCISE, 
                                  LibOp.DAYS(tp, 365), 
                                  strike);

      IntPtr pm = LibOp.NewPricingMethod(LibOp.PricingMethodId.EU_ANALYTIC, sigma, r, d);

      LibOp.OptionSetPricingMethod(opt, pm);

      IntPtr result = LibOp.NewResult();

      LibOp.OptionImpliedVolatility(opt, price, underlying, result);

      double implied_vol = LibOp.ResultGetImpliedVolatility(result);

      Console.WriteLine($"Implied Volatility = {implied_vol}");

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
