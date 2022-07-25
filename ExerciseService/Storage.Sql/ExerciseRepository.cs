namespace Storage.Sql;

using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ExerciseRepository : IExerciseRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ExerciseRepository> _logger;

    public ExerciseRepository(AppDbContext context, ILogger<ExerciseRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) >= 0;
    }

    public Task<List<Exercise>> GetAllExercises(CancellationToken cancellationToken)
    {
        return _context.Exercises.ToListAsync(cancellationToken);
    }

    public Task<Exercise?> GetExercise(int exerciseId, CancellationToken cancellationToken)
    {
        return _context.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseId, cancellationToken);
    }

    public async Task CreateExercise(Exercise exercise, CancellationToken cancellationToken)
    {
        if (exercise is null)
        {
            _logger.LogError("Can not create exercise, parameter is missing");
            throw new ArgumentNullException(nameof(exercise));
        }

        await _context.Exercises.AddAsync(exercise, cancellationToken);    
    }
}
