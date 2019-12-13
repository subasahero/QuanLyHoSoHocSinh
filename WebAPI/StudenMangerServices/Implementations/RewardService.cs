using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;
using Utility.Heplers;

namespace StudenMangerServices.Implementations
{
    public class RewardService : IRewardService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RewardService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<bool> CheckExistsAsync(Guid id)
        {
            RewardViewModel result = await GetByIdAsync(id);
            return result != null ? true : false; 
        }

        public async Task<bool> CheckExistsRecordAsync(string number, string description)
        {
            Reward reward = await _dataContext.Rewards.FirstOrDefaultAsync(x =>
                            x.Number.ToString().ToUpper().Trim() == number.ToUpper().Trim() &&
                            x.Description.ToString().ToUpper().Trim() == description.ToUpper().Trim());
            return reward != null ? true : false;
        }

        public async Task<RewardViewModel> CreateAsync(RewardViewModel rewardViewModel)
        {
            Reward reward = _mapper.Map<Reward>(rewardViewModel);
            await _dataContext.Rewards.AddAsync(reward);
            await _dataContext.SaveChangesAsync();
            RewardViewModel result = _mapper.Map<RewardViewModel>(reward);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            Reward reward = await _dataContext.Rewards
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Rewards.Remove(reward);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<RewardViewModel>> GetAllAsync()
        {
            IList<RewardViewModel> result = await _dataContext.Rewards
                                                            .Where(x => x.Status == true)
                                                            .OrderByDescending(x => x.CreatedDate)
                                                            .ProjectTo<RewardViewModel>(_mapper.ConfigurationProvider)
                                                            .ToListAsync();
            return result;
        }

        public async Task<PagedList<RewardViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<RewardViewModel> query = from r in _dataContext.Rewards
                                                orderby r.Number
                                                select new RewardViewModel
                                                {
                                                    Id = r.Id,
                                                    Number = r.Number,
                                                    Description = r.Description,
                                                    CreatedDate = r.CreatedDate,
                                                    ModifiedDate = r.ModifiedDate,
                                                    Status = r.Status
                                                };
            if(!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(
                    x => x.Number.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Number.ToUpper().Contains(keyword) ||
                    x.Description.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Description.ToUpper().Contains(keyword));
            }
            return await PagedList<RewardViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<RewardViewModel> GetByIdAsync(Guid id)
        {
            Reward reward = await _dataContext.Rewards
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            RewardViewModel result = _mapper.Map<RewardViewModel>(reward);
            return result;
        }

        public async Task<RewardViewModel> UpdateAsync(RewardViewModel rewardViewModel)
        {
            Reward reward = await _dataContext.Rewards.FirstAsync(x => x.Id == rewardViewModel.Id);
            _dataContext.Entry(reward).CurrentValues.SetValues(rewardViewModel);
            await _dataContext.SaveChangesAsync();
            RewardViewModel result = _mapper.Map<RewardViewModel>(reward);
            return result;
        }
    }
}
