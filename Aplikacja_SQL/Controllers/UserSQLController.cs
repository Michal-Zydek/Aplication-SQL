using Aplikacja_SQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using System.Dynamic;

namespace Aplikacja_SQL.Controllers
{
    

    public class UserSQLController : Controller
    {
        private APLIKACJA_SQLEntities db = new APLIKACJA_SQLEntities();
        public int Row;
        public int Column;
        public string wynik="";
        static public int ID_USER;
        static public int ID_TEST;

        //Get List Pytania
        public List<PYTANIE> getPytania()
        {
            List<PYTANIE> pYTANIE = db.PYTANIE.Where(m => m.ID_TEST == ID_TEST).ToList();
            return pYTANIE;
        }

        //SQL USER view 
        public ActionResult UserSQL_U(int ID_User, int ID_Test)
        {
            ID_USER = ID_User;
            ID_TEST = ID_Test;

            List<PYTANIE> pytanie = db.PYTANIE.Where(m => m.ID_TEST == ID_Test).ToList();
            ViewBag.Pytania = pytanie;

            var test = db.TEST.Where(m => m.ID_TEST == ID_TEST).FirstOrDefault();
            ViewBag.test = test.NAZWA;


            List<ODPOWIEDZ> ListOdpowiedz = new List<ODPOWIEDZ>();

            for(int i=0;i <pytanie.Count;i++)
            {
                ListOdpowiedz.Add(new ODPOWIEDZ { ID_PYTANIE = 0, ODPOWIEDZ1 = "", ID_TEST = 0 });
            }

            return View(ListOdpowiedz);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserSQL_U(List<ODPOWIEDZ> listO)
        {


            List<PYTANIE> pytanie = db.PYTANIE.Where(m => m.ID_TEST == ID_TEST).ToList();
            int[] Id_Pytanie = new int[pytanie.Count];
            int j = 0;
            foreach(PYTANIE PYT in pytanie )
            {
                Id_Pytanie[j] = PYT.ID_PYTANIE;              
                j++; 
            }

            j = 0;
            foreach (var i in listO)
            {
                i.ID_UZYTKOWNIKA = ID_USER;
                i.ID_TEST = ID_TEST;
                if(i.ODPOWIEDZ1== null)
                {
                    i.ODPOWIEDZ1 = "Brak";
                }
                i.ID_PYTANIE = Id_Pytanie[j];
                j++;
                db.ODPOWIEDZ.Add(i);
                db.SaveChanges();

            }

            return RedirectToAction("UserMain", "User", new { ID_User = ID_USER, DETALIS = 6});
        }


        


        //Query SYS use Ajax
        public ActionResult Query_sys(string query)
        {
            var Row = "";
            string column = "";
            string row = "";
               
            TEST test = db.TEST.Where(m => m.ID_TEST == ID_TEST).FirstOrDefault();
            BAZA_DANYCH bAZA_DANYCH = db.BAZA_DANYCH.Where(m => m.ID_BAZA == test.ID_BAZA).FirstOrDefault(); 



            //PostreSQL Query
            if(bAZA_DANYCH.TYP== "POSTGRESQL          ")
            {
                 string connetionString = "Host=" + bAZA_DANYCH.SCIEZKA + ";Username=" + bAZA_DANYCH.LOGIN + ";Password=" + bAZA_DANYCH.HASLO + ";Database=" + bAZA_DANYCH.NAZWA + ";";

            try
            {
                NpgsqlConnection cnn = new NpgsqlConnection(connetionString);

                cnn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, cnn);
                NpgsqlDataReader dr = command.ExecuteReader();


                for (int i = 0; i < dr.FieldCount; i++)
                {
                    column += "<th style=\"background-color:#808080\"> " + dr.GetName(i) + "</th>";
                }
                var Column = "<tr>" + column + "</tr>";



                while (dr.Read())
                {

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        row += "<th>" + dr[i].ToString() + "</th>";
                    }
                    Row += "<tr>" + row + "</tr>";
                    row = "";
                }
                var Table = Column + Row;
                cnn.Close();
                return Json(Table);
            }
            catch (Exception exp)
            {
                return Json(exp.Message);
            }



            }
            else
            {
                //Oracle Query

               string connetionString = "Data Source="+bAZA_DANYCH.SCIEZKA+";User Id="+bAZA_DANYCH.LOGIN+";Password="+bAZA_DANYCH.HASLO+";DBA Privilege=SYSDBA;";

                try
                {
                    OracleConnection cnn = new OracleConnection(connetionString);
                    cnn.Open();
                    OracleCommand command = new OracleCommand(query, cnn);
                    OracleDataReader dr = command.ExecuteReader();


                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        column += "<th style=\"background-color:#808080\"> " + dr.GetName(i) + "</th>";
                    }
                    var Column = "<tr>" + column + "</tr>";



                    while (dr.Read())
                    {

                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            row += "<th>" + dr[i].ToString() + "</th>";
                        }
                        Row += "<tr>" + row + "</tr>";
                        row = "";
                    }
                    var Table = Column + Row;
                    cnn.Close();
                    return Json(Table);

                }
                catch(Exception exp)
                {
                    return Json(exp.Message);
                }
            }





           


        }



         

    }
}