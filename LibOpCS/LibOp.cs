using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
//using Microsoft.Win32.SafeHandles;

namespace LibOpCS
{
  public class LibOp
  {
    private const string LibOp_dll = "libop.dll";

    /**
      * option_data.h
      */

    public enum OptionType
    {
      OPTION_CALL,
      OPTION_PUT
    };

    public enum ExerciseType
    {
      EU_EXERCISE,
      AM_EXERCISE
    };

    /**  
      * option.h 
      */

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_option")]
    public static extern
    IntPtr NewOption(OptionType option_type,
        ExerciseType exercise_type, IntPtr time_period, double strike);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_option")]
    public static extern
    void DeleteOption(IntPtr option);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_set_pricing_method")]
    public static extern
    int OptionSetPricingMethod(IntPtr option, IntPtr pricing_method);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_price")]
    public static extern
    int OptionPrice(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_prices")]
    private static extern
    int OptionPrices_(IntPtr option, int size, double[] S, IntPtr result);

    public static
    int OptionPrices(IntPtr option, double[] S, IntPtr result)
    {
      return OptionPrices_(option, S.Length, S, result);
    }

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_delta")]
    public static extern
    int OptionDelta(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_gamma")]
    public static extern
    int OptionGamma(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_theta")]
    public static extern
    int OptionTheta(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_rho")]
    public static extern
    int OptionRho(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_vega")]
    public static extern
    int OptionVega(IntPtr option, double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_impl_vol")]
    public static extern
    int OptionImpliedVolatility(IntPtr option, double V,
                                double S, IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "option_price_and_greeks")]
    public static extern
    int OptionPriceAndGreeks(IntPtr option, double S, IntPtr result);

    /**
     * pricing_method.h 
     */

    public enum PricingMethodId {
      EU_ANALYTIC,
      AM_FD
    };

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_pricing_method")]
    public static extern
    IntPtr NewPricingMethod(PricingMethodId id, double volatility,
                            double risk_free_rate, IntPtr dividend);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_pricing_method")]
    public static extern
    void DeletePricingMethod(IntPtr pricing_method);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_pricing_method")]
    public static extern
    int PMSetSettings(IntPtr pricing_method, IntPtr PMSettings);

    /**
     * pm_settings.h
     */

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_pm_settings")]
    public static extern
    IntPtr NewPMSettings();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_pm_settings")]
    public static extern
    void DeletePMSettings(IntPtr pm_settings);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_grid_size")]
    public static extern
    int PMSettingsSetGridSize(IntPtr pm_settings, int grid_size);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_Smax")]
    public static extern
    int PMSettingsSetSMax(IntPtr pm_settings, double s_max);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_tol")]
    public static extern
    int PMSettingsSetTol(IntPtr pm_settings, double tol);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_abstol")]
    public static extern
    int PMSettingsSetAbsTol(IntPtr pm_settings, double abstol);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_iv_max_it")]
    public static extern
    int PMSettingsSetIVMaxIterations(IntPtr pm_settings, int max);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_iv_eps")]
    public static extern
    int PMSettingsSetIVTol(IntPtr pm_settings, double tol);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "pm_settings_set_iv_init")]
    public static extern
    int PMSettingsSetIVInitialGuesses(IntPtr pm_settings,
                                      double guess_vol0,
                                      double guess_vol1);

    /**
     * time_period.h
     */

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_time_period")]
    public static extern
    IntPtr NewTimePeriod(int days_per_year);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_time_period_365d")]
    public static extern
    IntPtr NewTimePeriod365();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_time_period_252d")]
    public static extern
    IntPtr NewTimePeriod252();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_time_period")]
    public static extern
    IntPtr DeleteTimePeriod();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tp_set_days")]
    public static extern
    int TimePeriodSetDays(IntPtr time_period, int days);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tp_set_years")]
    public static extern
    int TimePeriodSetYears(IntPtr time_period, int years);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tp_get_date")]
    public static extern
    double TimePeriodGetDate(IntPtr time_period, int days);

    public static
    IntPtr DAYS(IntPtr time_period, int days)
    {
      TimePeriodSetDays(time_period, days);
      return time_period;
    }

    public static
    IntPtr YEARS(IntPtr time_period, int years)
    {
      TimePeriodSetYears(time_period, years);
      return time_period;
    }

    /**
     * dividend.h
     */

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_continuous_dividend")]
    public static extern
    IntPtr NewContinuousDividend(double cont_dividend);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_discrete_dividend")]
    public static extern
    IntPtr NewDiscreteDividend();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_dividend")]
    public static extern
    void DeleteDividend(IntPtr dividend);
    
    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "div_disc_set_dates_")]
    private static extern
    int DiscDivSetDates_(IntPtr dividend, IntPtr time_period, 
                        int size, int[] dates);

    public static
    int DiscDivSetDates(IntPtr dividend, IntPtr time_period, int[] dates)
    {
      return DiscDivSetDates_(dividend, time_period, dates.Length, dates);
    }

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "div_disc_set_ammounts_")]
    private static extern
    int DiscDivSetAmmounts_(IntPtr dividend, int size, 
                            double[] ammounts);

    public static
    int DiscDivSetAmmounts(IntPtr dividend, double[] ammounts)
    {
      return DiscDivSetAmmounts_(dividend, ammounts.Length, ammounts);
    }
    /**
     * result.h
     */

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "new_result")]
    public static extern
    IntPtr NewResult();

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "delete_result")]
    public static extern
    void DeleteResult(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_price")]
    public static extern
    double ResultGetPrice(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_prices")]
    public static extern
    double[] ResultGetPrices(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_delta")]
    public static extern
    double ResultGetDelta(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_gamma")]
    public static extern
    double ResultGetGamma(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_theta")]
    public static extern
    double ResultGetTheta(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_rho")]
    public static extern
    double ResultGetRho(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_vega")]
    public static extern
    double ResultGetVega(IntPtr result);

    [DllImport(LibOp_dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "result_get_impl_vol")]
    public static extern
    double ResultGetImpliedVolatility(IntPtr result);
  }
}
