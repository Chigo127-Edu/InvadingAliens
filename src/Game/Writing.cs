using InvadingAliens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InvadingAliens
{
    public class Writing
    {
        // Corners
        private static int[] TopLeft = new int[] { 1, 1 };
        private static int[] TopRight = new int[] { RetroGameFramework.GameConfig.PixelsMatrixWidth - 2, 1 };
        private static int[] BottomLeft = new int[] { 1, RetroGameFramework.GameConfig.PixelsMatrixHeight - 2 };
        private static int[] BottomRight = new int[] { RetroGameFramework.GameConfig.PixelsMatrixWidth - 2, RetroGameFramework.GameConfig.PixelsMatrixHeight - 2 };

        // Char radius, like enemies and projectiles'
        private static int[] Char_Radius = new int[] { 2, 3 };

        // Since the array doesn't start with ASCII element 0 (but 32), the requested index will be converted
        // For now, letters are forcibly capitalized.
        private static int GetChar(int Value)
        {
            if (Value > 31 && Value < 127) { return Value - 32; }
            else return 0;
        }

        // Print the string onto the screen. Currently, only the top sides are supported,
        // and carriage return is supported only for the top left
        public static void Print(int[,] pixels, string Value, int Corner)
        {
            switch (Corner)
            {
                case 0:
                    {
                        int OffsetX = 0;
                        int OffsetY = 0;
                        for (int i = 0; i < Value.Length; i++)
                        {
                            if (Value[i] == '\r' && Value[i + 1] == '\n' || Value[i] == '\n' && Value[i + 1] == '\r') { OffsetX = -2; OffsetY += 8; }
                            Draw.Element(pixels, new int[] { TopLeft[0] + (6 * OffsetX) + Char_Radius[0], TopLeft[1] + Char_Radius[1] + OffsetY }, Chars[GetChar(Value[i])]);
                            OffsetX++;
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, new int[] { TopRight[0] - (6 * i) - Char_Radius[0], TopRight[1] + Char_Radius[1] }, Chars[GetChar(Value[Value.Length - i - 1])]);
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, new int[] { BottomLeft[0] + 6 * i + Char_Radius[0], BottomLeft[1] - Char_Radius[1] }, Chars[GetChar(Value[i])]);
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, new int[] { BottomRight[0] - (6 * i) - Char_Radius[0], BottomRight[1] - Char_Radius[1] }, Chars[GetChar(Value[Value.Length - i - 1])]);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        // An overwhelming array containing capital letters, numbers and some symbols, as RetroGameFramework.RetroGameFramework.GameImages
        private static RetroGameFramework.GameImage[] Chars = new RetroGameFramework.GameImage[]
        {
            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " * * ",
            " * * ",
            " * * ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " * * ",
            " * * ",
            "*****",
            " * * ",
            "*****",
            " * * ",
            " * * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            " ****",
            "* *  ",
            " *** ",
            "  * *",
            "**** ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "**  *",
            "**  *",
            "   * ",
            "  *  ",
            " *   ",
            "*  **",
            "*  **",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " **  ",
            "*  * ",
            " **  ",
            "*  **",
            "*  * ",
            "*  * ",
            " ** *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "   * ",
            "  *  ",
            " *   ",
            " *   ",
            " *   ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "   * ",
            "   * ",
            "   * ",
            "  *  ",
            " *   ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "  *  ",
            "* * *",
            " *** ",
            "* * *",
            "  *  ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "  *  ",
            "  *  ",
            "*****",
            "  *  ",
            "  *  ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "   * ",
            "   *  ",
            " **  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "*****",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "    *",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*  **",
            "* * *",
            "**  *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            " **  ",
            "* *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "    *",
            " *** ",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "    *",
            " *** ",
            "    *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  ** ",
            " * * ",
            "*  * ",
            "*  * ",
            "*****",
            "   * ",
            "   * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "**** ",
            "    *",
            "    *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "*   *",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            " *** ",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            " ****",
            "    *",
            "    *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "  *  ",
            "     ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "   * ",
            "     ",
            "   * ",
            " **  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "   **",
            " **  ",
            "*    ",
            " **  ",
            "   **",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*****",
            "     ",
            "*****",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "**   ",
            "  ** ",
            "    *",
            "  ** ",
            "**   ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "   * ",
            "  *  ",
            "  *  ",
            "     ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "* ***",
            "* ***",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*****",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*    ",
            "*    ",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "*    ",
            "*****",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "*    ",
            "*    ",
            "*****",
            "*    ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*    ",
            "*  **",
            "*   *",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*****",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            "*  * ",
            " **  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*  * ",
            "* *  ",
            "**   ",
            "* *  ",
            "*  * ",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "** **",
            "* * *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "**  *",
            "* * *",
            "*  **",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "*    ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            "* * *",
            "*  * ",
            " ** *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "**** ",
            "*   *",
            "*   *",
            "**** ",
            "* *  ",
            "*  * ",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " ****",
            "*    ",
            "*    ",
            " *** ",
            "    *",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "* * *",
            "* * *",
            "* * *",
            " * * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
            " * * ",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            " * * ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*****",
            "    *",
            "   * ",
            "  *  ",
            " *   ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            " *   ",
            " *   ",
            " *   ",
            " *   ",
            " *   ",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "*    ",
            "*    ",
            " *   ",
            "  *  ",
            "   * ",
            "    *",
            "    *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *** ",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            "   * ",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            " * * ",
            "*   *",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "     ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "   * ",
            "     ",
            "     ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "    *",
            " ****",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*    ",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "    *",
            "    *",
            " ****",
            "*   *",
            "*   *",
            " ****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*****",
            "*    ",
            " ****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            " *** ",
            "*   *",
            "*    ",
            "***  ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*   *",
            " ****",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*    ",
            "**** ",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "     ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "   * ",
            "     ",
            "   * ",
            "   * ",
            "   * ",
            " * * ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "*    ",
            "*   *",
            "* ** ",
            "**   ",
            "* ** ",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "** * ",
            "* * *",
            "* * *",
            "* * *",
            "* * *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "* ** ",
            "**  *",
            "*   *",
            "*   *",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *** ",
            "*   *",
            "*   *",
            "*   *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "**** ",
            "*   *",
            "**** ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*   *",
            " ****",
            "    *",
            "    *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*  **",
            "* *  ",
            "**   ",
            "*    ",
            "*    ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " ****",
            "*    ",
            " *** ",
            "    *",
            "**** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            " *** ",
            "  *  ",
            "  *  ",
            "  *  ",
            "   **",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            "*   *",
            "*  **",
            " ** *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            "*   *",
            " * * ",
            "  * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "* * *",
            "* * *",
            "* * *",
            "* * *",
            " * * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            " * * ",
            "  *  ",
            " * * ",
            "*   *",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*   *",
            "*   *",
            " ****",
            "    *",
            " *** ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            "*****",
            "    *",
            " *** ",
            "*    ",
            "*****",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "   * ",
            "  *  ",
            "  *  ",
            " *   ",
            "  *  ",
            "  *  ",
            "   * ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
            "  *  ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            " *   ",
            "  *  ",
            "  *  ",
            "   * ",
            "  *  ",
            "  *  ",
            " *   ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),

            RetroGameFramework.GameImage.CreateFromRows(new string[] {
            "     ",
            "     ",
            " *  *",
            "* * *",
            "*  * ",
            "     ",
            "     ",
        }, new char[] { ' ', '*' }, RetroGameFramework.AnchorType.Center),
        };

    }
}
