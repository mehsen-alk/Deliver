namespace Deliver.Application.Contracts.Service;

public interface IProfitService
{
    public double? GetCaptainProfitFromCalculatedDistance(double? distance);
}