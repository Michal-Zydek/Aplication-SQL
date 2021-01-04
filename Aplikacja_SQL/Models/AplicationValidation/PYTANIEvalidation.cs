using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplikacja_SQL.Models
{
    [MetadataType(typeof(PYTANIEMetaData))]
    public partial class PYTANIE
    {
    }


    public class PYTANIEMetaData
    {
        public int ID_PYTANIE { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Pytanie jest wymagane !")]
        public string PYTANIE1 { get; set; }
        public int ID_TEST { get; set; }

    }

}