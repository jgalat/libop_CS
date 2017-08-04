using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LibOpCS;

/**
 * Opción put americana con precio de strike $100 y tiempo a la expiración de 182 días.
 * Posee un dividendo continuo del 10%, volatilidad de 25% y una tasa libre de
 * riesgo de 10%.
 * Se calcula el precio de la opción para un precio de subyacente de $100.
 * Aumentamos el tamaño de la grilla para obtener mayor precisión.
 */

namespace AmPutGridSize
{
  class Program
  {
    static void Main(string[] args)
    {
      double  underlying = 100,
              strike = 100;
      IntPtr sigma = LibOp.NewVolatility(0.25);
      IntPtr r = LibOp.NewRiskFreeRate(0.1);
      IntPtr d = LibOp.NewContinuousDividend(0.1);

      IntPtr tp = LibOp.NewTimePeriod365();

      IntPtr opt = LibOp.NewOption(LibOp.OptionType.OPTION_PUT,
                                  LibOp.ExerciseType.AM_EXERCISE,
                                  LibOp.DAYS(tp, 182), 
                                  strike);

      /* Se usan diferencias finitas con una grilla no uniforme (AM_FD_NUG) */
      IntPtr pm = LibOp.NewPricingMethod(LibOp.PricingMethodId.AM_FD_NUG, sigma, r, d);

      LibOp.OptionSetPricingMethod(opt, pm);

      IntPtr pms = LibOp.NewPMSettings();

      /* Establecemos una grilla de 300 puntos */
      LibOp.PMSettingsSetGridSize(pms, 300);
      LibOp.PMSetSettings(pm, pms);

      IntPtr result = LibOp.NewResult();

      LibOp.OptionPrice(opt, underlying, result);

      double price = LibOp.ResultGetPrice(result);

      Console.WriteLine($"Option price = {price}");

      LibOp.DeleteResult(result);
      LibOp.DeleteTimePeriod(tp);
      LibOp.DeletePricingMethod(pm);
      LibOp.DeletePMSettings(pms);
      LibOp.DeleteRiskFreeRate(r);
      LibOp.DeleteVolatility(sigma);
      LibOp.DeleteDividend(d);
      LibOp.DeleteOption(opt);

      Console.ReadLine();
    }
  }
}
