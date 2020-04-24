using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Interfaces
{
    public interface IDiemLopTamService
    {
        Task<DiemLopTamViewModel> GetById(Guid id);
        Task<bool> Insert(DiemLopTamViewModel model);
        Task<bool> Update(DiemLopTamViewModel model);
    }
}
