using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_SQL.Models
{
    [MetadataType(typeof(UZYTKOWNIKMetaData))]
    public partial class UZYTKOWNIK
    {
    }

    public class UZYTKOWNIKMetaData
    { public object TYP_KONTA { get; set; }
        public object ID_UZYTKOWNIKA { get; set; }

        [Required(ErrorMessage = "Imie jest wymagane !")]
        public object IMIE { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane !")]
        public object NAZWISKO { get; set; }



        [Required(ErrorMessage = "Numer Indeksu jest wymagany !")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Podaj 5 cyfr")]
        public object NR_INDEKSU { get; set; }


        [Required(ErrorMessage = "E-mail jest wymagany !")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Wprowadź prawidłowy E-mail")]
        public object EMAIL { get; set; }


        [Required(ErrorMessage = "Login jest wymagany !")]
        [MinLength(5, ErrorMessage = "Login musi zawierać co najmniej 5 znaków")]
        
        public object LOGIN { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagany !")]
        [MinLength(5, ErrorMessage = "Hasło musi zawierać co najmniej 5 znaków")]
        [DataType(DataType.Password)]
        public object HASLO { get; set; }
    }
}