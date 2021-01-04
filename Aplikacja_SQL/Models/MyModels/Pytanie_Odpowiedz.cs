using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aplikacja_SQL.Models;

namespace Aplikacja_SQL.Models.MyModels
{
    public class Pytanie_Odpowiedz
    {
        public List <PYTANIE> pYTANIE { get; set; }

        public List <ODPOWIEDZ> oDPOWIEDZ { get; set; }
    }
}