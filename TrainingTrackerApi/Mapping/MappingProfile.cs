using AutoMapper;
using TrainingTrackerApi.Dtos;
using TrainingTrackerApi.Models;

namespace TrainingTrackerApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<TrainingWeek, TrainingWeekResponseDto>();
            CreateMap<TrainingWeekCreateDto, TrainingWeek>();
            CreateMap<TrainingWeekUpdateDto, TrainingWeek>();

            CreateMap<LiftEntry, LiftEntryResponseDto>();
            CreateMap<LiftEntryCreateDto, LiftEntry>();
            CreateMap<LiftEntryUpdateDto, LiftEntry>();

            CreateMap<NutritionEntry, NutritionEntryDto>();
            CreateMap<NutritionEntryCreateDto, NutritionEntry>();
            CreateMap<NutritionEntryUpdateDto, NutritionEntry>();
        }
    }
}
