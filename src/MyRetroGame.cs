using System;
using System.Windows.Forms;
using System.Drawing;
using RetroGameFramework;
using System.Windows.Media;
using System.Windows.Media.Effects;

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

        GameImage Spaceship = new GameImage(
        new int[,]
        {
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
        },
        AnchorType.Center);

        int[] SpaceshipPos;
        int[] SpaceshipVel;

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
            SpaceshipPos = new int[] { GameConfig.PixelsMatrixWidth / 2, GameConfig.PixelsMatrixHeight -10 };
            
            SpaceshipVel = new int[] { 2, 2 };
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
            if (FrameCount == 0)
            {
                FirstFrameLoop();
            }
            else
            {
                if (SpaceshipPos[0] < 10) SpaceshipPos[0] = 7;
                if (SpaceshipPos[0] > GameConfig.PixelsMatrixWidth - 7) SpaceshipPos[0] = GameConfig.PixelsMatrixWidth -7;
                if (SpaceshipPos[1] < 3) SpaceshipPos[1] = 3;
                if (SpaceshipPos[1] > GameConfig.PixelsMatrixHeight - 3) SpaceshipPos[1] = GameConfig.PixelsMatrixHeight - 3;
            }

        }

        // Called once per frame, AFTER the OnLoopGame event.
        protected override void OnDraw(int[,] pixels)
        {
            int ScreenWidth = pixels.GetLength(0);
            int ScreenHeight = pixels.GetLength(1);

            GameUtils.DrawImageOnScreen(pixels, Spaceship, new Point((int)(SpaceshipPos[0]), (int)(SpaceshipPos[1])));
        }

        // Called at the end of the last frame of the game.
        // Its main purpose it's to dispose resources, as the game will end immediately after this call.
        protected override void OnEndGame()
        {

        }

        // Called the first frame a key is pressed, and not called anymore unless the key is released
        protected override void OnKeyDown(Keys KeyCode)
        {
            if (KeyCode == Keys.W) SpaceshipPos[1] -= 3;
            if (KeyCode == Keys.A) SpaceshipPos[0] -= 3;
            if (KeyCode == Keys.S) SpaceshipPos[1] += 3;
            if (KeyCode == Keys.D) SpaceshipPos[0] += 3;
        }

        // Called if a key has been released (even in the same frame it has been released)
        protected override void OnKeyUp(Keys KeyCode)
        {

        }

        // Called during the frame a key is pressed and in all the following frames until it's released (excluding the frame it's released)
        protected override void OnKeyPress(Keys KeyCode)
        {
            bool pausa = false;
            if (KeyCode == Keys.P)
            {
                pausa = !pausa;
                SetPaused(pausa);
            }
        }

    }
}
