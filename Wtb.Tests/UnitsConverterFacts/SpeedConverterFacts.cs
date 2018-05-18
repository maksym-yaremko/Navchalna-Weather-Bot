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
    public class SpeedConverterFacts
    {
        private IUnitsConverterFactory _factory;

        public SpeedConverterFacts()
        {
            var kernel = new StandardKernel(new TestModule());
            _factory = kernel.Get<IUnitsConverterFactory>();
        }

        [Fact]
        public void Convert()
        {
            var result = _factory.Convert(ConverterType.Speed, "120");

            Assert.Equal(result, 193d);
        }

        [Fact]
        public void WrongConvert()
        {
            var result = _factory.Convert(ConverterType.Speed, "string");

            Assert.Equal(result, 0d);
        }
    }
}
