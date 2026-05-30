namespace JobTrackr.Domain.Enums;

public enum ApplicationStatus
{
    Applied,
    Screening,
    Interview,
    TechnicalTest,
    Offer,
    Rejected,
    Withdrawn
}

public enum ApplicationSource
{
    LinkedIn,
    Direct,
    Recruiter,
    Indeed,
    Referral,
    Other
}
