using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame; // requires mcs version 4+,
using BattleShip;
using static BattleShip.GameResources;
using static BattleShip.GameController;

//using Resources;
// using SwinGameSDK.SwinGame; // requires mcs version 4+, 

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);
            //Load Resources
            LoadResources();
            //LoadResources();

            SwinGame.PlayMusic(GameMusic("Background"));

            //Game Loop
            do
            {
                HandleUserInput();
                DrawScreen();
            } while (!(SwinGame.WindowCloseRequested() == true | CurrentState == GameState.Quitting));

            SwinGame.StopMusic();

            //Free Resources and Close Audio, to end the program.
            GameResources.FreeResources();

            ////Open the game window
            //OpenGraphicsWindow("GameMain", 800, 600);
            //ShowSwinGameSplashScreen();
            
            ////Run the game loop
            //while(false == WindowCloseRequested())
            //{
            //    //Fetch the next batch of UI interaction
            //    ProcessEvents();
                
            //    //Clear the screen and draw the framerate
            //    ClearScreen(Color.White);
            //    DrawFramerate(0,0);
                
            //    //Draw onto the screen
            //    RefreshScreen(60);
            //}
        }
    }
}