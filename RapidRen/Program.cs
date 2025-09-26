// C:\Program Files\Affinity\Publisher 2
using System;
using System.IO;
using System.Timers;
using Myne;


namespace Myne
{
    class RapREN
    {

        private static System.Timers.Timer? _timer;
        static string? Aggregate;
        static string? Target;
        static bool Agg;
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            if (args != null && args.Length > 0)
            {
                if (ParseValid(args))
                {
                    Execute(args);
                }
                else
                {
                    Console.WriteLine("❌ Error: Argumentos inválidos.");
                    ShowHelp();
                }
            }
            else
            {
                Start(); // modo interactivo
            }
        }
        static void ShowHelp()
        {
            Console.WriteLine("Uso: Aggregar A, Renombrar R");
            Console.WriteLine("  myapp.exe /A -agg <nuevo_nombre> -target <extension>");
            Console.WriteLine("  myapp.exe /R -agg <nuevo_nombre> -target <extension>");
            Console.WriteLine("Ejemplo:");
            Console.WriteLine("  myapp.exe /A -agg nuevoNombre -target .txt");
            Console.WriteLine("Si no se pasan parámetros, el programa entra en modo interactivo.");
        }
        static void Execute(string[] args)
        {
            if (args.Any(a => a == "?" || a.ToLowerInvariant() == "--help" || a.ToLowerInvariant() == "-h"))
            {
                ShowHelp();
                return;
            }
            var argsLower = args.Select(a => a.ToLowerInvariant()).ToArray();
            int aggIndex = Array.IndexOf(argsLower, "-agg");
            int targetIndex = Array.IndexOf(argsLower, "-target");

            Aggregate = args[aggIndex + 1]; // no usar .ToLower aquí, mantener original
            Target = args[targetIndex + 1].ToLowerInvariant();
            Agg = args.Any(r=>r.ToLower().Contains("/a"));
            Console.WriteLine($"✅ Ejecutando con parámetros:");
            Console.WriteLine($"   Nuevo nombre: {Aggregate}");
            Console.WriteLine($"   Target: {Target}");
            PrepareStart();
        }
        static bool ParseValid(string[] args)
        {
            // Ejemplo de sintaxis esperada:
            // RapidRen.exe -agg "nuevo_nombre" -target ".txt"

            if (args.Length < 4) return false;

            var argsLower = args.Select(a => a.ToLowerInvariant()).ToArray();

            int aggIndex = Array.IndexOf(argsLower, "-agg");
            int targetIndex = Array.IndexOf(argsLower, "-target");
            if (aggIndex == -1 || targetIndex == -1) return false;
            if (aggIndex + 1 >= args.Length || targetIndex + 1 >= args.Length) return false;
            return true;
        }
        internal static void Start()
        {             
            while (true)
            {
                Console.WriteLine("RapidRen v.0.1");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Seleccione modo de accion:");
                Console.WriteLine("1-Agregar nombre a los archivos de la carpeta actual");
                Console.WriteLine("2-Cambiar el nombre de los archivos de la carpeta actual");
                Console.WriteLine();
                Console.WriteLine("ingrese opcion");
                string input= Console.ReadLine() ?? "";
                if(input.ToLower().Contains("x")|| input.ToLower().Contains("exit")|| input.ToLower().Contains("Salir"))
                {
                    Console.WriteLine("Saliendo...");
                    return;
                }
                if (int.TryParse(input, out int res))
                {                    
                    if (res==1||res==2)
                    {
                        if (res == 1)
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Ingrese el filtro (parte del nombre o extensión):");
                                string filter = Console.ReadLine()??"";
                                if (string.IsNullOrWhiteSpace(filter))
                                {
                                    Console.WriteLine("El filtro no puede estar en blanco a menos que quiera renombrar todos los archivos");
                                    Console.WriteLine("¿Quiere agregar texto a todos los archivos en la carpeta?");
                                    string response= Console.ReadLine() ?? "";
                                    if(!response?.ToLowerInvariant().Contains("y")??true)
                                        continue;
                                }

                                Console.WriteLine("Ingrese el texto a agregar:");
                                string im = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(im))
                                {
                                    Console.WriteLine("El texto a agregar no puede estar en blanco");
                                    continue;
                                }

                                if(string.IsNullOrWhiteSpace(im))
                                    Console.WriteLine($"Se agregarán '{im}' a TODOS los archivos en la carpeta actual");
                                else
                                    Console.WriteLine($"Se agregarán '{im}' a los archivos que contengan '{filter}'");
                                Console.WriteLine("(S)Si  (N)No");
                                string yesNo = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(yesNo) || !yesNo.ToLowerInvariant().Contains("s"))
                                {
                                    if (yesNo.ToLowerInvariant().Contains("n"))
                                        continue;
                                    Console.WriteLine("Debe indicar una respuesta. (S para si, N para no)");
                                    Console.ReadLine();
                                    continue;
                                }

                                Target = filter?.ToLowerInvariant();
                                Aggregate = im;
                                Agg = true;
                                PrepareStart();
                                return;
                            }

                        }
                        else
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Ingrese el filtro (parte del nombre o extensión):");
                                string filter = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(filter))
                                {
                                    Console.WriteLine("El filtro no puede estar en blanco");
                                    continue;
                                }

                                Console.WriteLine("Ingrese el nuevo nombre base:");
                                string im = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(im))
                                {
                                    Console.WriteLine("El nombre base no puede estar en blanco");
                                    continue;
                                }

                                Console.WriteLine($"Se cambiarán los archivos que contengan '{filter}' al patrón '{im}_N.ext'");
                                Console.WriteLine("(S)Si  (N)No");
                                string yesNo = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(yesNo) || !yesNo.ToLowerInvariant().Contains("s"))
                                {
                                    if (yesNo.ToLowerInvariant().Contains("n"))
                                        continue;
                                    Console.WriteLine("Debe indicar una respuesta. (S para si, N para no)");
                                    Console.ReadLine();
                                    continue;
                                }

                                Target = filter.ToLowerInvariant();
                                Aggregate = im;
                                Agg = false;
                                PrepareStart();
                                return;
                            }

                        }

                    }
                    Console.Clear();
                    Console.WriteLine("No se escogio un input valido, intente de nuevo. (" + input + ")");
                }
                else {
                    Console.Clear();
                    Console.WriteLine("No se escogio un input valido, intente de nuevo. ("+input+")");
                }
            }
            
        }
        static void PrepareStart()
        {
            // Configura el timer para que se dispare cada 20 segundos (20000 ms)
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent; 
            _timer.AutoReset = false;
            _timer.Enabled = true;
            Console.WriteLine("Timer iniciado. Presiona cualquier tecla para detener...");
            Console.ReadKey();
            _timer.Stop();
            _timer.Dispose();
        }

        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"\nEjecutando proceso a las {DateTime.Now:HH:mm:ss}");
            RenameFiles();
        }        
        static void RenameFiles()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(currentDir);
            if (Aggregate == null){ Console.WriteLine("Erro Desconocido, el texto de Agregado se perdio."); return; }
            int fileCounter = 1;
            int ErrorCounter = 0;
            int SuccedCounter = 0;
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileNameLower = fileName.ToLowerInvariant();
                if(Target!=null)
                    if (!fileNameLower.Contains(Target))
                        continue;
                string extension = Path.GetExtension(file);
                string newName;
                if (fileNameLower.StartsWith(Aggregate.ToLowerInvariant()))
                    continue;
                if (Agg)
                    newName = $"{Aggregate}{fileName}";
                else
                    newName = $"{Aggregate}_{SuccedCounter+1}{extension}";                
                string newPath = Path.Combine(currentDir, newName);
                try
                {
                    File.Move(file, newPath);
                    Console.WriteLine($"Renombrado: {fileName} -> {newName}");
                    SuccedCounter++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al renombrar {fileName}: {ex.Message}");
                    ErrorCounter++;
                }
                fileCounter++;
            }
            Console.WriteLine("Completado. Archivos totales: " + (SuccedCounter+ErrorCounter));
            Console.WriteLine("Correctos: " + SuccedCounter);
            Console.WriteLine("Errores: " + ErrorCounter);
            _timer.Stop();
            _timer.Dispose();
        }
    }
}