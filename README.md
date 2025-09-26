# RapidRen — Renombrador rápido de archivos

Herramienta de consola en **C#** para renombrar archivos en la carpeta actual.  
Soporta modo **interactivo** y ejecución por **línea de comandos (CLI)** para agregar prefijos o renombrar con base + índice.

---

## ✨ Características
- Filtrado por **subcadena** en nombre o extensión (`target`).
- Dos modos de uso:
  - **Agregar texto** al inicio del nombre de archivo.
  - **Cambiar nombre** a una base nueva con índice (`Base_1.ext`, `Base_2.ext`).
- **Modo interactivo** con menú en consola.
- **Modo CLI** para automatización con argumentos.
- Manejo de errores por archivo (si falla `File.Move`, se registra y continúa).
- Prevención de duplicados (no vuelve a renombrar si ya tiene el prefijo).

---

## ⚙️ Requisitos
- .NET SDK (6.0 o superior recomendado).
- Windows CMD / PowerShell (ejecutable de consola).

---

## 📦 Instalación
Clona el repositorio y compila:

``bash
git clone <url-del-repo>
cd RapidRen
dotnet build -c Release

El ejecutable quedará en:

bin/Release/net6.0/RapidRen.exe


(ajusta net6.0 según la versión de .NET usada)

🚀 Uso
1) Modo interactivo

Ejecuta sin parámetros:

RapidRen.exe


El programa mostrará un menú:

1 → Agregar texto (prefijo).

2 → Cambiar nombre por base + índice.

2) Modo CLI

Ejemplo:

RapidRen.exe -agg Reporte_ -target .txt


-agg <texto> → texto que se agregará o usará como base.

-target <filtro> → filtro de extensión o subcadena (en minúsculas).

Mostrar ayuda
RapidRen.exe ?
RapidRen.exe --help

📖 Ejemplos

Prefijar Reporte_ a todos los .txt:

RapidRen.exe -agg Reporte_ -target .txt


Prefijar BACKUP_ a todos los archivos (interactivo → filtro vacío + confirmación).

Renombrar a FOTO_1.jpg, FOTO_2.jpg... (interactivo, opción 2 con filtro .jpg y base FOTO).

⚠️ Notas y limitaciones

Si el nuevo nombre ya existe, el archivo se salta y se registra error.

Actualmente el modo CLI siempre usa el modo Agregar texto.

Se recomienda probar en carpetas de prueba antes de ejecutar en producción.

Idea futura: añadir --dry-run, -mode, y resolución automática de colisiones.

🤝 Contribuir

Haz fork del repositorio.

Crea una rama (feature/nueva-funcion).

Envía un Pull Request.

Sugerencias útiles:

Soporte --dry-run.

Elección de modo en CLI (-mode 1|2).

Resolución automática de nombres en conflicto.
