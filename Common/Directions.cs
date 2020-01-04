namespace Common
{
    public enum Directions
    {
        Up, Down, Left, Right
    }

    public static class DirectionsMethods
    {
        public static Directions Opposite(this Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    return Directions.Down;
                case Directions.Down:
                    return Directions.Up;
                case Directions.Left:
                    return Directions.Right;
                case Directions.Right:
                    return Directions.Left;
                default:
                    throw new System.Exception();
            }
        }
    }
}
