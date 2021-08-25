# NewsWeatherAPI
Esta solucion realizada en C# .Net:

Archivo NewsController donde se definen 2 endpoints:

CurrentNews : obtiene y muestra las noticias y el estado del clima

GetLastRequest : obtiene el historial de las últimas consultas realizadas

Se utiliza la interfaz INewsRepository para el acceso a datos implementando Insight DataBase

En App_Start/WebApiConfig.cs se define la estructura de las rutas a los servicios definidos
