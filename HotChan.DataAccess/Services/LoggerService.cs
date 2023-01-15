using Microsoft.Extensions.Logging;

namespace HotChan.DataAccess.Services;

public class LoggerService<T>
{
    private readonly ILogger _logger;
    public LoggerService(ILogger logger) 
    { 
        _logger = logger;
    }

    public T? LogErrorAndReturnDefault(string message) 
    { 
        _logger.LogError(message);

        return default;
    }
}
