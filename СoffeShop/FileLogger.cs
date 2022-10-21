namespace СoffeShop
{
    public class FileLogger: ILogger, IDisposable
    {
        private string _filePath;
        static object _lock = new object();
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
        public void Dispose() { }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<Tstate>(LogLevel logLevel,EventId eventId, Tstate state, Exception? exception, Func<Tstate,Exception?,string> formatter)
        {
            lock(_lock)
            {
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}
