//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabmasterAdminapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prowadzacy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prowadzacy()
        {
            this.Przedmioty = new HashSet<Przedmioty>();
        }
    
        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string mail { get; set; }
        public string adname { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Przedmioty> Przedmioty { get; set; }
    }
}
