using JobTrackr.Application.DTOs;
using JobTrackr.Application.Interfaces;
using JobTrackr.Domain.Entities;

namespace JobTrackr.Application.Services;

public class JobApplicationService : IJobApplicationService
{
    private readonly IJobApplicationRepository _repo;

    public JobApplicationService(IJobApplicationRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<JobApplicationResponseDto>> GetAllAsync(int userId)
    {
        var apps = await _repo.GetAllByUserIdAsync(userId);
        return apps.Select(MapToDto);
    }

    public async Task<JobApplicationResponseDto?> GetByIdAsync(int id, int userId)
    {
        var app = await _repo.GetByIdAsync(id, userId);
        return app is null ? null : MapToDto(app);
    }

    public async Task<JobApplicationResponseDto> CreateAsync(int userId, CreateJobApplicationDto dto)
    {
        var app = new JobApplication
        {
            UserId = userId,
            CompanyName = dto.CompanyName,
            RoleTitle = dto.RoleTitle,
            Country = dto.Country,
            JobUrl = dto.JobUrl,
            Notes = dto.Notes,
            Status = dto.Status,
            Source = dto.Source,
            AppliedDate = dto.AppliedDate,
            FollowUpDate = dto.FollowUpDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _repo.CreateAsync(app);
        return MapToDto(created);
    }

    public async Task<JobApplicationResponseDto?> UpdateAsync(int id, int userId, UpdateJobApplicationDto dto)
    {
        var app = await _repo.GetByIdAsync(id, userId);
        if (app is null) return null;

        app.CompanyName = dto.CompanyName;
        app.RoleTitle = dto.RoleTitle;
        app.Country = dto.Country;
        app.JobUrl = dto.JobUrl;
        app.Notes = dto.Notes;
        app.Status = dto.Status;
        app.Source = dto.Source;
        app.AppliedDate = dto.AppliedDate;
        app.FollowUpDate = dto.FollowUpDate;
        app.UpdatedAt = DateTime.UtcNow;

        var updated = await _repo.UpdateAsync(app);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var app = await _repo.GetByIdAsync(id, userId);
        if (app is null) return false;

        await _repo.DeleteAsync(app);
        return true;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync(int userId)
    {
        var apps = (await _repo.GetAllByUserIdAsync(userId)).ToList();

        var total = apps.Count;
        var responses = apps.Count(a => a.Status != Domain.Enums.ApplicationStatus.Applied);
        var interviews = apps.Count(a => a.Status == Domain.Enums.ApplicationStatus.Interview
                                      || a.Status == Domain.Enums.ApplicationStatus.TechnicalTest
                                      || a.Status == Domain.Enums.ApplicationStatus.Offer);
        var offers = apps.Count(a => a.Status == Domain.Enums.ApplicationStatus.Offer);
        var responseRate = total == 0 ? 0 : Math.Round((double)responses / total * 100, 1);

        var byStatus = apps
            .GroupBy(a => a.Status.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        var byCountry = apps
            .GroupBy(a => a.Country)
            .ToDictionary(g => g.Key, g => g.Count());

        return new DashboardStatsDto(total, responses, interviews, offers, responseRate, byStatus, byCountry);
    }

    private static JobApplicationResponseDto MapToDto(JobApplication app) => new(
        app.Id,
        app.CompanyName,
        app.RoleTitle,
        app.Country,
        app.JobUrl,
        app.Notes,
        app.Status.ToString(),
        app.Source.ToString(),
        app.AppliedDate,
        app.FollowUpDate,
        app.CreatedAt,
        app.UpdatedAt
    );
}
