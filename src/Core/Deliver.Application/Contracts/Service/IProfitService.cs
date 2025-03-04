namespace Deliver.Application.Contracts.Service;

public interface IProfitService
{
    public decimal GetTripCost(double? distance);
    public decimal GetCaptainProfit(decimal tripCost);
    public decimal CompanyCommission();
}