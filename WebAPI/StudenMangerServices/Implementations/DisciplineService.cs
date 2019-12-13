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
    public class DisciplineService : IDisciplineService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DisciplineService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            DisciplineViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<bool> CheckExistsRecordAsync(string number, string description)
        {
            Discipline discipline = await _dataContext.Disciplines.FirstOrDefaultAsync(x =>
                                            x.Number.ToString().ToUpper().Trim() == number.ToUpper().Trim() &&
                                            x.Description.ToString().ToUpper().Trim() == description.ToUpper().Trim());
            return discipline != null ? true : false;
        }

        public async Task<DisciplineViewModel> CreateAsync(DisciplineViewModel disciplineViewModel)
        {
            Discipline discipline = _mapper.Map<Discipline>(disciplineViewModel);
            await _dataContext.Disciplines.AddAsync(discipline);
            await _dataContext.SaveChangesAsync();
            DisciplineViewModel result = _mapper.Map<DisciplineViewModel>(discipline);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            Discipline discipline = await _dataContext.Disciplines
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Disciplines.Remove(discipline);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<DisciplineViewModel>> GetAllAsync()
        {
            IList<DisciplineViewModel> result = await _dataContext.Disciplines
                                                            .Where(x => x.Status == true)
                                                            .OrderByDescending(x => x.CreatedDate)
                                                            .ProjectTo<DisciplineViewModel>(_mapper.ConfigurationProvider)
                                                            .ToListAsync();
            return result;
        }

        public async Task<PagedList<DisciplineViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<DisciplineViewModel> query = from r in _dataContext.Disciplines
                                                orderby r.Number
                                                select new DisciplineViewModel
                                                {
                                                    Id = r.Id,
                                                    Number = r.Number,
                                                    Description = r.Description,
                                                    CreatedDate = r.CreatedDate,
                                                    ModifiedDate = r.ModifiedDate,
                                                    Status = r.Status
                                                };
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(
                    x => x.Number.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Number.ToUpper().Contains(keyword) ||
                    x.Description.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Description.ToUpper().Contains(keyword));
            }
            return await PagedList<DisciplineViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<DisciplineViewModel> GetByIdAsync(Guid id)
        {
            Discipline discipline = await _dataContext.Disciplines
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            DisciplineViewModel result = _mapper.Map<DisciplineViewModel>(discipline);
            return result;
        }

        public async Task<DisciplineViewModel> UpdateAsync(DisciplineViewModel disciplineViewModel)
        {
            Discipline discipline = await _dataContext.Disciplines.FirstAsync(x => x.Id == disciplineViewModel.Id);
            _dataContext.Entry(discipline).CurrentValues.SetValues(disciplineViewModel);
            await _dataContext.SaveChangesAsync();
            DisciplineViewModel result = _mapper.Map<DisciplineViewModel>(discipline);
            return result;
        }
    }
}
