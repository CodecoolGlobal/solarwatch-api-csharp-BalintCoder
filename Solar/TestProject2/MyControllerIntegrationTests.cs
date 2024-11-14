using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Solar.Data;
using Solar.Services;

namespace TestProject2;
[Collection("IntegrationTests")]
public class MyControllerIntegrationTests
{
    private readonly SolarWatchWebApplicationFactory _app;
    private readonly HttpClient _client;
    
    public MyControllerIntegrationTests()
    {
        _app = new SolarWatchWebApplicationFactory();
        _client = _app.CreateClient();
    }

    [Fact]
    public async Task TestAddingAndRetrievingUserAsync()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();

        var testUser = new IdentityUser
        {
            UserName = "testuserasd",
            Email = "testuserasd@example.com"
        };

        await userContext.Users.AddAsync(testUser);
        await userContext.SaveChangesAsync();

        var retrievedUser = await userContext.Users.FirstOrDefaultAsync(u => u.Email == "testuserasd@example.com");
        Assert.NotNull(retrievedUser);
        Assert.Equal("testuserasd@example.com", retrievedUser.Email);
    }
    
    [Fact]
    public async Task TestUpdatingUserEmailAsync()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();

        var testUser = new IdentityUser
        {
            UserName = "testuserupdate",
            Email = "updateuser@example.com"
        };

        await userContext.Users.AddAsync(testUser);
        await userContext.SaveChangesAsync();

        testUser.Email = "newemail@gmail.com";
        userContext.Users.Update(testUser);
        await userContext.SaveChangesAsync();

        var retrievedUser = await userContext.Users.FirstOrDefaultAsync(u => u.UserName == "testuserupdate");
        Assert.NotNull(retrievedUser);
        Assert.Equal("newemail@gmail.com", retrievedUser.Email);
    }
    
    [Fact]
    public async Task TestDeletingUserAsync()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();

        var testUser = new IdentityUser
        {
            UserName = "testuserdelete",
            Email = "deleteuser@example.com"
        };

        await userContext.Users.AddAsync(testUser);
        await userContext.SaveChangesAsync();

        userContext.Users.Remove(testUser);
        await userContext.SaveChangesAsync();

        var retrievedUser = await userContext.Users.FirstOrDefaultAsync(u => u.Email == "deleteuser@example.com");
        Assert.Null(retrievedUser);
    }

  
}