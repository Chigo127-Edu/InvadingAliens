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
    internal class MyRetroGame : GameLogic
    {
        public MyRetroGame(GameConfig GameConfig) : base(GameConfig) { }

        // NOTICE: All the variables and objects are static, because this way they can be accessed elsewhere without creating objects.

        // GameConfig is a variable already accessible in methods to retrieve the game configs
        // bool IsPaused() is a function already accessible in methods to check if the game is paused
        // void SetPaused(bool) is a function already accessible in methods to set the game in pause and to resume it

        // GAME DATA
        // Declare here game-specific data that should survive the frame

        // Game Difficulty
        public static float Difficulty = 1f;
        public static int FramesPlayed = 0;

        // Game score
        public static int Score = 0;

        // Player's lives
        public static int Lives = 5;

        // Boolean if the game is ongoing
        public static bool OnGoing = true;

        // Boolean to order pause
        public static bool ToBePaused = false;

        // Array for spaceship pos, and speed vector (Coordinates)
        public static int[] Spaceship_Pos;
        public static int[] Spaceship_Vel;

        // Array for bi-dimensional range
        public static int[] Spaceship_Radius = new int[] { 6, 4 };

        // Booleans for spaceship status in a certain moment
        public static bool isSpaceshipAttacked = false;
        public static bool isSpaceshipShooting = false;

        // Projectile Image, position and speed
        public static GameImage Projectile_Image = new GameImage(
        new int[,]
        {
            {1}
        },
        AnchorType.Center);

        // Projectiles' Position and speeds are saved in lists (Resizeable arrays),
        // because the number varies during the game
        public static List<int[]> Projectile_Pos = new List<int[]>();
        public static List<int[]> Projectile_Vel = new List<int[]>();

        // However, the radius remains a single array, as this is common for all projectiles
        public static int[] Projectile_Radius = new int[] { 1, 1 };

        // Enemy Image, position and speed
        public static GameImage Enemy_Image = new GameImage(
        new int[,]
        {
            {1, 0, 1},
            {0, 1, 0},
            {1, 0, 1},
        }, AnchorType.Center);

        // Enemies' Position and speeds are saved in lists (Resizeable arrays),
        // because the number varies during the game
        public static List<int[]> Enemy_Pos = new List<int[]>();
        public static List<int[]> Enemy_Vel = new List<int[]>();

        // However, the radius remains a single array, as this is common for all enemies
        public static int[] Enemy_Radius = new int[] { 2, 2 };

        // This is the interval (in frames) for enemy generation 
        public static int Enemy_Generation_Interval = 24;


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

        // Called at the start of the first frame of the game.
        // It's main purpose it's to setup the scene.
        private void FirstFrameLoop()
        {
            // Defining spaceship position and potential speed
            Spaceship_Pos = new int[] { GameConfig.PixelsMatrixWidth / 2, GameConfig.PixelsMatrixHeight - 10 };
            Spaceship_Vel = new int[] { 2, 2 };
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
            Difficulty = 1 * (float)Math.Sqrt(FramesPlayed);

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
                    Generation.Despawn(Enemy_Pos, Enemy_Vel);
                    Generation.Despawn(Projectile_Pos, Projectile_Vel);

                    // Checking for collisions, between projectiles and enemies. If yes, both will be deleted and the score will increase
                    Collisions.Check(Enemy_Pos, Enemy_Radius, Projectile_Pos, Projectile_Radius);

                    // Checking for collisions, between enemies and the spaceship. If yes, the enemy will be deleted and the spaceship will lose one life
                    Collisions.Check_Spaceship(Spaceship_Pos, Spaceship_Radius, Enemy_Pos, Enemy_Radius);

                    // If the player runs out of lives, the game will end
                    if (Lives < 0) OnGoing = false;

                    // Enemies will move automatically, like existing projectiles
                    Movement.Kind_Of_Element(Enemy_Pos, Enemy_Vel);
                    Movement.Kind_Of_Element(Projectile_Pos, Projectile_Vel);

                    // The spaceship cannot go away from the main field (the window)
                    Movement.Spaceship_Border(Spaceship_Pos, Spaceship_Vel, Spaceship_Radius, 1);
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
                Writing.Print(pixels, $"V:{Lives}\n\rD:{(int) Difficulty}", "TopLeft");
                Writing.Print(pixels, $"S:{Score}", "TopRight");
                if (IsPaused()) Writing.Print(pixels, "Paused", "BottomLeft");

                // Every projectile and every enemy will be drawn
                Draw.KindOfElements(pixels, Projectile_Image, Projectile_Pos);
                Draw.KindOfElements(pixels, Enemy_Image, Enemy_Pos);

                // Draw the spaceship differently, depending on the state
                Spaceship_Style.Draw(pixels, Spaceship_Pos);
            }else
            {
                SetPaused(true);
                Writing.Print(pixels, $"Game over!{Environment.NewLine}Score: {Score}" +
                                      $"{Environment.NewLine}{Environment.NewLine}Difficulty Reached: {(int)Difficulty}" +
                                      $"{Environment.NewLine}{Environment.NewLine}Press ESC to quit", "TopLeft");
                Writing.Print(pixels, "Thank you!", "BottomRight");
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
                Movement.Spaceship(KeyCode);

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
                Movement.Spaceship(KeyCode);
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
        public static void KindOfElements(int[,] pixels, GameImage This_Image, List<int[]> This_Pos)
        {
            for (int i = 0; i < This_Pos.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[i][0]), (int)(This_Pos[i][1])));
            }
        }

        // All the elements of the same type (Like projectiles and enemies) can be drawn in one cycle
        public static void Element(int[,] pixels, GameImage This_Image, int[] This_Pos)
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
        public static void Despawn(List<int[]> This_Pos, List<int[]> This_Vel)
        {
            for (global::System.Int32 i = 0; i < This_Pos.Count; i++)
            {
                if (This_Pos[i][0] > GameConfig.PixelsMatrixWidth || This_Pos[i][0] < 0 || This_Pos[i][1] > GameConfig.PixelsMatrixWidth || This_Pos[i][1] < 0)
                {
                    This_Pos.Remove(This_Pos[i]);
                    This_Vel.Remove(This_Vel[i]);
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
        public static void Check(List<int[]> First_Pos, int[] First_Radius, List<int[]> Second_Pos, int[] Second_Radius)
        {
            for (int i = 0; i < First_Pos.Count; i++)
            {
                for (int j = 0; j < Second_Pos.Count; j++)
                {
                    if (Verify_Collision(First_Pos[i], First_Radius, Second_Pos[j], Second_Radius))
                    {
                        // Removal of overlapping elements
                        First_Pos.Remove(First_Pos[i]);
                        Second_Pos.Remove(Second_Pos[j]);

                        // Setting indexes to values out of the for cycle range
                        i = First_Pos.Count;
                        j = Second_Pos.Count;

                        // The score is increased by the difficulty
                        MyRetroGame.Score += (int)(MyRetroGame.Difficulty);
                    }
                }
            }
        }
        /* This function is made forthe Spaceship (First) and enemies (Second).
         * This is like the previus function, with the main difference being the number of cycles: It's only one,
         * as the spaceship remains the same.
         * The spaceship will be marked as attacked, and the lives count will decrease by 1
         */
        public static void Check_Spaceship(int[] Spaceship_Pos, int[] Spaceship_Radius, List<int[]> Second_Pos, int[] Second_Radius)
        {
            for (int j = 0; j < Second_Pos.Count; j++)
            {
                if (Verify_Collision(Spaceship_Pos, Spaceship_Radius, Second_Pos[j], Second_Radius))
                {
                    MyRetroGame.Lives--;
                    MyRetroGame.isSpaceshipAttacked = true;
                    Second_Pos.Remove(Second_Pos[j]);
                }
            }
        }
    }

    // Public class for Generation of Enemies and Projectiles
    public class Generation : Dynamicity
    {
        // Enemies generation, depending on the interval and the difficulty
        public static void Enemies()
        {
            if ((int) ((double) MyRetroGame.FrameCount % (100/Math.Sqrt(MyRetroGame.Difficulty))) == 0)
            {
                for (int i = 0; i < (int)Math.Sqrt(MyRetroGame.Difficulty); i++)
                {
                    MyRetroGame.Enemy_Pos.Add(new int[] { (int)(GameConfig.PixelsMatrixWidth * (float)MyRetroGame.RandomGenerator.Next() / 2147483647), (int)(5) });
                    MyRetroGame.Enemy_Vel.Add(new int[] { 0, (int) Math.Sqrt((Math.Sqrt(MyRetroGame.Difficulty))) }); // Velocità da cambiare in seguito
                }
            }
        }

        // Projectiles generation, marking the spaceship as shooting
        public static void Projectiles()
        {
            MyRetroGame.isSpaceshipShooting = true;
            MyRetroGame.Projectile_Pos.Add(new int[] { MyRetroGame.Spaceship_Pos[0], MyRetroGame.Spaceship_Pos[1] });
            MyRetroGame.Projectile_Vel.Add(new int[] { 0, -2 }); // Da rendere flessibile
        }
    }

    // Movement class
    public class Movement
    {
        // Manual movement
        public static void Spaceship(Keys KeyCode)
        {
            if (KeyCode == Keys.W) MyRetroGame.Spaceship_Pos[1] -= MyRetroGame.Spaceship_Vel[1];
            if (KeyCode == Keys.A) MyRetroGame.Spaceship_Pos[0] -= MyRetroGame.Spaceship_Vel[0];
            if (KeyCode == Keys.S) MyRetroGame.Spaceship_Pos[1] += MyRetroGame.Spaceship_Vel[1];
            if (KeyCode == Keys.D) MyRetroGame.Spaceship_Pos[0] += MyRetroGame.Spaceship_Vel[1];
        }

        // Automatic movement
        public static void Kind_Of_Element(List<int[]> This_Pos, List<int[]> This_Vel)
        {
            for (int i = 0; i < This_Pos.Count; i++)
            {
                This_Pos[i][0] += This_Vel[i][0];
                This_Pos[i][1] += This_Vel[i][1];
            }
        }

        // Automatic movement
        public static void Spaceship_Border(int[] This_Pos, int[] This_Vel, int[] This_Radius, int Offset)
        {
            if (This_Pos[0] < This_Radius[0] + Offset) This_Pos[0] = This_Radius[0] + Offset;
            if (This_Pos[0] > GameConfig.PixelsMatrixWidth - This_Radius[0] - 1 - Offset) This_Pos[0] = GameConfig.PixelsMatrixWidth - This_Radius[0] - 1 - Offset;
            if (This_Pos[1] < This_Radius[1] + Offset) This_Pos[1] = This_Radius[1] + Offset;
            if (This_Pos[1] > GameConfig.PixelsMatrixHeight - This_Radius[1] - Offset) This_Pos[1] = GameConfig.PixelsMatrixHeight - This_Radius[1] - Offset;
        }
    }

    // Public class for Spaceship Style
    public class Spaceship_Style
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

        // Drawing depending on the state
        public static void Draw(int[,] pixels, int[] Spaceship_Pos)
        {
            if (MyRetroGame.isSpaceshipAttacked)
            {
                GameUtils.DrawImageOnScreen(pixels, Spaceship_Style.Attacked, new Point((int)(Spaceship_Pos[0]), (int)(Spaceship_Pos[1])));
                MyRetroGame.isSpaceshipAttacked = false;
            }
            else if (MyRetroGame.isSpaceshipShooting)
            {
                GameUtils.DrawImageOnScreen(pixels, Spaceship_Style.Shooting, new Point((int)(Spaceship_Pos[0]), (int)(Spaceship_Pos[1])));
                MyRetroGame.isSpaceshipShooting = false;
            }
            else
            {
                GameUtils.DrawImageOnScreen(pixels, Spaceship_Style.Normal, new Point((int)(Spaceship_Pos[0]), (int)(Spaceship_Pos[1])));
            }
        }
    }
}
