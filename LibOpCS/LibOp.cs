using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

//using Option = System.IntPtr;
//using PricingMethod = System.IntPtr;
//using PMSettings = System.IntPtr;

//using TimePeriod = System.IntPtr;

//using Volatility = double;
//using RiskFreeRate = double;
//using Dividend = System.IntPtr;

namespace LibOpCS
{
    public class LibOp
    {
        private const string LibOp_dll = "libop.dll";

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

        [DllImport(LibOp_dll, EntryPoint="new_option")]
        public static extern 
        IntPtr NewOption(OptionType ot,
            ExerciseType et, IntPtr time_period, double strike);

        [DllImport(LibOp_dll, EntryPoint = "delete_option")]
        public static extern 
        void DeleteOption(IntPtr option);

        [DllImport(LibOp_dll, EntryPoint = "option_set_pricing_method")]
        public static extern 
        int OptionSetPricingMethod(IntPtr option, IntPtr pricing_method);


    }
}
