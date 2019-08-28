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
      
        public static void AddToGameLog(string message)
        {
            gameLog.Add(message);
        }

        public static string ShowGameLog()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string item in gameLog)
            {
                sb.AppendLine(item);
            }

            return sb.ToString();
        }

        public static void ClearCombatLog(bool forceClear = false)
        {
            if (gameLog.Count > 6 || forceClear )
            {
                gameLog.Clear();
            }           
        }

        public static string CharacterStatus(Character character)
        {
            StringBuilder sb = new StringBuilder();

            string border = new string('═',WINDOW_WIDTH - 2);
            string emptyLine = new string(' ', WINDOW_WIDTH - 2);

            sb.AppendLine($"╔{border}╗");
            sb.AppendLine($"║{emptyLine}║");
            sb.AppendLine(CenterText($"CLASSE : {character.CharacterClass.ToUpper()}", "║"));
            sb.AppendLine(CenterText($"PV [ {character.CurrentHP} / {character.MaxHP} ] PM [ {character.CurrentMP} / {character.MaxMP} ] | ATT [ {character.Attack} ] DEF [ {character.Defense} ]", "║"));
            sb.AppendLine(CenterText($"ARME [ {character.Weapon} ] | ARMURE [ {character.Armor} ] | POTIONS [ {character.NumOfPotions} ]", "║"));
            sb.AppendLine($"║{emptyLine}║");
            sb.AppendLine($"╚{border}╝");

            return sb.ToString();
        }

        public static string MonsterStatus(Monster monster)
        {
            StringBuilder sb = new StringBuilder();

            string border = new string('═', WINDOW_WIDTH - 2);
            string emptyLine = new string(' ', WINDOW_WIDTH - 2);

            sb.AppendLine($"╔{border}╗");
            sb.AppendLine($"║{emptyLine}║");
            sb.AppendLine(CenterText($"NOM : {monster.Name.ToUpper()}", "║"));
            sb.AppendLine(CenterText($"PV [ {monster.CurrentHP} / {monster.MaxHP} ] | ATT [ {monster.Attack} ] DEF [ {monster.Defense} ]", "║"));      
            sb.AppendLine($"║{emptyLine}║");
            sb.AppendLine($"╚{border}╝");

            return sb.ToString();
        }

        public static string TitleScreen()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine(CenterText(@"    __  ___  ____   _____ ___  _       ___      ____  ____   ____ "));
            sb.AppendLine(CenterText(@"   /  ]/   \|    \ / ___//   \| |     /  _]    |    \|    \ /    |"));
            sb.AppendLine(CenterText(@"  /  /|     |  _  (   \_|     | |    /  [_     |  D  )  o  )   __|"));
            sb.AppendLine(CenterText(@" /  / |  O  |  |  |\__  |  O  | |___|    _]    |    /|   _/|  |  |"));
            sb.AppendLine(CenterText(@"/   \_|     |  |  |/  \ |     |     |   [_     |    \|  |  |  |_ |"));
            sb.AppendLine(CenterText(@"\     |     |  |  |\    |     |     |     |    |  .  \  |  |     |"));
            sb.AppendLine(CenterText(@" \____|\___/|__|__| \___|\___/|_____|_____|    |__|\_|__|  |___,_|"));
            sb.AppendLine();
            sb.AppendLine(CenterText(@" |\                     /)  "));
            sb.AppendLine(CenterText(@" /\_\\__               (_// "));
            sb.AppendLine(CenterText(@"|   `>\-`     _._       //`)"));
            sb.AppendLine(CenterText(@" \ /` \\  _.-`:::`-._  //   "));
            sb.AppendLine(CenterText(@"  `    \|`    :::    `|/    "));
            sb.AppendLine(CenterText(@"        |     :::     |     "));
            sb.AppendLine(CenterText(@"        |.....:::.....|     "));
            sb.AppendLine(CenterText(@"        |:::::::::::::|     "));
            sb.AppendLine(CenterText(@"        |     :::     |     "));
            sb.AppendLine(CenterText(@"        \     :::     /     "));
            sb.AppendLine(CenterText(@"         \    :::    /      "));
            sb.AppendLine(CenterText(@"          `-. ::: .-'       "));
            sb.AppendLine(CenterText(@"           //`:::`\\        "));
            sb.AppendLine(CenterText(@"          //   '   \\       "));
            sb.AppendLine(CenterText(@"         |/         \\      "));
            sb.AppendLine();
            sb.AppendLine(CenterText("- Appuyez sur une touche pour commencer -"));
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(string.Format("{0, -40} {1, 43}"," © Sammy-James Simms et Pier-Olivier St-Pierre-Chouinard", 2019));

            return sb.ToString();
        }

        
        public static void UpdateCombatUI(Character character, Monster monster)
        {        
            Console.Clear();

            for (int i = 0; i < MonsterStatus(monster).Length; i++)
            {
                if (char.IsDigit(MonsterStatus(monster)[i]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }

                Console.Write(MonsterStatus(monster)[i]);
                Console.ResetColor();
            }


            Console.SetCursorPosition(0, 8);
            Console.Write(ShowGameLog());

            Console.SetCursorPosition(0, WINDOW_HEIGHT - 7);
            for (int k = 0; k < CharacterStatus(character).Length; k++)
            {
                if (char.IsDigit(CharacterStatus(character)[k]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write(CharacterStatus(character)[k]);
                Console.ResetColor();
            }

            Console.SetCursorPosition(0,0);
        }

      
        public static string CenterText(string content, string decoration = "")
        {
            int windowWidth = WINDOW_WIDTH - (2 * decoration.Length);
            return string.Format("{0}" + "{1," + ((windowWidth / 2) + (content.Length / 2)) + "}" +
                                 "{0," + (windowWidth - (windowWidth/2) - (content.Length / 2) + decoration.Length) + "}",
                                 decoration, content);
        }
      
    }
}
