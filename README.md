# Tarea 9 – Conexión a GastosDB (C# + ADO.NET)

*Curso:* Algoritmos
*Estudiante:* [Giovanny Gonzalez]
*Carnet:* [1790-25-28835]
*Fecha:* 04/11/2026

## Descripción

Aplicación de consola en C# que se conecta a una base de datos SQL Server LocalDB (GastosDB) usando ADO.NET (Microsoft.Data.SqlClient). Lee los registros de la tabla Gastos, los guarda en una lista de objetos Gasto y muestra el listado con el total acumulado.

## Tecnologías

- C# / .NET (aplicación de consola)
- Visual Studio
- SQL Server LocalDB ((localdb)\MSSQLLocalDB)
- Paquete NuGet: Microsoft.Data.SqlClient

## Estructura del proyecto


├── Program.cs      → clase Gasto y lógica de conexión/lectura
├── GastosDB.sql    → script para crear la base de datos, la tabla y los datos de prueba
└── README.md


## Base de datos

Tabla Gastos:

| Columna     | Tipo               | Descripción           |
|-------------|--------------------|-----------------------|
| Id          | INT (PK, IDENTITY) | Identificador         |
| Descripcion | NVARCHAR(100)      | Descripción del gasto |
| Monto       | DECIMAL(10,2)      | Monto en quetzales    |
| Categoria   | NVARCHAR(50)       | Categoría del gasto   |

## Cadena de conexión

csharp
"Server=(localdb)\\MSSQLLocalDB;Database=GastosDB;Integrated Security=True;TrustServerCertificate=True"


## Cómo ejecutarlo

1. Abrir GastosDB.sql en Visual Studio, conectarlo a (localdb)\MSSQLLocalDB y ejecutarlo para crear la base de datos y los datos de prueba.
2. Abrir la solución en Visual Studio.
3. Restaurar los paquetes NuGet (se instala Microsoft.Data.SqlClient).
4. Ejecutar con Ctrl + F5.

## Escenarios de prueba

### Escenario 1: Conexión exitosa con datos

La aplicación se conecta y muestra los gastos con el total.


===== LISTA DE GASTOS =====
1 | Almuerzo | Q45.50 | Comida
2 | Gasolina | Q250.00 | Transporte
3 | Cine | Q75.00 | Entretenimiento

Total: Q370.50




![Escenario 1](capturas/escenario1.png)



### Escenario 2: Tabla sin registros

Resultado cuando la tabla Gastos está vacía (total en 0).



![Escenario 2](capturas/escenario2.png)



### Escenario 3: Servidor incorrecto

Con un nombre de servidor inválido en la cadena de conexión, el catch muestra el mensaje de error y el programa termina de forma controlada.



![Escenario 3](capturas/escenario3.png)



### Escenario 4: Base de datos inexistente

Con un nombre de base de datos que no existe, se muestra el error de conexión capturado por el try/catch.



![Escenario 4](capturas/escenario4.png)



## Manejo de errores

La conexión y la lectura están dentro de un bloque try/catch. Si ocurre cualquier excepción, se muestra Error: ... en la consola y la aplicación finaliza sin cerrarse de golpe.