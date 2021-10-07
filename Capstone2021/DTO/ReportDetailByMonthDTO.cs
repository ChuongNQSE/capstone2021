using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone2021.DTO
{
    public class ReportDetailByMonthDTO
    {
        public int idOfJob { get; set; }
        /// <summary>
        /// Tên nhà tuyển dụng 
        /// </summary>
        public string nameOfRecruiters { get; set; }

        /// <summary>
        /// Tên công việc 
        /// </summary>
        public string nameOfJobs { get; set; }

        /// <summary>
        /// Số lượng sinh viên nhà tuyển dụng muốn tuyển trong 1 tháng
        /// </summary>
        public int numberOfDesiredStudents { get; set; }

        /// <summary>
        /// Số lượng sinh viên ứng tuyển công việc trong 1 tháng
        /// </summary>
        public int numberOfStudents { get; set; }
        /// <summary>
        /// Mức lương tối thiểu
        /// </summary>
        public int salaryMin { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// Số tiền và ngày 
        /// </summary>
        public int activeDays { get; set; }
    }
}