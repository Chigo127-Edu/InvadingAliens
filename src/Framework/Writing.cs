/*
    Writing part of the Framework, to write text onto the pixel matrix.
    Copyright (C) 2026 Chigo127-Edu, Ale-Cioffo

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Chigo127-Edu: https://github.com/Chigo127-Edu/
    Ale-Cioffo: https://github.com/Ale-Cioffo/
 */

using System;
using System.Drawing;

namespace RetroGameFramework
{
    public class Writing
    {
        // Draw elements more easily
        private static void DrawElement(int[,] pixels, int[] This_Pos, GameImage This_Image, PaintStyle paintStyle)
        {
            GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[0]), (int)(This_Pos[1])), paintStyle);
        }

        public static int Top_Left = 0;
        public static int Top_Right = 1;
        public static int Bottom_Left = 2;
        public static int Bottom_Right = 3;

        // Corners
        private static int[] Pos_Top_Left = new int[] { 1, 1 };
        private static int[] Pos_Top_Right = new int[] { GameLogic.GameConfig.PixelsMatrixWidth - 2, 1 };
        private static int[] Pos_Bottom_Left = new int[] { 1, GameLogic.GameConfig.PixelsMatrixHeight - 2 };
        private static int[] Pos_Bottom_Right = new int[] { GameLogic.GameConfig.PixelsMatrixWidth - 2, GameLogic.GameConfig.PixelsMatrixHeight - 2 };

        // Char radius, like enemies and projectiles'
        private static int[] Char_Radius = new int[] { 2, 3 };

        // Since the array doesn't start with ASCII element 0 (but 32), the requested index will be converted
        // For now, letters are forcibly capitalized.
        private static int GetChar(int Value)
        {
            if (Value > 31 && Value < 127) { return Value - 32; }
            else return 0;
        }

        // Print the string onto the screen. Currently line feed and carriage return is supported only for the top left
        public static void Print(int[,] pixels, string Value, int Corner, PaintStyle paintStyle)
        {
            switch (Corner)
            {
                case 0:
                    {
                        // Offsets for line feed and carriage return
                        int OffsetX = 0;
                        int OffsetY = 0;
                        for (int i = 0; i < Value.Length; i++)
                        {
                            if (Value[i] == '\n') { OffsetY += 8; OffsetX -= 1; }
                            else if (Value[i] == '\r') OffsetX = -1;
                            else if (i < Value.Length) DrawElement(pixels, new int[] { Pos_Top_Left[0] + (6 * OffsetX) + Char_Radius[0], Pos_Top_Left[1] + Char_Radius[1] + OffsetY }, Chars[GetChar(Value[i])], paintStyle);
                            OffsetX++;
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            // Line feed and carrige return are only supported for top left
                            if (Value[i] == '\n' || Value[i] == '\r') i++;

                            if (i < Value.Length) DrawElement(pixels, new int[] { Pos_Top_Right[0] - (6 * i) - Char_Radius[0], Pos_Top_Right[1] + Char_Radius[1] }, Chars[GetChar(Value[Value.Length - i - 1])], paintStyle);
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            // Line feed and carrige return are only supported for top left
                            if (Value[i] == '\n' || Value[i] == '\r') i++;

                            if (i < Value.Length) DrawElement(pixels, new int[] { Pos_Bottom_Left[0] + 6 * i + Char_Radius[0], Pos_Bottom_Left[1] - Char_Radius[1] }, Chars[GetChar(Value[i])], paintStyle);
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            // Line feed and carrige return are only supported for top left
                            if (Value[i] == '\n' || Value[i] == '\r') i++;

                            if (i < Value.Length) DrawElement(pixels, new int[] { Pos_Bottom_Right[0] - (6 * i) - Char_Radius[0], Pos_Bottom_Right[1] - Char_Radius[1] }, Chars[GetChar(Value[Value.Length - i - 1])], paintStyle);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public static void Print(int[,] pixels, string Value, int Corner)
        {
            Print(pixels, Value, Corner, PaintStyle.Default);
        }

        // An overwhelming array containing capital letters, numbers and some symbols, as GameImages
        private static GameImage[] Chars = new GameImage[]
        {
            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " * * ",
            " * * ",
            " * * ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " * * ",
            " * * ",
            "*****",
            " * * ",
            "*****",
            " * * ",
            " * * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            " ****",
            "* *  ",
            " *** ",
            "  * *",
            "**** ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "**  *",
            "**  *",
            "   * ",
            "  *  ",
            " *   ",
            "*  **",
            "*  **",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " **  ",
            "*  * ",
            " **  ",
            "*  **",
            "*  * ",
            "*  * ",
            " ** *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "   * ",
            "  *  ",
            " *   ",
            " *   ",
            " *   ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "   * ",
            "   * ",
            "   * ",
            "  *  ",
            " *   ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "  *  ",
            "* * *",
            " *** ",
            "* * *",
            "  *  ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "  *  ",
            "  *  ",
            "*****",
            "  *  ",
            "  *  ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "   * ",
            "   *  ",
            " **  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "*****",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "    *",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*  **",
            "* * *",
            "**  *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            " **  ",
            "* *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "    *",
            " *** ",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "    *",
            " *** ",
            "    *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  ** ",
            " * * ",
            "*  * ",
            "*  * ",
            "*****",
            "   * ",
            "   * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "**** ",
            "    *",
            "    *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "*   *",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            " *** ",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            " ****",
            "    *",
            "    *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "  *  ",
            "     ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "   * ",
            "     ",
            "   * ",
            " **  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "   **",
            " **  ",
            "*    ",
            " **  ",
            "   **",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*****",
            "     ",
            "*****",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "**   ",
            "  ** ",
            "    *",
            "  ** ",
            "**   ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "   * ",
            "  *  ",
            "  *  ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "* ***",
            "* ***",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*****",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*    ",
            "*    ",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "*    ",
            "*****",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "*    ",
            "*****",
            "*    ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*    ",
            "*  **",
            "*   *",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*****",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            "*  * ",
            " **  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*  * ",
            "* *  ",
            "**   ",
            "* *  ",
            "*  * ",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "** **",
            "* * *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "**  *",
            "* * *",
            "*  **",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "*    ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            "* * *",
            "*  * ",
            " ** *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "* *  ",
            "*  * ",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " ****",
            "*    ",
            "*    ",
            " *** ",
            "    *",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "* * *",
            "* * *",
            "* * *",
            " * * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
            " * * ",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*****",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            " *   ",
            " *   ",
            " *   ",
            " *   ",
            " *   ",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "*    ",
            "*    ",
            " *   ",
            "  *  ",
            "   * ",
            "    *",
            "    *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *** ",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            " * * ",
            "*   *",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "   * ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "    *",
            " ****",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "    *",
            "    *",
            " ****",
            "*   *",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*****",
            "*    ",
            " ****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            " *** ",
            "*   *",
            "*    ",
            "***  ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*   *",
            " ****",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "     ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "   * ",
            "     ",
            "   * ",
            "   * ",
            "   * ",
            " * * ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*   *",
            "* ** ",
            "**   ",
            "* ** ",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "** * ",
            "* * *",
            "* * *",
            "* * *",
            "* * *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "* ** ",
            "**  *",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "**** ",
            "*   *",
            "**** ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*   *",
            " ****",
            "    *",
            "    *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*  **",
            "* *  ",
            "**   ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*    ",
            " *** ",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            " *** ",
            "  *  ",
            "  *  ",
            "  *  ",
            "   **",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            "*   *",
            "*  **",
            " ** *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            "*   *",
            " * * ",
            "  * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "* * *",
            "* * *",
            "* * *",
            "* * *",
            " * * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            " * * ",
            "  *  ",
            " * * ",
            "*   *",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            " ****",
            "    *",
            " *** ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*****",
            "    *",
            " *** ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "   * ",
            "  *  ",
            "  *  ",
            " *   ",
            "  *  ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "  *  ",
            "   * ",
            "  *  ",
            "  *  ",
            " *   ",
        }, new char[] { ' ', '*' }, AnchorType.Center),

            GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *  *",
            "* * *",
            "*  * ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, AnchorType.Center),
        };
    }
}
