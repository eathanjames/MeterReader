
using Domain.Entities;

namespace Application.Interfaces;

public interface IAccountService
{
    public Task<Account?> GetAsync(int accountId);
    public Task<IEnumerable<Account>> GetAllAsync();
}