using Domain.Entities;

namespace Application.Interfaces;

public interface IAccountRepository
{
    public Task CreateAsync(Account account);
    public Task<Account?> GetAsync(int accountId);
    public Task<IEnumerable<Account>> GetAllAsync();
}
