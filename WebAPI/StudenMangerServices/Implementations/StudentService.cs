using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StudenMangerServices.Interfaces;
using StudenMangerServices.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentService(DataContext dataContext, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
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

        public async Task<bool> DinhChiHocAsync(DinhChiHocViewModel model)
        {
            try
            {
                Student student = await _dataContext.Students
                                                .FirstOrDefaultAsync(x => x.Id == model.Id.Value);
                student.GradeId = model.GradeId.Value;
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
                                                        address = s.address,
                                                        imageLink = s.imageLink,
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
                                                 //join c in _dataContext.Certificates on s.Id equals c.StudentId into tmpCertificates
                                                 //from c in tmpCertificates.DefaultIfEmpty()
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
                                                     address = s.address,
                                                     imageLink = s.imageLink,
                                                     GradeVM = _mapper.Map<GradeViewModel>(g),
                                                     //CertificateVM = _mapper.Map<CertificateViewModel>(c),
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

        public async Task<StudentViewModel> GetByIdAllInfoAsync(Guid id)
        {
            StudentViewModel query = (from m in _dataContext.Students
                                      where m.Id == id
                                      join sc in _dataContext.StudentScores on m.Id equals sc.StudentId into tmpStudentScores
                                      from sc in tmpStudentScores.DefaultIfEmpty()
                                      select new StudentViewModel
                                      {
                                          Id = m.Id,
                                          GradeId = m.GradeId,
                                          Code = m.Code,
                                          Name = m.Name,
                                          Sex = m.Sex,
                                          Birthday = m.Birthday,
                                          BirthLocate = m.BirthLocate,
                                          Talent = m.Talent,
                                          address = m.address,
                                          imageLink = m.imageLink,
                                          DateGoShcool = m.DateGoShcool,
                                          CreatedDate = m.CreatedDate,
                                          ModifiedDate = m.ModifiedDate,
                                          Status = m.Status,
                                          StudentScoreVM = _mapper.Map<StudentScoreViewModel>(sc),
                                          DetailRewardVM = (from r in _dataContext.DetailRewards
                                                            where r.StudentId == m.Id
                                                            join rm in _dataContext.Rewards on r.RewardId equals rm.Id
                                                            select new DetailRewardViewModel
                                                            {
                                                                Id = r.Id,
                                                                StudentId = r.StudentId,
                                                                RewardId = r.RewardId,
                                                                Reason = r.Reason,
                                                                Gift = r.Gift,
                                                                DateReward = r.DateReward,
                                                                CreatedDate = r.CreatedDate,
                                                                ModifiedDate = r.ModifiedDate,
                                                                Status = r.Status,
                                                                Reward = _mapper.Map<RewardViewModel>(rm),
                                                            }).ToList(),
                                          DetailDisciplineVM = (from d in _dataContext.DetailDisciplines
                                                                where d.StudentId == m.Id
                                                                join dm in _dataContext.Disciplines on d.DisciplineId equals dm.Id
                                                                select new DetailDisciplineViewModel
                                                                {
                                                                    Id = d.Id,
                                                                    StudentId = d.StudentId,
                                                                    DisciplineId = d.DisciplineId,
                                                                    Reason = d.Reason,
                                                                    Punishment = d.Punishment,
                                                                    DatePunish = d.DatePunish,
                                                                    CreatedDate = d.CreatedDate,
                                                                    ModifiedDate = d.ModifiedDate,
                                                                    Status = d.Status,
                                                                    Discipline = _mapper.Map<DisciplineViewModel>(dm)
                                                                }).ToList(),
                                          DiemLopSauHK1VM = (from ls in _dataContext.DiemLopSaus
                                                             where ls.StudentId == m.Id && ls.loai == 0
                                                             select new DiemLopSauViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopBayHK1VM = (from ls in _dataContext.DiemLopBays
                                                             where ls.StudentId == m.Id && ls.loai == 0
                                                             select new DiemLopBayViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopTamHK1VM = (from ls in _dataContext.DiemLopTams
                                                             where ls.StudentId == m.Id && ls.loai == 0
                                                             select new DiemLopTamViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopChinHK1VM = (from ls in _dataContext.DiemLopChins
                                                              where ls.StudentId == m.Id && ls.loai == 0
                                                              select new DiemLopChinViewModel
                                                              {
                                                                  Id = ls.Id,
                                                                  StudentId = ls.StudentId,
                                                                  Toan = ls.Toan,
                                                                  NguVan = ls.NguVan,
                                                                  NgoaiNgu = ls.NgoaiNgu,
                                                                  Tin = ls.Tin,
                                                                  LichSu = ls.LichSu,
                                                                  DiaLy = ls.DiaLy,
                                                                  GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                  CongNghe = ls.CongNghe,
                                                                  VatLy = ls.VatLy,
                                                                  SinhHoc = ls.SinhHoc,
                                                                  AmNhac = ls.AmNhac,
                                                                  MyThuat = ls.MyThuat,
                                                                  DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                  loai = ls.loai,
                                                              }).AsNoTracking().FirstOrDefault(),
                                          DiemLopSauHK2VM = (from ls in _dataContext.DiemLopSaus
                                                             where ls.StudentId == m.Id && ls.loai == 1
                                                             select new DiemLopSauViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopBayHK2VM = (from ls in _dataContext.DiemLopBays
                                                             where ls.StudentId == m.Id && ls.loai == 1
                                                             select new DiemLopBayViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopTamHK2VM = (from ls in _dataContext.DiemLopTams
                                                             where ls.StudentId == m.Id && ls.loai == 1
                                                             select new DiemLopTamViewModel
                                                             {
                                                                 Id = ls.Id,
                                                                 StudentId = ls.StudentId,
                                                                 Toan = ls.Toan,
                                                                 NguVan = ls.NguVan,
                                                                 NgoaiNgu = ls.NgoaiNgu,
                                                                 Tin = ls.Tin,
                                                                 LichSu = ls.LichSu,
                                                                 DiaLy = ls.DiaLy,
                                                                 GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                 CongNghe = ls.CongNghe,
                                                                 VatLy = ls.VatLy,
                                                                 SinhHoc = ls.SinhHoc,
                                                                 AmNhac = ls.AmNhac,
                                                                 MyThuat = ls.MyThuat,
                                                                 DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                 loai = ls.loai,
                                                             }).AsNoTracking().FirstOrDefault(),
                                          DiemLopChinHK2VM = (from ls in _dataContext.DiemLopChins
                                                              where ls.StudentId == m.Id && ls.loai == 1
                                                              select new DiemLopChinViewModel
                                                              {
                                                                  Id = ls.Id,
                                                                  StudentId = ls.StudentId,
                                                                  Toan = ls.Toan,
                                                                  NguVan = ls.NguVan,
                                                                  NgoaiNgu = ls.NgoaiNgu,
                                                                  Tin = ls.Tin,
                                                                  LichSu = ls.LichSu,
                                                                  DiaLy = ls.DiaLy,
                                                                  GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                  CongNghe = ls.CongNghe,
                                                                  VatLy = ls.VatLy,
                                                                  SinhHoc = ls.SinhHoc,
                                                                  AmNhac = ls.AmNhac,
                                                                  MyThuat = ls.MyThuat,
                                                                  DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                  loai = ls.loai,
                                                              }).AsNoTracking().FirstOrDefault(),
                                      }).FirstOrDefault();

            return query;
        }

        public async Task<StudentViewModel> GetByIdAsync(Guid id)
        {
            Student student = await _dataContext.Students
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(x => x.Id == id);
            StudentViewModel result = _mapper.Map<StudentViewModel>(student);
            result.DetailRewardVM = await _dataContext.DetailRewards
                                                    .AsNoTracking()
                                                    .Where(x => x.StudentId == student.Id)
                                                    .Include(x => x.Reward)
                                                    .ProjectTo<DetailRewardViewModel>(_mapper.ConfigurationProvider)
                                                    .ToListAsync();
            result.DetailDisciplineVM = await _dataContext.DetailDisciplines
                                                    .AsNoTracking()
                                                    .Where(x => x.StudentId == student.Id)
                                                    .Include(x => x.Discipline)
                                                    .ProjectTo<DetailDisciplineViewModel>(_mapper.ConfigurationProvider)
                                                    .ToListAsync();
            return result;
        }

        public async Task<List<StudentViewModel>> GetStudentByLevelAsync(LevelEnum levelEnum)
        {
            List<StudentViewModel> result = await (from s in _dataContext.Students
                                                    join g in _dataContext.Grades on s.GradeId equals g.Id
                                                   //join c in _dataContext.Certificates on s.Id equals c.StudentId into tmpCertificates
                                                   //from c in tmpCertificates.DefaultIfEmpty()
                                                   //join r in _dataContext.DetailRewards on s.Id equals r.StudentId into tmpDetailRewards
                                                   //from r in tmpDetailRewards.DefaultIfEmpty()
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
                                                        address = s.address,
                                                        imageLink = s.imageLink,
                                                        DateGoShcool = s.DateGoShcool,
                                                        CreatedDate = s.CreatedDate,
                                                        ModifiedDate = s.ModifiedDate,
                                                        Status = s.Status,
                                                        GradeVM = _mapper.Map<GradeViewModel>(g),
                                                        DetailRewardVM = (from r in _dataContext.DetailRewards
                                                                          where r.StudentId == s.Id
                                                                          join rm in _dataContext.Rewards on r.RewardId equals rm.Id
                                                                          select new DetailRewardViewModel {
                                                                              Id = r.Id,
                                                                              StudentId = r.StudentId,
                                                                              RewardId = r.RewardId,
                                                                              Reason = r.Reason,
                                                                              Gift = r.Gift,
                                                                              DateReward = r.DateReward,
                                                                              CreatedDate = r.CreatedDate,
                                                                              ModifiedDate = r.ModifiedDate,
                                                                              Status = r.Status,
                                                                              Reward = _mapper.Map<RewardViewModel>(rm),
                                                                          }).ToList(),
                                                        DetailDisciplineVM = (from d in _dataContext.DetailDisciplines
                                                                              where d.StudentId == d.Id
                                                                              join dm in _dataContext.Disciplines on d.DisciplineId equals dm.Id
                                                                              select new DetailDisciplineViewModel {
                                                                                  Id = d.Id,
                                                                                  StudentId = d.StudentId,
                                                                                  DisciplineId = d.DisciplineId,
                                                                                  Reason = d.Reason,
                                                                                  Punishment = d.Punishment,
                                                                                  DatePunish = d.DatePunish,
                                                                                  CreatedDate = d.CreatedDate,
                                                                                  ModifiedDate = d.ModifiedDate,
                                                                                  Status = d.Status,
                                                                                  Discipline = _mapper.Map<DisciplineViewModel>(dm)
                                                                              }).ToList()
                                                        //CertificateVM = _mapper.Map<CertificateViewModel>(c)
                                                    }).ToListAsync();
            result = result.Where(x => x.GradeVM.levelEnum == levelEnum).ToList();

            return result;
        }

        public async Task<PagedList<StudentViewModel>> GetStudentByLevelPagingAsync(PagingParams pagingParams, LevelEnum levelEnum)
        {
            IQueryable<StudentViewModel> query = from s in _dataContext.Students
                                                 join g in _dataContext.Grades on s.GradeId equals g.Id
                                                 join c in _dataContext.StudentScores on s.Id equals c.StudentId into tmpStudentScores
                                                 from c in tmpStudentScores.DefaultIfEmpty()
                                                     //join cc in _dataContext.Certificates on s.Id equals cc.StudentId into tmpCertificates
                                                     //from cc in tmpCertificates.DefaultIfEmpty()
                                                 select new StudentViewModel
                                                 {
                                                     Id = s.Id,
                                                     GradeId = s.GradeId,
                                                     Code = s.Code,
                                                     Name = s.Name,
                                                     Sex = s.Sex,
                                                     Birthday = s.Birthday,
                                                     BirthLocate = s.BirthLocate,
                                                     Talent = s.Talent ?? string.Empty,
                                                     DateGoShcool = s.DateGoShcool,
                                                     address = s.address,
                                                     imageLink = s.imageLink,
                                                     GradeVM = _mapper.Map<GradeViewModel>(g),
                                                     StudentScoreVM = _mapper.Map<StudentScoreViewModel>(c),
                                                     //CertificateVM = _mapper.Map<CertificateViewModel>(cc),
                                                     DiemLopSauHK1VM = (from ls in _dataContext.DiemLopSaus
                                                                        where ls.StudentId == s.Id && ls.loai == 0
                                                                        select new DiemLopSauViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopBayHK1VM = (from ls in _dataContext.DiemLopBays
                                                                        where ls.StudentId == s.Id && ls.loai == 0
                                                                        select new DiemLopBayViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopTamHK1VM = (from ls in _dataContext.DiemLopTams
                                                                        where ls.StudentId == s.Id && ls.loai == 0
                                                                        select new DiemLopTamViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopChinHK1VM = (from ls in _dataContext.DiemLopChins
                                                                        where ls.StudentId == s.Id && ls.loai == 0
                                                                        select new DiemLopChinViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopSauHK2VM = (from ls in _dataContext.DiemLopSaus
                                                                        where ls.StudentId == s.Id && ls.loai == 1
                                                                        select new DiemLopSauViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopBayHK2VM = (from ls in _dataContext.DiemLopBays
                                                                        where ls.StudentId == s.Id && ls.loai == 1
                                                                        select new DiemLopBayViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopTamHK2VM = (from ls in _dataContext.DiemLopTams
                                                                        where ls.StudentId == s.Id && ls.loai == 1
                                                                        select new DiemLopTamViewModel
                                                                        {
                                                                            Id = ls.Id,
                                                                            StudentId = ls.StudentId,
                                                                            Toan = ls.Toan,
                                                                            NguVan = ls.NguVan,
                                                                            NgoaiNgu = ls.NgoaiNgu,
                                                                            Tin = ls.Tin,
                                                                            LichSu = ls.LichSu,
                                                                            DiaLy = ls.DiaLy,
                                                                            GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                            CongNghe = ls.CongNghe,
                                                                            VatLy = ls.VatLy,
                                                                            SinhHoc = ls.SinhHoc,
                                                                            AmNhac = ls.AmNhac,
                                                                            MyThuat = ls.MyThuat,
                                                                            DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                            loai = ls.loai,
                                                                        }).AsNoTracking().FirstOrDefault(),
                                                     DiemLopChinHK2VM = (from ls in _dataContext.DiemLopChins
                                                                         where ls.StudentId == s.Id && ls.loai == 1
                                                                         select new DiemLopChinViewModel
                                                                         {
                                                                             Id = ls.Id,
                                                                             StudentId = ls.StudentId,
                                                                             Toan = ls.Toan,
                                                                             NguVan = ls.NguVan,
                                                                             NgoaiNgu = ls.NgoaiNgu,
                                                                             Tin = ls.Tin,
                                                                             LichSu = ls.LichSu,
                                                                             DiaLy = ls.DiaLy,
                                                                             GiaoDucCongDan = ls.GiaoDucCongDan,
                                                                             CongNghe = ls.CongNghe,
                                                                             VatLy = ls.VatLy,
                                                                             SinhHoc = ls.SinhHoc,
                                                                             AmNhac = ls.AmNhac,
                                                                             MyThuat = ls.MyThuat,
                                                                             DiemTrungBinhCong = ls.DiemTrungBinhCong,
                                                                             loai = ls.loai,
                                                                         }).AsNoTracking().FirstOrDefault(),
                                                     CreatedDate = s.CreatedDate,
                                                     ModifiedDate = s.ModifiedDate,
                                                     Status = s.Status
                                                 };

            query = query.Where(x => x.GradeVM.levelEnum == levelEnum);
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

            if(!string.IsNullOrEmpty(pagingParams.gradeId))
            {
                query = query.Where(x => x.GradeId.ToString() == pagingParams.gradeId);
            }

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
                                                     address = s.address,
                                                     imageLink = s.imageLink,
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
            if (studentViewModel.File != null && studentViewModel.File.Length > 0)
            {
                string folder = $"Assets/uploaded/img/";
                string oldFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, folder + student.imageLink);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                string getExtension = Path.GetExtension(studentViewModel.File.FileName);
                string imgFileName = Guid.NewGuid() + getExtension;
                string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Assets/uploaded/img");
                string imglFilePath = Path.Combine(uploadFolder, imgFileName);
                FileInfo fileLocation = new FileInfo(imglFilePath);
                using (var fileStream = new FileStream(imglFilePath, FileMode.Create))
                {
                    await studentViewModel.File.CopyToAsync(fileStream);
                }
                studentViewModel.imageLink = imgFileName;

            }
            _dataContext.Entry(student).CurrentValues.SetValues(studentViewModel);
            await _dataContext.SaveChangesAsync();
            StudentViewModel result = _mapper.Map<StudentViewModel>(student);
            return result;
        }
    }
}
