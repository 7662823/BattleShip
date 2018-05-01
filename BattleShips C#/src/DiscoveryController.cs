using SwinGameSDK;
using System.Collections.Generic;
using System;
using static BattleShip.UtilityFunctions;
using static BattleShip.GameController;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
/// 
namespace BattleShip
{
    class DiscoveryController
    {
       
        /// <summary>
        /// Handles input during the discovery phase of the game.
        /// </summary>
        /// <remarks>
        /// Escape opens the game menu. Clicking the mouse will
        /// attack a location.
        /// </remarks>
        public void HandleDiscoveryInput(GameController con)
        {
            if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                con.AddNewState(GameState.ViewingGameMenu);
            }

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                DoAttack(con);
            }
        }

        /// <summary>
        /// Attack the location that the mouse if over.
        /// </summary>
        private void DoAttack(GameController con)
        {
            Point2D mouse;

            mouse = SwinGame.MousePosition();

            //Calculate the row/col clicked
            int row;
            int col;
            row = Convert.ToInt32(Math.Floor((mouse.Y - FIELD_TOP) / (CELL_HEIGHT + CELL_GAP)));
            col = Convert.ToInt32(Math.Floor((mouse.X - FIELD_LEFT) / (CELL_WIDTH + CELL_GAP)));

            if (row >= 0 & row < con.HumanPlayer.EnemyGrid.Height)
            {
                if (col >= 0 & col < con.HumanPlayer.EnemyGrid.Width)
                {
                    con.Attack(row, col);
                }
            }
        }

        /// <summary>
        /// Draws the game during the attack phase.
        /// </summary>s

        public void DrawCustomAdditions(GameController con)
        {
            if (con._playerTurn == true)
            {
                SwinGame.DrawBitmap(con._resources.GameImage("PlayerTurn"), 0, 0);
            }
            else
            {
                SwinGame.DrawBitmap(con._resources.GameImage("AITurn"), 0, 0);
            }
        }

        public void DrawDiscovery(GameController con)
        {
            const int SCORES_LEFT = 172;
            const int SHOTS_TOP = 157;
            const int HITS_TOP = 206;
            const int SPLASH_TOP = 256;
            const int SCORE_TOP = 306;


            if ((SwinGame.KeyDown(KeyCode.vk_LSHIFT) | SwinGame.KeyDown(KeyCode.vk_RSHIFT)) & SwinGame.KeyDown(KeyCode.vk_c))
            {
                con._utility.DrawField(con.HumanPlayer.EnemyGrid, con.ComputerPlayer, true, con);
            }
            else
            {
                con._utility.DrawField(con.HumanPlayer.EnemyGrid, con.ComputerPlayer, false, con);
            }

            con._utility.DrawSmallField(con.HumanPlayer.PlayerGrid, con.HumanPlayer, con);
            con._utility.DrawMessage(con);

            SwinGame.DrawText(con.HumanPlayer.Shots.ToString(), Color.White, con._resources.GameFont("Menu"), SCORES_LEFT, SHOTS_TOP);
            SwinGame.DrawText(con.HumanPlayer.Hits.ToString(), Color.White, con._resources.GameFont("Menu"), SCORES_LEFT, HITS_TOP);
            SwinGame.DrawText(con.HumanPlayer.Missed.ToString(), Color.White, con._resources.GameFont("Menu"), SCORES_LEFT, SPLASH_TOP);

            //Draw Score
            SwinGame.DrawText(con.HumanPlayer.Score.ToString(), Color.White, con._resources.GameFont("Menu"), SCORES_LEFT, SCORE_TOP);
            //Draw Turn
            DrawCustomAdditions(con);
        }

    }
}