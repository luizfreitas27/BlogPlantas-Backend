using BlogPlantasDomesticas.Api.Enums;
using BlogPlantasDomesticas.Api.Models;
using BlogPlantasDomesticas.Api.Settings;
using BlogPlantasDomesticas.Api.Shared;
using BlogPlantasDomesticas.Api.Shared.Repositories;
using Microsoft.Extensions.Options;

namespace BlogPlantasDomesticas.Api.Services;

public class SeedService : ISeedService
{
    private readonly SeedSettings _seedSettings;
    private readonly ILogger<SeedService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public SeedService(IOptions<SeedSettings> options, IUnitOfWork unitOfWork, ILogger<SeedService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _seedSettings = options.Value;
    }
    
    public async Task SeedAdminAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting SeedAdmin...");

        var adminExist = await _unitOfWork.Users.AdminExistsAsync(cancellationToken);

        if (adminExist)
        {
            _logger.LogWarning("Admin user already exists");
            return;
        }

        if (string.IsNullOrEmpty(_seedSettings.Username))
        {
            _logger.LogWarning("AdminSeed:Username is required.");
            return;
        }

        if (string.IsNullOrEmpty(_seedSettings.Email))
        {
            _logger.LogWarning("AdminSeed:Email is required.");
            return;
        }

        if (string.IsNullOrEmpty(_seedSettings.Password))
        {
            _logger.LogWarning("AdminSeed:Password is required.");
            return;
        }

        if (string.IsNullOrEmpty(_seedSettings.Name))
        {
            _logger.LogWarning("AdminSeed:Name is required.");
            return;
        }

        var user = new User
        {
            Email = _seedSettings.Email,
            Username = _seedSettings.Username,
            Name = _seedSettings.Name,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(_seedSettings.Password),
            RoleId = (int)RoleType.Admin,
            CreatedAt = DateTime.UtcNow
        };
       
       _unitOfWork.Users.Add(user);
       await _unitOfWork.CommitAsync(cancellationToken);
       
       _logger.LogInformation("SeedAdmin completed");
    }
} 