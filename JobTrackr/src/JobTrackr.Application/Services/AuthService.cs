using BCrypt.Net;
using JobTrackr.Application.DTOs;
using JobTrackr.Application.Interfaces;
using JobTrackr.Domain.Entities;

namespace JobTrackr.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user is null) return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        var token = _tokenService.GenerateToken(user);
        return new AuthResponseDto(token, user.FullName, user.Email);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow
        };

        var created = await _userRepo.CreateAsync(user);
        var token = _tokenService.GenerateToken(created);
        return new AuthResponseDto(token, created.FullName, created.Email);
    }
}
