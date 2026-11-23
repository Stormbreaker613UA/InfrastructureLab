var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("API-B", client =>
{
    client.BaseAddress = new Uri("https://apib:8080");
});

var app = builder.Build();

app.MapGet("/", () => "Hello its API-A!");

app.MapGet("/call-b", async (
    IHttpClientFactory factory,
    CancellationToken cancellationToken) =>
{
    var client = factory.CreateClient("API-B");

    var response = await client.GetStringAsync(
        "/",
        cancellationToken);

    return Results.Ok(new
    {
        Service = "ApiA",
        ResponseFromB = response
    });
});

app.Run();