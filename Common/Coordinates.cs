using System;

namespace Common
{
    public class Coordinates : IEquatable<Coordinates>
    {
        public Coordinates(int columnX, int rowY)
        {
            X = columnX;
            Y = rowY;
        }

        public int X { get; }
        public int Y { get; }

        #region Equals
        public bool Equals(Coordinates coordinates)
        {
            return coordinates != null &&
                    X == coordinates.X &&
                    Y == coordinates.Y;
        }

        public static bool operator ==(Coordinates a, Coordinates b) => Equals(a, b);

        public static bool operator !=(Coordinates a, Coordinates b) => !Equals(a, b);

        public override bool Equals(object obj) => Equals(obj as Coordinates);

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        #endregion
    }
}
