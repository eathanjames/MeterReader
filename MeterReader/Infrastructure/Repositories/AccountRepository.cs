using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task CreateAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        
        await context.SaveChangesAsync();
    }

    public async Task<Account?> GetAsync(int accountId)
    {
        return await context.Accounts.FindAsync(accountId);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        //Limit to 100 for now for safety, but this would be configurable.
        return await context.Accounts.Take(100).ToListAsync();
    }
}