using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Core.Logging
{
	public class MyLogger : ILogger
	{
		protected readonly IMyLoggerOptions options;

		public MyLogger(IMyLoggerOptions options)
		{
			this.options = options;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return logLevel != LogLevel.None;
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel) && eventId.Id != 1)
				return;

			if (!Directory.Exists(options.FolderPath))
			{
				Directory.CreateDirectory(options.FolderPath);
			}

			var fullFilePath = options.FolderPath + options.FilePath;
			var logRecord = string.Format($"[{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss+00:00}] : {formatter(state, exception)}");

			using (var streamWriter = new StreamWriter(fullFilePath, true))
			{
				streamWriter.WriteLine(logRecord);
			}
		}
	}
	public class MyLoggerOptions : IMyLoggerOptions
	{
		public string FilePath { get; set; }

		public string FolderPath { get; set; }
	}
	public interface IMyLoggerOptions
	{
		string FilePath { get; set; }

		string FolderPath { get; set; }
	}

}
