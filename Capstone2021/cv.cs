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
    
    public partial class cv
    {
        public int student_id { get; set; }
        public string name { get; set; }
        public Nullable<bool> sex { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string avatar { get; set; }
        public string school { get; set; }
        public string experience { get; set; }
        public string foreign_language { get; set; }
        public Nullable<int> desired_salary_minimum { get; set; }
        public Nullable<int> working_form { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<bool> is_subscribed { get; set; }
        public Nullable<bool> is_public { get; set; }
    
        public virtual student student { get; set; }
    }
}
