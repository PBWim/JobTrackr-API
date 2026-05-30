using JobTrackr.Domain.Enums;

namespace JobTrackr.Domain.Entities;

public class JobApplication
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string RoleTitle { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? JobUrl { get; set; }
    public string? Notes { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public ApplicationSource Source { get; set; } = ApplicationSource.LinkedIn;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public DateTime? FollowUpDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
