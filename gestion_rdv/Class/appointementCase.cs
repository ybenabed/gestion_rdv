using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_rdv
{
    public class appointementCase
    {
        private appointementCase(string value, int range) { toString = value; toInt = range; }
        public string toString { get; set; }
        public int toInt { get; set; }
        public static appointementCase Valide { get { return new appointementCase("Validé", 0); } }
        public static appointementCase Enattente { get { return new appointementCase("En Attente", 1); } }
        public static appointementCase Annule { get { return new appointementCase("Annulé", 2); } }

        public static appointementCase corresponding(int range)
        {
            switch (range)
            {
                case 0:
                    return appointementCase.Valide;
                case 1:
                    return appointementCase.Enattente;
                case 2 :
                    return appointementCase.Annule;
                default :
                    return appointementCase.Valide;
            }
        }
    }
}
