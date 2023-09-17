using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebMvc.Application.Services.Configuration;

namespace WebMvc.Application.Services.AuthService;

public class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly FileServiceConfiguration _fileServiceConfiguration;

    public FileService(ILogger<FileService> logger,
        IOptions<FileServiceConfiguration> fileServiceConfiguration)
    {
        _logger = logger;
        _fileServiceConfiguration = fileServiceConfiguration.Value;
        _logger.LogInformation(_fileServiceConfiguration.Address);
    }
}