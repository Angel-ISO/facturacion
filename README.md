

#    -- 💰💵Facturacion(PDF Generator) 💵💰--

Esta api fue desarrollada por motivo de llevar a cabo conocimientos previos y ser ejecutados de manera que el proceso de construccion sea mas eficiente. Ademas debido a un tema anteriormente asignado es necesario ubicarlo como tema central del proyecto.


#  Acompañamiento para la ejecución del proyecto


#### Como primer paso para probar el proyecto. Es necesario que se reemplacen las credenciales ya existentes por sus credenciales instaladas en su ordenador para efectuar la conexión a la base de datos. Ya sea virtual o local


## ubicación del archivo en el proyecto


el archivo estara dentro de : facturacionAPI\appsettings.Development.json

![ubicacion del archivo](/Media/UbicacionCredenciales.PNG)


### Seguidamente, habiendo reemplazado las credenciales, es hora de generar las migraciones que brevemente nos permitirán poder iniciar nuestro servidor local y poder iniciar la API que es el eje focal del proyecto (en este caso ya tenemos la carpeta de migraciones alojada dentro el proyecto) para poder ejecutar las migraciones usar el siguiente comando




```c#
dotnet ef database update --project ./Persistencia/ --startup-project ./facturacionAPI/
```






#### Ya habiendo realizado los pasos anteriores, la base de datos debería observarse de la siguiente manera




![base de datos](/Media/DiagramaBaseDeDatos.PNG)

#### En caso de que base de datos no se pueda visualizar en su entorno, cabe resaltar que necesita tener en su sistema la librería de herramientas de .net-ef la cual se instala con el siguiente comando (asegúrese de salir del proyecto y ejecutarlo de manera global en su ordenador)


```c#
dotnet tool install --global dotnet-ef
```

En caso de tener una versión anterior de la misma. Por favor realizar la respectiva actualización haciendo uso del siguiente comando:


```c#
dotnet tool update --global dotnet-ef
```

## Realizar peticiones a la api

Una vez iniciado el servidor se puede testear la api con los 4 metodos (get, post, update, delete) el endpoint se encontrara en /facturacionAPI/Controllers en el archivo BaseApiController.cs


![ubicacion endpoint](/Media/EndPointUbicacion.PNG)



### luego puede ejecutar todas las acciones con un limite de 2 peticiones cada 10 segundos esto debido al rate limiter que fue configurado e instalado en las dependencias del proyecto para evitar ataques externos de procedencia no humana

##### ejemplo peticion 

luego de todo lo anterior visto es libre de ya sea consumir la api desde otro proyecto o realizar peticiones como la siguiente


![peticion muestra](/Media/PeticionAceptada.PNG)

en este ejemplo se resalta que los endpoints tiene inyeccion y flujo de data completamente funcional y logica. Debido a los dto configurados dentro del proyecto permitiendo que nuestra DATA tenga un flujo coherente y limpio añadiendo versionado de apis que nos facilitan el proceso


## ya que repasamos la estructura y funcionalidad del proyecto ahora sige el tema central por el cual fue creado. en los siguientes fragmentos veremos como añadir itext7 y castle bouncy adaptor en nuestro proyecto para la generacion correcta de nuestros pdf.



### paso 1 : instalar las extenciones

itext7 y bouncy castle adaptor de itext7 acontinuacion de mostramos cuales son:

![itext7 ](/Media/itext7Extension.PNG)
![castle bouncy itext7 adaptor](/Media/BouncyCastleAdapter.PNG)



###  en que nos ayudan estas extenciones?


itext7: este nos permite la generacion denuestros pdf usando codigo c# y funciones nativas asociandolas con funciones propias de esta libreria.

bouncy castle adaptor:   Bouncy Castle en iText 7 es esencial para habilitar funciones de seguridad y cifrado en tus documentos PDF. Al utilizar esta biblioteca, puedes asegurarte de que los documentos PDF generados y manipulados en tu aplicación sean seguros y cumplan con los estándares de seguridad necesarios de lo contrario nos denegara la creacion.


## proceso de implementacion en proyecto de 4 capas de abstraccion

primero crearemos un nuevo servicio al cual llamaremos: 
"PdfInvoiceService"

![Metodo](/Media/CreadorPdfMetodo.PNG)


seguidamente creado este servicio debemos implementarlo en nuestro contenedor de dependencias para poder hacer uso de el en nuestros controladores:

![dependencias implementacion](/Media/Inyector%20de%20dependencias.PNG)



finalmente continuaremos la implementacion en nuestro controlador 

![controlador implementacion](/Media/controlador.PNG)

cabe aclarar que en este caso especifico se creo un metodo en las interfaces para obtener las facturas por medio del id del cliente pero esto puede variar segun la logica del proyecto.



## finalmente se puede probar el uso y verificar la generacion del pdf. deberia verse de la siguiente manera

![endpoint pdf](/Media/EndpointPdf.PNG)


Cabe aclarar que el proyecto no está 100% culminado. Estar atento a esta documentación que será paulatinamente actualizada 🤗.



## Tecnologias Usadas

<div align="center">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/mysql/mysql-original.svg" height="40" alt="mysql logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" height="40" alt="dotnetcore logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="40" alt="csharp logo"  />
   <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dot-net/dot-net-original.svg" height="40" alt="dot-net logo"  />

###
</div>


## Soporte

para soporte y consultar adicionales, email angelgabrielorteg@gmail.com o enviame solicitud por discord🥰!

video explicacion del proyecto: https://youtu.be/9stNVynT_6U

## Authors

- [@Angel-ISO](https://www.github.com/Angel-ISO)


