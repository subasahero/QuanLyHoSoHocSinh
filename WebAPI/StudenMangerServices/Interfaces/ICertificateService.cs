using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;

namespace StudenMangerServices.Interfaces
{
    public interface ICertificateService
    {
        Task<CertificateViewModel> GetByIdAsync(Guid id);
        Task<CertificateViewModel> CreateAsync(CertificateViewModel certificateViewModel);
        Task UpdateAsync(CertificateViewModel certificateViewModel);
        Task DeleteAsync(Guid id);
        Task<bool> CheckExistsAsync(Guid id);
    }
}
