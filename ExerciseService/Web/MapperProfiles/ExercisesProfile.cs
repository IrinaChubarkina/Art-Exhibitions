namespace Web.MapperProfiles;

using AutoMapper;
using Core.Models;
using FacadeModels;

public class ExercisesProfile : Profile
{
    public ExercisesProfile()
    {
        CreateMap<Exercise, ExerciseResponse>()
            .ReverseMap();

        CreateMap<Exercise, CreateExerciseRequest>()
            .ReverseMap();
    }
}
