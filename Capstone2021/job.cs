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
    
    public partial class job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public job()
        {
            this.student_apply_job = new HashSet<student_apply_job>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int working_form { get; set; }
        public int location { get; set; }
        public string working_place { get; set; }
        public string description { get; set; }
        public string requirement { get; set; }
        public bool type { get; set; }
        public string offer { get; set; }
        public Nullable<int> sex { get; set; }
        public int quantity { get; set; }
        public int salary_min { get; set; }
        public int salary_max { get; set; }
        public int recruiter_id { get; set; }
        public System.DateTime create_date { get; set; }
        public Nullable<int> manager_id { get; set; }
        public int status { get; set; }
    
        public virtual manager manager { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_apply_job> student_apply_job { get; set; }
        public virtual recruiter recruiter { get; set; }
    }
}
