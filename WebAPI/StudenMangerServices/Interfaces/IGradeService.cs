using Data.Enum;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IGradeService
    {
        Task<IList<GradeViewModel>> GetAllAsync();
        Task<List<GradeViewModel>> GetGradeByLevelAsync(int level);
        Task<GradeViewModel> GetByIdAsync(Guid id);
        Task<PagedList<GradeViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<GradeViewModel> CreateAsync(GradeViewModel gradeViewModel);
        Task UpdateAsync(GradeViewModel gradeViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<bool> CheckExistsRecordAsync(string name, LevelEnum levelEnum);
    }
}
