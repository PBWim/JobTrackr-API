using JobTrackr.Domain.Enums;

namespace JobTrackr.Application.DTOs;

// ── Job Application DTOs ──────────────────────────────────────────────────────

public record CreateJobApplicationDto(
    string CompanyName,
    string RoleTitle,
    string Country,
    string? JobUrl,
    string? Notes,
    ApplicationStatus Status,
    ApplicationSource Source,
    DateTime AppliedDate,
    DateTime? FollowUpDate
);

public record UpdateJobApplicationDto(
    string CompanyName,
    string RoleTitle,
    string Country,
    string? JobUrl,
    string? Notes,
    ApplicationStatus Status,
    ApplicationSource Source,
    DateTime AppliedDate,
    DateTime? FollowUpDate
);

public record JobApplicationResponseDto(
    int Id,
    string CompanyName,
    string RoleTitle,
    string Country,
    string? JobUrl,
    string? Notes,
    string Status,
    string Source,
    DateTime AppliedDate,
    DateTime? FollowUpDate,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

// ── Dashboard DTO ─────────────────────────────────────────────────────────────

public record DashboardStatsDto(
    int TotalApplications,
    int TotalResponses,
    int TotalInterviews,
    int TotalOffers,
    double ResponseRate,
    Dictionary<string, int> ByStatus,
    Dictionary<string, int> ByCountry
);

// ── Auth DTOs ─────────────────────────────────────────────────────────────────

public record RegisterDto(
    string FullName,
    string Email,
    string Password
);

public record LoginDto(
    string Email,
    string Password
);

public record AuthResponseDto(
    string Token,
    string FullName,
    string Email
);
