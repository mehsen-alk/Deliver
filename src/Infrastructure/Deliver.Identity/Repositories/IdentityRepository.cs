using Deliver.Identity.Models;

namespace Deliver.Identity.Repositories
{
    public class IdentityRepository
    {

        private readonly DeliverIdentityDbContext _dbContext;

        public IdentityRepository(DeliverIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVerificationToken(VerificationCode verificationCode)
        {
            await _dbContext.VerificationCodes.AddAsync(verificationCode);
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
