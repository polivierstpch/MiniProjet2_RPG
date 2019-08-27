using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Personnage
    {
        private string classePerso, weapon, armor;
        private int pVMax, pVAct, pMMax, pMAct, attac, def, nombrePotions;
        private bool isDefending;

        //AXcceseur
        public string ClassePerso { get { return classePerso; } }
        public int PVMax { get { return pVMax; } }
        public int PVAct { get { return pVAct; } set { pVAct = value; } }
        public int PMMax { get { return pMMax; } }
        public int PMAct { get { return pMAct; } set { pMAct = value; } }
        public int NombrePotions { get { return nombrePotions; } set { nombrePotions = value; } }
        public string Weapon { get { return weapon; } set { weapon = value; } }
        public int Attac { get { return attac; } set { attac = value; } }
        public string Armor { get { return armor; } set { armor = value; } }
        public int Def { get { return def; } set { def = value; } }
        public bool IsDefending { get { return isDefending; } set { isDefending = value; } }

        public Personnage(string classePerso) // public Personnage (string typePerso)
        {
            switch (classePerso)
            {
                case "guerrier":
                    this.classePerso = classePerso;
                    weapon = "Epée à deux mains";
                    armor = "• Armure en cuire";
                    pVMax = 200;
                    pVAct = 200;
                    pMMax = 100;
                    PMAct = 100;
                    Attac = 5;
                    Def = 4;
                    NombrePotions = 2;
                    break;
                case "mage":;
                    this.classePerso = classePerso;
                    Weapon = "Dague";
                    Armor = "Cape_elfique";
                    pVMax = 100;
                    PVAct = 100;
                    pMMax = 200;
                    PMAct = 200;
                    Attac = 3;
                    Def = 6;
                    NombrePotions = 2;
                    break;
            }
        }
        public void TakeDamage(int damage)
        {
            if (isDefending == true)
            {
                this.pVAct -= Math.Max(0, damage - this.def); 
            }
            else
            {
                this.pVAct = this.pVAct - damage;
            }
        }
        public void UsePotions(string Mana_Ou_Vie)
        {
            if (this.nombrePotions > 0)
            {
                switch (Mana_Ou_Vie)
                {
                    case "Vie":
                        if (this.pVAct + 50 < this.PVMax)
                        {
                            this.pVAct += 50;
                        }
                        else
                        {
                            this.pVAct = this.pVMax;
                        }
                        break;
                    case "Mana":
                        if (this.pMAct + 50 < this.PMMax)
                        {
                            this.pMAct += 50;
                        }
                        else
                        {
                            this.pMAct = this.pMMax;
                        }
                        break;
                }
            }
        }
        public int SpecialAttack()
        {
            switch (this.classePerso)
            {
                case "guerrier":
                    if (this.pMAct >= 20)
                    {
                        this.pMAct -= 20;
                        return 100;
                    }
                    else
                    {
                        Console.WriteLine("SpecialAttack Miss, low MANA!");
                        return 0;
                    }
                case "mage":
                    if (this.pMAct >= 25)
                    {
                        this.pMAct -= 25;
                        return 15;
                    }
                    else
                    {
                        Console.WriteLine("SpecialAttack Miss, low MANA!");
                        return 0;
                    }
                default:
                    return 0;
            }
        } 
    }
}
