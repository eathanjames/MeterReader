using Application.Interfaces;

namespace Application.Services;

public class AccountService : IAccountService
{
    public Task CreateAsync(int accountId, string firstName, string lastName)
    {
        throw new NotImplementedException();
    }

    public Task GetAsync(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task GetAllAsync()
    {
        throw new NotImplementedException();
    }
}