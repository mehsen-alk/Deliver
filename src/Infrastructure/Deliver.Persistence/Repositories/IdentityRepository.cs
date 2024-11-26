using Deliver.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class IdentityRepository
{
    private readonly DeliverDbContext _dbContext;

    public IdentityRepository(DeliverDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddVerificationCodeAsync(VerificationCode verificationCode)
    {
        await _dbContext.VerificationCodes.AddAsync(verificationCode);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    ///     used to fetch an active verification code for test
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>active verification code if exist</returns>
    public async Task<VerificationCode?> GetVerificationCodeAsync(int userId)
    {
        var verificationCode = await _dbContext.VerificationCodes.FirstOrDefaultAsync(vc =>
            vc.UserId == userId && !vc.IsUsed && vc.ExpirationDate > DateTime.UtcNow);

        return verificationCode;
    }

    public async Task<VerificationCode?> GetVerificationCodeAsync(int userId, string code)
    {
        var verificationCode = await _dbContext.VerificationCodes.FirstOrDefaultAsync(vc =>
            vc.UserId == userId && !vc.IsUsed && vc.ExpirationDate > DateTime.UtcNow && vc.Code == code);

        return verificationCode;
    }

    public async Task UpdateVerificationCodeAsync(VerificationCode verificationCode)
    {
        await _dbContext.SaveChangesAsync();
    }
}