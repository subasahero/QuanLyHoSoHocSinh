using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;
using Utility.Heplers;

namespace StudenMangerServices.Implementations
{
    public class DetailDisciplineService : IDetailDisciplineService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DetailDisciplineService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            DetailDisciplineViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<DetailDisciplineViewModel> CreateAsync(DetailDisciplineViewModel detailDisciplineViewModel)
        {
            DetailDiscipline detailDiscipline = _mapper.Map<DetailDiscipline>(detailDisciplineViewModel);
            await _dataContext.DetailDisciplines.AddAsync(detailDiscipline);
            await _dataContext.SaveChangesAsync();
            DetailDisciplineViewModel result = _mapper.Map<DetailDisciplineViewModel>(detailDiscipline);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            DetailDiscipline detailDiscipline = await _dataContext.DetailDisciplines
                                                                .AsNoTracking()
                                                                .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.DetailDisciplines.Remove(detailDiscipline);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<DetailDisciplineViewModel>> GetAllAsync()
        {
            IList<DetailDisciplineViewModel> result = await (from d in _dataContext.DetailDisciplines
                                                        join s in _dataContext.Students on d.StudentId equals s.Id
                                                        join r in _dataContext.Disciplines on d.DisciplineId equals r.Id
                                                        orderby d.CreatedDate
                                                        select new DetailDisciplineViewModel
                                                        {
                                                            Id = d.Id,
                                                            StudentId = d.StudentId,
                                                            DisciplineId = d.DisciplineId,
                                                            Reason = d.Reason,
                                                            Punishment = d.Punishment,
                                                            StudentVM = _mapper.Map<StudentViewModel>(s),
                                                            DisciplineVM = _mapper.Map<DisciplineViewModel>(r),
                                                            CreatedDate = d.CreatedDate,
                                                            ModifiedDate = d.ModifiedDate,
                                                            Status = d.Status
                                                        }).ToListAsync();
            return result;
        }

        public async Task<PagedList<DetailDisciplineViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<DetailDisciplineViewModel> query = from dd in _dataContext.DetailDisciplines 
                                                          join s in _dataContext.Students on dd.StudentId equals s.Id
                                                          join d in _dataContext.Disciplines on dd.DisciplineId equals d.Id
                                                          orderby dd.CreatedDate descending
                                                          select new DetailDisciplineViewModel
                                                          {
                                                              Id = dd.Id,
                                                              StudentId = dd.StudentId,
                                                              DisciplineId = dd.DisciplineId,
                                                              Reason = dd.Reason,
                                                              Punishment = dd.Punishment,
                                                              DatePunish = dd.DatePunish,
                                                              StudentVM = _mapper.Map<StudentViewModel>(s),
                                                              DisciplineVM = _mapper.Map<DisciplineViewModel>(d),
                                                              CreatedDate = dd.CreatedDate,
                                                              ModifiedDate = dd.ModifiedDate,
                                                              Status = dd.Status
                                                          };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x =>
                                    x.StudentVM.Name.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.StudentVM.Name.ToUpper().Contains(keyword) ||
                                    x.DisciplineVM.Description.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.DisciplineVM.Description.ToUpper().Contains(keyword) ||
                                    x.StudentVM.Code.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.StudentVM.Code.ToUpper().Contains(keyword) ||
                                    x.DisciplineVM.Number.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                    x.DisciplineVM.Number.ToUpper().Contains(keyword));
            }

            return await PagedList<DetailDisciplineViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<DetailDisciplineViewModel> GetByIdAsync(Guid id)
        {
            DetailDiscipline detailDiscipline = await _dataContext.DetailDisciplines
                                                                .AsNoTracking()
                                                                .FirstOrDefaultAsync(x => x.Id == id);
            DetailDisciplineViewModel result = _mapper.Map<DetailDisciplineViewModel>(detailDiscipline);
            return result;
        }

        public async Task UpdateAsync(DetailDisciplineViewModel detailDisciplineViewModel)
        {
            DetailDiscipline detailDiscipline = await _dataContext.DetailDisciplines
                                                                  .FirstOrDefaultAsync(x => x.Id == detailDisciplineViewModel.Id);
            _dataContext.Entry(detailDiscipline).CurrentValues.SetValues(detailDisciplineViewModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}
