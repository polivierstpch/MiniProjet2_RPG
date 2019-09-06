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
            int movement = chance.Next(1,4);
            if (this.position + movement > 32)
            {
                this.position += movement - 32;
            }
            else
            {
                this.position += movement;
            }
           

            GameManager.AddToGameLog($"Vous vous déplacez de {movement} pièces.");
            GameManager.SetGamePrompt("Appuyez sur une touche pour continuer");
            GameManager.UpdateLog();
            Console.ReadKey();
        }

        public void PlayGame(Character player)
        {
            Tile currentTile;

            GameManager.DrawUI(player);

            do
            {

                GameManager.ClearGameLog();

                currentTile = GetTileAtPosition(this.position);

                switch (currentTile.EventType)
                {
                    case "vide":
                        GameManager.AddToGameLog("La salle où vous vous trouvez est vide.");
                        GameManager.UpdateLog();
                        break;
                    case "monstre":                       
                        Combat(player, GetMonsterOnTile(currentTile.EventId));
                        break;
                    case "coffre":                       
                        GetRandomEquipement(player);                       
                        break;
                    case "sortie":                       
                        GameManager.AddToGameLog("Vous avez trouvé une sortie!");
                        GameManager.UpdateLog();
                        Console.ReadKey(true);
                        GameManager.VictoryScreen();
                        return;
                }

                GameManager.DrawUI(player);

                Movement();

            } while (currentTile.EventId != -1);

        }

        // Tile methods 

        // Returns the tile at the position given as parameter
        private Tile GetTileAtPosition(int position)
        {
            foreach (Tile tile in map)
            {
                if (position == tile.ID )
                {
                    return tile;
                }
            }

            return null;
        }

        private void Combat(Character player, Monster encounter)
        {
            //Set up UI for combat.
            GameManager.ClearGameLog();

            GameManager.AddToGameLog($"Vous tombez sur un {encounter.Name}!");
            GameManager.SetGamePrompt("A = Attaquer | S = Attaque spéciale | D = Défendre | P = Utilise une potion");
            GameManager.UpdateCombatUI(player, encounter);

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

                GameManager.UpdateCombatUI(player, encounter);
                GameManager.ClearGameLog();
            }
            //Le Joueur Meurt!!!
            if (player.CurrentHP < 1)
            {
                Console.Clear();
                GameManager.GameOverScreen();
                return;
            }
            else
            {
                GameManager.AddToGameLog($"Vous avez défait le {encounter.Name}!");
                GameManager.SetGamePrompt("Appuyez sur une touche pour continuer.");               
                GameManager.UpdateLog();               
            }

            encounter.CurrentHP = encounter.MaxHP;

            Console.ReadKey(true);
            
        }

        private void PlayerChoice(Character player, Monster encounter)
        {
            bool wrongInput = false;
            do
            {
                switch (Console.ReadKey(true).KeyChar)
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
                            case 'h':
                                player.UsePotions("Vie");
                                break;
                            case 'm':
                                player.UsePotions("Mana");
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

        private Monster GetMonsterOnTile(int eventId)
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

        private void GetRandomEquipement(Character player)
        {
            GameManager.AddToGameLog("Vous trouvez un coffre!");
            
            int EquipmentGenerator = chance.Next(1,6);
            foreach (Equipment item in items)
            {
                if (EquipmentGenerator == item.ID)
                {
                    GameManager.AddToGameLog($"Vous trouvez un objet : {item.Name}!");
                    
                    switch (item.ItemType)
                    {
                        case "potion":
                            GameManager.AddToGameLog("Vous mettez la potion dans votre sac.");
                            player.NumOfPotions++;                          
                            break;
                        case "arme":
                            if (item.Modifier > player.Attack && item.ClassRequired == player.CharacterClass)
                            {                               
                                GameManager.AddToGameLog($"Vous changez votre {player.Weapon} pour {item.Name}.");
                                player.Attack = item.Modifier;
                                player.Weapon = item.Name;                                
                            }
                            else
                            {
                                GameManager.AddToGameLog("L'objet est sans valeur. Vous le laissez là où vous l'avez trouvé.");
                            }
                            break;
                        case "armure":
                            if (item.Modifier > player.Defense && item.ClassRequired == player.CharacterClass)
                            {
                                GameManager.AddToGameLog($"Vous changez votre {player.Armor} pour {item.Name}.");
                                player.Defense = item.Modifier;
                                player.Armor = item.Name;
                            }
                            else
                            {
                                GameManager.AddToGameLog($"{item.Name} est sans valeur. Vous le laissez là où vous l'avez trouvé.");
                            }
                            break;
                    }

                    GameManager.UpdateItemUI(item);
                    GameManager.SetGamePrompt("Appuyez sur une touche pour continuer.");
                    GameManager.UpdateLog();
                    GameManager.ClearGameLog();

                    Console.ReadKey(true);
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
