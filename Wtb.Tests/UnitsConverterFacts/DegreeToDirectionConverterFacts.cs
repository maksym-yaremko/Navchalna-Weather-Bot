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
    public class DegreeToDirectionConverterFacts
    {
        private IUnitsConverterFactory _factory;

        public DegreeToDirectionConverterFacts()
        {
            var kernel = new StandardKernel(new TestModule());
            _factory = kernel.Get<IUnitsConverterFactory>();
        }

        [Fact]
        public void Convert_North()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "0");

            Assert.Equal(result, "North");
        }

        [Fact]
        public void Convert_South()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "180");

            Assert.Equal(result, "South");
        }

        [Fact]
        public void Convert_East()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "90");

            Assert.Equal(result, "East");
        }

        [Fact]
        public void Convert_West()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "270");

            Assert.Equal(result, "West");
        }

        [Fact]
        public void Convert_NorthEast()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "45");

            Assert.Equal(result, "North-East");
        }

        [Fact]
        public void Convert_SouthEast()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "135");

            Assert.Equal(result, "South-East");
        }

        [Fact]
        public void Convert_NorthWest()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "315");

            Assert.Equal(result, "North-West");
        }

        [Fact]
        public void Convert_SouthWest()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "225");

            Assert.Equal(result, "South-West");
        }

        [Fact]
        public void WrongConvert()
        {
            var result = _factory.Convert(ConverterType.DegreeToDirection, "500");

            Assert.Equal(result, string.Empty);
        }
    }
}
