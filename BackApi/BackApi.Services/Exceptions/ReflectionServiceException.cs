namespace BackApi.Services.Exceptions
{
    public class ReflectionServiceException(string message) : Exception(message)
    {
        public const string DllScanErrorMessage = "An error occurred while scanning DLLs for ImporterInterface implementations.";
    }
}
