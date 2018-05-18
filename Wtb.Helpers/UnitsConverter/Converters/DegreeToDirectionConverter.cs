using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Wtb.Helpers.UnitsConverter.Converters
{
    public static class DegreeToDirectionConverter
    {
        private static ILog _logger = LogManager.GetLogger("DegreeToDirectionConverter");

        public static string Convert(object value)
        {
            try
            {
                var degree = int.Parse((string) value);
                var direction = string.Empty;
                switch (degree)
                {
                    case 0:
                        direction = "North";
                        _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                        return direction;
                    case 90:
                        direction = "East";
                        _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                        return direction;
                    case 180:
                        direction = "South";
                        _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                        return direction;
                    case 270:
                        direction = "West";
                        _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                        return direction;
                }
                if (degree > 0 && degree < 90)
                {
                    direction = "North-East";
                    _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                    return direction;
                }
                if (degree > 90 && degree < 180)
                {
                    direction = "South-East";
                    _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                    return direction;
                }
                if (degree > 180 && degree < 270)
                {
                    direction = "South-West";
                    _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                    return direction;
                }
                if (degree > 270 && degree < 360)
                {
                    direction = "North-West";
                    _logger.Debug($"Degree to direction converted from {degree} to {direction}");
                    return direction;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Degree to direction converter error: ", ex);
            }
            return string.Empty;
        }
    }
}
