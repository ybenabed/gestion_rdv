using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace gestion_rdv
{
    public struct dataofRdv
    {
        public int key { get; set; }
        public int docKey { get; set; }
        public int patKey { get; set; }
        public DateTime date { get; set; }
        public appointementCase etat { get; set; }
        public string comment { get; set; }
    }
    class Rendez_Vous
    {
        private int Id_Rdv { get; set; }
        private int Id_medecin { get; set; }
        private int Id_patient { get; set; }
        private DateTime Date_RDV { get; set; }
        private appointementCase etat { get; set; }
        private String Commentaire_Rdv { get; set; }
        public Rendez_Vous( DateTime date, String Cmnt, appointementCase eta)
        {
            this.Date_RDV = date;
            this.Commentaire_Rdv = Cmnt; this.etat = eta;
        }
        public Rendez_Vous(DateTime date){ this.Date_RDV = date;}
        public Rendez_Vous(dataofRdv rdv)
        {
            Id_Rdv = rdv.key; Id_medecin = rdv.docKey; Id_patient = rdv.patKey;
            Date_RDV = rdv.date; Commentaire_Rdv = rdv.comment; etat = rdv.etat;
        }
        public dataofRdv convertTo()
        {
            dataofRdv rdv = new dataofRdv();
            rdv.key =Id_Rdv ; rdv.docKey=Id_medecin; rdv.patKey=Id_patient;
            rdv.date = Date_RDV; rdv.comment = Commentaire_Rdv; rdv.etat = etat;
            return rdv;
        } 

        public static Dictionary<DateTime, List<dataofRdv>> appointementIn(DateTime month, int medcin)
        {
            Dictionary<DateTime, List<dataofRdv>> map = new Dictionary<DateTime, List<dataofRdv>>();
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "getAppointement";
            Datab.cmd.Parameters.Add("smaller", SqlDbType.Date).Value = month;
            Datab.cmd.Parameters.Add("bigger", SqlDbType.Date).Value = month.AddMonths(1).AddDays(-1);
            Datab.cmd.Parameters.Add("med", SqlDbType.Int).Value = medcin;
            Datab.cmd.Connection = Datab.cnx;
            SqlDataReader dr = Datab.cmd.ExecuteReader();
            while (dr.Read())
            {
                dataofRdv rdv = new dataofRdv();
                rdv.key = int.Parse(dr[0].ToString()); rdv.docKey = int.Parse(dr[2].ToString()); rdv.patKey = int.Parse(dr[1].ToString());
                rdv.date = DateTime.Parse(dr[3].ToString());
                rdv.etat = appointementCase.corresponding(int.Parse(dr[4].ToString()));
                List<dataofRdv> list;
                if (map.TryGetValue(rdv.date.Date, out list))
                {
                    list.Add(rdv);
                }
                else
                {
                    list = new List<dataofRdv>();
                    list.Add(rdv);
                    map.Add(rdv.date.Date, list);
                }
            }
            Datab.deconnecter();
            return map;
        }
        public void deleteAppointement()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "deleteApp";
            Datab.cmd.Parameters.Add("@idApp", SqlDbType.Int).Value = Id_Rdv;
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.ExecuteNonQuery();
            Datab.deconnecter();
        }
        public void Ajouter_RDV()
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "Insert_Appoointement";
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.Parameters.Add("@patient", SqlDbType.Int).Value = Id_patient;
            Datab.cmd.Parameters.Add("@doctor",SqlDbType.Int).Value = Id_medecin;
            Datab.cmd.Parameters.Add("@etat",SqlDbType.Int).Value = etat.toInt;
            Datab.cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = Date_RDV;
            Datab.cmd.ExecuteNonQuery();
            Datab.deconnecter();
        }
        public void Update1(String Attribut, String NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Rendez-vous SET " + Attribut + "='" + NvlValeur + "' where Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();
        }

        public void updateState(int etat)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "updateAppointementState";
            Datab.cmd.Connection = Datab.cnx;
            Datab.cmd.Parameters.Add("@id", SqlDbType.Int).Value = Id_Rdv;
            Datab.cmd.Parameters.Add("@etat", SqlDbType.Int).Value = etat;
            Datab.cmd.ExecuteNonQuery();
            Datab.deconnecter();
        }

        public void Update2(String Attribut, int NvlValeur)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update Rendez-vous SET " + Attribut + "=" + NvlValeur + " where Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();
        }
        public void update_Id_pat(int Id_pat)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            string Command = @"Update [Rendez-vous] SET Id_Patient = " + Id_pat + " WHERE Id_RDV=" + Id_Rdv;
            SqlCommand Macmd = new SqlCommand(Command, Datab.cnx);
            Macmd.ExecuteNonQuery();
            Datab.deconnecter();
        }
        public void update_Imp(int imp)
        {
            Update2("Imporant", imp);
        }
        public void update_Description(String Cmnt)
        {
            Update1("Commentaire", Cmnt);
        }
        public void update_Lieu(String lieu)
        {
            Update1("Lieu", lieu);
        }

        public static List<dataofRdv> getPatientAppointement(int idPat, int idMed)
        {
            ConnexionBDD Datab = new ConnexionBDD();
            Datab.connecter();
            Datab.cmd.CommandType = CommandType.StoredProcedure;
            Datab.cmd.CommandText = "getPatApp";
            Datab.cmd.Parameters.Add("@idpat", SqlDbType.Int).Value = idPat;
            Datab.cmd.Parameters.Add("@idmed", SqlDbType.Int).Value = idMed;
            Datab.cmd.Connection = Datab.cnx;
            List<dataofRdv> list = new List<dataofRdv>();
            SqlDataReader dr = Datab.cmd.ExecuteReader();
            while (dr.Read())
            {
                dataofRdv rdv = new dataofRdv();
                rdv.key = int.Parse(dr[0].ToString()); rdv.docKey = int.Parse(dr[2].ToString()); rdv.patKey = int.Parse(dr[1].ToString());
                rdv.date = DateTime.Parse(dr[3].ToString());
                rdv.etat = appointementCase.corresponding(int.Parse(dr[4].ToString()));
                list.Add(rdv);
            }
            Datab.deconnecter();
            return list;
        }

        public static void imprimerRendezVous(dataOfPatient patinet, dataofRdv rendezvous,string path)
        {
            String text = "\t\t\t\tRendez-vous\n\t\t\t--------------------------\n\nConcerné: ";
            text += patinet.nom.ToUpper() + " " + patinet.prenom.ToUpper() + "\nDate: ";
            string dayofweek = rendezvous.date.ToString("dddd", new System.Globalization.CultureInfo("fr-FR"));
            dayofweek = char.ToUpper(dayofweek[0]) + dayofweek.Substring(1);
            text += dayofweek + rendezvous.date.ToShortDateString() + "\nHeure: ";
            text += rendezvous.date.ToShortTimeString() + "\nObjet: " + rendezvous.comment;
            System.IO.File.WriteAllText(path, text);
        }
    }
}
