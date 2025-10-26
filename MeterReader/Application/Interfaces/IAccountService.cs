
namespace Application.Interfaces;

public interface IAccountService
{
    public Task CreateAsync(int accountId, string firstName, string lastName);
    public Task GetAsync(int accountId);
    public Task GetAllAsync();
}