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
    public class StudentScoreService : IStudentScoreService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public StudentScoreService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<bool> CheckExistsAsync(Guid id)
        {
            StudentScoreViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<StudentScoreViewModel> CreateAsync(StudentScoreViewModel studentScoreViewModel)
        {
            StudentScore studentScore = _mapper.Map<StudentScore>(studentScoreViewModel);
            await _dataContext.StudentScores.AddAsync(studentScore);
            await _dataContext.SaveChangesAsync();
            StudentScoreViewModel result = _mapper.Map<StudentScoreViewModel>(studentScore);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            StudentScore studentScore = await _dataContext.StudentScores
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.StudentScores.Remove(studentScore);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<StudentScoreViewModel> GetByIdAsync(Guid id)
        {
            StudentScore studentScore = await _dataContext.StudentScores
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            StudentScoreViewModel result = _mapper.Map<StudentScoreViewModel>(studentScore);
            return result;
        }

        public async Task UpdateAsync(StudentScoreViewModel studentScoreViewModel)
        {
            StudentScore studentScore = await _dataContext.StudentScores.FirstOrDefaultAsync(x => x.Id == studentScoreViewModel.Id);
            _dataContext.Entry(studentScore).CurrentValues.SetValues(studentScoreViewModel);
            await _dataContext.SaveChangesAsync();
        }
    }
}
