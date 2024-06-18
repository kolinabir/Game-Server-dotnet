using firstProject.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (1, "GTA V", "Action", 29.99m, new DateOnly(2013, 9, 17)),
    new (2, "FIFA 22", "Sport", 59.99m, new DateOnly(2021, 10, 1)),
    new (3, "Cyberpunk 2077", "RPG", 49.99m, new DateOnly(2020, 12, 10))
];

app.MapGet("/", () => "Hello World!!!!!!");

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));

app.MapGet("/games/find/{name}", (string name) => games.Find((game) => game.Name == name));


app.Run();
