using AutoMapper;
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
    public class DetailRewardService : IDetailRewardService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DetailRewardService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            DetailRewardViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<DetailRewardViewModel> CreateAsync(DetailRewardViewModel detailRewardViewModel)
        {
            DetailReward detailReward = _mapper.Map<DetailReward>(detailRewardViewModel);
            await _dataContext.DetailRewards.AddAsync(detailReward);
            await _dataContext.SaveChangesAsync();
            DetailRewardViewModel result = _mapper.Map<DetailRewardViewModel>(detailReward);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            DetailReward detailReward = await _dataContext.DetailRewards
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.DetailRewards.Remove(detailReward);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<DetailRewardViewModel>> GetAllAsync()
        {
            IList<DetailRewardViewModel> result = await (from d in _dataContext.DetailRewards
                                                  join s in _dataContext.Students on d.StudentId equals s.Id
                                                  join r in _dataContext.Rewards on d.RewardId equals r.Id
                                                  orderby d.CreatedDate
                                                  select new DetailRewardViewModel
                                                  {
                                                      Id = d.Id,
                                                      StudentId = d.StudentId,
                                                      RewardId = d.RewardId,
                                                      Reason = d.Reason,
                                                      Gift = d.Gift,
                                                      StudentVM = _mapper.Map<StudentViewModel>(s),
                                                      RewardVM = _mapper.Map<RewardViewModel>(r),
                                                      CreatedDate = d.CreatedDate,
                                                      ModifiedDate = d.ModifiedDate,
                                                      Status = d.Status
                                                  }).ToListAsync();
            return result;
        }

        public async Task<PagedList<DetailRewardViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<DetailRewardViewModel> query = from dr in _dataContext.DetailRewards
                                                      join s in _dataContext.Students on dr.StudentId equals s.Id
                                                      join r in _dataContext.Rewards on dr.RewardId equals r.Id
                                                      orderby dr.CreatedDate descending
                                                      select new DetailRewardViewModel
                                                      {
                                                          Id = dr.Id,
                                                          StudentId = dr.StudentId,
                                                          RewardId = dr.RewardId,
                                                          Reason = dr.Reason,
                                                          Gift = dr.Gift,
                                                          DateReward = dr.DateReward,
                                                          StudentVM = _mapper.Map<StudentViewModel>(s),
                                                          RewardVM = _mapper.Map<RewardViewModel>(r),
                                                          CreatedDate = dr.CreatedDate,
                                                          ModifiedDate = dr.ModifiedDate,
                                                          Status = dr.Status
                                                      };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                    x.StudentVM.Name.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.StudentVM.Name.ToUpper().Contains(keyword) ||
                                    x.RewardVM.Description.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.RewardVM.Description.ToUpper().Contains(keyword) ||
                                    x.StudentVM.Code.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.StudentVM.Code.ToUpper().Contains(keyword) ||
                                    x.RewardVM.Number.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.RewardVM.Number.ToUpper().Contains(keyword));
            }

            return await PagedList<DetailRewardViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<DetailRewardViewModel> GetByIdAsync(Guid id)
        {
            DetailReward detailReward = await _dataContext.DetailRewards
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            DetailRewardViewModel result = _mapper.Map<DetailRewardViewModel>(detailReward);
            return result;
        }

        public List<StudentFlowYearViewModel> GetRewardReport(string FromYear, string ToYear)
        {
            int YearBegin = Convert.ToInt16(FromYear);
            int YearEnd = Convert.ToInt16(ToYear);
            List<StudentFlowYearViewModel> data = new List<StudentFlowYearViewModel>();
            IQueryable<DetailRewardViewModel> query = from s in _dataContext.DetailRewards
                                                      select new DetailRewardViewModel
                                                      {
                                                          Id = s.Id,
                                                          RewardId = s.RewardId,
                                                          StudentId = s.StudentId,
                                                          Reason = s.Reason,
                                                          DateReward = s.DateReward,
                                                          Gift = s.Gift,
                                                          CreatedDate = s.CreatedDate,
                                                          ModifiedDate = s.ModifiedDate,
                                                          Status = s.Status
                                                      };
            for (int i = YearBegin; i <= YearEnd; i++)
            {
                StudentFlowYearViewModel studentFlowYearVM = new StudentFlowYearViewModel();
                studentFlowYearVM.Year = i.ToString();
                studentFlowYearVM.NumberStudent = query.Where(s => Convert.ToDateTime(s.DateReward).Year.Equals(i)).Count();
                data.Add(studentFlowYearVM);
            }
            return data;
        }

        public async Task UpdateAsync(DetailRewardViewModel detailRewardViewModel)
        {
            DetailReward detailReward = await _dataContext.DetailRewards.FirstOrDefaultAsync(x => x.Id == detailRewardViewModel.Id);
            _dataContext.Entry(detailReward).CurrentValues.SetValues(detailRewardViewModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}
