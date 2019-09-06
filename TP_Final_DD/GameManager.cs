using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    static class GameManager
    {
        public const int WINDOW_WIDTH = 100;
        public const int WINDOW_HEIGHT = 30;

        private static List<string> gameLog = new List<string>();
        private static string gamePrompt = "";

        public static void AddToGameLog(string message)
        {
            gameLog.Add(message);
        }

        public static void SetGamePrompt(string message)
        {
            gamePrompt = message;
        }

        public static void UpdateLog()
        {
            Console.SetCursorPosition(2, 7);
            ClearNumberOfLines(16);
            Console.SetCursorPosition(2, 7);
            ShowGameLog();
        }


        public static void ClearGameLog()
        {
            gameLog.Clear();
        }

        public static void ShowGameLog()
        {

            foreach (string item in gameLog)
            {
                Console.SetCursorPosition(2, Console.CursorTop);

                for (int i = 0; i < item.Length; i++)
                {
                    if (char.IsDigit(item[i]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }

                    Console.Write(item[i]);
                    Console.ResetColor();
                }

                Console.Write("\n");
            }

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 8);
            Console.Write(CenterText(gamePrompt));
            Console.SetCursorPosition(0, 7);

        }

        public static Character SetupCharacter()
        {
            string input = "";
            bool isGoodInput = false;

            do
            {
                Console.Clear();

                Console.SetCursorPosition(0, 6);
                Console.WriteLine(CenterText("1. LE GUERRIER"));

                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.WriteLine(CenterText("Le guerrier est un combattant avec une endurance et une puissance d'attaque supérieure."));
                Console.WriteLine(CenterText("Il a malgré tout une défense moindre et une capacité magique moindre."));
                Console.WriteLine(CenterText("---------------------------------------------------------------------------------------"));
                Console.WriteLine(CenterText("     POINTS DE VIE : 200 | POINTS DE MANA : 100   "));
                Console.WriteLine(CenterText("            ATTAQUE : 10 | DEFENSE : 7            "));
                Console.WriteLine(CenterText("ARME : Épée à deux mains | ARMURE : Armure de cuir"));
                Console.WriteLine(CenterText("---------------------------------------------------------------------------------------"));

                Console.SetCursorPosition(0, Console.CursorTop + 2);
                Console.WriteLine(CenterText("2. LE MAGE"));
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.WriteLine(CenterText("Le mage est un combattant utilisant la magie pour vaincre ses ennemis."));
                Console.WriteLine(CenterText("Il est fragile et attaque moins fort avec ses attaques normales."));
                Console.WriteLine(CenterText("---------------------------------------------------------------------------------------"));
                Console.WriteLine(CenterText("     POINTS DE VIE : 100 | POINTS DE MANA : 200   "));
                Console.WriteLine(CenterText("             ATTAQUE : 7 | DEFENSE : 10           "));
                Console.WriteLine(CenterText("            ARME : Dague | ARMURE : Cape elfique  "));
                Console.WriteLine(CenterText("---------------------------------------------------------------------------------------"));

                Console.SetCursorPosition(2, 2);
                Console.Write("Veuillez choisir une classe de personnage : ");
                input = Console.ReadLine().ToLower();

                isGoodInput = input == "guerrier" || input == "mage" || input == "1" || input == "2";

                if (!isGoodInput)
                {
                    Console.SetCursorPosition(2, Console.CursorTop + 1);
                    Console.WriteLine("Mauvaise saisie, appuyez sur une touche pour recommencer");
                    Console.ReadKey(true);
                }

            } while (!isGoodInput);

            return new Character(input);
        }

        public static void TitleScreen()
        {

            Console.SetCursorPosition(0, 1);

            Console.WriteLine(CenterText(@"    __  ___  ____   _____ ___  _       ___      ____  ____   ____ "));
            Console.WriteLine(CenterText(@"   /  ]/   \|    \ / ___//   \| |     /  _]    |    \|    \ /    |"));
            Console.WriteLine(CenterText(@"  /  /|     |  _  (   \_|     | |    /  [_     |  D  )  o  )   __|"));
            Console.WriteLine(CenterText(@" /  / |  O  |  |  |\__  |  O  | |___|    _]    |    /|   _/|  |  |"));
            Console.WriteLine(CenterText(@"/   \_|     |  |  |/  \ |     |     |   [_     |    \|  |  |  |_ |"));
            Console.WriteLine(CenterText(@"\     |     |  |  |\    |     |     |     |    |  .  \  |  |     |"));
            Console.WriteLine(CenterText(@" \____|\___/|__|__| \___|\___/|_____|_____|    |__|\_|__|  |___,_|"));

            Console.SetCursorPosition(0, Console.CursorTop + 1);

            Console.WriteLine(CenterText(@" |\                     /)  "));
            Console.WriteLine(CenterText(@" /\_\\__               (_// "));
            Console.WriteLine(CenterText(@"|   `>\-`     _._       //`)"));
            Console.WriteLine(CenterText(@" \ /` \\  _.-`:::`-._  //   "));
            Console.WriteLine(CenterText(@"  `    \|`    :::    `|/    "));
            Console.WriteLine(CenterText(@"        |     :::     |     "));
            Console.WriteLine(CenterText(@"        |.....:::.....|     "));
            Console.WriteLine(CenterText(@"        |:::::::::::::|     "));
            Console.WriteLine(CenterText(@"        |     :::     |     "));
            Console.WriteLine(CenterText(@"        \     :::     /     "));
            Console.WriteLine(CenterText(@"         \    :::    /      "));
            Console.WriteLine(CenterText(@"          `-. ::: .-'       "));
            Console.WriteLine(CenterText(@"           //`:::`\\        "));
            Console.WriteLine(CenterText(@"          //   '   \\       "));
            Console.WriteLine(CenterText(@"         |/         \\      "));

            Console.SetCursorPosition(0, Console.CursorTop + 2);

            Console.WriteLine(CenterText("- Appuyez sur une touche pour commencer -"));

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 1);

            Console.WriteLine(" © Sammy-James Simms et Pier-Olivier St-Pierre-Chouinard" + new string(' ', (WINDOW_WIDTH - 60)) + "2019");

            Console.SetCursorPosition(0, 0);

            Console.ReadKey(true);
        }


        public static void DrawUI(Character character)
        {
            Console.Clear();

            string border = new string('═', WINDOW_WIDTH - 2);
            string emptyLine = new string(' ', WINDOW_WIDTH - 2);

            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║{emptyLine}║");
            Console.WriteLine($"║{emptyLine}║");
            Console.WriteLine($"║{emptyLine}║");
            Console.WriteLine($"║{emptyLine}║");
            Console.WriteLine($"╚{border}╝");

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 7);
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║{emptyLine}║");

            string characterStatus = GetCharacterStatus(character);

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 5);
            for (int k = 0; k < characterStatus.Length; k++)
            {
                if (char.IsDigit(characterStatus[k]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(characterStatus[k]);
                Console.ResetColor();
            }

            Console.WriteLine($"║{emptyLine}║");
            Console.WriteLine($"╚{border}╝");

            Console.SetCursorPosition(0, 0);

            UpdateLog();

        }
        public static void UpdateCombatUI(Character character, Monster monster)
        {
            UpdateLog();

            Console.SetCursorPosition(0, 2);

            string monsterStatus = GetMonsterStatus(monster);

            for (int i = 0; i < monsterStatus.Length; i++)
            {
                if (char.IsDigit(monsterStatus[i]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }

                Console.Write(monsterStatus[i]);
                Console.ResetColor();
            }

            string characterStatus = GetCharacterStatus(character);

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 5);
            for (int k = 0; k < characterStatus.Length; k++)
            {
                if (char.IsDigit(characterStatus[k]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(characterStatus[k]);
                Console.ResetColor();
            }
           
        }

        public static void UpdateItemUI(Equipment equipement)
        {
            Console.SetCursorPosition(0, 2);

            string itemInfo = GetChestInfo(equipement);

            for (int i = 0; i < itemInfo.Length; i++)
            {
                if (char.IsDigit(itemInfo[i]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }

                Console.Write(itemInfo[i]);
                Console.ResetColor();
            }
        }
        

        public static string GetChestInfo(Equipment equipment)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(CenterText($"OBJET : {equipment.Name.ToUpper()}", "║"));
            sb.AppendLine(CenterText($"TYPE D'OBJET : {equipment.ItemType.ToUpper()} | MODIFICATEUR : {equipment.Modifier}", "║"));

            return sb.ToString();
        }

        public static string GetCharacterStatus(Character character)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(CenterText($"CLASSE : {character.CharacterClass.ToUpper()}", "║"));
            sb.AppendLine(CenterText($"PV [ {character.CurrentHP} / {character.MaxHP} ] PM [ {character.CurrentMP} / {character.MaxMP} ] | ATT [ {character.Attack} ] DEF [ {character.Defense} ]", "║"));
            sb.AppendLine(CenterText($"ARME [ {character.Weapon} ] | ARMURE [ {character.Armor} ] | POTIONS [ {character.NumOfPotions} ]", "║"));

            return sb.ToString();
        }

        public static string GetMonsterStatus(Monster monster)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(CenterText($"NOM : {monster.Name.ToUpper()}", "║"));
            sb.AppendLine(CenterText($"PV [ {monster.CurrentHP} / {monster.MaxHP} ] | ATT [ {monster.Attack} ] DEF [ {monster.Defense} ]", "║"));


            return sb.ToString();
        }

       

        public static void VictoryScreen()
        {
            Console.Clear();

            Console.SetCursorPosition(0, 1);

            Console.WriteLine(CenterText(@" __ __  ____   __ ______   ___  ____  ____     ___  __ "));
            Console.WriteLine(CenterText(@"|  |  ||    | /  ]      | /   \|    ||    \   /  _]|  |"));
            Console.WriteLine(CenterText(@"|  |  | |  | /  /|      ||     ||  | |  D  ) /  [_ |  |"));
            Console.WriteLine(CenterText(@"|  |  | |  |/  / |_|  |_||  O  ||  | |    / |    _]|__|"));
            Console.WriteLine(CenterText(@"|  :  | |  /   \_  |  |  |     ||  | |    \ |   [_  __ "));
            Console.WriteLine(CenterText(@" \   /  |  \     | |  |  |     ||  | |  .  \|     ||  |"));
            Console.WriteLine(CenterText(@"  \_/  |____\____| |__|   \___/|____||__|\_||_____||__|"));

            Console.SetCursorPosition(0, Console.CursorTop + 1);

            Console.WriteLine(CenterText("Appuyez sur A pour recommencer ou X pour quitter."));

            switch (Console.ReadKey(true).KeyChar)
            {
                case 'a':
                case 'A':
                    Program.Main(null);
                    goto default;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        public static void GameOverScreen()
        {
            Console.Clear();

            Console.SetCursorPosition(0, 1);

            Console.WriteLine(CenterText(@" __ __   ___   __ __  _____       ___ ______    ___  _____"));
            Console.WriteLine(CenterText(@"|  |  | /   \ |  |  |/ ___/      /  _]      |  /  _]/ ___/"));
            Console.WriteLine(CenterText(@"|  |  ||     ||  |  (   \_      /  [_|      | /  [_(   \_ "));
            Console.WriteLine(CenterText(@"|  |  ||  O  ||  |  |\__  |    |    _]_|  |_||    _]\__  |"));
            Console.WriteLine(CenterText(@"|  :  ||     ||  :  |/  \ |    |   [_  |  |  |   [_ /  \ |"));
            Console.WriteLine(CenterText(@" \   / |     ||     |\    |    |     | |  |  |     |\    |"));
            Console.WriteLine(CenterText(@"  \_/   \___/  \__,_| \___|    |_____| |__|  |_____| \___|"));
            Console.WriteLine(CenterText(@"            ___ ___   ___   ____  ______                  "));
            Console.WriteLine(CenterText(@"           |   |   | /   \ |    \|      |                 "));
            Console.WriteLine(CenterText(@"           | _   _ ||     ||  D  )      |                 "));
            Console.WriteLine(CenterText(@"           |  \_/  ||  O  ||    /|_|  |_|                 "));
            Console.WriteLine(CenterText(@"           |   |   ||     ||    \  |  |                   "));
            Console.WriteLine(CenterText(@"           |   |   ||     ||  .  \ |  |                   "));
            Console.WriteLine(CenterText(@"           |___|___| \___/ |__|\_| |__|                   "));

            Console.SetCursorPosition(0, Console.CursorTop + 1);

            Console.WriteLine(CenterText("Appuyez sur A pour recommencer ou X pour quitter."));

            switch (Console.ReadKey(true).KeyChar)
            {
                case 'a':
                case 'A':
                    Program.Main(null);
                    goto default;
                default:
                    Environment.Exit(0);
                    break;
            }

        }

        // Helper methods

        public static string CenterText(string content, string decoration = "")
        {
            int windowWidth = WINDOW_WIDTH - ((2 * decoration.Length));
            return string.Format("{0}" + "{1," + ((windowWidth / 2) + (content.Length / 2)) + "}" +
                                 "{0," + (windowWidth - (windowWidth / 2) - (content.Length / 2) + decoration.Length) + "}",
                                 decoration, content);
        }

        public static void ClearNumberOfLines(int count = 1)
        {
            int currentTopPos = Console.CursorTop;

            for (int i = 0; i < count; i++)
            {
                Console.SetCursorPosition(0, currentTopPos + i);
                Console.Write(new string(' ', WINDOW_WIDTH));            
            }
        }


    }
}
