using System;


namespace TP_Final_DD
{

    class Program
    {

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowWidth = GameManager.WINDOW_WIDTH;
            Console.BufferWidth = GameManager.WINDOW_WIDTH + 1;            

            GameManager.TitleScreen();

            Character player = GameManager.SetupCharacter();

            PlayMap game = new PlayMap();          
            game.PlayGame(player);
            

            Console.ReadKey(true);
        }

      
    }
}
