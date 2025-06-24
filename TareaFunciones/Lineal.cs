using System.Text.Json.Serialization;
using System.Xml.Serialization;

public class Lineal
{
    [XmlIgnore][JsonIgnore]
    public double b { get; set; }
    
    [XmlIgnore]
    [JsonIgnore]
    public double? m { get; set; }
    
    [XmlIgnore]
    [JsonIgnore]
    public double? x1 { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public double? y1 { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public double? x2 { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public double? y2 { get; set; }

    public string? funcion { get; set; }

    public string? pendiente { get; set; }

    public string? interEjeX { get; set; }

    public string? interEjeY { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public bool? XML { get; set; }

    public void calcularPendiente()
    {
        if (m == null)
        {
            m = calcularPendientePuntos();
        }

        if (m < 0)
        {
            pendiente = "decreciente";
        }
        else if (m > 0)
        {
            pendiente = "creciente";
        }
        else
        {
            pendiente = "constante";
        }

    }


    private double? calcularPendientePuntos()
    {

        if (x2 == x1)
        {
            return null; // Evitar división por cero
        }
        return (y2 - y1) / (x2 - x1);
    }

    public void calcularIntersecciones()
{
    if (m == null)
        {
            m = calcularPendientePuntos();
        }

        // Intersección con eje Y (siempre existe: x = 0)
        interEjeY = $"(0, {b})";

        // Intersección con eje X (cuando y = 0 → mx + b = 0 → x = -b / m)
        if (m.HasValue && m != 0)
        {
            double x = Math.Round(-b / m.Value, 2);
            interEjeX = $"({x}, 0)";
        }
        else if (m == 0)
        {
            interEjeX = "no hay";
        }
        else
        {
            interEjeX = "indefinida";
        }
}

    
    public void calcularFuncion()
    {
        if (m == null)
        {
            m = calcularPendientePuntos();
        }

        if (m == 0)
        {
            funcion = $"f(x) = {b}";
        }
        else
        {
            funcion = $"f(x) = {m}x + {b}";
        }
    }
    


}