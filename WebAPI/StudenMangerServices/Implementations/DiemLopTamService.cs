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

namespace StudenMangerServices.Implementations
{
    public class DiemLopTamService : IDiemLopTamService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DiemLopTamService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<DiemLopTamViewModel> GetById(Guid id)
        {
            try
            {
                DiemLopTam entity = await _dataContext.DiemLopTams
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                DiemLopTamViewModel result = _mapper.Map<DiemLopTamViewModel>(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Insert(DiemLopTamViewModel model)
        {
            try
            {
                DiemLopTam entity = _mapper.Map<DiemLopTam>(model);
                await _dataContext.DiemLopTams.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                DiemLopTamViewModel result = _mapper.Map<DiemLopTamViewModel>(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(DiemLopTamViewModel model)
        {
            try
            {
                DiemLopTam entity = await _dataContext.DiemLopTams.FirstAsync(x => x.Id == model.Id);
                _dataContext.Entry(entity).CurrentValues.SetValues(model);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
