using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public static class AccountEndpoints
{
    private const string Route = "api/accounts";
    private const string Tag = "Accounts";
    
    
    public static void Map(WebApplication app)
    {
        app.MapGet($"{Route}/{{id:int}}", (
            [FromServices] IAccountService accountService,
            [FromRoute] int id) => accountService.GetAsync(id))
            .Produces<Account>()
            .Produces(404)
            .WithTags(Tag)
            .WithDescription("Get account by id");
        
        app.MapGet($"{Route}", (
                [FromServices] IAccountService accountService) =>
                accountService.GetAllAsync())
            .Produces<IEnumerable<Account>>()
            .Produces(404)
            .WithTags(Tag)
            .WithDescription("Get all accounts");
    }
}