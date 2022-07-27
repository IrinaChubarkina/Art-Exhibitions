using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

using AutoMapper;
using Core.Models;
using Core.Repositories;
using FacadeModels;
using TrainingProgramClient;

[ApiController]
[Route("api/exercises")]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    private readonly ITrainingClient _trainingClient;
    private readonly ILogger<ExercisesController> _logger;

    public ExercisesController(
        IExerciseRepository exerciseRepository,
        IMapper mapper,
        ITrainingClient trainingClient,
        ILogger<ExercisesController> logger)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
        _trainingClient = trainingClient;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetExercises(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all exercises...");
        var exercises = await _exerciseRepository.GetAllExercises(cancellationToken);
        
        return Ok(_mapper.Map<IEnumerable<ExerciseResponse>>(exercises));
    }

    [HttpGet("{exerciseId}")]
    public async Task<IActionResult> GetExercise(int exerciseId, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting the exercise {exerciseId}...",
            exerciseId);

        var exercise = await _exerciseRepository.GetExercise(exerciseId, cancellationToken);

        if (exercise != null)
        {
            return Ok(_mapper.Map<ExerciseResponse>(exercise));
        }

        _logger.LogInformation(
            "Client requested non-existing exercise {exerciseId}",
            exerciseId);
        return NotFound("Exercise not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateExercise(CreateExerciseRequest request, CancellationToken cancellationToken)
    {
        var exercise = _mapper.Map<Exercise>(request);
        await _exerciseRepository.CreateExercise(exercise, cancellationToken);
        await _exerciseRepository.SaveChanges(cancellationToken);

        var createdExercise = _mapper.Map<ExerciseResponse>(exercise);

        try
        {
            await _trainingClient.SendExerciseToTraining(createdExercise);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                "Could not send sync request to Training Service: {ex.Message}",
                ex.Message);
        }

        return CreatedAtAction(
            nameof(GetExercise),
            new { exerciseId = createdExercise.Id },
            createdExercise);
    }
}
