using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEquipement
{
    [Serializable]
    public class Equipement
    {
        public int IdEquipement { get; set; }
        public string Serial_number { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public int Fk_tag { get; set; }
        public DateTime DateTime { get; set; }
        public int Etage { get; set; }
        public int Piece { get; set; }
        public string Description { get; set; }
        public string Statut { get; set; }
        public int Fk_statut { get; set; }


        public Equipement()
        {

        }
    }
}
