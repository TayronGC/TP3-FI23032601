using System.Text.Json.Serialization;
using System.Xml.Serialization;
public class Exponencial
{

    [XmlIgnore]
    [JsonIgnore]
    public double b { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    public bool? XML { get; set; }

    public string? Funcion { get; set; }
    public string? Monotonia { get; set; }


    public void CalcularExponencial()
    {
        if (b <= 0 || b == 1)
        {
            Funcion = "Indefinida"; // B debe ser > 0 y != 1
            Monotonia = "Indefinida"; // B debe ser > 0 y
            return; // B debe ser > 0 y != 1
        }
        
        if (b > 0 && b < 1)
        {
            Monotonia = "decreciente"; // B entre 0 y 1
        }
        else if (b > 1)
        {
            Monotonia = "creciente"; // B mayor que 1
        }

        Funcion = $"f(x) = {b}^x";
        

    }
}