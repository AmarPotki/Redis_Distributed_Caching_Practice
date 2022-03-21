namespace Redis_WebApi.Controllers;

public class ServiceTwo
{
    public Task<string> GetNameAsync(string id) => Task.FromResult("Bob");
}