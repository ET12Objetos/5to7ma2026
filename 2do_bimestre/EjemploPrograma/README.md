# EjemploPrograma

Proyecto de consola en .NET que apunta al framework `net10.0`.

## Que hace `dotnet publish`

El comando `dotnet publish` prepara una aplicacion para ser distribuida o ejecutada fuera del entorno de desarrollo.

A diferencia de `dotnet build`, que solo compila el proyecto, `dotnet publish` genera una version final de publicacion. Esa salida incluye el programa compilado y los archivos necesarios para ejecutarlo, por ejemplo:

- `EjemploPrograma.exe`: ejecutable de la aplicacion.
- `EjemploPrograma.dll`: ensamblado principal compilado.
- `EjemploPrograma.deps.json`: informacion de dependencias.
- `EjemploPrograma.runtimeconfig.json`: configuracion del runtime de .NET.
- `EjemploPrograma.pdb`: simbolos de depuracion, utiles para diagnostico.

## Comando basico

```powershell
dotnet publish
```

Este comando restaura dependencias si hace falta, compila el proyecto y genera la carpeta de publicacion.

## Donde genera el build publicado

Si no se indica una carpeta de salida, `dotnet publish` genera los archivos publicados dentro de `bin`.

Para este proyecto, una salida tipica seria:

```text
bin\Release\net10.0\publish\
```

Si se publica para un runtime especifico, la ruta puede incluir tambien el identificador del sistema operativo:

```text
bin\Release\net10.0\win-x64\publish\
```

En este proyecto tambien existe una carpeta llamada `VersionFinal`, que corresponde a una publicacion generada indicando manualmente la salida:

```powershell
dotnet publish -c Release -o VersionFinal
```

En ese caso, los archivos finales se generan directamente en:

```text
VersionFinal\
```

## Variantes utiles de parametros

### Publicar en modo Release

```powershell
dotnet publish -c Release
```

`-c` significa `--configuration`. Las configuraciones mas comunes son:

- `Debug`: pensada para desarrollo y pruebas.
- `Release`: pensada para distribuir la aplicacion.

Para una version final, normalmente se usa `Release`.

### Indicar carpeta de salida

```powershell
dotnet publish -c Release -o VersionFinal
```

`-o` significa `--output`. Permite elegir en que carpeta se guardara la publicacion.

Ejemplos:

```powershell
dotnet publish -o publicacion
dotnet publish -c Release -o VersionFinal
dotnet publish -c Release -o C:\Apps\EjemploPrograma
```

### Publicar para un sistema operativo concreto

```powershell
dotnet publish -c Release -r win-x64
```

`-r` significa `--runtime`. Sirve para indicar el sistema donde se ejecutara la aplicacion.

Algunos runtimes comunes:

- `win-x64`: Windows de 64 bits.
- `win-x86`: Windows de 32 bits.
- `linux-x64`: Linux de 64 bits.
- `osx-x64`: macOS Intel.
- `osx-arm64`: macOS Apple Silicon.

### Publicacion dependiente del framework

```powershell
dotnet publish -c Release --self-contained false
```

Esta variante genera una aplicacion mas liviana, pero la computadora donde se ejecute debe tener instalado el runtime de .NET compatible.

Es util cuando el entorno de destino ya tiene .NET instalado.

### Publicacion autocontenida

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

Esta variante incluye el runtime de .NET dentro de la publicacion. La aplicacion puede ejecutarse aunque la computadora destino no tenga .NET instalado.

Ventaja:

- No requiere instalar .NET en la maquina destino.

Desventaja:

- La carpeta generada ocupa mas espacio.

### Generar un solo archivo

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

`PublishSingleFile` intenta empaquetar la aplicacion en un unico ejecutable.

Es comun combinarlo con un runtime especifico, por ejemplo `win-x64`.

### Reducir el tamanio de la publicacion

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishTrimmed=true
```

`PublishTrimmed` intenta quitar partes no usadas del framework y de las dependencias.

Puede reducir el tamanio final, pero hay que probar bien la aplicacion porque en proyectos mas complejos podria eliminar codigo necesario en tiempo de ejecucion.

## Ejemplos recomendados

Publicacion simple en la carpeta por defecto:

```powershell
dotnet publish -c Release
```

Publicacion en la carpeta `VersionFinal`:

```powershell
dotnet publish -c Release -o VersionFinal
```

Publicacion para Windows 64 bits, autocontenida:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -o VersionFinal
```

Publicacion para Windows 64 bits, autocontenida y en un solo archivo:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o VersionFinal
```

## Como ejecutar la publicacion

Si se genero un `.exe`, se puede ejecutar desde PowerShell:

```powershell
.\VersionFinal\EjemploPrograma.exe
```

Si se quiere ejecutar usando el archivo `.dll`:

```powershell
dotnet .\VersionFinal\EjemploPrograma.dll
```

## Resumen

`dotnet publish` es el comando que genera la version lista para distribuir. El build publicado se guarda por defecto dentro de `bin\Release\net10.0\publish\`, aunque se puede cambiar con `-o`.

Para este proyecto, una forma clara de generar la version final es:

```powershell
dotnet publish -c Release -o VersionFinal
```
