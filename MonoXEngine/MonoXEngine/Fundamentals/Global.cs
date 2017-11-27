using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoXEngine
{
    public static class Global
    {
        /// <summary>
        /// Game
        /// </summary>
        public static Game Game;

        /// <summary>
        /// Content
        /// </summary>
        public static ContentManager Content
        {
            get { return Global.Game.Content; }
        }

        /// <summary>
        /// GameTime
        /// </summary>
        public static GameTime GameTime;

        /// <summary>
        /// Updated in game updates
        /// </summary>
        public static float DeltaTime;

        /// <summary>
        /// Resolution (Set in initialise)
        /// </summary>
        public static Point Resolution;

        /// <summary>
        /// ScreenBounds
        /// </summary>
        public static Rectangle ScreenBounds
        {
            get { return new Rectangle(new Point(-Global.Resolution.X / 2, -Global.Resolution.Y / 2), Global.Resolution); }
        }

        /// <summary>
        /// GraphicsDevice
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return MonoXEngineGame.Instance.GraphicsDevice; }
        }

        /// <summary>
        /// Cameras
        /// </summary>
        public static List<Camera> Cameras;

        /// <summary>
        /// Camera
        /// </summary>
        public static Camera Camera;

        /// <summary>
        /// SpriteBatchLayers
        /// </summary>
        public static Dictionary<string, SpriteBatchLayer> SpriteBatchLayers;

        /// <summary>
        /// String to point array. Use "1,0|2,0|.."
        /// </summary>
        /// <param name="pointListStr"></param>
        /// <returns></returns>
        public static Point[] Str2Points(string pointListStr)
        {
            string[] points = pointListStr.Split('|');

            List<Point> pointList = new List<Point>();
            foreach (string point in points)
            {
                string[] pointXY = point.Trim().Split(',');
                pointList.Add(new Point(Convert.ToInt16(pointXY[0]), Convert.ToInt16(pointXY[1])));
            }

            return pointList.ToArray();
        }

        /// <summary>
        /// String to point array. Use "0,1,2,3,4,5", "0"
        /// </summary>
        /// <param name="pointListStr"></param>
        /// <returns></returns>
        public static Point[] Str2Points(string xPointList = "0", string yPointList = "0")
        {
            string[] xPoints = xPointList.Split(',');
            string[] yPoints = yPointList.Split(',');

            List<Point> pointList = new List<Point>();
            int largestArr = Math.Max(xPoints.Length, yPoints.Length);
            int x = 0, y = 0;
            for (int I = 0; I < largestArr; I++ )
            {
                if(I < xPoints.Length-1) x = Convert.ToInt16(xPoints[I]);
                if(I < yPoints.Length-1) y = Convert.ToInt16(yPoints[I]);

                pointList.Add(new Point(x, y));
            }

            return pointList.ToArray();
        }
    }
}
