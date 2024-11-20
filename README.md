# Tambo - Sistema de Gestión de Tiendas

## Descripción
**Tambo** es un sistema de gestión desarrollado en **C#** con **Windows Forms** que permite a los usuarios administrar inventarios, empleados, ventas y reportes en una cadena de tiendas de conveniencia. Este sistema está diseñado para facilitar el control y monitoreo de las operaciones diarias de las sucursales.

## Características principales
- **Gestión de Inventarios**:
  - Registrar inventarios por sucursal.
  - Actualizar y visualizar productos con stock bajo.
  - Control del stock de productos en tiempo real.
  
- **Gestión de Empleados**:
  - Registro, modificación y eliminación de empleados.
  - Restricción de accesos para usuarios que no son administradores.

- **Gestión de Ventas**:
  - Registro de boletas y detalle de boletas.
  - Control de pagos y eliminación lógica de boletas.

- **Reportes**:
  - Productos más vendidos.
  - Sucursales con más ventas.
  - Ventas por trabajador.
  - Productos con stock bajo.

- **Interfaz amigable**:
  - Timer en tiempo real con fecha y hora.
  - Bienvenida personalizada para cada trabajador al iniciar sesión.
  - Menú lateral intuitivo para acceder rápidamente a cada funcionalidad.

## Requisitos del sistema
- **Entorno de desarrollo**: Visual Studio 2022 o superior.
- **Framework**: .NET Framework 4.8.
- **Base de datos**: SQL Server.
- **Sistema operativo**: Windows 10 o superior.

## Estructura del proyecto

- **Datos:** Capa encargada de interactuar con la base de datos.
- **Negocio:** Capa de lógica de negocio y validaciones.
- **TPTAMBO:** Interfaz de usuario y formularios principales.

## Colaboradores
- Elvis Larico
- Adrian Ruiz
- Stefany Peña
- Fabrizio Palomino
- Joao Ñaña
  
## Licencia
Este proyecto es de uso privado y no está permitido su distribución sin autorización previa.
