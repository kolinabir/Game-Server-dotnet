using firstProject.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (1, "GTA V", "Action", 29.99m, new DateOnly(2013, 9, 17)),
    new (2, "FIFA 22", "Sport", 59.99m, new DateOnly(2021, 10, 1)),
    new (3, "Cyberpunk 2077", "RPG", 49.99m, new DateOnly(2020, 12, 10))
];

app.MapGet("/", () => "Hello World!!!!!!");

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

app.MapGet("/games/find/{name}", (string name) => games.Find((game) => game.Name == name));

app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

});



app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games[index] = new GameDto(
        id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate
    );

    return Results.Accepted(GetGameEndpointName, new
    {
        id,
        updatedGame
    });
});

app.MapDelete("/games/{id}", (int id) =>
{
    var index = games.FindIndex(game => game.Id == id);
    games.RemoveAt(index);

    return Results.NoContent();
});


app.Run();
