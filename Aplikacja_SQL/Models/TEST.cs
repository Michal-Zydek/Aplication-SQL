//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aplikacja_SQL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEST()
        {
            this.ODPOWIEDZ = new HashSet<ODPOWIEDZ>();
            this.PYTANIE = new HashSet<PYTANIE>();
        }
    
        public int ID_TEST { get; set; }
        public string NAZWA { get; set; }
        public string CZAS_START { get; set; }
        public string CZAS_STOP { get; set; }
        public string KLUCZ { get; set; }
        public Nullable<int> ID_BAZA { get; set; }
        public Nullable<int> ID_UZYTKOWNIKA { get; set; }
    
        public virtual BAZA_DANYCH BAZA_DANYCH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ODPOWIEDZ> ODPOWIEDZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PYTANIE> PYTANIE { get; set; }
        public virtual UZYTKOWNIK UZYTKOWNIK { get; set; }

        
    }
}