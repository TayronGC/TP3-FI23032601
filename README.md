# Tarea Programada 3 - Minimal API en .NET

**Nombre:** Tayron Jhossue Guzman Campos  
**Carné:** FI23032601  
**Repositorio:** [https://github.com/TayronGC/TP3-FI23032601.git](https://github.com/TayronGC/TP3-FI23032601.git)

---

## 📦 Comandos `dotnet` utilizados

```bash
dotnet new sln -n TareaProgramada3    
dotnet new webapi -o TareaFunciones   
dotnet sln add TareaFunciones
```

## Paginas web que me ayudaron

Durante el desarrollo del proyecto, consulté los siguientes recursos 
- Tec funcion_lineal.pdf:  
  [https://www.tec.ac.cr/sites/default/files/media/doc/funcion_lineal.pdf](https://www.tec.ac.cr/sites/default/files/media/doc/funcion_lineal.pdf)  
  _Para ver todo lo relacionado de la funcion lineal, para ver como obtener la pendiente y las intersecciones en el eje x y y_

- Profesor en linea, Funcion Cuadrática de forma general estandar:  
  [https://profesorenlinea.cl/matematica/Funcion_cuadratica_forma_general_a_estandar.html](https://profesorenlinea.cl/matematica/Funcion_cuadratica_forma_general_a_estandar.html)  
  _Explicacion general de la forma de la función, el vertice la concavidad,etc._

- Profesor en linea, Funcion Cuadrática:  
  [https://profesorenlinea.cl/matematica/funcion_cuadratica.html](https://profesorenlinea.cl/matematica/funcion_cuadratica.html)  
  _Información sobre cómo obtener el vértice y la interpretación de los coeficientes._

## 🤖 Prompts (consultas y respuestas) de ChatGPT utilizados

---

### 🟠 1. Pregunta sobre la funcion exponencial

**Entrada:**

> como se si una funcion exponencial su monotonia es creciente o decreciente?

**Razón:**  
No recordaba como encontrar la monotonia de una funcion exponencial

**Respuesta de ChatGPT:**
Buena pregunta:
La **monotonía de una función exponencial** depende principalmente de **la base** de la potencia.

La función exponencial típica es:

$$
f(x) = a^x
$$

Donde:

* $a$ es la **base** (un número real positivo),
* $x$ es la variable.

---

## ✅ Reglas para la monotonicidad

| Base $a$    | Monotonía       | Ejemplo                          |
| ----------- | --------------- | -------------------------------- |
| $a > 1$     | **Creciente**   | $f(x) = 2^x$, $f(x) = 10^x$      |
| $0 < a < 1$ | **Decreciente** | $f(x) = (1/2)^x$, $f(x) = 0.3^x$ |

> ⚠️ La base **debe ser positiva** y distinta de 1 (si $a = 1$, la función es constante: $f(x) = 1$).

---

### 📈 Ejemplo: función creciente

$$
f(x) = 3^x
$$

* La base $a = 3 > 1$
* Por lo tanto, la función es **creciente**: al aumentar $x$, $f(x)$ también crece.

---

### 📉 Ejemplo: función decreciente

$$
f(x) = \left(\frac{1}{2}\right)^x
$$

* La base $a = 0.5$ → entre 0 y 1
* Por lo tanto, la función es **decreciente**: al aumentar $x$, $f(x)$ disminuye.

---

## 🧠 Tip: ¿cómo saberlo rápido?

* Si la base está **arriba de 1 → creciente**
* Si la base está **entre 0 y 1 → decreciente**

---

¿Querés que te dé un pequeño método en C# que diga si la función exponencial es creciente o decreciente, dado un valor de base?

---
### 🟠 2. Pregunta para ocultar atributos del response

**Entrada:**

> como puedo hacer para que no se muestren los demas atributos del modelo en el response, para que no se muestre b, m , x1,y1,x2,y2, xml, tambien como no mostrarlos en xml,  es una minimal web api en .net 
{
  "b": 3,
  "m": -5,
  "x1": null,
  "y1": null,
  "x2": null,
  "y2": null,
  "funcion": "f(x) = -5x + 3",
  "pendiente": "decreciente",
  "interEjeX": "(0,6, 0)",
  "interEjeY": "(0, 3)",
  "xml": false
}

**Razón:**  
Queria ocultar esos atributos del response porque no debia mostrarlos

**Respuesta de ChatGPT:**

Para ocultar ciertos atributos del modelo en una **Minimal Web API en .NET**, puedes usar varias técnicas dependiendo de cómo estés serializando (JSON o XML) y qué tan dinámico debe ser el control. Aquí te explico ambas formas (JSON y XML):

---

### ✅ **1. Opción más simple (usar un DTO)**

Lo ideal es **no devolver directamente el modelo completo**, sino usar un objeto DTO (**Data Transfer Object**) que solo tenga las propiedades que deseas exponer.

#### 🛠 Ejemplo:

```csharp
public class ResultadoDTO
{
    public string Funcion { get; set; }
    public string Pendiente { get; set; }
    public string InterEjeX { get; set; }
    public string InterEjeY { get; set; }
}
```

Y en tu endpoint:

```csharp
app.MapGet("/resultado", () =>
{
    var modelo = new TuModelo
    {
        b = 3,
        m = -5,
        x1 = null,
        y1 = null,
        x2 = null,
        y2 = null,
        Funcion = "f(x) = -5x + 3",
        Pendiente = "decreciente",
        InterEjeX = "(0.6, 0)",
        InterEjeY = "(0, 3)",
        Xml = false
    };

    var resultado = new ResultadoDTO
    {
        Funcion = modelo.Funcion,
        Pendiente = modelo.Pendiente,
        InterEjeX = modelo.InterEjeX,
        InterEjeY = modelo.InterEjeY
    };

    return Results.Ok(resultado);
});
```

---

### ✅ **2. Si quieres seguir usando el modelo original, puedes usar atributos para ignorar propiedades**

#### Para **JSON**:

```csharp
using System.Text.Json.Serialization;

public class TuModelo
{
    [JsonIgnore]
    public double b { get; set; }

    [JsonIgnore]
    public double m { get; set; }

    [JsonIgnore]
    public double? x1 { get; set; }

    [JsonIgnore]
    public double? y1 { get; set; }

    [JsonIgnore]
    public double? x2 { get; set; }

    [JsonIgnore]
    public double? y2 { get; set; }

    public string Funcion { get; set; }
    public string Pendiente { get; set; }
    public string InterEjeX { get; set; }
    public string InterEjeY { get; set; }

    [JsonIgnore]
    public bool Xml { get; set; }
}
```

---

#### Para **XML**, usa el atributo `[XmlIgnore]`

```csharp
using System.Xml.Serialization;

public class TuModelo
{
    [XmlIgnore]
    public double b { get; set; }

    [XmlIgnore]
    public double m { get; set; }

    [XmlIgnore]
    public double? x1 { get; set; }

    [XmlIgnore]
    public double? y1 { get; set; }

    [XmlIgnore]
    public double? x2 { get; set; }

    [XmlIgnore]
    public double? y2 { get; set; }

    public string Funcion { get; set; }
    public string Pendiente { get; set; }
    public string InterEjeX { get; set; }
    public string InterEjeY { get; set; }

    [XmlIgnore]
    public bool Xml { get; set; }
}
```

---

### ✅ **Resumen**

* ✔️ Usa un DTO si quieres total control sobre lo que se devuelve.
* ✔️ Usa `[JsonIgnore]` para ocultar propiedades en JSON.
* ✔️ Usa `[XmlIgnore]` para ocultar propiedades en XML.
* 🔁 Puedes combinar ambos si necesitas servir ambas respuestas.

¿Quieres que detecte automáticamente si devolver JSON o XML dependiendo del `Accept` header? También puedo ayudarte con eso.

---

