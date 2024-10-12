using System.Net.Http.Json;
using Microsoft.AspNetCore.Identity;
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

    // [Fact]
    // public void TestUserExistsInDatabase()
    // {
    //     // Arrange: Create a scope to access the services
    //     using var scope = _app.Services.CreateScope();
    //     var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
    //
    //     // Act: Query for the specific user
    //     var user = userContext.Users.FirstOrDefault(u => u.Email == "gbalint@gmail.com");
    //
    //     // Assert: Verify the user exists
    //     Assert.NotNull(user);
    //     Assert.Equal("gbalint@gmail.com", user.Email); // Further validation
    // }

    [Fact]
    public void TestAddingAndRetrevingUser()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();

        var testUser = new IdentityUser
        {
            UserName = "testuserasd",
            Email = "testuserasd@example.com"
        };

        userContext.Users.Add(testUser);
        userContext.SaveChanges();

        var retrivedUser = userContext.Users.FirstOrDefault(u => u.Email == "testuserasd@example.com");
        Assert.NotNull(retrivedUser);
        Assert.Equal("testuserasd@example.com", retrivedUser.Email);
    }
    
    [Fact]
    public void TestUpdatingUserEmail()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
        var testUser = new IdentityUser
        {
            UserName = "testuserupdate",
            Email = "updateuser@example.com"
        };

        userContext.Users.Add(testUser);
        userContext.SaveChanges();
        testUser.Email = "newemail@gmail.com";
        userContext.Users.Update(testUser);
        userContext.SaveChanges();
        var retrievedUser = userContext.Users.FirstOrDefault(u => u.UserName == "testuserupdate");
        Assert.NotNull(retrievedUser);
        Assert.Equal("newemail@gmail.com", retrievedUser.Email);
    }
    
    [Fact]
    public void TestDeletingUser()
    {
        using var scope = _app.Services.CreateScope();
        var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();

        
        var testUser = new IdentityUser
        {
            UserName = "testuserdelete",
            Email = "deleteuser@example.com"
        };

        userContext.Users.Add(testUser);
        userContext.SaveChanges();

        
        userContext.Users.Remove(testUser);
        userContext.SaveChanges();

        
        var retrievedUser = userContext.Users.FirstOrDefault(u => u.Email == "deleteuser@example.com");
        Assert.Null(retrievedUser); 
    }

  
}