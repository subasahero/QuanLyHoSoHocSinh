using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Interfaces
{
    public interface IDiemLopSauService
    {
        Task<DiemLopSauViewModel> GetById(Guid id);
        Task<bool> Insert(DiemLopSauViewModel model);
        Task<bool> Update(DiemLopSauViewModel model);
    }
}
