using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Wtb.Helpers.UnitsConverter;
using Xunit;

namespace Wtb.Tests.UnitsConverterFacts
{
    public class TemperatureConverterFacts
    {
        private IUnitsConverterFactory _factory;

        public TemperatureConverterFacts()
        {
            var kernel = new StandardKernel(new TestModule());
            _factory = kernel.Get<IUnitsConverterFactory>();
        }

        [Fact]
        public void Convert()
        {
            var result = _factory.Convert(ConverterType.Temperature, "70");

            Assert.Equal(result, 21d);
        }

        [Fact]
        public void WrongConvert()
        {
            var result = _factory.Convert(ConverterType.Temperature, "string");

            Assert.Equal(result, 0d);
        }
    }
}
