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
            IntPtr Option =
                LibOp.NewOption(LibOp.OptionType.OPTION_CALL,
                        LibOp.ExerciseType.EU_EXERCISE, IntPtr.Zero, 21.0);

            Thread.Sleep(5000);
        }
    }
}
