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
    public class DiemLopSauService : IDiemLopSauService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DiemLopSauService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<DiemLopSauViewModel> GetById(Guid id)
        {
            try
            {
                DiemLopSau diemLopSau = await _dataContext.DiemLopSaus
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                DiemLopSauViewModel result = _mapper.Map<DiemLopSauViewModel>(diemLopSau);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Insert(DiemLopSauViewModel model)
        {
            try
            {
                DiemLopSau diemLopSau = _mapper.Map<DiemLopSau>(model);
                await _dataContext.DiemLopSaus.AddAsync(diemLopSau);
                await _dataContext.SaveChangesAsync();
                DiemLopSauViewModel result = _mapper.Map<DiemLopSauViewModel>(diemLopSau);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(DiemLopSauViewModel model)
        {
            try
            {
                DiemLopSau diemLopSau = await _dataContext.DiemLopSaus.FirstAsync(x => x.Id == model.Id);
                _dataContext.Entry(diemLopSau).CurrentValues.SetValues(model);
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
