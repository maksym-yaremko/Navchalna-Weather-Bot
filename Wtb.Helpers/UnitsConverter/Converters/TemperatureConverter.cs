using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Wtb.Helpers.UnitsConverter.Converters
{
    public static class TemperatureConverter
    {
        private const int FREEZING_POINTER_OF_WATER = 32; //In Fahrenheit
        /*
         * Temperature difference of 1°F temperature difference equivalent to 0.556°C
         */
        private const double TEMPERATURE_DIFFERENCE = 0.556; //In Celsius
        private static ILog _logger = LogManager.GetLogger("TemperatureConverter");

        public static double Convert(object value)
        {
            try
            {
                var fahrenheitValue = double.Parse((string)value);
                var celsiusValue = (fahrenheitValue - FREEZING_POINTER_OF_WATER)*TEMPERATURE_DIFFERENCE;
                _logger.Debug($"Temperature converted from {fahrenheitValue}°F to {celsiusValue}°C");
                return Math.Round(celsiusValue);
            }
            catch (Exception ex)
            {
                _logger.Error("Temperature converter error: ", ex);
            }
            return 0;
        }
    }
}
