using JobTrackr.Application.DTOs;
using JobTrackr.Domain.Entities;
using JobTrackr.Domain.Enums;

namespace JobTrackr.Application.Interfaces;

public interface IJobApplicationRepository
{
    Task<IEnumerable<JobApplication>> GetAllByUserIdAsync(int userId);
    Task<JobApplication?> GetByIdAsync(int id, int userId);
    Task<JobApplication> CreateAsync(JobApplication application);
    Task<JobApplication> UpdateAsync(JobApplication application);
    Task DeleteAsync(JobApplication application);
}

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
}

public interface IJobApplicationService
{
    Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(int userId);
    Task<JobApplicationResponseDto?> GetByIdAsync(int id, int userId);
    Task<JobApplicationResponseDto> CreateAsync(int userId, CreateJobApplicationDto dto);
    Task<JobApplicationResponseDto?> UpdateAsync(int id, int userId, UpdateJobApplicationDto dto);
    Task<bool> DeleteAsync(int id, int userId);
    Task<DashboardStatsDto> GetDashboardStatsAsync(int userId);
}

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}

public interface ITokenService
{
    string GenerateToken(User user);
}
