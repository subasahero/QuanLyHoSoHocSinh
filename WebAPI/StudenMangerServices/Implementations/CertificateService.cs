using AutoMapper;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Implementations
{
    public class CertificateService : ICertificateService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CertificateService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public Task<bool> CheckExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CertificateViewModel> CreateAsync(CertificateViewModel certificateViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CertificateViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CertificateViewModel certificateViewModel)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> CheckExistsAsync(Guid id)
        //{
        //    CertificateViewModel result = await GetByIdAsync(id);
        //    return result != null ? true : false;
        //}

        //public async Task<CertificateViewModel> CreateAsync(CertificateViewModel certificateViewModel)
        //{
        //    Certificate certificate = _mapper.Map<Certificate>(certificateViewModel);
        //    await _dataContext.Certificates.AddAsync(certificate);
        //    await _dataContext.SaveChangesAsync();
        //    CertificateViewModel result = _mapper.Map<CertificateViewModel>(certificate);
        //    return result;
        //}

        //public async Task DeleteAsync(Guid id)
        //{
        //    Certificate certificate = await _dataContext.Certificates
        //                                    .AsNoTracking()
        //                                    .FirstOrDefaultAsync(x => x.Id == id);
        //    _dataContext.Certificates.Remove(certificate);
        //    await _dataContext.SaveChangesAsync();
        //}

        //public async Task<CertificateViewModel> GetByIdAsync(Guid id)
        //{
        //    Certificate certificate = await _dataContext.Certificates
        //                                    .AsNoTracking()
        //                                    .FirstOrDefaultAsync(x => x.Id == id);
        //    CertificateViewModel result = _mapper.Map<CertificateViewModel>(certificate);
        //    return result;
        //}

        //public async Task UpdateAsync(CertificateViewModel certificateViewModel)
        //{
        //    Certificate certificate = await _dataContext.Certificates.FirstOrDefaultAsync(x => x.Id == certificateViewModel.Id);
        //    _dataContext.Entry(certificate).CurrentValues.SetValues(certificateViewModel);
        //    await _dataContext.SaveChangesAsync();
        //}
    }
}
