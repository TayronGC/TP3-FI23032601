using System.Text.Json.Serialization;
using System.Xml.Serialization;

public class Cuadratica
{

    [XmlIgnore]
    [JsonIgnore]
    public double a { get; set; }
    [XmlIgnore]
    [JsonIgnore]
    public double b { get; set; }
    [XmlIgnore]
    [JsonIgnore]
    public double c { get; set; }
[XmlIgnore]
    [JsonIgnore]
    public bool? XML { get; set; } 

    public string? Funcion { get; set; }
    public string? Discriminante { get; set; }
    public string? EjeDeSimetria { get; set; }
    public string? Concavidad { get; set; }
    public string? Vertice { get; set; }
    public string? InterseccionConEjeX { get; set; }
    public string? InterseccionConEjeY { get; set; }
    public string? Ambito { get; set; }
    public string? Monotonias { get; set; }


    public void CalcularCuadratica()
{
    if (a == 0)
        return;

    Funcion = $"f(x) = {a}x² + {b}x + {c}";

    double discriminante = b * b - 4 * a * c;
    Discriminante = $"Δ = {discriminante}";

    double xVertice = -b / (2 * a);
    double yVertice = a * xVertice * xVertice + b * xVertice + c;
    Vertice = $"({xVertice}, {yVertice})";
    EjeDeSimetria = $"x = {xVertice}";
    Concavidad = a > 0 ? "hacia arriba" : "hacia abajo";

    if (discriminante < 0)
    {
        InterseccionConEjeX = "no hay";
    }
    else if (discriminante == 0)
    {
        double xInterseccion = -b / (2 * a);
        InterseccionConEjeX = $"({xInterseccion}, 0)";
    }
    else
    {
        double raizDiscriminante = Math.Sqrt(discriminante);
        double x1 = (-b + raizDiscriminante) / (2 * a);
        double x2 = (-b - raizDiscriminante) / (2 * a);
        InterseccionConEjeX = $"({x1}, 0), ({x2}, 0)";
    }

    InterseccionConEjeY = $"(0, {c})";

    if (a > 0)
    {
        Ambito = $"[{yVertice}, +∞[";
        Monotonias = $"decrece en ]−∞, {xVertice}[, crece en ]{xVertice}, +∞[";
    }
    else
    {
        Ambito = $"]−∞, {yVertice}]";
        Monotonias = $"crece en ]−∞, {xVertice}[, decrece en ]{xVertice}, +∞[";
    }
}

}