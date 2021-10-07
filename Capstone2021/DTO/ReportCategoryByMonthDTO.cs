using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone2021.DTO
{
    public class ReportCategoryByMonthDTO
    {
        public int idOfCategory { get; set; }
        /// <summary>
        /// Tên ngành nghề trong 1 tháng
        /// </summary>
        public string nameOfCategory { get; set; }

        /// <summary>
        /// Số lượng sinh viên nhà tuyển dụng muốn tuyển trong 1 tháng
        /// </summary>
        public int numberOfDesiredStudents { get; set; }

        /// <summary>
        /// Số lượng công việc đăng ký trong 1 tháng
        /// </summary>
        public int numberOfJobs { get; set; }

        public string month { get; set; }
        /// <summary>
        /// Số lượng sinh viên đăng ký trong 1 tháng
        /// </summary>
        public int numberOfStudent { get; set; }

    }
}