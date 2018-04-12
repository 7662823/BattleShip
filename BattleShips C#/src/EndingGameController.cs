using SwinGameSDK;
using System;
using System.Collections.Generic;

/// <summary>
/// The EndingGameController is responsible for managing the interactions at the end
/// of a game.
/// </summary>

namespace BattleShip
{
    class EndingGameController
    {

        /// <summary>
        /// Draw the end of the game screen, shows the win/lose state
        /// </summary>
        public void DrawEndOfGame(GameController con)
        {

            Rectangle toDraw = new Rectangle();
            string whatShouldIPrint;

            con._utility.DrawField(con.ComputerPlayer.PlayerGrid, con.ComputerPlayer, true, con);
            con._utility.DrawSmallField(con.HumanPlayer.PlayerGrid, con.HumanPlayer, con);

            toDraw.X = 0;
            toDraw.Y = 250;
            toDraw.Width = SwinGame.ScreenWidth();
            toDraw.Height = SwinGame.ScreenHeight();

            if (con.HumanPlayer.IsDestroyed)
            {
                whatShouldIPrint = "YOU LOSE!";
            }
            else
            {
                whatShouldIPrint = "-- WINNER --";
            }

            SwinGame.DrawTextLines(whatShouldIPrint, Color.White, Color.Transparent, con._resources.GameFont("ArialLarge"), FontAlignment.AlignCenter, toDraw);


        }

        /// <summary>
        /// Handle the input during the end of the game. Any interaction
        /// will result in it reading in the highsSwinGame.
        /// </summary>
        public void HandleEndOfGameInput(GameController con)
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_RETURN) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                con._highScore.ReadHighScore(con.HumanPlayer.Score, con);
                con.EndCurrentState();
            }
        }

    }
}