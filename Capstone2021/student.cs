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
    
    public partial class student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public student()
        {
            this.student_apply_job = new HashSet<student_apply_job>();
        }
    
        public int id { get; set; }
        public string gmail { get; set; }
        public string phone { get; set; }
        public Nullable<bool> is_banned { get; set; }
        public System.DateTime create_date { get; set; }
        public bool profile_status { get; set; }
        public string avatar { get; set; }
        public string google_id { get; set; }
        public string last_applied_job_string { get; set; }
    
        public virtual cv cv { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_apply_job> student_apply_job { get; set; }
    }
}
