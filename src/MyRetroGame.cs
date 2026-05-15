using System;
using System.Windows.Forms;
using System.Drawing;
using RetroGameFramework;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Collections.Generic;

namespace RetroGameDemo
{
    internal class MyRetroGame : GameLogic
    {
        public MyRetroGame(GameConfig GameConfig) : base(GameConfig) { }

        // GameConfig is a variable already accessible in methods to retrieve the game configs
        // bool IsPaused() is a function already accessible in methods to check if the game is paused
        // void SetPaused(bool) is a function already accessible in methods to set the game in pause and to resume it

        // GAME DATA
        // Declare here game-specific data that should survive the frame

        int Lives = 1; // da cambiare
        bool OnGoing = true;

        // Spaceship Image, position and speed
        GameImage Spaceship_Image = new GameImage(
        new int[,]
        {
            {0, 0, 0, 0, 0 ,1, 1, 1, 0, 0, 0, 0, 0,},
            {0, 0, 0, 0, 0 ,1, 1, 1, 0, 0, 0, 0, 0,},
            {0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0,},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            {0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
        },
        AnchorType.Center);

        int[] Spaceship_Pos;
        int[] Spaceship_Vel;
        int[] Spaceship_Radius = new int[] { 6, 4 };

        // Projectile Image, position and speed
        GameImage Projectile_Image = new GameImage(
        new int[,]
        {
            {1}
        },
        AnchorType.Center);

        List<int[]> Projectile_Pos = new List<int[]>();
        List<int[]> Projectile_Vel = new List<int[]>();
        int[] Projectile_Radius = new int[] { 1, 1 };

        // Enemy Image, position and speed
        GameImage Enemy_Image = new GameImage(
        new int[,]
        {
            {1, 0, 1},
            {0, 1, 0},
            {1, 0, 1},
        }, AnchorType.Center);

        List<int[]> Enemy_Pos = new List<int[]>();
        List<int[]> Enemy_Vel = new List<int[]>();
        int[] Enemy_Radius = new int[] { 2, 2 };
        int Enemy_Generation_Interval = 24;

        // Initialization call, used to customize GameConfig data (used to customize the engine behaviour)
        protected override void OnInitGameConfig(GameConfig GameConfig)
        {
            GameConfig.Title = "SpaceInvaders";
            GameConfig.PixelsMatrixWidth = 100;
            GameConfig.PixelsMatrixHeight = 100;
            GameConfig.PixelSize = 5;
            GameConfig.FrameRate = 24;
            GameConfig.ForegroundColor = System.Drawing.Color.White;
            GameConfig.BackgroundColor = System.Drawing.Color.Black;
            GameConfig.randomGeneratorSeed = 852346891;
        }

        // Called at the start of the first frame of the game.
        // It's main purpose it's to setup the scene.
        private void FirstFrameLoop()
        {
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
            if(OnGoing)
            {
                Enemy_Generation();
                if (FrameCount == 0)
                {
                    FirstFrameLoop();
                }
                else
                {
                    Despawn(Enemy_Pos, Enemy_Vel);
                    Despawn(Projectile_Pos, Projectile_Vel);

                    Check_For_Collisions(Enemy_Pos, Enemy_Radius, Projectile_Pos, Projectile_Radius);
                    Check_For_Spaceship_Threat(Spaceship_Pos, Spaceship_Radius, Enemy_Pos, Enemy_Radius);

                    if (Lives < 0) OnGoing = false;

                    Movement(Enemy_Pos, Enemy_Vel);
                    Movement(Projectile_Pos, Projectile_Vel);

                    Spaceship_Border(Spaceship_Pos, Spaceship_Vel, Spaceship_Radius, 1);
                }
            } else
            {
                OnEndGame();
            }
        }

        // Called once per frame, AFTER the OnLoopGame event.
        protected override void OnDraw(int[,] pixels)
        {
            int ScreenWidth = pixels.GetLength(0);
            int ScreenHeight = pixels.GetLength(1);

            DrawElementsKind(pixels, Projectile_Image, Projectile_Pos);

            DrawElementsKind(pixels, Enemy_Image, Enemy_Pos);

            GameUtils.DrawImageOnScreen(pixels, Spaceship_Image, new Point((int)(Spaceship_Pos[0]), (int)(Spaceship_Pos[1])));
        }

        // Called at the end of the last frame of the game.
        // Its main purpose it's to dispose resources, as the game will end immediately after this call.
        protected override void OnEndGame()
        {

        }

        // Called the first frame a key is pressed, and not called anymore unless the key is released
        protected override void OnKeyDown(Keys KeyCode)
        {
            Projectile_Shoot(KeyCode);
        }

        // Called if a key has been released (even in the same frame it has been released)
        protected override void OnKeyUp(Keys KeyCode)
        {

        }

        // Called during the frame a key is pressed and in all the following frames until it's released (excluding the frame it's released)
        protected override void OnKeyPress(Keys KeyCode)
        {
            Spaceship_Movement(KeyCode);

            bool Pause = false;
            if (KeyCode == Keys.P)
            {
                Pause = !Pause;
                SetPaused(Pause);
            }
        }

        void Spaceship_Movement(Keys KeyCode)
        {
            if (KeyCode == Keys.W) Spaceship_Pos[1] -= Spaceship_Vel[1];
            if (KeyCode == Keys.A) Spaceship_Pos[0] -= Spaceship_Vel[0];
            if (KeyCode == Keys.S) Spaceship_Pos[1] += Spaceship_Vel[1];
            if (KeyCode == Keys.D) Spaceship_Pos[0] += Spaceship_Vel[1];
        }

        void Projectile_Shoot(Keys KeyCode)
        {
            if (KeyCode == Keys.Space)
            {
                Projectile_Pos.Add(new int[] { Spaceship_Pos[0], Spaceship_Pos[1] });
                Projectile_Vel.Add(new int[] { 0, -2 }); // Da rendere flessibile
            }
        }
        
        void Enemy_Generation()
        {
            if (FrameCount % Enemy_Generation_Interval == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    //Enemy_Pos.Add(new int[] { (int)(GameConfig.PixelsMatrixWidth * (2 * i + 1) / 12), (int)(5) });
                    Enemy_Pos.Add(new int[] { (int)(GameConfig.PixelsMatrixWidth * (float) RandomGenerator.Next()/2147483647), (int)(5) });
                    Enemy_Vel.Add(new int[] { 0, 1 }); // Velocità da cambiare in seguito
                }
            }
        }

        void Despawn(List<int[]> This_Pos, List<int[]> This_Vel)
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

        void Movement(List<int[]> This_Pos, List<int[]> This_Vel)
        {
            for (int i = 0; i < This_Pos.Count; i++)
            {
                This_Pos[i][0] += This_Vel[i][0];
                This_Pos[i][1] += This_Vel[i][1];
            }
        }
        
        void Spaceship_Border(int[] This_Pos, int[] This_Vel, int[] This_Radius, int Offset)
        {
            if (This_Pos[0] < This_Radius[0] + Offset) This_Pos[0] = This_Radius[0] + Offset;
            if (This_Pos[0] > GameConfig.PixelsMatrixWidth - This_Radius[0] -1 - Offset) This_Pos[0] = GameConfig.PixelsMatrixWidth - This_Radius[0] -1 - Offset;
            if (This_Pos[1] < This_Radius[1] + Offset) This_Pos[1] = This_Radius[1] + Offset;
            if (This_Pos[1] > GameConfig.PixelsMatrixHeight - This_Radius[1] - Offset) This_Pos[1] = GameConfig.PixelsMatrixHeight - This_Radius[1] - Offset;
        }

        void Check_For_Collisions(List<int[]> First_Pos, int[] First_Radius, List<int[]> Second_Pos, int[] Second_Radius)
        {
            for (int i = 0; i < First_Pos.Count; i++)
            {
                for (int j = 0; j < Second_Pos.Count; j++)
                {
                    if (Verify_Collision(First_Pos[i], First_Radius, Second_Pos[j], Second_Radius))
                    {
                        First_Pos.Remove(First_Pos[i]);
                        Second_Pos.Remove(Second_Pos[j]);
                    }
                }
            }
        }

        void Check_For_Spaceship_Threat(int[] Spaceship_Pos, int[] Spaceship_Radius, List<int[]> Second_Pos, int[] Second_Radius)
        {
            for (int j = 0; j < Second_Pos.Count; j++)
            {
                if (Verify_Collision(Spaceship_Pos, Spaceship_Radius, Second_Pos[j], Second_Radius))
                {
                    Lives--;
                    Second_Pos.Remove(Second_Pos[j]);
                }
            }
        }

        int AlwaysPositive(int Number)
        {
            if (Number < 0) return -Number;
            else return Number;
        }

        int DeltaCoord(int First_Point, int Second_Point)
        {
            return AlwaysPositive(First_Point - Second_Point);
        }

        bool Verify_Collision(int[] First_Pos, int[] First_Radius, int[] Second_Pos, int[] Second_Radius)
        {
            if (DeltaCoord(First_Pos[0], Second_Pos[0]) - (First_Radius[0] + Second_Radius[0]) < 0
                && DeltaCoord(First_Pos[1], Second_Pos[1]) - (First_Radius[1] + Second_Radius[1]) < 0) return true;
            else return false;
        }

        void DrawElementsKind(int[,] pixels, GameImage This_Image, List<int[]> This_Pos)
        {
            for (int i = 0; i < This_Pos.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[i][0]), (int)(This_Pos[i][1])));
            }
        }
    }
}
