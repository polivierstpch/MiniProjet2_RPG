using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TP_Final_DD
{
    class PlayMap
    {
        private int position = 1;
        private List<Tuile> map = new List<Tuile>();
        private List<Monstre> monstres = new List<Monstre>();
        private List<Equipement> items = new List<Equipement>();
        public Random chance = new Random();

        public void DataReader()
        {
            if (File.Exists("carte.csv"))
            {
                using (StreamReader cartesList = new StreamReader("carte.csv"))
                {
                    string line;
                    while (!cartesList.EndOfStream)
                    {
                        line = cartesList.ReadLine();
                        Tuile nTuile = new Tuile(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], int.Parse(line.Split(',').ToList()[2]));
                        this.map.Add(nTuile);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not Existe! 01");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            if (File.Exists("equipement.csv"))
            {
                using (StreamReader equipementList = new StreamReader("equipement.csv"))
                {
                    string line;
                    while (!equipementList.EndOfStream)
                    {
                        line = equipementList.ReadLine();
                        Equipement nEquip = new Equipement(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], line.Split(',').ToList()[2], line.Split(',').ToList()[3], int.Parse(line.Split(',').ToList()[4]));
                        this.items.Add(nEquip);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not Existe! 02");
                Console.ReadKey();
                Environment.Exit(-1);
            }

            if (File.Exists("monstre.csv"))
            {
                using (StreamReader monsterList = new StreamReader("monstre.csv"))
                {
                    string line;
                    while (!monsterList.EndOfStream)
                    {
                        line = monsterList.ReadLine();
                        Monstre nMonster = new Monstre(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], int.Parse(line.Split(',').ToList()[2]), int.Parse(line.Split(',').ToList()[3]), int.Parse(line.Split(',').ToList()[4]));
                        this.monstres.Add(nMonster);
                    }
                }
            }
            else
            {
                Console.WriteLine("File not Existe! 03");
                Console.ReadKey();
                Environment.Exit(-1);
            }
        }
        private void Mouvement()
        {
            int mouvement = this.chance.Next(1,4);
            if (this.position + mouvement > 32)
            {
                this.position += mouvement - 32;
            }
            else
            {
                this.position += mouvement;
            }
        }

        public void PlayEvent(Personnage joueur)
        {
            foreach (Tuile tuile in map)
            {
                if (this.position == tuile.Id)
                {
                    switch (tuile.EventType)
                    {
                        case "vide":
                            Mouvement();
                            break;
                        case "monstre":
                            Combat(joueur, MonsterGenerateur(tuile.EventId));
                            Mouvement();
                            break;
                        case "coffre":
                            Equipement_Random(joueur);
                            Mouvement();
                            break;
                        case "sortie":
                            Console.Clear();
                            Console.WriteLine("Victoir Game Over");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }

        private void Combat(Personnage joueur, Monstre enconter)
        {
            int initiative = chance.Next(2);
            while (joueur.PVAct > 0 && enconter.PVAct > 0)
            {
                if (initiative == 0)
                {
                    //Joueur agit
                    ChoixJouer(joueur, enconter);
                    //Monstre agit
                    AttackOrPass(joueur, enconter);
                }
                else
                {
                    //Monstre agit
                    AttackOrPass(joueur, enconter);
                    //Joueur agit
                    ChoixJouer(joueur, enconter);
                }
            }
            //Le Joueur Meur!!!
            if (joueur.PVAct < 1)
            {
                Console.Clear();
                Console.WriteLine("Vous et mort Game Over");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private void ChoixJouer(Personnage joueur, Monstre enconter)
        {
            while (Console.KeyAvailable) Console.ReadKey(true); 
            switch (Console.ReadKey().KeyChar)
            {
                case 'a':
                    joueur.IsDefending = false;
                    enconter.TakeDamage(joueur.Attac);
                    break;
                case 's':
                    joueur.IsDefending = false;
                    enconter.TakeDamage(joueur.SpecialAttack());
                    break;
                case 'd':
                    joueur.IsDefending = true;
                    break;
                case 'p':
                    joueur.IsDefending = false;
                    Console.WriteLine("H pour étendre la potion sur vos blaissure");
                    Console.WriteLine("M pour la boir et regagnier du MANA!");
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case 'H':
                            joueur.UsePotions("Vie");
                            break;
                        case 'M':
                            joueur.UsePotions("Mana");
                            break;
                    }
                    break;
            }
        }

        private Monstre MonsterGenerateur(int EventId)
        {
            foreach (Monstre enconter in monstres)
            {
                if (EventId == enconter.ID)
                {
                    return enconter;
                }
            }
            return null;
        }

        private void Equipement_Random(Personnage joueur)
        {
            int EquipemenGenerateur = chance.Next(1,6);
            foreach (Equipement item in items)
            {
                if (EquipemenGenerateur == item.Id)
                {
                    switch (item.Type)
                    {
                        case "potion":
                            joueur.NombrePotions++;
                            break;
                        case "arme":
                            if (item.Mod > joueur.Attac && item.Classe == joueur.ClassePerso)
                            {
                                joueur.Attac = item.Mod;
                                joueur.Weapon = item.Nom;
                            }
                            else
                            {
                                Console.WriteLine($"{item.Nom} est sans valeure");
                            }
                            return;
                        case "armure":
                            if (item.Mod > joueur.Def && item.Classe == joueur.ClassePerso)
                            {
                                joueur.Def = item.Mod;
                                joueur.Armor = item.Nom;
                            }
                            else
                            {
                                Console.WriteLine($"{item.Nom} est sans valeure");
                            }
                            return;
                    }
                } 
            }
        }

        private void AttackOrPass(Personnage joueur, Monstre enconter)
        {
            int deforatt = chance.Next(2);
            if (deforatt == 0)
            {
                enconter.IsDefending = true;
            }
            else
            {
                enconter.IsDefending = false;
                joueur.TakeDamage(enconter.Attac);
            }
        }
    }
}
