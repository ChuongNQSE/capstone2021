//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Capstone2021
{
    using System;
    using System.Collections.Generic;
    
    public partial class student_save_job
    {
        public int id { get; set; }
        public int job_id { get; set; }
        public int student_id { get; set; }
        public System.DateTime createDate { get; set; }
    
        public virtual job job { get; set; }
        public virtual student student { get; set; }
    }
}
