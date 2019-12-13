using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface IRewardService
    {
        Task<IList<RewardViewModel>> GetAllAsync();
        Task<RewardViewModel> GetByIdAsync(Guid id);
        Task<PagedList<RewardViewModel>> GetAllPagingAsync(PagingParams pagingParams);
        Task<RewardViewModel> CreateAsync(RewardViewModel rewardViewModel);
        Task<RewardViewModel> UpdateAsync(RewardViewModel rewardViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
        Task<bool> CheckExistsRecordAsync(string number, string description);
    }
}
