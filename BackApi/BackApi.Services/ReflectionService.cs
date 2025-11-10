using BackApi.Interfaces;
using BackApi.Services.Exceptions;
using System.Reflection;
using System.Runtime.Loader;

namespace BackApi.Services
{
    public class ReflectionService : IReflectionService
    {
        private string GetImporterInterfaceImplementorDllName(string dllPath)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(dllPath));

            Type[] types;
            try
            {
                types = assembly.GetExportedTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray();
            }

            bool implementsImporterInterface = types
                .Where(t => t != null && t.IsClass && !t.IsAbstract && t.IsPublic)
                .Any(t =>
                    t.GetInterfaces().Any(i =>
                        i.Name == "ImporterInterface"
                        || (i.FullName != null && i.FullName.EndsWith(".ImporterInterface"))
                    )
                );

            if (!implementsImporterInterface)
                throw new ReflectionServiceException("The specified DLL does not implement the ImporterInterface.");

            return Path.GetFileName(dllPath);
        }

        public string[] GetImporterInterfaceImplementorDllsNames(string folderPath)
        {
            var reflectionDir = Path.Combine(AppContext.BaseDirectory, folderPath);

            if (!Directory.Exists(reflectionDir))
                return Array.Empty<string>();

            var dllNames = new List<string>();

            foreach (var dllPath in Directory.EnumerateFiles(reflectionDir, "*.dll", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    string dllName = GetImporterInterfaceImplementorDllName(dllPath);
                    dllNames.Add(Path.GetFileName(dllPath));
                }
                catch (ReflectionServiceException)
                {
                    Console.WriteLine($"Skipping DLL without ImporterInterface implementation: {dllPath}");
                    continue;
                }
                catch (BadImageFormatException)
                {
                    Console.WriteLine($"Skipping non-.NET assembly: {dllPath}");
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error loading assembly from {dllPath}: {e.Message}");
                    throw new ReflectionServiceException(ReflectionServiceException.DllScanErrorMessage);
                }
            }

            return dllNames.ToArray();
        }
    }
}
