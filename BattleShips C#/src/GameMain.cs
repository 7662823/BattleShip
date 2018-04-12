using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame;
//using static SwinGameSDK.SwinGame; // requires mcs version 4+,
using BattleShip;


//using Resources;
// using SwinGameSDK.SwinGame; // requires mcs version 4+, 

namespace MyGame
{
    public class GameMain
    {
        public static void Main()
        {
            SwinGame.OpenGraphicsWindow("BattleShips", 800, 600);
           // ShowSwinGameSplashScreen(); //This wasn't in the vB
            //Load Resources

            GameResources res = new GameResources();
            GameController con = new GameController(res);
            con._resources.LoadResources();
            //LoadResources();

            SwinGame.PlayMusic(con._resources.GameMusic("Background"));

            /* //Game Loop
             do
             {
                 con.HandleUserInput();
                 con.DrawScreen();
             } while (!(SwinGame.WindowCloseRequested() == true | con.CurrentState == GameState.Quitting));
             */

            ////Run the game loop
            while (false == SwinGame.WindowCloseRequested() && con.CurrentState != GameState.Quitting)
            {
                //Fetch the next batch of UI interaction
                con.HandleUserInput();

                //Clear the screen and draw the framerate
                ClearScreen(Color.White);
                DrawFramerate(0, 0);

                //Draw onto the screen
                con.DrawScreen();
            }

            StopMusic();

            //Free Resources and Close Audio, to end the program.
            con._resources.FreeResources();
        }
    }
}
