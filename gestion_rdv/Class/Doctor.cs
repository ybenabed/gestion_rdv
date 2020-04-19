using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace gestion_rdv
{
    public class Doctor 
    {
        private int Id_Doc { get; set; }
        protected String Nom { get; set; }
        private String Username { get; set; }
        private String Password { get; set; }
        public Doctor(int i)
        {
            Id_Doc = i;
        }
        public int ID() { return Id_Doc; }
        public Doctor(String user, String password)
        {
            this.Username = user; this.Password = password;
        }
        public int accountConnect(out Exception exx)
        {
            exx = new Exception();
            try
            {
                ConnexionBDD Datab = new ConnexionBDD();
                Datab.connecter();
                this.CrypterMdp();
                string command = "SELECT * FROM Medcin WHERE Username='" + this.Username + "' AND Password='" + this.Password + "'";
                SqlCommand Macmd = new SqlCommand(command, Datab.cnx);
                SqlDataReader dr;
                dr = Macmd.ExecuteReader();
                int count = 0;
                string typecompt = ""; int etat = -1;
                if (dr.Read())
                {
                    count++;
                    Id_Doc = int.Parse(dr[0].ToString());
                    Nom = dr[1].ToString();
                    Username = dr[2].ToString();
                    Password = dr[3].ToString();
                    typecompt = dr[4].ToString();
                    etat = int.Parse(dr[5].ToString());
                }
                dr.Close();
                if (count == 1)
                {
                    if (etat == 1)
                    {
                        if (typecompt == "MedcinAdmin")
                        {
                            //Réussi = 0 
                            return 0;
                        }
                        else
                        {
                            if (typecompt == "Medcin")
                            {
                                return 1;
                            }
                        }

                    }
                    else
                    {
                        //Désactivé = 1 
                        return 2;
                    }

                }
                else
                {
                    //Erreur 
                    return 3;
                }
            }
            catch (Exception ex)
            {
                //Exception 
                exx = ex;
                return 4;
            }
            return 5;
        }

        private void CrypterMdp()
        {
            MD5CryptoServiceProvider serv = new MD5CryptoServiceProvider();
            serv.ComputeHash(ASCIIEncoding.ASCII.GetBytes(this.Password));
            byte[] encrypted = serv.Hash;
            StringBuilder strbuild = new StringBuilder();
            for (int i = 0; i < encrypted.Length; i++)
            {
                strbuild.Append(encrypted[i].ToString("x2"));
            }
            this.Password = strbuild.ToString();
        } 
    }
}
