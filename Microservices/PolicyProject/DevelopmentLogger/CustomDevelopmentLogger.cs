using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DevelopmentLogger
{
    public class CustomDevelopmentLogger : ILogger
    {
        private readonly string _fileName;

        public CustomDevelopmentLogger(string fileName)
        {
            _fileName = fileName;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            new Task(() =>
            {
                var locker = new object();

                lock (locker)
                {
                    File.AppendAllText(_fileName,
                        $"{DateTime.Now} - {logLevel} - {formatter(state, exception)}{Environment.NewLine}");
                }
            }).Start();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
