using Deliver.Domain.Enums;

namespace Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;

public class DriverEarningsVm
{
    public decimal TotalEarnings { get; set; }
    public List<PaymentRecordVm> Payments { get; set; } = new();
}

public class PaymentRecordVm
{
    public DateTime CreatedDate { get; set; }
    public decimal Amount { private get; set; }
    public decimal CompanyCommission { private get; set; }
    public decimal Value => Amount - Amount * CompanyCommission;
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentType PaymentType { get; set; }
    public int? TripId { get; set; }
}