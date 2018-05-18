using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Wtb.Helpers.UnitsConverter.Converters;

namespace Wtb.Helpers.UnitsConverter
{
    public class UnitsConverterFactory : IUnitsConverterFactory
    {
        private ILog _logger;

        public UnitsConverterFactory()
        {
            _logger = LogManager.GetLogger("UnitConverterFactory");
        }

        public object Convert(ConverterType type, object value)
        {
            switch (type)
            {
                case ConverterType.Temperature:
                    _logger.Debug("Temperature converter started");
                    return TemperatureConverter.Convert(value);
                case ConverterType.Speed:
                    _logger.Debug("Speed converter started");
                    return SpeedConverter.Convert(value);
                case ConverterType.DegreeToDirection:
                    _logger.Debug("Degree to direction converter started");
                    return DegreeToDirectionConverter.Convert(value);
                case ConverterType.Pressure:
                    _logger.Debug("Pressure converter started");
                    return PressureConverter.Convert(value);
                default:
                    _logger.Error("Unknown converter type");
                    return null;
            }
        }
    }
}
