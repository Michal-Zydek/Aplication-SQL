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
    
    public partial class BAZA_DANYCH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAZA_DANYCH()
        {
            this.TEST = new HashSet<TEST>();
        }
    
        public int ID_BAZA { get; set; }
        public string TYP { get; set; }
        public string SCIEZKA { get; set; }
        public string HASLO { get; set; }
        public string LOGIN { get; set; }
        public string NAZWA { get; set; }
        public Nullable<int> ID_UZYTKOWNIKA { get; set; }
    
        public virtual UZYTKOWNIK UZYTKOWNIK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TEST> TEST { get; set; }

       

    }
}
