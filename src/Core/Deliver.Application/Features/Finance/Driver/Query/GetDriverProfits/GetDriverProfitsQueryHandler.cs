using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Finance.Driver.Query.GetDriverProfits;

public class GetDriverProfitsQueryHandler
    : IRequestHandler<GetDriverProfitsQuery, DriverEarningsVm>

{
    private readonly IDriverFinanceRepository _driverFinanceRepository;

    public GetDriverProfitsQueryHandler(IDriverFinanceRepository driverFinanceRepository)
    {
        _driverFinanceRepository = driverFinanceRepository;
    }

    public async Task<DriverEarningsVm> Handle(
        GetDriverProfitsQuery request,
        CancellationToken cancellationToken
    )
    {
        var vm = await _driverFinanceRepository.GetDriverProfits(request.UserId);

        return vm;
    }
}