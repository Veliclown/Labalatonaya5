using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw; 
        }
    }

    private void LogException(Exception ex)
    {
        string logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        string logFilePath = Path.Combine(logDirectoryPath, "exceptions.txt");

        try
        {
            
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            var logMessage = $"{DateTime.Now}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";

            
            File.AppendAllText(logFilePath, logMessage);
            Console.WriteLine($"Лог записан в файл: {logFilePath}");
        }
        catch (Exception loggingEx)
        {
            Console.WriteLine($"Не удалось записать в файл логов: {loggingEx.Message}");
            Console.WriteLine($"Стек вызовов: {loggingEx.StackTrace}");
        }
    }
}
