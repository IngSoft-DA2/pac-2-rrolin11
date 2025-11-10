namespace BackApi.Interfaces
{
    public interface IReflectionService
    {
        public string[] GetImporterInterfaceImplementorDllsNames(string folderPath);
    }
}
