using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<Account?> GetAsync(int accountId)
    {
        return await accountRepository.GetAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await accountRepository.GetAllAsync();
    }
}