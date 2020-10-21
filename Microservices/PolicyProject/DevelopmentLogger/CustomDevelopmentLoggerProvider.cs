using Microsoft.Extensions.Logging;

namespace DevelopmentLogger
{
    public class CustomDevelopmentLoggerProvider : ILoggerProvider
    {
        private CustomDevelopmentLogger _customDevLogger;
        private readonly string _fileName;

        public CustomDevelopmentLoggerProvider(string fileName)
        {
            _fileName = fileName;
        }

        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            _customDevLogger = new CustomDevelopmentLogger(_fileName);
            return _customDevLogger;
        }
    }
}