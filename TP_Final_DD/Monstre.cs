using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Monstre
    {
        private int iD, attac, def, pVMax, pVAct;
        private string nom;
        private bool isDefending;

        //AXcceseur
        public int ID { get { return iD; } }
        public int PVMax { get { return pVMax; } }
        public int PVAct { get { return pVAct; } }
        public int Attac { get { return attac; } }
        public int Def { get { return def; } }
        public bool IsDefending { get { return isDefending; } set { isDefending = value; } }

        public Monstre(int ID, string Nom, int Attac, int Def, int PVMax)
        {
            iD = ID;
            attac = Attac;
            def = Def;
            pVMax = PVMax;
            pVAct = PVMax;
            nom = Nom;
        }
        public void TakeDamage(int damage)
        {
            if (isDefending == true)
            {
                this.pVAct -= Math.Max(0, damage - this.def);
            }
            else
            {
                this.pVAct -= damage;
            }
        }
    }
}
