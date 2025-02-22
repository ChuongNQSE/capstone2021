﻿using Capstone2021.DTO;
using Capstone2021.Repositories.StudentRepository;
using System;
using System.Collections.Generic;

namespace Capstone2021.Services
{
    interface StudentService : StudentRepository, IDisposable
    {
        /// <summary>
        /// Login trả về DTO Student,nếu chưa có sẽ tạo mới rồi trả về
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        DTO.Student login(DTO.Student obj);


        /// <summary>
        /// Update lại url dẫn đến image của student
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        string updateImage(String imageUrl, int id);


        /// <summary>
        /// Trả về lastAppliedJobString của student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        string getLastAppliedJobString(int studentId);

        /// <summary>
        /// Trả về thông tin của những student apply vào job này
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        IList<ReturnAppliedStudentDTO> getAppliedStudentsOfThisJob(int jobId);

        /// <summary>
        /// Lưu lại job,1 : ok,2 : đă lưu,3 : job ko tìm thấy,4 : exception
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        int saveJob(int jobId, int studentId);

        /// <summary>
        /// Bỏ lưu job,1 : ok,2 : ko tồn tại relationship,3 : exception
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        int removeSavedJob(int jobId, int studentId);

        /// <summary>
        /// Trả về những job student đã save
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        IList<ReturnSavedJobDTO> getSavedJobs(int studentId);

        /// <summary>
        /// Trả về thông tin của student gửi request
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        ReturnStudentDTO getSelfInfo(int studentId);
    }
}
