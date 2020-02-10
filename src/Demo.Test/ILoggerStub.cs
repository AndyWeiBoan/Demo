using System;
using Microsoft.Extensions.Logging;

namespace Demo.Test {
    public abstract class ILoggerStub<T> : ILogger<T> {

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter) {
            Log(logLevel, exception, formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state) {
            throw new NotImplementedException();
        }

        public abstract void Log(
            LogLevel logLevel,
            Exception exception,
            string content);
    }
}
