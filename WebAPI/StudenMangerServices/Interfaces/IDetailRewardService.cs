using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IDetailRewardService
    {
        Task<IList<DetailRewardViewModel>> GetAllAsync();
        Task<DetailRewardViewModel> GetByIdAsync(Guid id);
        Task<PagedList<DetailRewardViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<DetailRewardViewModel> CreateAsync(DetailRewardViewModel detailRewardViewModel);
        Task UpdateAsync(DetailRewardViewModel detailRewardViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        List<StudentFlowYearViewModel> GetRewardReport(string FromYear, string ToYear);
    }
}
