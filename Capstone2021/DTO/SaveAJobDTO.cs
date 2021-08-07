﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone2021.DTO
{
    public class SaveAJobDTO
    {
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "JobId là bắt buộc")]
        public int jobId;
    }
}