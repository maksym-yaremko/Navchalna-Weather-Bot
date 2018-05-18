using Ninject;
using Wtb.Helpers.UnitsConverter;
using Xunit;

namespace Wtb.Tests.UnitsConverterFacts
{
    public class PressureConverterFacts
    {
        private IUnitsConverterFactory _factory;

        public PressureConverterFacts()
        {
            var kernel = new StandardKernel(new TestModule());
            _factory = kernel.Get<IUnitsConverterFactory>();
        }

        [Fact]
        public void Convert()
        {
            var result = _factory.Convert(ConverterType.Pressure, "30");

            Assert.Equal(result, 1015.91);
        }

        [Fact]
        public void WrongConvert()
        {
            var result = _factory.Convert(ConverterType.Pressure, "string");

            Assert.Equal(result, 0d);
        }
    }
}