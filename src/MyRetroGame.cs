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

        // Spaceship Image, position and speed
        GameImage Spaceship_Image = new GameImage(
        new int[,]
        {
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
        },
        AnchorType.Center);

        int[] Spaceship_Pos;
        int[] Spaceship_Vel;

        // Projectile Image, position and speed
        GameImage Projectile_Image = new GameImage(
        new int[,]
        {
            {1}
        },
        AnchorType.Center);

        List<int[]> Projectile_Pos = new List<int[]>();
        List<int[]> Projectile_Vel = new List<int[]>();

        // Enemy Image, position and speed
        GameImage Enemy_Image = new GameImage(
        new int[,]
        {
            {1, 1, 0, 1, 1},
            {1, 1, 0, 1, 1},
            {0, 0, 1, 0, 0},
            {1, 1, 0, 1, 1},
            {1, 1, 0, 1, 1}
        }, AnchorType.Center);

        List<int[]> Enemy_Pos = new List<int[]>();
        List<int[]> Enemy_Vel = new List<int[]>();

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
            if (FrameCount % 24 == 0)
            {
                Enemy_Pos.Add(new int[] { GameConfig.PixelsMatrixWidth / 2, GameConfig.PixelsMatrixHeight / 2 });
                Enemy_Vel.Add(new int[] { 3, 2 });
            }

            if (FrameCount == 0)
            {
                FirstFrameLoop();
            }
            else
            {
                for (global::System.Int32 i = 0; i < Enemy_Pos.Count; i++)
                {
                    // DEMO: DA LEVARE
                    if (Enemy_Pos[i][0] > GameConfig.PixelsMatrixWidth || Enemy_Pos[i][0] < 0)
                    {
                        Enemy_Vel[i][0] = -Enemy_Vel[i][0];
                    }
                    if (Enemy_Pos[i][1] > GameConfig.PixelsMatrixWidth || Enemy_Pos[i][1] < 0)
                    {
                        Enemy_Vel[i][1] = -Enemy_Vel[i][1];
                    }

                    Enemy_Pos[i][0] += Enemy_Vel[i][0];
                    Enemy_Pos[i][1] += Enemy_Vel[i][1];
                }

                for (global::System.Int32 i = 0; i < Projectile_Pos.Count; i++)
                {
                    Projectile_Pos[i][0] += Projectile_Vel[i][0];
                    Projectile_Pos[i][1] += Projectile_Vel[i][1];
                }

                if (Spaceship_Pos[0] < 10) Spaceship_Pos[0] = 7;
                if (Spaceship_Pos[0] > GameConfig.PixelsMatrixWidth - 7) Spaceship_Pos[0] = GameConfig.PixelsMatrixWidth - 7;
                if (Spaceship_Pos[1] < 3) Spaceship_Pos[1] = 3;
                if (Spaceship_Pos[1] > GameConfig.PixelsMatrixHeight - 3) Spaceship_Pos[1] = GameConfig.PixelsMatrixHeight - 3;
            }

        }

        // Called once per frame, AFTER the OnLoopGame event.
        protected override void OnDraw(int[,] pixels)
        {
            int ScreenWidth = pixels.GetLength(0);
            int ScreenHeight = pixels.GetLength(1);

            for (int i = 0; i < Enemy_Pos.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, Enemy_Image, new Point((int)(Enemy_Pos[i][0]), (int)(Enemy_Pos[i][1])));
            }

            for (int i = 0; i < Projectile_Pos.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, Projectile_Image, new Point((int)(Projectile_Pos[i][0]), (int)(Projectile_Pos[i][1])));
            }
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

        }

        // Called if a key has been released (even in the same frame it has been released)
        protected override void OnKeyUp(Keys KeyCode)
        {

        }

        // Called during the frame a key is pressed and in all the following frames until it's released (excluding the frame it's released)
        protected override void OnKeyPress(Keys KeyCode)
        {
            if (KeyCode == Keys.W) Spaceship_Pos[1] -= 3;
            if (KeyCode == Keys.A) Spaceship_Pos[0] -= 3;
            if (KeyCode == Keys.S) Spaceship_Pos[1] += 3;
            if (KeyCode == Keys.D) Spaceship_Pos[0] += 3;

            if (KeyCode == Keys.Space)
            {
                Projectile_Pos.Add(new int[] { Spaceship_Pos[0], Spaceship_Pos[1] });
                Projectile_Vel.Add(new int[] { FrameCount % 2, FrameCount % 3 });
            }
            bool pausa = false;
            if (KeyCode == Keys.P)
            {
                pausa = !pausa;
                SetPaused(pausa);
            }
        }

    }
}
