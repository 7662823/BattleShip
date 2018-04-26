using SwinGameSDK;


/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>
namespace BattleShip
{
    class MenuController
    {

        /// <summary>
        /// The menu structure for the game.
        /// </summary>
        /// <remarks>
        /// These are the text captions for the menu items.
        /// </remarks>
        private readonly string[][] _menuStructure = {
        new string[] {
            "PLAY",
            "SETUP",
            "SCORES",
            "QUIT"
        },
        new string[] {
            "RETURN",
            "SURRENDER",
            "QUIT"
        },
        new string[] {
            "EASY",
            "MEDIUM",
            "HARD"
        }

    };
        private const int MENU_TOP = 575;
        private const int MENU_LEFT = 30;
        private const int MENU_GAP = 0;
        private const int BUTTON_WIDTH = 75;
        private const int BUTTON_HEIGHT = 15;
        private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

        private const int TEXT_OFFSET = 0;
        private const int MAIN_MENU = 0;
        private const int GAME_MENU = 1;

        private const int SETUP_MENU = 2;
        private const int MAIN_MENU_PLAY_BUTTON = 0;
        private const int MAIN_MENU_SETUP_BUTTON = 1;
        private const int MAIN_MENU_TOP_SCORES_BUTTON = 2;

        private const int MAIN_MENU_QUIT_BUTTON = 3;
        private const int SETUP_MENU_EASY_BUTTON = 0;
        private const int SETUP_MENU_MEDIUM_BUTTON = 1;
        private const int SETUP_MENU_HARD_BUTTON = 2;

        private const int SETUP_MENU_EXIT_BUTTON = 3;
        private const int GAME_MENU_RETURN_BUTTON = 0;
        private const int GAME_MENU_SURRENDER_BUTTON = 1;

        private const int GAME_MENU_QUIT_BUTTON = 2;
        private readonly Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);

        private readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);
        /// <summary>
        /// Handles the processing of user input when the main menu is showing
        /// </summary>
        public void HandleMainMenuInput(GameController con)
        {
            HandleMenuInput(MAIN_MENU, 0, 0, con);
        }

        /// <summary>
        /// Handles the processing of user input when the main menu is showing
        /// </summary>
        public void HandleSetupMenuInput(GameController con)
        {
            bool handled;
            handled = HandleMenuInput(SETUP_MENU, 1, 1, con);

            if (!handled)
            {
                HandleMenuInput(MAIN_MENU, 0, 0, con);
            }
        }

        /// <summary>
        /// Handle input in the game menu.
        /// </summary>
        /// <remarks>
        /// Player can return to the game, surrender, or quit entirely
        /// </remarks>
        public void HandleGameMenuInput(GameController con)
        {
            HandleMenuInput(GAME_MENU, 0, 0, con);
        }

        /// <summary>
        /// Handles input for the specified menu.
        /// </summary>
        /// <param name="menu">the identifier of the menu being processed</param>
        /// <param name="level">the vertical level of the menu</param>
        /// <param name="xOffset">the xoffset of the menu</param>
        /// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
        private bool HandleMenuInput(int menu, int level, int xOffset, GameController con)
        {
            if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                con.EndCurrentState();
                return true;
            }

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                int i;
                for (i = 0; i <= _menuStructure[menu].Length - 1; i++)
                {
                    //IsMouseOver the i'th button of the menu
                    if (IsMouseOverMenu(i, level, xOffset, con))
                    {
                        PerformMenuAction(menu, i, con);
                        return true;
                    }
                }

                if (level > 0)
                {
                    //none clicked - so end this sub menu
                    con.EndCurrentState();
                }
            }

            return false;
        }

        /// <summary>
        /// Draws the main menu to the screen.
        /// </summary>
        public void DrawMainMenu(GameController con)
        {
            //Clears the Screen to Black
            //SwinGame.DrawText("Main Menu", Color.White, GameFont("ArialLarge"), 50, 50)

            DrawButtons(MAIN_MENU, con);
        }

        /// <summary>
        /// Draws the Game menu to the screen
        /// </summary>
        public void DrawGameMenu(GameController con)
        {
            //Clears the Screen to Black
            //SwinGame.DrawText("Paused", Color.White, GameFont("ArialLarge"), 50, 50)

            DrawButtons(GAME_MENU, con);
        }

        /// <summary>
        /// Draws the settings menu to the screen.
        /// </summary>
        /// <remarks>
        /// Also shows the main menu
        /// </remarks>
        public void DrawSettings(GameController con)
        {
            //Clears the Screen to Black
            //SwinGame.DrawText("Settings", Color.White, GameFont("ArialLarge"), 50, 50)

            DrawButtons(MAIN_MENU, con);
            DrawButtons(SETUP_MENU, 1, 1, con);
        }

        /// <summary>
        /// Draw the buttons associated with a top level menu.
        /// </summary>
        /// <param name="menu">the index of the menu to draw</param>
        private void DrawButtons(int menu, GameController con)
        {
            DrawButtons(menu, 0, 0, con);
        }

        /// <summary>
        /// Draws the menu at the indicated level.
        /// </summary>
        /// <param name="menu">the menu to draw</param>
        /// <param name="level">the level (height) of the menu</param>
        /// <param name="xOffset">the offset of the menu</param>
        /// <remarks>
        /// The menu text comes from the _menuStructure field. The level indicates the height
        /// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
        /// to allow the submenus to be positioned correctly.
        /// </remarks>
        private void DrawButtons(int menu, int level, int xOffset, GameController con)
        {
            int btnTop;
            Rectangle toDraw = new Rectangle();

            btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
            int i;
            for (i = 0; i <= _menuStructure[menu].Length - 1; i++)
            {
                int btnLeft;

                btnLeft = MENU_LEFT + BUTTON_SEP * (i + xOffset);
                //SwinGame.FillRectangle(Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT)
                toDraw.X = btnLeft + TEXT_OFFSET;
                toDraw.Y = btnTop + TEXT_OFFSET;
                toDraw.Width = BUTTON_WIDTH;
                toDraw.Height = BUTTON_HEIGHT;
                SwinGame.DrawTextLines(_menuStructure[menu][i], MENU_COLOR, Color.Black, con._resources.GameFont("Menu"), FontAlignment.AlignCenter, toDraw); //FIX

                if (SwinGame.MouseDown(MouseButton.LeftButton) & IsMouseOverMenu(i, level, xOffset, con))
                {
                    SwinGame.DrawRectangle(HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
                }
            }
        }

        /// <summary>
        /// Determined if the mouse is over one of the button in the main menu.
        /// </summary>
        /// <param name="button">the index of the button to check</param>
        /// <returns>true if the mouse is over that button</returns>
        private bool IsMouseOverButton(int button, GameController con)
        {
            return IsMouseOverMenu(button, 0, 0, con);
        }

        /// <summary>
        /// Checks if the mouse is over one of the buttons in a menu.
        /// </summary>
        /// <param name="button">the index of the button to check</param>
        /// <param name="level">the level of the menu</param>
        /// <param name="xOffset">the xOffset of the menu</param>
        /// <returns>true if the mouse is over the button</returns>
        private bool IsMouseOverMenu(int button, int level, int xOffset, GameController con)
        {
            int btnTop = MENU_TOP - (MENU_GAP + BUTTON_HEIGHT) * level;
            int btnLeft = MENU_LEFT + BUTTON_SEP * (button + xOffset);

            return con._utility.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
        }

        /// <summary>
        /// A button has been clicked, perform the associated action.
        /// </summary>
        /// <param name="menu">the menu that has been clicked</param>
        /// <param name="button">the index of the button that was clicked</param>
        private void PerformMenuAction(int menu, int button, GameController con)
        {
            switch (menu)
            {
                case MAIN_MENU:
                    PerformMainMenuAction(button, con);
                    break;
                case SETUP_MENU:
                    PerformSetupMenuAction(button, con);
                    break;
                case GAME_MENU:
                    PerformGameMenuAction(button, con);
                    break;
            }
        }

        /// <summary>
        /// The main menu was clicked, perform the button's action.
        /// </summary>
        /// <param name="button">the button pressed</param>
        private void PerformMainMenuAction(int button, GameController con)
        {
            switch (button)
            {
                case MAIN_MENU_PLAY_BUTTON:
                    con.StartGame();
                    break;
                case MAIN_MENU_SETUP_BUTTON:
                    con.AddNewState(GameState.AlteringSettings);
                    break;
                case MAIN_MENU_TOP_SCORES_BUTTON:
                    con.AddNewState(GameState.ViewingHighScores);
                    break;
                case MAIN_MENU_QUIT_BUTTON:
                    con.EndCurrentState();
                    break;
            }
        }

        /// <summary>
        /// The setup menu was clicked, perform the button's action.
        /// </summary>
        /// <param name="button">the button pressed</param>
        private void PerformSetupMenuAction(int button, GameController con)
        {
            switch (button)
            {
                case SETUP_MENU_EASY_BUTTON:
                    con.SetDifficulty(AIOption.Easy);
                    break;
                case SETUP_MENU_MEDIUM_BUTTON:
                    con.SetDifficulty(AIOption.Medium);
                    break;
                case SETUP_MENU_HARD_BUTTON:
                    con.SetDifficulty(AIOption.Hard);
                    break;
            }
            //Always end state - handles exit button as well
            con.EndCurrentState();
        }

        /// <summary>
        /// The game menu was clicked, perform the button's action.
        /// </summary>
        /// <param name="button">the button pressed</param>
        private void PerformGameMenuAction(int button, GameController con)
        {
            switch (button)
            {
                case GAME_MENU_RETURN_BUTTON:
                    con.EndCurrentState();
                    break;
                case GAME_MENU_SURRENDER_BUTTON:
                    con.EndCurrentState();
                    //end game menu
                    //end game
                    con.EndCurrentState();
                    break;
                case GAME_MENU_QUIT_BUTTON:
                    con.AddNewState(GameState.Quitting);
                    break;
            }
        }
    }
}