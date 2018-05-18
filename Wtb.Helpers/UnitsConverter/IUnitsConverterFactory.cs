namespace Wtb.Helpers.UnitsConverter
{
    public interface IUnitsConverterFactory
    {
        object Convert(ConverterType type, object value);
    }
}