namespace WebAPI.Services
{
    public class DatabaseLogger : ILoggerService
    {

        public void Write(string message)
        {
            Console.WriteLine($"[ Database Logger ] {message}");
        }
    }
}
