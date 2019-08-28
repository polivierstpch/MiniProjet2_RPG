using System;


namespace TP_Final_DD
{

    class Program
    {

        static void Main(string[] args)
        {
            
            Console.WindowWidth = GameManager.WINDOW_WIDTH;
            Console.BufferWidth = GameManager.WINDOW_WIDTH + 1;
            Console.CursorVisible = false;


            Console.Write(GameManager.TitleScreen());
            Console.ReadKey();
            Console.Clear();

            ////Test
            Console.WriteLine("guerrier ou mage");
            Character joueur = new Character(Console.ReadLine());
            PlayMap partie = new PlayMap();
           
            partie.PlayEvent(joueur);
            

            Console.ReadKey(true);
        }

      
    }
}
