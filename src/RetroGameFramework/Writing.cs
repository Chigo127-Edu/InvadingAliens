using SpaceInvaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Aggiunta personale rispetto al RetroGameFramework Originale
 */
namespace RetroGameFramework
{
    public class Writing
    {
        // Corners
        private static int[] TopLeft = new int[] { 1, 1 };
        private static int[] TopRight = new int[] { GameConfig.PixelsMatrixWidth - 2, 1 };
        private static int[] BottomLeft = new int[] { 1, GameConfig.PixelsMatrixHeight - 2 };
        private static int[] BottomRight = new int[] { GameConfig.PixelsMatrixWidth - 2, GameConfig.PixelsMatrixHeight - 2 };

        // Char radius, like enemies and projectiles'
        private static int[] Char_Radius = new int[] { 2, 3 };

        // Since the array doesn't start with ASCII element 0 (but 32), the requested index will be converted
        // For now, letters are forcibly capitalized.
        private static int GetChar(int Value)
        {
            if (Value > 96 && Value < 123) { return Value - 64; }
            if (Value > 31 && Value < 91) { return Value - 32; }
            else return 0;
        }

        // Print the string onto the screen. Currently, only the top sides are supported,
        // and carriage return is supported only for the top left
        public static void Print(int[,] pixels, string Value, string Corner)
        {
            switch (Corner)
            {
                case "TopLeft":
                    {
                        int OffsetX = 0;
                        int OffsetY = 0;
                        for (int i = 0; i < Value.Length; i++)
                        {
                            if (Value[i] == '\r' && Value[i + 1] == '\n' || Value[i] == '\n' && Value[i + 1] == '\r') { OffsetX = -2; OffsetY += 8; }
                            Draw.Element(pixels, Chars[GetChar(Value[i])], new int[] { TopLeft[0] + (6 * OffsetX) + Char_Radius[0], TopLeft[1] + Char_Radius[1] + OffsetY });
                            OffsetX++;
                        }
                    }
                    break;
                case "TopRight":
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, Chars[GetChar(Value[Value.Length - i - 1])], new int[] { TopRight[0] - (6 * i) - Char_Radius[0], TopRight[1] + Char_Radius[1] });
                        }
                    }
                    break;
                case "BottomLeft":
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, Chars[GetChar(Value[i])], new int[] { BottomLeft[0] + 6 * i + Char_Radius[0], BottomLeft[1] - Char_Radius[1] });
                        }
                    }
                    break;
                case "BottomRight":
                    {
                        for (int i = 0; i < Value.Length; i++)
                        {
                            Draw.Element(pixels, Chars[GetChar(Value[Value.Length - i - 1])], new int[] { BottomRight[0] - (6 * i) - Char_Radius[0], BottomRight[1] - Char_Radius[1] });
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        // An overwhelming array containing capital letters, numbers and some symbols, as gameImages
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
            " ***",
        }, new char[] { ' ', '*' }, AnchorType.Center),

        GameImage.CreateFromRows(new string[] {
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            "*   *",
            " * * ",
            "  * ",
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
        };

    }
}
