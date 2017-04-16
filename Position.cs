namespace NaturesProphet
{
    public struct Position
    {
        private static readonly Position[] positionForADirection = new Position[]
        {
            new Position(+1, 0), //down
            new Position(0, -1), //left
            new Position(0, +1), //right
            new Position(-1, 0)  //up
        };

        private int row;
        private int col;

        public Position(int[] rowAndCol)
            : this (rowAndCol[0], rowAndCol[1])
        {

        }

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        public static Position GetPositionForADirection(Direction direction)
        {
            return positionForADirection[(int)direction];
        }
    }
}
