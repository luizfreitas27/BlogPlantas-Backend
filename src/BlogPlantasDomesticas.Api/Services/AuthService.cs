using BlogPlantasDomesticas.Api.Dtos.Auth.Request;
using BlogPlantasDomesticas.Api.Dtos.Auth.Response;
using BlogPlantasDomesticas.Api.Enums;
using BlogPlantasDomesticas.Api.Exceptions;
using BlogPlantasDomesticas.Api.Shared.Auth;
using BlogPlantasDomesticas.Api.Shared.Repositories;

namespace BlogPlantasDomesticas.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private IJwtTokenProvider _jwtTokenProvider;
    private ILogger<AuthService> _logger;

    public AuthService(IUnitOfWork unitOfWork, IJwtTokenProvider jwtTokenProvider, ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenProvider = jwtTokenProvider;
        _logger = logger;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting user login {Username}", dto.Username);

        var user = await _unitOfWork.Users.GetByUsernameAsync(dto.Username, cancellationToken);

        if (user == null)
        {
            _logger.LogError("Username or  password is incorrect");
            throw new AppException("Username or password is incorrect", 401);
        }
        
        _logger.LogInformation("Starting password verification...");
        
        var passwordVerified = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!passwordVerified)
        {
            _logger.LogError("Password is incorrect");
            throw new AppException("Password is incorrect", 401);
        }
        
        _logger.LogInformation("Starting jwt token...");

        var (accessToken, expiresAt) = _jwtTokenProvider.GenerateToken(user);
        var newRefreshToken = _jwtTokenProvider.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.ExpiresAt = DateTime.UtcNow.AddDays(7);
        
        _unitOfWork.Users.Update(user);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation("The login has been completed successfully...");

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            RefreshToken = newRefreshToken,
            Username = user.Username,
            RoleId = (RoleType)user.RoleId,
            RoleName = ((RoleType)user.RoleId).ToString()
        };
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting refresh token...");

        var user = await _unitOfWork.Users.GetByRefreshTokenAsync(dto.RefreshToken, cancellationToken);

        if (user?.RefreshToken == null || user.ExpiresAt <= DateTime.UtcNow)
        {
            _logger.LogInformation("Refresh token is invalid");
            throw new AppException("Refresh token is invalid", 401);
        }

        var (accessToken, expiresAt) = _jwtTokenProvider.GenerateToken(user);
        var newRefreshToken = _jwtTokenProvider.GenerateRefreshToken();
        
        user.RefreshToken = newRefreshToken;
        user.ExpiresAt = DateTime.UtcNow.AddDays(7);
        
        _unitOfWork.Users.Update(user);

        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation("The Refresh Token has been generated successfully... ");

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
            RefreshToken = newRefreshToken,
            Username = user.Username,
            RoleId = (RoleType)user.RoleId,
            RoleName = ((RoleType)user.RoleId).ToString()
        }; 
    }
}