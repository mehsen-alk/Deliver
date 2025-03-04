using Deliver.Application.Contracts.Service;

namespace Deliver.Application.Services;

public class ProfitService : IProfitService
{
    public decimal GetTripCost(double? distance)
    {
        if (distance == null) return 0;

        var profit = distance * 4000;
        profit = Math.Round(profit ?? 0, 2);

        return (decimal)profit!;
    }

    public decimal GetCaptainProfit(decimal tripCost)
    {
        return tripCost - tripCost * CompanyCommission();
    }

    public decimal CompanyCommission()
    {
        return (decimal)0.2;
    }
}