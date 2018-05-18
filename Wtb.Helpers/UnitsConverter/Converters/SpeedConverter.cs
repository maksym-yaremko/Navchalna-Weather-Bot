using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Wtb.Helpers.UnitsConverter.Converters
{
    public static class SpeedConverter
    {
        private const double ONE_KMH_IN_ONE_MPH = 1.609344;
        private static ILog _logger = LogManager.GetLogger("SpeedConverter");

        public static double Convert(object value)
        {
            try
            {
                var mphValue = double.Parse((string) value);
                var kmhValue = mphValue*ONE_KMH_IN_ONE_MPH;
                _logger.Debug($"Speed converted from {mphValue}mph to {kmhValue}km/h");
                return Math.Round(kmhValue);
            }
            catch (Exception ex)
            {
                _logger.Error("Speed converter error: ", ex);
            }
            return 0;
        }
    }
}
