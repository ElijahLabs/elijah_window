using System;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;


namespace System.Drawing
{
    /// <summary>
    /// Summary description for GdiplusMethods.
    /// </summary>
    public class GdiplusMethods
    {
        private GdiplusMethods() { }


        private enum DriverStringOptions
        {
            CmapLookup = 1,
            Vertical = 2,
            Advance = 4,
            LimitSubpixel = 8,
        }


        public static void DrawDriverString(Graphics graphics, string text,
            Font font, Brush brush, PointF[] positions)
        {
            DrawDriverString(graphics, text, font, brush, positions, null);
        }


        public static void DrawDriverString(Graphics graphics, string text,
            Font font, Brush brush, PointF[] positions, Matrix matrix)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (text == null)
                throw new ArgumentNullException("text");
            if (font == null)
                throw new ArgumentNullException("font");
            if (brush == null)
                throw new ArgumentNullException("brush");
            if (positions == null)
                throw new ArgumentNullException("positions");


            // Get hGraphics
            FieldInfo field = typeof(Graphics).GetField("nativeGraphics",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hGraphics = (IntPtr)field.GetValue(graphics);


            // Get hFont
            field = typeof(Font).GetField("nativeFont",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hFont = (IntPtr)field.GetValue(font);


            // Get hBrush
            field = typeof(Brush).GetField("nativeBrush",
                BindingFlags.Instance | BindingFlags.NonPublic);
            IntPtr hBrush = (IntPtr)field.GetValue(brush);


            // Get hMatrix
            IntPtr hMatrix = IntPtr.Zero;
            if (matrix != null)
            {
                field = typeof(Matrix).GetField("nativeMatrix",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                hMatrix = (IntPtr)field.GetValue(matrix);
            }


            int result = GdipDrawDriverString(hGraphics, text, text.Length,
                hFont, hBrush, positions, (int)DriverStringOptions.CmapLookup, hMatrix);
        }


        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal extern static int GdipMeasureDriverString(IntPtr graphics,
            string text, int length, IntPtr font, PointF[] positions,
            int flags, IntPtr matrix, ref RectangleF bounds);


        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal extern static int GdipDrawDriverString(IntPtr graphics,
            string text, int length, IntPtr font, IntPtr brush,
            PointF[] positions, int flags, IntPtr matrix);
    }
}