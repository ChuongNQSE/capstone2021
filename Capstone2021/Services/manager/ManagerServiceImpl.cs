using Capstone2021.DTO;
using Capstone2021.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.WebPages;

namespace Capstone2021.Service
{
    /**
     * Class này hiện thực interface ManagerService
     */
    public class ManagerServiceImpl : ManagerService, IDisposable
    {
        private static Logger logger;
        private DbEntities context;

        public ManagerServiceImpl()
        {
            logger = LogManager.GetCurrentClassLogger();
            context = new DbEntities();//DbEntities là class đc Entity Framework tạo ra dùng để kết nối tới db,quản lý db đơn giản hơn
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public DashboardDataDTO getDashboardData()
        {
            DashboardDataDTO result = new DashboardDataDTO();
            using (context)
            {
                result.numberOfRecruiters = context.recruiters.Count();
                result.numberOfPostedJobs = context.jobs.Count();
                result.numberOfStudents = context.students.Count();
            }
            return result;
        }

        public bool create(Manager obj)
        {
            String username = obj.username;
            using (context)
            {
                Manager checkManager = context.managers.AsEnumerable().Where(s => s.username.Equals(obj.username)).Select(s => new Manager()
                {
                    id = s.id,
                    username = s.username
                }).FirstOrDefault<Manager>();
                if (checkManager != null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        manager saveObj = new manager();
                        saveObj.create_date = DateTime.Now;
                        saveObj.username = obj.username;
                        saveObj.password = obj.password;
                        saveObj.role = obj.role;
                        saveObj.full_name = obj.fullName;
                        saveObj.is_banned = false;
                        context.managers.Add(saveObj);
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return false;
                    }
                }
            }
            return true;
        }


        public int createAStaff(Manager obj)
        {
            String username = obj.username;
            using (context)
            {
                Manager checkManager = context.managers.AsEnumerable().Where(s => s.username.Equals(obj.username)).Select(s => new Manager()
                {
                    id = s.id,
                    username = s.username
                }).FirstOrDefault<Manager>();
                if (checkManager != null)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        manager saveObj = new manager();
                        saveObj.create_date = DateTime.Now;
                        saveObj.username = obj.username;
                        saveObj.password = obj.password;
                        saveObj.role = obj.role;
                        saveObj.full_name = obj.fullName;
                        saveObj.is_banned = false;
                        context.managers.Add(saveObj);
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 3;
                    }
                }
            }
        }

        public Manager get(int id)
        {
            Manager result = null;
            using (context)
            {
                //context.managers sẽ lấy ra DataSet của table manager ở phía dưới db
                result = context.managers.AsEnumerable().Where(s => s.id == id).Select(s => new Manager()
                {
                    id = s.id,
                    username = s.username,
                    password = "***",
                    role = s.role,
                    createDate = s.create_date != null ? s.create_date.Value.ToString("dd/MM/yyyy") : "null",
                    fullName = s.full_name
                }).FirstOrDefault<Manager>();
            }
            return result;
        }

        public IList<Manager> getAll()
        {
            IList<Manager> listResult = new List<Manager>();
            using (context)
            {
                listResult = context.managers.AsEnumerable().Where(s => s.role.Equals("ROLE_STAFF")).Select(s =>
                    new Manager()
                    {
                        id = s.id,
                        createDate = s.create_date != null ? s.create_date.Value.ToString("dd/MM/yyyy") : "null",
                        fullName = s.full_name,
                        username = s.username,
                        role = s.role,
                        password = "***",
                        isBanned = s.is_banned.Value
                    }
                ).ToList<Manager>();
            }
            return listResult;
        }

        public Manager login(string username, string password)
        {
            Manager result = null;
            using (context)
            {
                Manager checkManager = context.managers.AsEnumerable().Where(s => s.username.Equals(username)).Select(s => new Manager()
                {
                    id = s.id,
                    username = s.username,
                    password = s.password,
                    role = s.role,
                    isBanned = s.is_banned.HasValue ? s.is_banned.Value : false
                }).FirstOrDefault<Manager>();
                if (checkManager == null)
                {
                    return null;
                }
                if (Crypto.VerifyHashedPassword(checkManager.password, password))
                {
                    result = checkManager;
                }
            }
            return result;
        }

        public bool remove(int id)
        {
            throw new NotImplementedException();
        }

        public int updatePassword(string password, string username, string oldPassword)
        {
            using (context)
            {
                var checkManager = context.managers.SingleOrDefault(b => b.username.Equals(username));
                if (checkManager == null)
                {
                    return 3;
                }
                if (checkManager.is_banned == true)
                {
                    return 4;
                }
                else
                {

                    if (!Crypto.VerifyHashedPassword(checkManager.password, oldPassword))
                    {
                        return 2;
                    }
                    try
                    {
                        checkManager.password = Crypto.HashPassword(password);
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 3;
                    }
                }
            }
        }

        public bool updateFullName(string fullName, string username)
        {
            bool result = false;
            using (context)
            {
                var checkManager = context.managers.SingleOrDefault(b => b.username.Equals(username));
                if (checkManager == null)
                {
                    return result;
                }
                else
                {
                    try
                    {
                        checkManager.full_name = fullName;
                        context.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                    }
                }

            }
            return result;
        }

        public bool banAStaff(int id)
        {
            using (context)
            {
                var checkManager = context.managers.Find(id);
                if (checkManager == null)
                    return false;
                if (checkManager.role.Equals("ROLE_ADMIN"))
                    return false;
                if (checkManager.is_banned == true)
                    return true;
                try
                {
                    checkManager.is_banned = true;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception e)
                {
                    logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                }
            }
            return false;
        }


        public bool softRemove(int id)
        {
            throw new NotImplementedException();
        }

        //check
        public ReportByMonthDTO reportByMonth(int month)
        {
            ReportByMonthDTO report = new ReportByMonthDTO();
            using (context)
            {
                int numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Month == month && s.create_date.Value.Year == DateTime.Now.Year).ToList().Count;
                var jobs = context.jobs.Where(s => s.create_date.Month == month && s.create_date.Year == DateTime.Now.Year);
                int numberOfJobs = jobs.Count();
                IList<int> listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                int numberOfDesiredStudents = 0;
                foreach (int quantity in listQuantity)
                {
                    numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                }
                int numberOfStudents = context.students.Where(s => s.create_date.Month == month && s.create_date.Year == DateTime.Now.Year).ToList().Count;
                report.numberOfDesiredStudents = numberOfDesiredStudents;
                report.numberOfJobs = numberOfJobs;
                report.numberOfRecruiters = numberOfRecruiters;
                report.numberOfStudents = numberOfStudents;
                report.month = month + "/" + DateTime.Now.Year.ToString();
            }
            return report;
        }
        //check detail month
        public List<ReportDetailByMonthDTO> reportDetailByMonth(int month)
        {
            List<ReportDetailByMonthDTO> report = new List<ReportDetailByMonthDTO>();
            using (context)
            {
                /*var reportRecord = (from j in context.jobs
                                    from r in context.recruiters 
                                    from s in context.student_apply_job
                                    where j.recruiter_id == r.id && j.id == s.job_id &&j.create_date.Month == month && j.create_date.Year == DateTime.Now.Year
                                    group j by new { j.name,r.last_name,j.salary_min,j.quantity,j.active_days,s.cv_id,r.phone } into g
                                    select new ReportDetailByMonthDTO()
                                    {
                                        nameOfJobs = g.Key.name,
                                        nameOfRecruiters = g.Key.last_name,
                                        salaryMin = g.Key.salary_min,
                                        numberOfDesiredStudents = g.Key.quantity,
                                        phone = g.Key.phone,
                                        activeDays = g.Key.active_days,
                                        numberOfStudents = g.Count(),
                                    }).ToList();
                foreach (var row in reportRecord)
                {
                    var tempReport = new ReportDetailByMonthDTO
                    {
                        nameOfJobs = row.nameOfJobs,
                        nameOfRecruiters = row.nameOfRecruiters,
                        activeDays = row.activeDays,
                        salaryMin = row.salaryMin,
                        numberOfDesiredStudents = row.numberOfDesiredStudents,
                        phone = row.phone,
                        numberOfStudents = row.numberOfStudents
                    };
                    report.Add(tempReport);
                }*/
                var reportRecord = (from j in context.jobs
                                    join r in context.recruiters on j.recruiter_id equals r.id
                                    where j.recruiter_id == r.id && j.create_date.Month == month && j.create_date.Year == DateTime.Now.Year 
                                     select new ReportDetailByMonthDTO()
                                    {
                                         idOfJob = j.id,
                                        nameOfJobs = j.name,
                                        nameOfRecruiters = r.last_name,
                                        salaryMin = j.salary_min,
                                        numberOfDesiredStudents = j.quantity,
                                        phone = r.phone,
                                        activeDays = j.active_days,
                                    }).ToList();
                 foreach (var row in reportRecord)
                 {
                    var tempReport = new ReportDetailByMonthDTO
                    {
                        nameOfJobs = row.nameOfJobs,
                        nameOfRecruiters = row.nameOfRecruiters,
                        activeDays = row.activeDays,
                        salaryMin = row.salaryMin,
                        numberOfDesiredStudents = row.numberOfDesiredStudents,
                        phone = row.phone,
                        numberOfStudents = context.student_apply_job.Where(c => c.job_id == row.idOfJob).Select(c => c.cv_id).Count(),
                     };
                     report.Add(tempReport);
                 }
            }
            return report;
        }
        //check category month
        public List<ReportCategoryByMonthDTO> reportCategoryByMonth(int month)
        {
            List<ReportCategoryByMonthDTO> report = new List<ReportCategoryByMonthDTO>();
            List<ReportCategoryByMonthDTO> reportTemp = new List<ReportCategoryByMonthDTO>();
            using (context)
            {
                var reportRecord = (from c in context.categories
                                    from jc in context.job_has_category
                                    from j in context.jobs
                                    where c.id == jc.category_id && j.id == jc.job_id && j.create_date.Month == month && j.create_date.Year == DateTime.Now.Year
                                    group j by new { c.id,c.value, jc.category_id, j.quantity } into g
                                    select new ReportCategoryByMonthDTO
                                    {
                                        idOfCategory = g.Key.id,
                                        nameOfCategory = g.Key.value,
                                        numberOfJobs = g.Count(),
                                        numberOfDesiredStudents = g.Sum(x => x.quantity),
                                    }).ToList();
                var SumReportRecord = reportRecord.GroupBy(n => n.idOfCategory).Select(g=>new ReportCategoryByMonthDTO
                {
                    idOfCategory = g.Key,
                    nameOfCategory = g.Select(n => n.nameOfCategory).FirstOrDefault(),
                    numberOfJobs = g.Sum(n => n.numberOfJobs),
                    numberOfDesiredStudents = g.Sum(n => n.numberOfDesiredStudents),
                }).ToList();
                
                foreach (var row in SumReportRecord)
                {
                        var tempReport = new ReportCategoryByMonthDTO
                        {
                            idOfCategory = row.idOfCategory,
                            nameOfCategory = row.nameOfCategory,
                            numberOfDesiredStudents = row.numberOfDesiredStudents,
                            numberOfJobs = row.numberOfJobs,
                            numberOfStudent = (from jc in context.job_has_category
                                               from s in context.student_apply_job
                                               from j in context.jobs
                                               where jc.job_id == s.job_id && j.id == jc.job_id && row.idOfCategory == jc.category_id && j.create_date.Month == month && j.create_date.Year == DateTime.Now.Year
                                               select s.cv_id).Count()
                        };
                        report.Add(tempReport);
                }
            }
            return report;
        }
        //check
        public ReportByYearDTO reportByYear()
        {
            ReportByYearDTO report = new ReportByYearDTO();
            using (context)
            {
                int numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Year == DateTime.Now.Year).ToList().Count;
                var jobs = context.jobs.Where(s => s.create_date.Year == DateTime.Now.Year);
                int numberOfJobs = jobs.Count();
                IList<int> listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                int numberOfDesiredStudents = 0;
                foreach (int quantity in listQuantity)
                {
                    numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                }
                int numberOfStudents = context.students.Where(s => s.create_date.Year == DateTime.Now.Year).ToList().Count;
                report.numberOfDesiredStudents = numberOfDesiredStudents;
                report.numberOfJobs = numberOfJobs;
                report.numberOfRecruiters = numberOfRecruiters;
                report.year = DateTime.Now.Year;
            }
            return report;
        }

        public ReportyByQuarterDTO reportByQuarter(int quarter)
        {
            int numberOfRecruiters = 0;
            int numberOfJobs = 0;
            int numberOfDesiredStudents = 0;
            IList<int> listQuantity = null;
            ReportyByQuarterDTO report = new ReportyByQuarterDTO();
            using (context)
            {
                switch (quarter)
                {
                    case 1:
                        numberOfRecruiters = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 1 && s.create_date.Value.Month <= 3 && s.create_date.Value.Year == DateTime.Now.Year)
                            .Count();
                        var jobs = context.jobs.Where(s => s.create_date.Month >= 1 && s.create_date.Month <= 3 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }
                        break;
                    case 2:
                        numberOfRecruiters = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 4 && s.create_date.Value.Month <= 6 && s.create_date.Value.Year == DateTime.Now.Year)
                            .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 4 && s.create_date.Month <= 6 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }
                        break;
                    case 3:
                        numberOfRecruiters = context.recruiters
                           .AsEnumerable()
                           .Where(s => s.create_date.Value.Month >= 7 && s.create_date.Value.Month <= 9 && s.create_date.Value.Year == DateTime.Now.Year)
                           .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 7 && s.create_date.Month <= 9 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }
                        break;
                    case 4:
                        numberOfRecruiters = context.recruiters
                           .AsEnumerable()
                           .Where(s => s.create_date.Value.Month >= 10 && s.create_date.Value.Month <= 12 && s.create_date.Value.Year == DateTime.Now.Year)
                           .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 10 && s.create_date.Month <= 12 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }
                        break;
                }
            }
            report.numberOfDesiredStudents = numberOfDesiredStudents;
            report.numberOfJobs = numberOfJobs;
            report.numberOfRecruiters = numberOfRecruiters;
            report.year = DateTime.Now.Year;
            report.quarter = quarter;
            return report;
        }

        public ComboReportDTO generateReport(RequestForReportDTO dto)
        {
            ReportByMonthDTO byMonth1 = new ReportByMonthDTO();
            ReportByMonthDTO byMonth2 = new ReportByMonthDTO();
            List<ReportDetailByMonthDTO> byDetailMonth1 = new List<ReportDetailByMonthDTO>();
            List<ReportCategoryByMonthDTO> byCategoryMonth = new List<ReportCategoryByMonthDTO>();
            List<ReportCategoryByMonthDTO> byCategoryMonthTemp = new List<ReportCategoryByMonthDTO>();
            ReportyByQuarterDTO byQuarterThisYear = new ReportyByQuarterDTO();
            ReportyByQuarterDTO byQuarterPreviousYear = new ReportyByQuarterDTO();
            ReportByYearDTO byThisYear = new ReportByYearDTO();
            ReportByYearDTO byPreviousYear = new ReportByYearDTO();
            ComboReportDTO result = new ComboReportDTO();
            using (context)
            {
                int numberOfRecruiters = 0;
                int numberOfJobs = 0;
                int numberOfDesiredStudents = 0;

                int numberOfRecruiters2 = 0;
                int numberOfJobs2 = 0;
                int numberOfDesiredStudents2 = 0;
                IList<int> listQuantity = null;
                IList<int> listQuantity2 = null;
                //quarter
                switch (dto.quarter)
                {
                    case 1:
                        numberOfRecruiters = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 1 && s.create_date.Value.Month <= 3 && s.create_date.Value.Year == DateTime.Now.Year)
                            .Count();
                        var jobs = context.jobs.Where(s => s.create_date.Month >= 1 && s.create_date.Month <= 3 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }

                        numberOfRecruiters2 = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 1 && s.create_date.Value.Month <= 3 && s.create_date.Value.Year == DateTime.Now.Year - 1)
                            .Count();
                        var jobs5 = context.jobs.Where(s => s.create_date.Month >= 1 && s.create_date.Month <= 3 && s.create_date.Year == DateTime.Now.Year - 1);
                        numberOfJobs2 = jobs5.Count();
                        listQuantity2 = jobs5.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity2)
                        {
                            numberOfDesiredStudents2 = numberOfDesiredStudents2 + quantity;
                        }
                        break;
                    case 2:
                        numberOfRecruiters = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 4 && s.create_date.Value.Month <= 6 && s.create_date.Value.Year == DateTime.Now.Year)
                            .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 4 && s.create_date.Month <= 6 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }

                        numberOfRecruiters2 = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 4 && s.create_date.Value.Month <= 6 && s.create_date.Value.Year == DateTime.Now.Year - 1)
                            .Count();
                        var jobs6 = context.jobs.Where(s => s.create_date.Month >= 4 && s.create_date.Month <= 6 && s.create_date.Year == DateTime.Now.Year - 1);
                        numberOfJobs2 = jobs6.Count();
                        listQuantity2 = jobs6.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity2)
                        {
                            numberOfDesiredStudents2 = numberOfDesiredStudents2 + quantity;
                        }
                        break;
                    case 3:
                        numberOfRecruiters = context.recruiters
                           .AsEnumerable()
                           .Where(s => s.create_date.Value.Month >= 7 && s.create_date.Value.Month <= 9 && s.create_date.Value.Year == DateTime.Now.Year)
                           .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 7 && s.create_date.Month <= 9 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }

                        numberOfRecruiters2 = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 7 && s.create_date.Value.Month <= 9 && s.create_date.Value.Year == DateTime.Now.Year - 1)
                            .Count();
                        var jobs7 = context.jobs.Where(s => s.create_date.Month >= 7 && s.create_date.Month <= 9 && s.create_date.Year == DateTime.Now.Year - 1);
                        numberOfJobs2 = jobs7.Count();
                        listQuantity2 = jobs7.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity2)
                        {
                            numberOfDesiredStudents2 = numberOfDesiredStudents2 + quantity;
                        }
                        break;
                    case 4:
                        numberOfRecruiters = context.recruiters
                           .AsEnumerable()
                           .Where(s => s.create_date.Value.Month >= 10 && s.create_date.Value.Month <= 12 && s.create_date.Value.Year == DateTime.Now.Year)
                           .Count();
                        jobs = context.jobs.Where(s => s.create_date.Month >= 10 && s.create_date.Month <= 12 && s.create_date.Year == DateTime.Now.Year);
                        numberOfJobs = jobs.Count();
                        listQuantity = jobs.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity)
                        {
                            numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                        }

                        numberOfRecruiters2 = context.recruiters
                            .AsEnumerable()
                            .Where(s => s.create_date.Value.Month >= 10 && s.create_date.Value.Month <= 12 && s.create_date.Value.Year == DateTime.Now.Year - 1)
                            .Count();
                        var jobs8 = context.jobs.Where(s => s.create_date.Month >= 10 && s.create_date.Month <= 12 && s.create_date.Year == DateTime.Now.Year - 1);
                        numberOfJobs2 = jobs8.Count();
                        listQuantity2 = jobs8.Select(s => s.quantity).ToList<int>();
                        foreach (int quantity in listQuantity2)
                        {
                            numberOfDesiredStudents2 = numberOfDesiredStudents2 + quantity;
                        }
                        break;
                }
                byQuarterThisYear.numberOfDesiredStudents = numberOfDesiredStudents;
                byQuarterThisYear.numberOfJobs = numberOfJobs;
                byQuarterThisYear.numberOfRecruiters = numberOfRecruiters;
                byQuarterThisYear.year = DateTime.Now.Year;
                byQuarterThisYear.quarter = dto.quarter;

                byQuarterPreviousYear.numberOfDesiredStudents = numberOfDesiredStudents2;
                byQuarterPreviousYear.numberOfJobs = numberOfJobs2;
                byQuarterPreviousYear.numberOfRecruiters = numberOfRecruiters2;
                byQuarterPreviousYear.year = DateTime.Now.Year - 1;
                byQuarterPreviousYear.quarter = dto.quarter;

                //year
                numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Year == DateTime.Now.Year).ToList().Count;
                var jobs2 = context.jobs.Where(s => s.create_date.Year == DateTime.Now.Year);
                numberOfJobs = jobs2.Count();
                listQuantity = jobs2.Select(s => s.quantity).ToList<int>();
                numberOfDesiredStudents = 0;
                foreach (int quantity in listQuantity)
                {
                    numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                }
                int numberOfStudents = context.students.Where(s => s.create_date.Year == DateTime.Now.Year).ToList().Count;
                byThisYear.numberOfDesiredStudents = numberOfDesiredStudents;
                byThisYear.numberOfJobs = numberOfJobs;
                byThisYear.numberOfRecruiters = numberOfRecruiters;
                byThisYear.year = DateTime.Now.Year;

                numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Year == DateTime.Now.Year - 1).ToList().Count;
                jobs2 = context.jobs.Where(s => s.create_date.Year == DateTime.Now.Year - 1);
                numberOfJobs = jobs2.Count();
                listQuantity = jobs2.Select(s => s.quantity).ToList<int>();
                numberOfDesiredStudents = 0;
                foreach (int quantity in listQuantity)
                {
                    numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                }
                numberOfStudents = context.students.Where(s => s.create_date.Year == DateTime.Now.Year - 1).ToList().Count;
                byPreviousYear.numberOfDesiredStudents = numberOfDesiredStudents;
                byPreviousYear.numberOfJobs = numberOfJobs;
                byPreviousYear.numberOfRecruiters = numberOfRecruiters;
                byPreviousYear.year = DateTime.Now.Year - 1;

                //month
                numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Month == dto.month && s.create_date.Value.Year == DateTime.Now.Year).ToList().Count;
                var jobs3 = context.jobs.Where(s => s.create_date.Month == dto.month && s.create_date.Year == DateTime.Now.Year);
                numberOfJobs = jobs3.Count();
                listQuantity = jobs3.Select(s => s.quantity).ToList<int>();
                numberOfDesiredStudents = 0;
                foreach (int quantity in listQuantity)
                {
                    numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                }
                numberOfStudents = context.students.Where(s => s.create_date.Month == dto.month && s.create_date.Year == DateTime.Now.Year).ToList().Count;
                byMonth1.numberOfDesiredStudents = numberOfDesiredStudents;
                byMonth1.numberOfJobs = numberOfJobs;
                byMonth1.numberOfRecruiters = numberOfRecruiters;
                byMonth1.numberOfStudents = numberOfStudents;
                byMonth1.month = dto.month.ToString();

                if (dto.month == 1)
                {
                    numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Month == 12 && s.create_date.Value.Year == DateTime.Now.Year - 1).ToList().Count;
                    var jobs4 = context.jobs.Where(s => s.create_date.Month == 12 && s.create_date.Year == DateTime.Now.Year - 1);
                    numberOfJobs = jobs4.Count();
                    listQuantity = jobs4.Select(s => s.quantity).ToList<int>();
                    numberOfDesiredStudents = 0;
                    foreach (int quantity in listQuantity)
                    {
                        numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                    }
                    numberOfStudents = context.students.Where(s => s.create_date.Month == 12 && s.create_date.Year == DateTime.Now.Year - 1).ToList().Count;
                    byMonth2.numberOfDesiredStudents = numberOfDesiredStudents;
                    byMonth2.numberOfJobs = numberOfJobs;
                    byMonth2.numberOfRecruiters = numberOfRecruiters;
                    byMonth2.numberOfStudents = numberOfStudents;
                    byMonth2.month = 12.ToString() + "/" + (DateTime.Now.Year - 1).ToString();
                }
                else
                {
                    numberOfRecruiters = context.recruiters.Where(s => s.create_date.Value.Month == dto.month - 1 && s.create_date.Value.Year == DateTime.Now.Year).ToList().Count;
                    var jobs4 = context.jobs.Where(s => s.create_date.Month == dto.month - 1 && s.create_date.Year == DateTime.Now.Year);
                    numberOfJobs = jobs4.Count();
                    listQuantity = jobs4.Select(s => s.quantity).ToList<int>();
                    numberOfDesiredStudents = 0;
                    foreach (int quantity in listQuantity)
                    {
                        numberOfDesiredStudents = numberOfDesiredStudents + quantity;
                    }
                    numberOfStudents = context.students.Where(s => s.create_date.Month == dto.month - 1 && s.create_date.Year == DateTime.Now.Year).ToList().Count;
                    byMonth2.numberOfDesiredStudents = numberOfDesiredStudents;
                    byMonth2.numberOfJobs = numberOfJobs;
                    byMonth2.numberOfRecruiters = numberOfRecruiters;
                    byMonth2.numberOfStudents = numberOfStudents;
                    byMonth2.month = (dto.month - 1).ToString();
                }
                //month detail
                var reportRecord = (from j in context.jobs
                                    join r in context.recruiters on j.recruiter_id equals r.id
                                    where j.recruiter_id == r.id && j.create_date.Month == dto.month && j.create_date.Year == DateTime.Now.Year
                                    select new ReportDetailByMonthDTO()
                                    {
                                        idOfJob = j.id,
                                        nameOfJobs = j.name,
                                        nameOfRecruiters = r.last_name,
                                        salaryMin = j.salary_min,
                                        numberOfDesiredStudents = j.quantity,
                                        phone = r.phone,
                                        activeDays = j.active_days,
                                    }).ToList();
                foreach (var row in reportRecord)
                {
                    var tempReport = new ReportDetailByMonthDTO
                    {
                        nameOfJobs = row.nameOfJobs,
                        nameOfRecruiters = row.nameOfRecruiters,
                        activeDays = row.activeDays,
                        salaryMin = row.salaryMin,
                        numberOfDesiredStudents = row.numberOfDesiredStudents,
                        phone = row.phone,
                        numberOfStudents = context.student_apply_job.Where(c => c.job_id == row.idOfJob).Select(c => c.cv_id).Count(),
                    };
                    byDetailMonth1.Add(tempReport);
                }
                //month category
                var reportRecord1 = (from c in context.categories
                                     from jc in context.job_has_category
                                     from j in context.jobs
                                     where c.id == jc.category_id && j.id == jc.job_id && j.create_date.Month == dto.month && j.create_date.Year == DateTime.Now.Year
                                     group j by new { c.id, c.value, jc.category_id, j.quantity } into g
                                     select new ReportCategoryByMonthDTO
                                     {
                                         idOfCategory = g.Key.id,
                                         nameOfCategory = g.Key.value,
                                         numberOfJobs = g.Count(),
                                         numberOfDesiredStudents = g.Sum(x => x.quantity),
                                     }).ToList();
                var SumReportRecord = reportRecord1.GroupBy(n => n.idOfCategory).Select(g => new ReportCategoryByMonthDTO
                {
                    idOfCategory = g.Key,
                    nameOfCategory = g.Select(n => n.nameOfCategory).FirstOrDefault(),
                    numberOfJobs = g.Sum(n => n.numberOfJobs),
                    numberOfDesiredStudents = g.Sum(n => n.numberOfDesiredStudents),
                }).ToList();

                foreach (var row in SumReportRecord)
                {
                    var tempReport = new ReportCategoryByMonthDTO
                    {
                        idOfCategory = row.idOfCategory,
                        nameOfCategory = row.nameOfCategory,
                        numberOfDesiredStudents = row.numberOfDesiredStudents,
                        numberOfJobs = row.numberOfJobs,
                        numberOfStudent = (from jc in context.job_has_category
                                           from s in context.student_apply_job
                                           from j in context.jobs
                                           where jc.job_id == s.job_id && j.id == jc.job_id && row.idOfCategory == jc.category_id && j.create_date.Month == dto.month && j.create_date.Year == DateTime.Now.Year
                                           select s.cv_id).Count()
                    };
                    byCategoryMonth.Add(tempReport);
                }
                //done
            }
            result.listReportByMonth = new List<ReportByMonthDTO>();
            result.listReportByMonth.Add(byMonth1);
            result.listReportByMonth.Add(byMonth2);

            result.listreportdetailByMonth = new List<ReportDetailByMonthDTO>();
            result.listreportdetailByMonth = byDetailMonth1;

            result.listreportcategoryByMonth = new List<ReportCategoryByMonthDTO>();
            result.listreportcategoryByMonth = byCategoryMonth;

            result.listReportByQuarter = new List<ReportyByQuarterDTO>();
            result.listReportByQuarter.Add(byQuarterThisYear);
            result.listReportByQuarter.Add(byQuarterPreviousYear);

            result.listreportByYear = new List<ReportByYearDTO>();
            result.listreportByYear.Add(byThisYear);
            result.listreportByYear.Add(byPreviousYear);
            return result;
        }

        public int updateBanner(UpdateBannerDTO dto, int staffId)
        {
            using (context)
            {
                var staff = context.managers.Find(staffId);
                if (staff == null || staff.is_banned == true)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        var banner = context.banners.Find(dto.id);
                        if (banner == null)
                        {
                            return 4;
                        }
                        else
                        {
                            if (dto.url != null && !dto.url.IsEmpty())
                            {
                                banner.url = dto.url;
                            }
                            if (dto.imgUrl != null && !dto.imgUrl.IsEmpty())
                            {
                                banner.image_url = dto.imgUrl;
                            }
                            context.SaveChanges();
                            return 1;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 3;
                    }
                }
            }
        }

        public string updateBannerImgUrl(int id, string imgUrl, int staffId)
        {
            string result = "";
            using (context)
            {
                var staff = context.managers.Find(staffId);
                if (staff == null || staff.is_banned == true)
                {
                    result = "error";
                    return result;
                }
                try
                {
                    var banner = context.banners.Find(id);
                    result = "https://capstone2021-fpt.s3.ap-southeast-1.amazonaws.com/" + imgUrl;
                    banner.image_url = result;
                    context.SaveChanges();
                    return result;
                }
                catch (Exception e)
                {
                    logger.Info("Exception " + e.Message + "in StudentServiceImpl");
                    return "error";
                }
            }
        }

        public IList<Banner> getAllBanners()
        {
            IList<Banner> result = new List<Banner>();
            using (context)
            {
                result = context.banners.Select(s => new Banner() { id = s.id, url = s.url, imgUrl = s.image_url }).ToList<Banner>();
            }
            return result;
        }

        public bool unbanAStaff(int id)
        {
            using (context)
            {
                var checkManager = context.managers.Find(id);
                if (checkManager == null)
                    return false;
                if (checkManager.role.Equals("ROLE_ADMIN"))
                    return false;
                if (checkManager.is_banned == false)
                    return true;
                try
                {
                    checkManager.is_banned = false;
                    context.SaveChanges();
                    return true;

                }
                catch (Exception e)
                {
                    logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                    return false;
                }
            }
        }

        public IList<Student> getListStudentsWithPaging(int page)
        {
            IList<Student> list = new List<Student>();
            using (context)
            {
                list = context.students.AsEnumerable().Select(s => StudentMapper.mapToDto(s))
                    .OrderByDescending(s => s.gmail)
                    .Skip(5 * (page - 1))
                    .Take(5)
                    .ToList<Student>();
            }
            return list;
        }

        public IList<ReturnRecruiterForAdminDTO> getListRecruiterWithPaging(int page)
        {
            IList<ReturnRecruiterForAdminDTO> list = new List<ReturnRecruiterForAdminDTO>();
            using (context)
            {
                list = context.recruiters.AsEnumerable().Select(s => RecruiterMapper.mapFromModel(s))
                    .OrderByDescending(s => s.gmail)
                    .Skip(5 * (page - 1))
                    .Take(5)
                    .ToList<ReturnRecruiterForAdminDTO>();
            }
            return list;
        }

        public int getTotalPages(string choice)
        {
            int result = 0;
            using (context)
            {
                switch (choice)
                {
                    case "student":
                        int count = context.students.Count();
                        result = (int)Math.Ceiling((double)count / 5);
                        break;
                    case "recruiter":
                        count = context.recruiters.Count();
                        result = (int)Math.Ceiling((double)count / 5);
                        break;
                }

            }
            return result;
        }

        public IList<Category> getAllCategory()
        {
            IList<Category> result = new List<Category>();
            using (context)
            {
                result = context.categories.AsEnumerable().Select(s => new Category() { id = s.id, value = s.value }).ToList<Category>();
            }
            return result;
        }

        public IList<ActiveDaysAndPrice> getAllActiveDaysAndPrice()
        {
            IList<ActiveDaysAndPrice> result = new List<ActiveDaysAndPrice>();
            using (context)
            {
                result = context.active_days_price.AsEnumerable().Select(s => new ActiveDaysAndPrice() { id = s.id, activeDays = s.active_days, price = s.price }).ToList<ActiveDaysAndPrice>();
            }
            return result;
        }

        public bool createACategory(string value)
        {
            using (context)
            {
                try
                {
                    category model = new category();
                    model.value = value;
                    context.categories.Add(model);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                    return false;
                }
            }
        }

        public int updateACategory(int id, string value)
        {
            using (context)
            {
                var category = context.categories.Find(id);
                if (category == null)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        category.value = value;
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 3;
                    }
                }

            }
        }


        public int createAnActiveDaysAndPrice(int days, decimal price)
        {
            using (context)
            {
                var checkDuplicate = context.active_days_price.Where(s => s.active_days == days || s.price == price).FirstOrDefault();
                if (checkDuplicate != null)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        active_days_price model = new active_days_price();
                        model.active_days = days;
                        model.price = price;
                        context.active_days_price.Add(model);
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 3;
                    }
                }
            }
        }


        public int updateAnActiveDaysAndPrice(UpdateActiveDaysAndPriceDTO dto)
        {
            using (context)
            {
                var activeDaysAndPrice = context.active_days_price.Find(dto.id);
                if (activeDaysAndPrice == null)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        if (dto.activeDays.HasValue)
                        {
                            activeDaysAndPrice.active_days = dto.activeDays.Value;
                        }
                        if (dto.price.HasValue)
                        {

                            activeDaysAndPrice.price = dto.price.Value;
                        }
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException.InnerException.Message.Contains("Cannot insert duplicate key"))
                        {
                            logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                            return 4;
                        }
                        else
                        {
                            logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                            return 3;
                        }
                    }
                }
            }
        }


        public int deleteAnActiceDaysAndPrice(int id)
        {
            using (context)
            {
                if (context.active_days_price.Count() < 3)
                {
                    return 3;
                }
                var activeDaysAndPrice = context.active_days_price.Find(id);
                if (activeDaysAndPrice == null)
                {
                    return 2;
                }
                else
                {
                    try
                    {
                        context.active_days_price.Remove(activeDaysAndPrice);
                        context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        logger.Info("Exception " + e.Message + "in ManagerServiceImpl");
                        return 4;
                    }
                }
            }
        }

        
    }
}