using RetroGameFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvadingAliens
{
    // Public class for drawing
    public class Draw
    {
        // All the elements of the same type (Like projectiles and enemies) can be drawn in one cycle, with a specific style
        public static void AllElements(int[,] pixels, List<Element> This, GameImage Image, PaintStyle Style)
        {
            for (int i = 0; i < This.Count; i++)
            {
                GameUtils.DrawImageOnScreen(pixels, Image, new Point((int)(This[i].Position[0]), (int)(This[i].Position[1])), Style);
            }
        }

        // One element. With or without style
        public static void Element(int[,] pixels, int[] This_Pos, GameImage This_Image)
        {
            GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[0]), (int)(This_Pos[1])));
        }
        public static void Element(int[,] pixels, int[] This_Pos, GameImage This_Image, PaintStyle This_Style)
        {
            GameUtils.DrawImageOnScreen(pixels, This_Image, new Point((int)(This_Pos[0]), (int)(This_Pos[1])), This_Style);
        }
    }

}
