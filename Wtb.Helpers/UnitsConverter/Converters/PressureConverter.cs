using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Wtb.Helpers.UnitsConverter.Converters
{
    class PressureConverter
    {
        /*
         * To convert inches of mercury to millibars, multiply the inches value by 33.8637526
         */
        private const double PRESSURE_MULTIPLIER = 33.8637526;
        private static ILog _logger = LogManager.GetLogger("PressureConverter");

        public static double Convert(object value)
        {
            try
            {
                var inchesValue = double.Parse((string) value);
                var millibarsValue = inchesValue*PRESSURE_MULTIPLIER;
                _logger.Debug($"Pressure converted from {inchesValue}in to {millibarsValue} millibars");
                return Math.Round(millibarsValue, 2);
            }
            catch (Exception ex)
            {
                _logger.Error("Pressure converter error: ", ex);
            }
            return 0;
        }
    }
}
