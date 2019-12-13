using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IDisciplineService
    {
        Task<IList<DisciplineViewModel>> GetAllAsync();
        Task<DisciplineViewModel> GetByIdAsync(Guid id);
        Task<PagedList<DisciplineViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<DisciplineViewModel> CreateAsync(DisciplineViewModel disciplineViewModel);
        Task<DisciplineViewModel> UpdateAsync(DisciplineViewModel disciplineViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<bool> CheckExistsRecordAsync(string number, string description);
    }
}
