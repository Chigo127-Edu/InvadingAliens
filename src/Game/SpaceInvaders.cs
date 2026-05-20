using RetroGameFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SpaceInvaders
{
    enum Corners
    {
        Top_Left,
        Top_Right,
        Bottom_Left,
        Bottom_Right
    };

    internal class SpaceInvaders : GameLogic
    {
        public SpaceInvaders(GameConfig GameConfig) : base(GameConfig) { }

        // NOTICE: All the variables and objects are static, because this way they can be accessed elsewhere without creating objects.

        // GameConfig is a variable already accessible in methods to retrieve the game configs
        // bool IsPaused() is a function already accessible in methods to check if the game is paused
        // void SetPaused(bool) is a function already accessible in methods to set the game in pause and to resume it

        // GAME DATA
        // Declare here game-specific data that should survive the frame

        // Game Difficulty
        public static float Difficulty = 1f;

        // It's different from Framecount because it stops when paused
        public static int FramesPlayed = 0;

        // Game score
        public static int Score = 0;

        // Player's lives
        public static int Lives = 5;

        // Boolean if the game is ongoing
        public static bool OnGoing = true;

        // Boolean to order pause
        public static bool ToBePaused = false;

        // Lists of objects for projectiles and enemies
        public static List<Element> Projectiles = new List<Element>();
        public static List<Element> Enemies = new List<Element>();

        // Initialization call, used to customize GameConfig data (used to customize the engine behaviour)
        protected override void OnInitGameConfig(GameConfig GameConfig)
        {
            GameConfig.Title = "SpaceInvaders";
            GameConfig.PixelsMatrixWidth = 160;
            GameConfig.PixelsMatrixHeight = 120;
            GameConfig.PixelSize = 5;
            GameConfig.FrameRate = 24;
            GameConfig.ForegroundColor = System.Drawing.Color.White;
            GameConfig.BackgroundColor = System.Drawing.Color.Black;
            GameConfig.randomGeneratorSeed = System.DateTime.Now.Millisecond;

            GameConfig.AdditionalColors = new System.Drawing.Color[] {
                System.Drawing.Color.Red,
                System.Drawing.Color.Orange,
                System.Drawing.Color.Yellow,
                System.Drawing.Color.Green,
                System.Drawing.Color.Cyan,
                System.Drawing.Color.Blue,
                System.Drawing.Color.Violet,
           };
        }
        private void StartNewGame()
        {
            OnGoing = true;
        }
        
        // Called at the start of the first frame of the game.
        // It's main purpose it's to setup the scene.
        private void FirstFrameLoop()
        {
            var menu = new SimpleMenu(new[] { "Nuova Partita", "Esci" });
            int choice = menu.MostraMenu();
            if (choice == 0) StartNewGame();
            else if (choice == 1) Environment.Exit(0);
        }

        // Called once per frame, BEFORE the OnLoopGame event.
        protected override void OnClear(int[,] pixels)
        {
            GameUtils.ClearScreen(pixels);
        }

        // Called once per frame.
        // Here the actual logic happens.
        protected override void OnLoopGame(float deltaTime)
        {
            if (!IsPaused())
            {
                FramesPlayed += 1;
            }
            // Game difficulty is the square root of the framecount
            Difficulty = (float)Math.Sqrt(FramesPlayed);

            // Logic when the game is ongoing
            if (OnGoing)
            {
                // If this is the first frame, the spaceship position will be defined in FirstFrameLoop()
                if (FrameCount == 0)
                {
                    FirstFrameLoop();
                }
                // For any other frame
                else
                {
                    // Enemies are generated
                    Generation.Enemies();

                    // Enemies and projectiles that go out the player's sight (The window) will be deleted
                    Generation.Despawn(Enemies);
                    Generation.Despawn(Projectiles);

                    // Checking for collisions, between projectiles and enemies. If yes, both will be deleted and the score will increase
                    Collisions.EnemyShot(Enemies, Projectiles);

                    // Checking for collisions, between enemies and the spaceship. If yes, the enemy will be deleted and the spaceship will lose one life
                    Collisions.SpaceshipAttacked(Enemies);

                    // If the player runs out of lives, the game will end
                    if (Lives < 1) OnGoing = false;

                    // Enemies will move automatically, like existing projectiles
                    Movement.ElementsType(Enemies);
                    Movement.ElementsType(Projectiles);

                    // The spaceship cannot go away from the main field (the window)
                    Movement.Spaceship_Border(1);
                }
            }
            else
            {
                OnEndGame();
            }

            if (ToBePaused == true)
            {
                ToBePaused = false;
                SetPaused(true);
            }
        }

        // Called once per frame, AFTER the OnLoopGame event.
        protected override void OnDraw(int[,] pixels)
        {
            if (OnGoing)
            {
                // Lives and score and will be printed at the top of the window 
                // This time also Difficulty will be printed, but this information will be hidden in final releases
                Writing.Print(pixels, $"Lives:{Lives}{Environment.NewLine}Time:{(int)FrameCount / GameConfig.FrameRate}", (int) Corners.Top_Left);
                Writing.Print(pixels, $"Score:{Score}", (int)Corners.Top_Right);
                if (IsPaused()) Writing.Print(pixels, "Paused", (int)Corners.Bottom_Left);

                // Every projectile and every enemy will be drawn
                Draw.AllElements(pixels, Enemies, Enemy.Image, Enemy.Style);
                Draw.AllElements(pixels, Projectiles, Projectile.Image, Enemy.Style);

                // Draw the spaceship differently, depending on the state
                Spaceship.Style.Draw(pixels, Spaceship.Position);
            }
            else
            {
                SetPaused(true);
                Writing.Print(pixels, $"Game over!{Environment.NewLine}Score: {Score}" +
                                      $"{Environment.NewLine}{Environment.NewLine}Time Elapsed: {(int)FrameCount/GameConfig.FrameRate}" +
                                      $"{Environment.NewLine}{Environment.NewLine}Press ESC to quit", (int)Corners.Top_Left);
                Writing.Print(pixels, "Thank you!", (int)Corners.Bottom_Right);
            }
        }

        // Called at the end of the last frame of the game.
        // Its main purpose it's to dispose resources, as the game will end immediately after this call.
        protected override void OnEndGame()
        {

        }

        // Called the first frame a key is pressed, and not called anymore unless the key is released
        protected override void OnKeyDown(Keys KeyCode)
        {
            if (!IsPaused())
            {
                // Spaceship movement depending on the button pressed
                Movement.Player(KeyCode);

                // Shooting
                if (KeyCode == Keys.Space)
                {
                    Generation.Projectiles();
                }
            }

        }

        // Called if a key has been released (even in the same frame it has been released)
        protected override void OnKeyUp(Keys KeyCode)
        {

        }

        // Called during the frame a key is pressed and in all the following frames until it's released (excluding the frame it's released)
        protected override void OnKeyPress(Keys KeyCode)
        {
            if (!IsPaused())
            {
                // Spaceship movement
                Movement.Player(KeyCode);
            }

            // Pause the game
            if (KeyCode == Keys.P)
            {
                if (IsPaused()) SetPaused(false);
                else ToBePaused = true;
            }

            if (KeyCode == Keys.Escape)
            {
                if (OnGoing) OnGoing = false;
                else OnEndGame();
            }
        }
    }

    // Public class for drawing
    public class Draw
    {
        // All the elements of the same type (Like projectiles and enemies) can be drawn in one cycle
        public static void AllElements(int[,] pixels, List<Element> This, GameImage Image, PaintStyle Style)
        {
            for (int i = 0; i < This.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, Image, new Point((int)(This[i].Position[0]), (int)(This[i].Position[1])), Style);
            }
        }

        // One element
        public static void Element(int[,] pixels, int[] This_Pos, GameImage This_Image)
        {
            GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[0]), (int)(This_Pos[1])));
        }
    }

    // Public class for mathematical utilities
    public class Utilities
    {
        // This funcion just removes the minus from an integer (absolute value)
        private static int AlwaysPositive(int Number)
        {
            if (Number < 0) return -Number;
            else return Number;
        }

        // Distance between 2 points
        public static int DeltaCoord(int First_Point, int Second_Point)
        {
            return AlwaysPositive(First_Point - Second_Point);
        }
    }

    // Public class for Dynamic events
    public class Dynamicity
    {
        // Check for position: If X1 + X2 < RadiusX1 + RadiusX2 and the same with Ys then the 2 elements overlap 
        public static bool Verify_Collision(int[] First_Pos, int[] First_Radius, int[] Second_Pos, int[] Second_Radius)
        {
            if (Utilities.DeltaCoord(First_Pos[0], Second_Pos[0]) - (First_Radius[0] + Second_Radius[0]) < 0
                && Utilities.DeltaCoord(First_Pos[1], Second_Pos[1]) - (First_Radius[1] + Second_Radius[1]) < 0) return true;
            else return false;
        }

        // Check for all elements of the same kind. If one element is out of the pixel matrix, it gets deleted to save RAM
        public static void Despawn(List<Element> ElementType)
        {
            for (global::System.Int32 i = 0; i < ElementType.Count; i++)
            {
                if (ElementType[i].Position[0] > GameConfig.PixelsMatrixWidth || ElementType[i].Position[0] < 0 || ElementType[i].Position[1] > GameConfig.PixelsMatrixWidth || ElementType[i].Position[1] < 0)
                {
                    ElementType.Remove(ElementType[i]);
                }
            }
        }
    }

    // Public class for collisions management, inherited from Dynamicity
    public class Collisions : Dynamicity
    {
        /* This function is made for enemies (First) and projectiles (Second).
         * This will check for every combination between those 2 kinds, and when it finds a coincidence,
         * it remove the 2 elements from the lists and ends the cycle.
         * Then, the cicle gets interrupted, in order to prevent Exceptions(index out of array).
         * This check limits to act once per frame. However, Enemies and projectiles have more frames to
         * finally delete themselves,
         */
        public static void EnemyShot(List<Element> Enemies, List<Element> Projectiles)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                for (int j = 0; j < Projectiles.Count; j++)
                {
                    if (Verify_Collision(Enemies[i].Position, Enemy.Range, Projectiles[j].Position, Projectile.Range))
                    {
                        // Removal of overlapping elements
                        Enemies.Remove(Enemies[i]);
                        Projectiles.Remove(Projectiles[j]);

                        // Setting indexes to values out of the for cycle range
                        i = Enemies.Count;
                        j = Projectiles.Count;

                        // The score is increased by the difficulty
                        SpaceInvaders.Score += (int)(SpaceInvaders.Difficulty);
                    }
                }
            }
        }
        /* This function is made forthe Spaceship (First) and enemies (Second).
         * This is like the previus function, with the main difference being the number of cycles: It's only one,
         * as the spaceship remains the same.
         * The spaceship will be marked as attacked, and the lives count will decrease by 1
         */
        public static void SpaceshipAttacked(List<Element> Enemies)
        {
            for (int j = 0; j < Enemies.Count; j++)
            {
                if (Verify_Collision(Spaceship.Position, Spaceship.Range, Enemies[j].Position, Enemy.Range))
                {
                    SpaceInvaders.Lives--;
                    Spaceship.isAttacked = true;
                    Enemies.Remove(Enemies[j]);
                }
            }
        }
    }

    // Public class for Generation of Enemies and Projectiles
    public class Generation : Dynamicity
    {
        // Enemies generation, depending on the difficulty
        public static void Enemies()
        {
            if ((int)((double)SpaceInvaders.FrameCount % (100 / Math.Sqrt(SpaceInvaders.Difficulty))) == 0)
            {
                for (int i = 0; i < (int)Math.Sqrt(SpaceInvaders.Difficulty); i++)
                {
                    SpaceInvaders.Enemies.Add(new Element
                    {
                        Position = new int[] { (int)(GameConfig.PixelsMatrixWidth * (float)SpaceInvaders.RandomGenerator.Next() / 2147483647), (int)(5) },
                        Speed = new int[] { 0, (int)Math.Sqrt((Math.Sqrt(SpaceInvaders.Difficulty))) }
                    }
                    );
                }
            }
        }

        // Projectiles generation, marking the spaceship as shooting
        public static void Projectiles()
        {
            Spaceship.isShooting = true;
            SpaceInvaders.Projectiles.Add(new Element
            {
                Position = new int[] { Spaceship.Position[0], Spaceship.Position[1] },
                Speed = new int[] { 0, -2 }
            }
            );
        }
    }

    // Movement class
    public class Movement
    {
        // Manual movement
        public static void Player(Keys KeyCode)
        {
            if (KeyCode == Keys.W) Spaceship.Position[1] -= Spaceship.Speed[1];
            if (KeyCode == Keys.A) Spaceship.Position[0] -= Spaceship.Speed[0];
            if (KeyCode == Keys.S) Spaceship.Position[1] += Spaceship.Speed[1];
            if (KeyCode == Keys.D) Spaceship.Position[0] += Spaceship.Speed[1];
        }

        // Automatic movement
        public static void ElementsType(List<Element> Elements)
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].Position[0] += Elements[i].Speed[0];
                Elements[i].Position[1] += Elements[i].Speed[1];
            }
        }

        // Automatic movement
        public static void Spaceship_Border(int Offset)
        {
            if (Spaceship.Position[0] < Spaceship.Range[0] + Offset) Spaceship.Position[0] = Spaceship.Range[0] + Offset;
            if (Spaceship.Position[0] > GameConfig.PixelsMatrixWidth - Spaceship.Range[0] - 1 - Offset) Spaceship.Position[0] = GameConfig.PixelsMatrixWidth - Spaceship.Range[0] - 1 - Offset;
            if (Spaceship.Position[1] < Spaceship.Range[1] + Offset) Spaceship.Position[1] = Spaceship.Range[1] + Offset;
            if (Spaceship.Position[1] > GameConfig.PixelsMatrixHeight - Spaceship.Range[1] - Offset) Spaceship.Position[1] = GameConfig.PixelsMatrixHeight - Spaceship.Range[1] - Offset;
        }
    }

    // Public class for Spaceship Style. Static because it's only one
    public class Spaceship
    {
        public static int[] Position = new int[] { GameConfig.PixelsMatrixWidth / 2, GameConfig.PixelsMatrixHeight - 10 };
        public static int[] Speed = new int[] { 2, 2 };
        public static int[] Range = new int[] { 6, 4 };
        public static bool isAttacked = false;
        public static bool isShooting = false;

        public class Style
        {
            public static GameImage Normal = GameImage.CreateFromRows(new string[] {
            "     ***     ",
            "     ***     ",
            "   *******   ",
            "*************",
            "*************",
            "*************",
            "  **     **  ",
            "  **     **  "
        }, new char[] { ' ', '*', '$', '.' }, AnchorType.Center);
            public static PaintStyle Normal_Style = PaintStyle.Default;

            public static GameImage Attacked = GameImage.CreateFromRows(new string[]
            {
            "     $$$     ",
            "     $*$     ",
            "   $$$*$$$   ",
            "$$$$*****$$$$",
            "$***********$",
            "$$$$$$$$$$$$$",
            "  $$     $$  ",
            "  $$     $$  "
            }, new char[] { ' ', '*', '$', '.' }, AnchorType.Center);
            public static PaintStyle Attacked_Style = PaintStyle.Default;


            public static GameImage Shooting = GameImage.CreateFromRows(new string[] {
            "     ...     ",
            "     ...     ",
            "   **...**   ",
            "*************",
            "*************",
            "*************",
            "  **     **  ",
            "  **     **  "
        }, new char[] { ' ', '*', '$', '.', ',' }, AnchorType.Center);
            public static PaintStyle Shooting_Style = PaintStyle.Default;

            // Drawing depending on the state
            public static void Draw(int[,] pixels, int[] Spaceship_Pos)
            {
                if (Spaceship.isAttacked)
                {
                    GameUtils.DrawImageOnScreen(pixels, Style.Attacked, new Point((int)(Spaceship.Position[0]), (int)(Spaceship.Position[1])), Attacked_Style);
                    Spaceship.isAttacked = false;
                }
                else if (Spaceship.isShooting)
                {
                    GameUtils.DrawImageOnScreen(pixels, Style.Shooting, new Point((int)(Spaceship.Position[0]), (int)(Spaceship.Position[1])), Shooting_Style);
                    Spaceship.isShooting = false;
                }
                else
                {
                    GameUtils.DrawImageOnScreen(pixels, Style.Normal, new Point((int)(Spaceship.Position[0]), (int)(Spaceship.Position[1])), Normal_Style);
                }
            }
        }
    }

    public class Element : Dynamicity
    {
        public int[] Position = new int[2];
        public int[] Speed = new int[2];
    }

    public class Projectile
    {
        public static int[] Range = new int[] { 1, 1 };
        public static GameImage Image = new GameImage(
        new int[,]
        {
            {1}
        },
        AnchorType.Center);
        public static PaintStyle Style = PaintStyle.Default;
    }

    public class Enemy
    {
        public static int[] Range = new int[] { 2, 2 };

        public static int Generation_Interval = 24;

        public static GameImage Image = new GameImage(
        new int[,]
        {
            {1, 0, 1},
            {0, 1, 0},
            {1, 0, 1},
        }, AnchorType.Center);
        public static PaintStyle Style = PaintStyle.Default;
    }

}
