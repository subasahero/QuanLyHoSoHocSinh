using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Dtos;
using Utility.Heplers;

namespace StudenMangerServices.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public StudentService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> ChangeGradeAsync(ChangeGradeViewModel changeGradeViewModel)
        {
            foreach (Guid id in changeGradeViewModel.studentsId)
            {
                Grade grade = await _dataContext.Grades.FirstOrDefaultAsync(x => x.Id == changeGradeViewModel.gradeId.Value);
                Student student = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
                student.GradeId = changeGradeViewModel.gradeId.Value;
                student.ModifiedDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                var rs = await _dataContext.SaveChangesAsync() > 0;
                if (!rs) return false;
            }
            return true;
        }

        public async Task<bool> CheckExistsAsync(Guid id)
        {
            StudentViewModel result = await GetByIdAsync(id);
            return result != null ? true : false;
        }

        public async Task<bool> CheckExistsRecordAsync(string studentCode)
        {
            Student student = await _dataContext.Students.FirstOrDefaultAsync(x =>
                                    x.Code.ToString().ToUpper().Trim() == studentCode.ToString().ToUpper().Trim());
            return student != null ? true : false;
        }

        public async Task<StudentViewModel> CreateAsync(StudentViewModel studentViewModel)
        {
            Student student = _mapper.Map<Student>(studentViewModel);
            await _dataContext.Students.AddAsync(student);
            await _dataContext.SaveChangesAsync();
            StudentViewModel result = _mapper.Map<StudentViewModel>(student);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            Student student = await _dataContext.Students
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Students.Remove(student);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IList<StudentViewModel>> GetAllAsync()
        {
            IList<StudentViewModel> result = await (from s in _dataContext.Students
                                                    join g in _dataContext.Grades on s.GradeId equals g.Id
                                                    orderby s.Name
                                                    select new StudentViewModel
                                                    {
                                                        Id = s.Id,
                                                        GradeId = s.GradeId,
                                                        Code = s.Code,
                                                        Name = s.Name,
                                                        Sex = s.Sex,
                                                        Birthday = s.Birthday,
                                                        BirthLocate = s.BirthLocate,
                                                        Talent = s.Talent,
                                                        DateGoShcool = s.DateGoShcool,
                                                        CreatedDate = s.CreatedDate,
                                                        ModifiedDate = s.ModifiedDate,
                                                        Status = s.Status,
                                                        GradeVM = _mapper.Map<GradeViewModel>(g)
                                                    }).ToListAsync();
            return result;
        }

        public async Task<PagedList<StudentViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            IQueryable<StudentViewModel> query = from s in _dataContext.Students
                                                 join g in _dataContext.Grades on s.GradeId equals g.Id
                                                 select new StudentViewModel
                                                 {
                                                     Id = s.Id,
                                                     GradeId = s.GradeId,
                                                     Code = s.Code,
                                                     Name = s.Name,
                                                     Sex = s.Sex,
                                                     Birthday = s.Birthday,
                                                     BirthLocate = s.BirthLocate,
                                                     Talent = s.Talent,
                                                     DateGoShcool = s.DateGoShcool,
                                                     Certificate = s.Certificate,
                                                     GradeVM = _mapper.Map<GradeViewModel>(g),
                                                     CreatedDate = s.CreatedDate,
                                                     ModifiedDate = s.ModifiedDate,
                                                     Status = s.Status
                                                 };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                string keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(
                    x => x.Name.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Name.ToUpper().Contains(keyword) ||
                    x.Code.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.Code.ToUpper().Contains(keyword) ||
                    x.GradeVM.Name.ToUpper().ToUnSign().ToTrim().Contains(keyword.ToUnSign()) ||
                    x.GradeVM.Name.ToUpper().Contains(keyword));
            }

            return await PagedList<StudentViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<StudentViewModel> GetByIdAsync(Guid id)
        {
            Student student = await _dataContext.Students
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            StudentViewModel result = _mapper.Map<StudentViewModel>(student);
            return result;
        }

        public async Task<List<StudentViewModel>> GetStudentByLevelAsync(LevelEnum levelEnum)
        {
            List<StudentViewModel> result = await (from s in _dataContext.Students
                                                    join g in _dataContext.Grades on s.GradeId equals g.Id
                                                    orderby s.Name
                                                    select new StudentViewModel
                                                    {
                                                        Id = s.Id,
                                                        GradeId = s.GradeId,
                                                        Code = s.Code,
                                                        Name = s.Name,
                                                        Sex = s.Sex,
                                                        Birthday = s.Birthday,
                                                        BirthLocate = s.BirthLocate,
                                                        Talent = s.Talent,
                                                        DateGoShcool = s.DateGoShcool,
                                                        CreatedDate = s.CreatedDate,
                                                        ModifiedDate = s.ModifiedDate,
                                                        Status = s.Status,
                                                        GradeVM = _mapper.Map<GradeViewModel>(g)
                                                    }).ToListAsync();
            result = result.Where(x => x.GradeVM.levelEnum == levelEnum).ToList();
            return result;
        }

        public async Task<PagedList<StudentViewModel>> GetStudentByLevelPagingAsync(PagingParams pagingParams, LevelEnum levelEnum)
        {
            IQueryable<StudentViewModel> query = from s in _dataContext.Students
                                                 join g in _dataContext.Grades on s.GradeId equals g.Id
                                                 select new StudentViewModel
                                                 {
                                                     Id = s.Id,
                                                     GradeId = s.GradeId,
                                                     Code = s.Code,
                                                     Name = s.Name,
                                                     Sex = s.Sex,
                                                     Birthday = s.Birthday,
                                                     BirthLocate = s.BirthLocate,
                                                     Talent = s.Talent,
                                                     DateGoShcool = s.DateGoShcool,
                                                     Certificate = s.Certificate,
                                                     GradeVM = _mapper.Map<GradeViewModel>(g),
                                                     CreatedDate = s.CreatedDate,
                                                     ModifiedDate = s.ModifiedDate,
                                                     Status = s.Status
                                                 };

            query = query.Where(x => x.GradeVM.levelEnum == levelEnum);

            return await PagedList<StudentViewModel>
                .CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public List<StudentFlowYearViewModel> GetStudentReportEnrollment(string FromYear, string ToYear)
        {
            int YearBegin = Convert.ToInt16(FromYear);
            int YearEnd = Convert.ToInt16(ToYear);
            List<StudentFlowYearViewModel> data = new List<StudentFlowYearViewModel>();
            IQueryable<StudentViewModel> query = from s in _dataContext.Students
                                                 select new StudentViewModel
                                                 {
                                                     Id = s.Id,
                                                     GradeId = s.GradeId,
                                                     Code = s.Code,
                                                     Name = s.Name,
                                                     Sex = s.Sex,
                                                     Birthday = s.Birthday,
                                                     BirthLocate = s.BirthLocate,
                                                     Talent = s.Talent,
                                                     DateGoShcool = s.DateGoShcool,
                                                     CreatedDate = s.CreatedDate,
                                                     ModifiedDate = s.ModifiedDate,
                                                     Status = s.Status
                                                 };
            for (int i = YearBegin; i <= YearEnd; i++)
            {
                StudentFlowYearViewModel studentFlowYearVM = new StudentFlowYearViewModel();
                studentFlowYearVM.Year = i.ToString();
                studentFlowYearVM.NumberStudent = query.Where(s => Convert.ToDateTime(s.DateGoShcool).Year.Equals(i)).Count();
                data.Add(studentFlowYearVM);
            }
            return data;
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel studentViewModel)
        {
            Student student = await _dataContext.Students.FirstOrDefaultAsync(x => x.Id == studentViewModel.Id);
            _dataContext.Entry(student).CurrentValues.SetValues(studentViewModel);
            await _dataContext.SaveChangesAsync();
            StudentViewModel result = _mapper.Map<StudentViewModel>(student);
            return result;
        }
    }
}
