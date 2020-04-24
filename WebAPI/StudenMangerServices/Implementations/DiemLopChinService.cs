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
    public class DiemLopChinService : IDiemLopChinService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DiemLopChinService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<DiemLopChinViewModel> GetById(Guid id)
        {
            try
            {
                DiemLopChin entity = await _dataContext.DiemLopChins
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                DiemLopChinViewModel result = _mapper.Map<DiemLopChinViewModel>(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Insert(DiemLopChinViewModel model)
        {
            try
            {
                DiemLopChin entity = _mapper.Map<DiemLopChin>(model);
                await _dataContext.DiemLopChins.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                DiemLopChinViewModel result = _mapper.Map<DiemLopChinViewModel>(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(DiemLopChinViewModel model)
        {
            try
            {
                DiemLopChin entity = await _dataContext.DiemLopChins.FirstAsync(x => x.Id == model.Id);
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
