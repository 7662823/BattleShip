//using SwinGameSDK;
//using System.Collections.Generic;
//using System;
////using Resources;

//class GameLogic
//{
//	public void Main()
//	{
//		//Opens a new Graphics Window
//		SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);

//        //Load Resources

//        GameResources.LoadResources();
//		LoadResources();

//		SwinGame.PlayMusic(GameMusic("Background"));

//		//Game Loop
//		do {
//			HandleUserInput();
//			DrawScreen();
//		} while (!(SwinGame.WindowCloseRequested() == true | CurrentState == GameState.Quitting));

//		SwinGame.StopMusic();

//		//Free Resources and Close Audio, to end the program.
//		FreeResources();
//	}
//}