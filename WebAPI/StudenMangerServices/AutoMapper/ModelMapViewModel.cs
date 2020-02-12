using AutoMapper;
using Data.Entities;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.AutoMapper
{
    public class ModelMapViewModel : Profile
    {
        public ModelMapViewModel()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<Reward, RewardViewModel>();
            CreateMap<Grade, GradeViewModel>();
            CreateMap<Discipline, DisciplineViewModel>();
            CreateMap<DetailReward, DetailRewardViewModel>();
            CreateMap<DetailDiscipline, DetailDisciplineViewModel>();
            CreateMap<User, UserCreationViewModel>();
            CreateMap<UserRole, UserRoleViewModel>();
            CreateMap<User, UserSessionViewModel>();
            CreateMap<User, UserUpdationViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<StudentScore, StudentScoreViewModel>();
            CreateMap<Certificate, CertificateViewModel>();
        }
    }
}
