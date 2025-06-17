using Deliver.Domain.Enums;

namespace Deliver.Application.Features.Finance.Rider.Query.GetRiderPayment;

public class RiderPaymentsVm
{
    public List<RiderPaymentRecordVm> Payments { get; set; } = new();
}

public class RiderPaymentRecordVm
{
    public DateTime CreatedDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentType PaymentType { get; set; }
    public int? TripId { get; set; }
}