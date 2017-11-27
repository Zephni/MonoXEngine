using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine.Structs
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public struct Point3D
    {
        public int X;
        public int Y;
        public int Z;

        public Point3D(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }

        public static Point3D FromPoint(Point source)
        {
            return new Point3D(source.X, source.Y, 0);
        }

        public static Point3D operator +(Point3D value1, Point value2)
        {
            Point3D point3d = new Point3D(
                value1.X + value2.X,
                value1.Y + value2.Y,
                value1.Z
            );

            return point3d;
        }

        public static Point3D operator -(Point3D value1, Point value2)
        {
            Point3D point3d = new Point3D(
                value1.X - value2.X,
                value1.Y - value2.Y,
                value1.Z
            );

            return point3d;
        }

        public static Point3D operator *(Point3D value1, Point value2)
        {
            {
                Point3D point3d = new Point3D(
                    value1.X * value2.X,
                    value1.Y * value2.Y,
                    value1.Z
                );

                return point3d;
            }
        }

        public static Point3D operator /(Point3D source, Point divisor)
        {
            {
                Point3D point3d = new Point3D(
                    source.X / divisor.X,
                    source.Y / divisor.Y,
                    source.Z
                );

                return point3d;
            }
        }

        public static bool operator ==(Point3D a, Point b)
        {
            return (a.X == b.X && a.Y == b.Y);
        }

        public static bool operator !=(Point3D a, Point b)
        {
            return !(a.X == b.X && a.Y == b.Y);
        }
    }
}
