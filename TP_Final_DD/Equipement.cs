using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Equipement
    {
        private int id, mod;
        private string type, nom, classe;

        //AXcceseur
        public int Id { get { return id; } }
        public int Mod { get { return mod; } }
        public string Type { get { return type; } }
        public string Nom { get { return nom; } }
        public string Classe { get { return classe; } }

        public Equipement(int Id, string Type, string Nom, string Classe, int Mod)
        {
            this.id = Id;
            this.mod = Mod;
            this.type = Type;
            this.nom = Nom;
            this.classe = Classe;
        }
    }
}
