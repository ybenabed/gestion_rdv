using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace gestion_rdv
{
    public struct dataOfPatient
    {
        public int idpat { get; set; }
        public String nom { get; set; }
        public String prenom { get; set; }
        public String num { get; set; }
        public String email { get; set; }
        public DateTime date { get; set; }
        public String sexe { get; set; }
    }
    public class Patient 
    {
        
        private int Id_Patient { get; set; }
        protected String Nom { get; set; }
        protected String Prenom { get; set; }
        protected String Num { get; set; }
        private String email { get; set; }
        private DateTime Date_naissance { get; set; }
        private int Sexe { get; set; }

        public Patient(int i) { Id_Patient = i; }
        public Patient(int i,String n, String p, String numte, DateTime Date_n,int s,string e)
        {
            this.Id_Patient = i;
            Sexe = s; 
            Nom = n;
            Prenom = p;
            Num = numte;
            Date_naissance = Date_n.Date;
            email = e;
        }
        public Patient(String n, String p, String numte, DateTime Date_n, int s)
        {
            this.Sexe = s;
            this.Nom = n;
            this.Prenom = p;
            this.Num = numte;
            this.Date_naissance = Date_n.Date;
        }
        public void Insert_Nv_Patient()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "Insert_Patient";
            Datab.cmd.Parameters.Add("@nom", SqlDbType.NVarChar, 50).Value = this.Nom;
            Datab.cmd.Parameters.Add("@prenom", SqlDbType.NVarChar, 50).Value = this.Prenom;
            Datab.cmd.Parameters.Add("@numtel", SqlDbType.NVarChar, 14).Value = this.Num;
            Datab.cmd.Parameters.Add("@datenaiss", SqlDbType.Date).Value = this.Date_naissance;
            Datab.cmd.Parameters.Add("@sexe", SqlDbType.Int).Value = this.Sexe;
            SqlParameter sort = new SqlParameter("@Id_Pat", SqlDbType.Int);
            sort.Direction = ParameterDirection.Output;
            Datab.cmd.Parameters.Add(sort);
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Id_Patient = (int)sort.Value;
            Datab.deconnecter();
        }
        public static List<dataOfPatient> getListOfPatients()
        {
            List<dataOfPatient> listt = new List<dataOfPatient>();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String Command = @"select * from Patient where Deleted=0";
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            while (dr.Read())
            {
                dataOfPatient item = new dataOfPatient();
                item.idpat = int.Parse(dr[0].ToString()); item.nom = dr[1].ToString(); item.prenom = dr[2].ToString();
                item.num = dr[3].ToString(); item.date = DateTime.Parse(dr[4].ToString());
                if (bool.Parse(dr[5].ToString()) == false) item.sexe = "Femme";
                else item.sexe = "Homme";
                item.email = dr[7].ToString();
                listt.Add(item);
            }
            return listt; 
        }
        public dataOfPatient getItData()
        {
            dataOfPatient datapat = new dataOfPatient();
            datapat.nom = this.Nom; datapat.prenom = this.Prenom; datapat.num = this.Num; datapat.date = Date_naissance;
            datapat.sexe = (Sexe == 1) ? "HOMME" : "FEMME"; datapat.email = this.email;
            return datapat;
        }
        private void update()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "updatePat";
            Datab.cmd.Parameters.Add("@idpat", SqlDbType.Int).Value = Id_Patient;
            Datab.cmd.Parameters.Add("@nom", SqlDbType.NVarChar, 50).Value = Nom;
            Datab.cmd.Parameters.Add("@prenom", SqlDbType.NVarChar, 50).Value = Prenom;
            Datab.cmd.Parameters.Add("@num", SqlDbType.NVarChar, 14).Value = Num;
            Datab.cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 25).Value = email;
            Datab.cmd.Parameters.Add("@date", SqlDbType.Date).Value = Date_naissance;
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Datab.deconnecter();
        }      
        public String toString()
        {
            return Nom + " " + Prenom + " N" + Id_Patient;
        }
        public static dataOfPatient getPatient(int id)
        {
            dataOfPatient patient = new dataOfPatient();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            String command = "SELECT * FROM Patient WHERE Id_Patient=" + id;
            SqlCommand Macmd = new SqlCommand(command,Datab.cnx);
            SqlDataReader dr = Macmd.ExecuteReader();
            if (dr.Read())
            {
                patient.nom = dr[1].ToString(); patient.prenom = dr[2].ToString();
                patient.num = dr[3].ToString(); patient.date = DateTime.Parse(dr[4].ToString());
                patient.idpat = id; 
            }
            Datab.deconnecter();
            return patient;
        }
    }
}
