﻿using System.Collections.Generic;

namespace Capstone2021.DTO
{
    public class ComboReportDTO
    {
        public List<ReportByMonthDTO> listReportByMonth { get; set; }

        public List<ReportyByQuarterDTO> listReportByQuarter { get; set; }

        public List<ReportByYearDTO> listreportByYear { get; set; }

        public List<ReportDetailByMonthDTO> listreportdetailByMonth { get; set; }

        public List<ReportCategoryByMonthDTO> listreportcategoryByMonth { get; set; }


    }
}