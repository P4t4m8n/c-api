var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () =>
{
    return Results.Ok(new { status = "healthy" });
});

app.MapPost("/orders/quote", (QuoteRequest request) =>
{
    if (request.UnitPrice <= 0 || request.Quantity <= 0)
    {
        return Results.BadRequest(new
        {
            error = "Unit price and quantity must be greater than zero."
        });
    }

    var total = request.UnitPrice * request.Quantity;

    return Results.Ok(new { total });
});

app.Run();

record QuoteRequest(decimal UnitPrice, int Quantity);