namespace Web.TrainingProgramClient;

using Web.FacadeModels;

public interface ITrainingClient
{
    Task SendExerciseToTraining(ExerciseResponse exercise);
}
