using JobTrackr.Application.Interfaces;
using JobTrackr.Domain.Entities;
using JobTrackr.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobTrackr.Infrastructure.Repositories;

public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly AppDbContext _db;

    public JobApplicationRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<JobApplication>> GetAllByUserIdAsync(int userId) =>
        await _db.JobApplications
            .Where(j => j.UserId == userId)
            .OrderByDescending(j => j.AppliedDate)
            .ToListAsync();

    public async Task<JobApplication?> GetByIdAsync(int id, int userId) =>
        await _db.JobApplications
            .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);

    public async Task<JobApplication> CreateAsync(JobApplication application)
    {
        _db.JobApplications.Add(application);
        await _db.SaveChangesAsync();
        return application;
    }

    public async Task<JobApplication> UpdateAsync(JobApplication application)
    {
        _db.JobApplications.Update(application);
        await _db.SaveChangesAsync();
        return application;
    }

    public async Task DeleteAsync(JobApplication application)
    {
        _db.JobApplications.Remove(application);
        await _db.SaveChangesAsync();
    }
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByEmailAsync(string email) =>
        await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(int id) =>
        await _db.Users.FindAsync(id);

    public async Task<User> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
