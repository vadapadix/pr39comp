using PostService.Models;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Це API поштового клієнта");


var postings = new List<Posting>
{
    new Posting
    {
        Id = 1,
        From = "Alice",
        To = "Bob",
        Content = "Books",
        DeliveryType = DeliveryType.Courier,
        Weight = 2.5f,
        Width = 30,
        Height = 20,
        Depth = 10,
        Value = 50.0f,
        Price = 10.0f,
        CreatedAt = DateTime.UtcNow
    }
};

app.MapPost("/postings", (Posting posting) =>
{
    posting.Id = postings.Max(p => p.Id) + 1;
    postings.Add(posting);
    return Results.Created($"/postings/{posting.Id}", posting);
});

app.MapGet("/postings", () => postings);

app.MapGet("/postings/{id}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    return posting is null ? Results.NotFound() : Results.Ok(posting);
});
app.MapPut("/postings/{id}", (int id, Posting updated) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();
    posting.From = updated.From;
    posting.To = updated.To;
    posting.Content = updated.Content;
    posting.DeliveryType = updated.DeliveryType;
    posting.Weight = updated.Weight;
    posting.Width = updated.Width;
    posting.Height = updated.Height;
    posting.Depth = updated.Depth;
    posting.Value = updated.Value;
    posting.Price = updated.Price;
    return Results.Ok(posting);
});
app.MapDelete("/postings/{id}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();
    postings.Remove(posting);
    return Results.NoContent();
});

app.Run();