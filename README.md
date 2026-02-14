# Practica 2 - Documentación del Proyecto MAUI

Este documento provee una descripción de la arquitectura, ciclo de vida y configuración de este proyecto .NET MAUI.

---

## 1. Estructura del Proyecto

A continuación se describe el propósito de los archivos y carpetas clave en la arquitectura de la aplicación.

### Archivos Principales

-   **`MauiProgram.cs`**: Punto de entrada de la app. Crea y configura el `MauiApp` usando un `MauiAppBuilder`. Aquí se registran fuentes, servicios, y se configura el `App` principal.
-   **`App.xaml` / `App.xaml.cs`**: La clase `App` representa la aplicación. `App.xaml` define recursos globales (colores, estilos) y `App.xaml.cs` maneja el ciclo de vida de la aplicación (`OnStart`, `OnSleep`, `OnResume`) y define la página inicial.
-   **`MainPage.xaml` / `MainPage.xaml.cs`**: La página principal de la aplicación, definiendo su UI en XAML y su lógica en C#.

### Carpetas Clave

-   **`Resources/`**: Contiene todos los assets de la aplicación.
    -   `AppIcon/`: Icono de la aplicación.
    -   `Fonts/`: Fuentes personalizadas (TTF, OTF).
    -   `Images/`: Imágenes y vectores (PNG, SVG).
    -   `Splash/`: Pantalla de carga (splash screen).
    -   `Styles/`: Diccionarios de recursos XAML para estilos y colores globales.
-   **`Platforms/`**: Contiene el código y la configuración específica para cada plataforma de destino (Android, iOS, Windows, MacCatalyst). Esto permite anular o implementar funcionalidades nativas cuando es necesario.

---

## 2. Ciclo de Vida de la Aplicación

Los eventos del ciclo de vida se manejan en `App.xaml.cs` y son cruciales para gestionar el estado de la aplicación.

-   `OnStart()`: Se ejecuta cuando la aplicación se inicia. Es el lugar ideal para inicializar servicios o cargar configuraciones.
-   `OnSleep()`: Se ejecuta cuando la aplicación pasa a segundo plano (el usuario cambia a otra app o va a la pantalla de inicio). Aquí se debe guardar el estado del usuario y detener tareas en segundo plano.
-   `OnResume()`: Se ejecuta cuando la aplicación regresa del segundo plano al primer plano. Es el momento de restaurar el estado y reanudar las tareas que fueron pausadas.

---

## 3. Análisis de `MauiProgram.cs`

Este archivo es el corazón de la configuración de la aplicación.

-   **¿Qué es `MauiApp.CreateBuilder()`?**
    -   Crea un constructor (`MauiAppBuilder`) que permite configurar la aplicación MAUI de forma fluida antes de que se construya y ejecute.

-   **¿Para qué sirve `.UseMauiApp<App>()`?**
    -   Especifica que la clase `App` es el punto de entrada y la definición principal de la aplicación.

-   **¿Qué hace `.ConfigureFonts()`?**
    -   Registra las fuentes personalizadas (archivos `.ttf` u `.otf` de la carpeta `Resources/Fonts/`) para que puedan ser utilizadas en toda la aplicación mediante un alias.

-   **¿Por qué es importante `builder.Build()`?**
    -   Finaliza el proceso de configuración y construye el objeto `MauiApp` que representa la aplicación configurada, lista para ser ejecutada.

---

## 4. Análisis de Código Específico de Plataforma (Bonus)

Esta sección aborda las preguntas del bonus sobre los archivos específicos de la plataforma.

-   **¿Qué información contiene `AndroidManifest.xml`?**
    -   Contiene información esencial sobre la aplicación para las herramientas de compilación de Android, el sistema operativo Android y Google Play. Esto incluye el nombre del paquete de la aplicación, los permisos que requiere la aplicación (como el acceso a Internet) y las definiciones de los componentes de la aplicación.

-   **¿Para qué sirve el archivo `Package.appxmanifest` en Windows?**
    -   Es el archivo de manifiesto para las aplicaciones de Windows (UWP). Define la identidad de la aplicación (nombre, editor), las capacidades (lo que puede hacer, como `runFullTrust`), los elementos visuales (logotipos, pantalla de inicio) y las dependencias necesarias para ejecutarse en Windows.

-   **¿Por qué necesitamos archivos específicos si MAUI es multiplataforma?**
    -   Aunque MAUI abstrae la mayor parte de la interfaz de usuario y la lógica en una única base de código, cada plataforma (Android, iOS, Windows) tiene un conjunto único de características, permisos y requisitos de empaquetado. Estos archivos específicos de la plataforma permiten a los desarrolladores configurar estos ajustes nativos y, si es necesario, escribir código nativo que las API multiplataforma de MAUI no cubren.

### Contenido de `AndroidManifest.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
	<application android:allowBackup="true" android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round" android:supportsRtl="true"></application>
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.INTERNET" />
</manifest>
```
