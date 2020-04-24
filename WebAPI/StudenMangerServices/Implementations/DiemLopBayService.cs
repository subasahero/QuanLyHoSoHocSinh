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
    public class DiemLopBayService : IDiemLopBayService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DiemLopBayService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<DiemLopBayViewModel> GetById(Guid id)
        {
            try
            {
                DiemLopBay diemLopBay = await _dataContext.DiemLopBays
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                DiemLopBayViewModel result = _mapper.Map<DiemLopBayViewModel>(diemLopBay);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Insert(DiemLopBayViewModel model)
        {
            try
            {
                DiemLopBay diemLopBay = _mapper.Map<DiemLopBay>(model);
                await _dataContext.DiemLopBays.AddAsync(diemLopBay);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(DiemLopBayViewModel model)
        {
            try
            {
                DiemLopBay diemLopBay = await _dataContext.DiemLopBays.FirstAsync(x => x.Id == model.Id);
                _dataContext.Entry(diemLopBay).CurrentValues.SetValues(model);
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
