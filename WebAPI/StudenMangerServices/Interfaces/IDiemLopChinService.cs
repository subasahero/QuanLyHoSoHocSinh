using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Interfaces
{
    public interface IDiemLopChinService
    {
        Task<DiemLopChinViewModel> GetById(Guid id);
        Task<bool> Insert(DiemLopChinViewModel model);
        Task<bool> Update(DiemLopChinViewModel model);
    }
}
