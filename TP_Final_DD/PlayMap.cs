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
        private List<Tile> map = new List<Tile>();
        private List<Monster> monsters = new List<Monster>();
        private List<Equipment> items = new List<Equipment>();
        private static Random chance = new Random();

        public PlayMap()
        {
            DataReader();
        }

        // Get data from csv files
        private void DataReader()
        {
            if (File.Exists("carte.csv"))
            {
                using (StreamReader cartesList = new StreamReader("carte.csv"))
                {
                    string line;
                    while (!cartesList.EndOfStream)
                    {
                        line = cartesList.ReadLine();
                        Tile nTuile = new Tile(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], int.Parse(line.Split(',').ToList()[2]));
                        this.map.Add(nTuile);
                    }
                }
            }
            else
            {
                Console.WriteLine("le fichier carte.csv n'a pas été trouvé!");
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
                        Equipment nEquip = new Equipment(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], line.Split(',').ToList()[2], line.Split(',').ToList()[3], int.Parse(line.Split(',').ToList()[4]));
                        this.items.Add(nEquip);
                    }
                }
            }
            else
            {
                Console.WriteLine("Le fichier equipement.csv n'a pas été trouvé!");
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
                        Monster nMonster = new Monster(int.Parse(line.Split(',').ToList()[0]), line.Split(',').ToList()[1], int.Parse(line.Split(',').ToList()[2]), int.Parse(line.Split(',').ToList()[3]), int.Parse(line.Split(',').ToList()[4]));
                        this.monsters.Add(nMonster);
                    }
                }
            }
            else
            {
                Console.WriteLine("Le fichier 'monstre.csv' n'a pas été trouvé!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        private void Movement()
        {
            int movemement = chance.Next(1,4);
            if (this.position + movemement > 32)
            {
                this.position += movemement - 32;
            }
            else
            {
                this.position += movemement;
            }
        }

        public void PlayEvent(Character player)
        {
            foreach (Tile tile in map)
            {
                if (this.position == tile.ID)
                {
                    switch (tile.EventType)
                    {
                        case "vide":
                            Movement();
                            break;
                        case "monstre":
                            Combat(player, MonsterGenerator(tile.EventId));
                            Movement();
                            break;
                        case "coffre":
                            RandomEquipment(player);
                            Movement();
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

        private void Combat(Character player, Monster encounter)
        {
            int initiative = chance.Next(2);
            while (player.CurrentHP > 0 && encounter.CurrentHP > 0)
            {
                             
                if (initiative == 0)
                {
                    //Joueur agit
                    PlayerChoice(player, encounter);
                    //Monstre agit
                    AttackOrPass(player, encounter);
                }
                else
                {
                    //Monstre agit
                    AttackOrPass(player, encounter);
                    //Joueur agit
                    PlayerChoice(player, encounter);
                }

                GameManager.UpdateCombatUI(player,encounter);
                GameManager.ClearCombatLog();
            }
            //Le Joueur Meur!!!
            if (player.CurrentHP < 1)
            {
                Console.Clear();
                //GameLogger.GameOverScreen();
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private void PlayerChoice(Character player, Monster encounter)
        {
            bool wrongInput = false;
            do
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case 'a':
                        GameManager.AddToGameLog($"Vous attaquez le {encounter.Name} avec votre {player.Weapon}");
                        player.IsDefending = false;
                        encounter.TakeDamage(player.Attack);
                        wrongInput = false;
                        break;
                    case 's':
                        GameManager.AddToGameLog($"Vous utiliser votre attaque spéciale!");
                        player.IsDefending = false;
                        encounter.TakeDamage(player.SpecialAttack());
                        wrongInput = false;
                        break;
                    case 'd':
                        player.IsDefending = true;
                        GameManager.AddToGameLog("Vous prenez une posture défensive.");
                        wrongInput = false;
                        break;
                    case 'p':
                        player.IsDefending = false;
                        GameManager.AddToGameLog("Vous prenez une potion.");
                        GameManager.AddToGameLog("Appuyez sur H pour récupérer 50 points de vie ou appuyez sur M pour récupérer 50 points de mana!");
                        GameManager.UpdateCombatUI(player, encounter);
                        wrongInput = false;
                        switch (Console.ReadKey(true).KeyChar)
                        {
                            case 'H':
                                player.UsePotions("Vie");
                                GameManager.AddToGameLog("Vous récupérer 50 points de vie.");
                                break;
                            case 'M':
                                player.UsePotions("Mana");
                                GameManager.AddToGameLog("Vous récupérer 50 points de mana.");
                                break;
                            default:
                                wrongInput = true;
                                break;
                        }
                        
                        break;
                    default:
                        wrongInput = true;
                        break;
         
                }              
            } while (wrongInput);

        }

        private Monster MonsterGenerator(int eventId)
        {
            foreach (Monster encounter in monsters)
            {
                if (eventId == encounter.ID)
                {
                    return encounter;
                }
            }

            // If no monsters are found, return a null Monster
            return null;
        }

        private void RandomEquipment(Character player)
        {
            int EquipmentGenerator = chance.Next(1,6);
            foreach (Equipment item in items)
            {
                if (EquipmentGenerator == item.ID)
                {
                    switch (item.ItemType)
                    {
                        case "potion":
                            player.NumOfPotions++;
                            break;
                        case "arme":
                            if (item.Modifier > player.Attack && item.ClassRequired == player.CharacterClass)
                            {
                                player.Attack = item.Modifier;
                                player.Weapon = item.Name;
                            }
                            else
                            {
                                Console.WriteLine($"{item.Name} est sans valeure");
                            }
                            return;
                        case "armure":
                            if (item.Modifier > player.Defense && item.ClassRequired == player.CharacterClass)
                            {
                                player.Defense = item.Modifier;
                                player.Armor = item.Name;
                            }
                            else
                            {
                                Console.WriteLine($"{item.Name} est sans valeure");
                            }
                            return;
                    }
                } 
            }
        }

        private void AttackOrPass(Character player, Monster encounter)
        {
            int defenseOrAttack = chance.Next(2);
            if (defenseOrAttack == 0)
            {
                GameManager.AddToGameLog($"Le {encounter.Name} prend une posture défensive.");
                encounter.IsDefending = true;                         
            }
            else
            {
                GameManager.AddToGameLog($"Le {encounter.Name} vous attaque!");
                encounter.IsDefending = false;
                player.TakeDamage(encounter.Attack);
                
            }

        }
    }
}
