using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IStudentScoreService
    {
        Task<StudentScoreViewModel> GetByIdAsync(Guid id);
        Task<StudentScoreViewModel> CreateAsync(StudentScoreViewModel studentScoreViewModel);
        Task UpdateAsync(StudentScoreViewModel studentScoreViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
    }
}
