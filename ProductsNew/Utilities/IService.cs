namespace ProductsNew
{
    public interface IService
    {
        decimal EstimatePrice(decimal volume);
        decimal EstimateVolume(decimal length, decimal width, decimal height);
    }
}