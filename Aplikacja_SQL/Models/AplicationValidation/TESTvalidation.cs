using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacja_SQL.Models
{
    [MetadataType(typeof(TESTMetaData))]
    public partial class TEST
    {
    }


    public class TESTMetaData
    {

        public int ID_TEST { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana !")]
        public string NAZWA { get; set; }



        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD.MM.YYYY hh:mm:ss}"),]
        public string CZAS_START { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD.MM.YYYY hh:mm:ss}")]
        public string CZAS_STOP { get; set; }
        public string KLUCZ { get; set; }


        [Required(ErrorMessage = "Baza jest wymagana !")]
        public Nullable<int> ID_BAZA { get; set; }
        public Nullable<int> ID_UZYTKOWNIKA { get; set; }
    }

}