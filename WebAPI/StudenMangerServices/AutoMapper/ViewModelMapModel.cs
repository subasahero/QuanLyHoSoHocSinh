using AutoMapper;
using Data.Entities;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.AutoMapper
{
    public class ViewModelMapModel : Profile
    {
        public ViewModelMapModel()
        {
            CreateMap<StudentViewModel, Student>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<RewardViewModel, Reward>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<DisciplineViewModel, Discipline>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<DetailRewardViewModel, DetailReward>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<DetailDisciplineViewModel, DetailDiscipline>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<GradeViewModel, Grade>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<StudentScoreViewModel, StudentScore>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status ?? true);
                });
            CreateMap<UserViewModel, User>();
            CreateMap<UserRoleViewModel, UserRole>();
            CreateMap<RoleViewModel, Role>();
            CreateMap<UserSessionViewModel, User>();
            CreateMap<UserUpdationViewModel, User>();
            
        }
    }
}
