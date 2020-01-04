using Common;

namespace Domain
{
    public class Tile
    {
        public Tile(int row, int column, bool isStartingPosition)
        {
            Coordinates = new Coordinates(column, row);
            IsStartingPosition = isStartingPosition;
        }

        public Coordinates Coordinates { get; }
        public bool IsStartingPosition { get; private set; }

        public void SetStartingPosition(bool isStartingPosition)
        {
            IsStartingPosition = isStartingPosition;
        }
    }
}
