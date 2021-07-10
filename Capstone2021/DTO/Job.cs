﻿using System;

namespace Capstone2021.DTO
{
    public class Job
    {
        public int id { get; set; }
        public string name { get; set; }
        /// <summary>
        /// Part time hay full time hay cả 2,1 cho pt,2 cho ft,3 cho cả 2
        /// </summary>
        public int workingForm { get; set; }
        /// <summary>
        /// quận:từ 1 tới 12,13 bình tân,14 bình thạnh,15 gò vấp,16 phú nhuận,17 tân bình
        /// ,18 tân phú,19 thủ đức,20 bình chánh,21 cần giờ,22 củ chi,23 hóc môn,24 nhà bè
        /// </summary>
        public int location { get; set; }

        /// <summary>
        /// Chi tiết địa chỉ nơi làm việc
        /// </summary>
        public string workingPlace { get; set; }
        public string description { get; set; }
        /// <summary>
        /// Những requirements tuyển dụng đưa ra,1 đoạn text
        /// </summary>
        public string requirement { get; set; }

        /// <summary>
        /// true là việc làm chuyên ngành,false là việc làm phổ thông
        /// </summary>
        public bool type { get; set; }

        /// <summary>
        /// offer của nhà tuyển dụng,1 đoạn text
        /// </summary>
        public string offer { get; set; }

        /// <summary>
        /// Yêu cầu giới tính,1 là nam,2 là nữ,3 là cả 2 giới
        /// </summary>
        public Int32 sex { get; set; }

        /// <summary>
        /// Số lượng cần tuyển
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// Mức lương offer từ min tới max,VD:2 000 000 tới 5 000 000
        /// </summary>
        public int salaryMin { get; set; }
        public int salaryMax { get; set; }

        /// <summary>
        /// Id của nhà tuyển dụng đăng tin
        /// </summary>
        public int recruiterId { get; set; }
        public String createDate { get; set; }

        /// <summary>
        /// Id của staff đã duyệt job 
        /// </summary>
        public Int32 managerId { get; set; }

        /// <summary>
        /// 1 là pending(đợi duyệt),2 là ok(đã duyệt),3 là edited(đã đc update,lúc này job sẽ ko thể update nữa)
        /// </summary>
        public int status { get; set; }
    }
}