using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Enum;
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
    public class GradeService : IGradeService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GradeService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<bool> CheckExistsAsync(Guid id)
        {
            GradeViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<bool> CheckExistsRecordAsync(string name, LevelEnum levelEnum)
        {
            Grade grade = await _dataContext.Grades.FirstOrDefaultAsync(x => 
            x.Name.ToString().ToUpper().Trim() == name.ToUpper().Trim());
            return grade != null ? true : false;
        }

        public async Task<GradeViewModel> CreateAsync(GradeViewModel gradeViewModel)
        {
            Grade grade = _mapper.Map<Grade>(gradeViewModel);
            await _dataContext.Grades.AddAsync(grade);
            await _dataContext.SaveChangesAsync();
            GradeViewModel result = _mapper.Map<GradeViewModel>(grade);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            Grade grade = await _dataContext.Grades
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Grades.Remove(grade);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<GradeViewModel>> GetAllAsync()
        {
            IList<GradeViewModel> result = await _dataContext.Grades
                                                            .Where(x => x.Status == true)
                                                            .OrderBy(x => x.Name)
                                                            .ProjectTo<GradeViewModel>(_mapper.ConfigurationProvider)
                                                            .ToListAsync();
            return result;
        }

        public async Task<PagedList<GradeViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<GradeViewModel> query = from g in _dataContext.Grades
                                               orderby g.Name
                                               select new GradeViewModel
                                               {
                                                   Id = g.Id,
                                                   Name = g.Name,
                                                   levelEnum = g.levelEnum,
                                                   CreatedDate = g.CreatedDate,
                                                   ModifiedDate = g.ModifiedDate,
                                                   Status = g.Status
                                               };
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                    x.Name.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.Name.ToUpper().Contains(keyword));
            }
                return await PagedList<GradeViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<GradeViewModel> GetByIdAsync(Guid id)
        {
            Grade grade = await _dataContext.Grades
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            GradeViewModel result = _mapper.Map<GradeViewModel>(grade);
            return result;
        }

        public async Task<List<GradeViewModel>> GetGradeByLevelAsync(int level)
        {
            IQueryable<GradeViewModel> query = from g in _dataContext.Grades
                                               where (int)g.levelEnum == level
                                               select new GradeViewModel
                                               {
                                                   Id = g.Id,
                                                   levelEnum = g.levelEnum,
                                                   Name = g.Name
                                               };
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(GradeViewModel gradeViewModel)
        {
            Grade grade = await _dataContext.Grades.FirstOrDefaultAsync(x => x.Id == gradeViewModel.Id);
            _dataContext.Entry(grade).CurrentValues.SetValues(gradeViewModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}
