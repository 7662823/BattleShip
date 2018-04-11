using System.Collections.Generic;
using System;
using SwinGameSDK;
using static SwinGameSDK.SwinGame;
using Battleship;

namespace Battleship 
{

    class GameLogic
    {

	    public static void Main()
	    {
            GameController con = new GameController();

		    //Opens a new Graphics Window
		    SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);
            //ShowSwinGameSplashScreen(); - this wasn't in original code

            //Load Resources
            con.res.LoadResources();
            SwinGame.PlayMusic(con.res.GameMusic("Background"));

    		//Game Loop - VB.NET version
	    	/*do {
		    	con.HandleUserInput();
			    con.DrawScreen();
	    	} while (!(SwinGame.WindowCloseRequested() == true | con.CurrentState == GameState.Quitting));
            */

            //Run the game loop
            while (false == WindowCloseRequested() && con.CurrentState == GameState.Quitting)
            {
                //Fetch the next batch of UI interaction
                ProcessEvents();
                con.HandleUserInput();

                //Clear the screen and draw the framerate
                ClearScreen(Color.White);
                DrawFramerate(0, 0);

                //Draw onto the screen
                con.DrawScreen();
                RefreshScreen(60);
            }

            SwinGame.StopMusic();

    		//Free Resources and Close Audio, to end the program.
	    	con.res.FreeResources();
	    }
    }
}