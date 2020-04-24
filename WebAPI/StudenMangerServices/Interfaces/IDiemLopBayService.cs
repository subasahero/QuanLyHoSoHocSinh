using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudenMangerServices.Interfaces
{
    public interface IDiemLopBayService
    {
        Task<DiemLopBayViewModel> GetById(Guid id);
        Task<bool> Insert(DiemLopBayViewModel model);
        Task<bool> Update(DiemLopBayViewModel model);
    }
}
