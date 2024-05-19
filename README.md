<h2>Sistema de manejo de inventario</h2>
Este es un proyecto desarrollado en .NET que busca hacer una gestión de inventario sencilla con algunos productos. <br>
<h3>Requerimientos</h3>
<p></p>Lo principal a la hora de correrlo es tener instalado previamente el paquete de .NET para el sistema oparativo correspondiente.
Este lo pueden instalar en el siguiente link:</p>
<a href="https://dotnet.microsoft.com/es-es/download">Descargar .NET</a>
<p>Otro aspecto importante a la hora de correr este proyecto es tener corriendo el simulador de proveedor. Este proyecto se encuentra publicado en el siguiente link:</p>
<a href="https://github.com/Cam1101/Simulador-restablecer-productos">Proveedor</a>
<h3>Cómo usarlo</h3>
<p>Una vez que ya se cumple con todo lo anterior, desde la linea de comandos ejecuta los comandos <code>dotnet restore</code> y <code>dotnet run</code> para permitir que el código escuche peticiones http</p>
<p>Es importante tener en cuenta los puertos que se tienen expuestos para recibir las peticiones, esto se puede verificar en el archivo <code>Properties/launchSettings.json</code>, en caso de 
tener inconveniente con los puertos asignados se pueden modificar cambiando las líneas <code>"applicationUrl": "http://localhost:#puerto"</code> y 
  <code>"applicationUrl": "https://localhost:#puerto;http://localhost:#puerto"</code></p>
<h3>Base de datos</h3>
<p>Este proyecto usa una base de datos sencilla para almacenar los productos que se van a mostar, recomiendo usar MySql Workbench ya que es bastante fácil de usar y es la que se usó para el proyecto. 
  Cuando se tiene definido qué base de datos se va a usar, se modifica el archivo <code>appsettings.json</code> con los datos propios de la base de datos:</p>
<pre>
},
  "AllowedHosts": "*",
  "ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=<em>Nombre-de-la-Base-de-Datos</em>;user=<em>Usuario</em>;password=<em>Contraseña</em>"
}
</pre>

<p>Un ejemplo de un Isert de algunos productos usados para la base de datos son los siguientes:</p>

<pre>
INSERT INTO Productos (Name, Price, Description, Stock, MaxStock) VALUES ('Camiseta', 10, 'Camiseta de algodón', 100, 150);
INSERT INTO Productos (Name, Price, Description, Stock, MaxStock) VALUES ('Bicicleta', 20, 'Bicicleta BMW', 10, 50);
INSERT INTO Productos (Name, Price, Description, Stock, MaxStock) VALUES ('Escoba', 1.55, 'Una escoba común y corriente', 1, 200);
INSERT INTO Productos (Name, Price, Description, Stock, MaxStock) VALUES ('Reloj', 25, 'Un reloj marca Rolex', 5, 10);
</pre>
