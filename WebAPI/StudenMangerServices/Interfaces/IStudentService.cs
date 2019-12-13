using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IStudentService
    {
        Task<IList<StudentViewModel>> GetAllAsync();
        Task<StudentViewModel> GetByIdAsync(Guid id);
        Task<PagedList<StudentViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<StudentViewModel> CreateAsync(StudentViewModel studentViewModel);
        Task<StudentViewModel> UpdateAsync(StudentViewModel studentViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<bool> CheckExistsRecordAsync(string studentCode);
        Task<bool> ChangeGradeAsync(ChangeGradeViewModel changeGradeViewModel);
        List<StudentFlowYearViewModel> GetStudentReportEnrollment(string FromYear, string ToYear);
    }
}
