using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string SerializeToXml<T>(T obj)
{
    using var stringWriter = new StringWriter();
    var serializer = new XmlSerializer(typeof(T));
    serializer.Serialize(stringWriter, obj);
    return stringWriter.ToString();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/swagger/index.html"));

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/lineal", (
    double b,
    double? m,
    double? x1,
    double? y1,
    double? x2,
    double? y2,
    bool? XML) =>
    {

        var lineal = new Lineal
        {
            b = b,
            m = m,
            x1 = x1,
            y1 = y1,
            x2 = x2,
            y2 = y2,
            XML = XML ?? false
        };
if(lineal.m == null && (lineal.x1 == null || lineal.y1 == null || lineal.x2 == null || lineal.y2 == null))
        {
            return Results.BadRequest("Debe proporcionar m o los puntos (x1, y1) y (x2, y2).");
        }
        lineal.calcularPendiente();
        lineal.calcularIntersecciones();
        lineal.calcularFuncion();
        System.Console.WriteLine(lineal);

        if (XML == true)
        {
            return Results.Content(SerializeToXml(lineal), "application/xml");
        }
        
        return Results.Ok(lineal);
    });


app.MapGet("/cuad", (
    double a,
    double b,
    double c,
    bool? XML) =>
    {
        var cuadratica = new Cuadratica
        {
            a = a,
            b = b,
            c = c,
            XML = XML
        };
        if (cuadratica.a == 0)
        {
            return Results.BadRequest("Parámetro 'a' debe ser diferente de cero.");
        }
        cuadratica.CalcularCuadratica();
        System.Console.WriteLine(cuadratica);

        if (XML == true)
        {
            return Results.Content(SerializeToXml(cuadratica), "application/xml");
        }

        return Results.Ok(cuadratica);
    });

app.MapGet("/exp", (
    double b,
    bool? XML) =>
    {
        var exponencial = new Exponencial
        {
            b = b,
            XML = XML ?? false
        };
        if (exponencial.b <= 0 || exponencial.b == 1)
        {
            return Results.BadRequest("El parámetro 'b' debe ser mayor que 0 y diferente de 1.");
        }
        exponencial.CalcularExponencial();
        System.Console.WriteLine(exponencial);

        if (XML == true)
        {
            return Results.Content(SerializeToXml(exponencial), "application/xml");
        }

        return Results.Ok(exponencial);
    });

/*
app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
*/
app.Run();

/*
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/
