using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IDetailDisciplineService
    {
        Task<IList<DetailDisciplineViewModel>> GetAllAsync();
        Task<DetailDisciplineViewModel> GetByIdAsync(Guid id);
        Task<PagedList<DetailDisciplineViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<DetailDisciplineViewModel> CreateAsync(DetailDisciplineViewModel detailDisciplineViewModel);
        Task UpdateAsync(DetailDisciplineViewModel detailDisciplineViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
    }
}
