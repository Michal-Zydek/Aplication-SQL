using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacja_SQL.Models
{
    [MetadataType(typeof(BAZA_DANYCHMetaData))]
    public partial class BAZA_DANYCH
    {
    }


   public class BAZA_DANYCHMetaData
    {
        public int ID_BAZA { get; set; }

        [Required(ErrorMessage = "Typ bazy jest wymagany !")]
        public string TYP { get; set; }


        [Required(ErrorMessage = "DataSource jest wymagana !")]
        public string SCIEZKA { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane !")]
        public string HASLO { get; set; }

        [Required(ErrorMessage = "Login jest wymagane !")]
        public string LOGIN { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagane !")]
        public string NAZWA { get; set; }
        public Nullable<int> ID_UZYTKOWNIKA { get; set; }
    }
}