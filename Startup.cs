namespace NaturesProphet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Startup
    {
        private static List<Position> allSeedPositions;
        private static int[,] field;

        private static int rowsOfTheField;
        private static int colsOfTheField;

        public static void Main()
        {
            int[] dimensionsOfTheField = ConvertStringToIntegerArray(Console.ReadLine());

            rowsOfTheField = dimensionsOfTheField[0];
            colsOfTheField = dimensionsOfTheField[1];

            field = new int[rowsOfTheField, colsOfTheField];

            allSeedPositions = new List<Position>();
            ReadTheSeedPositions();

            GrowTheSeeds();

            PrintTheField();
        }

        private static void PrintTheField()
        {
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < rowsOfTheField; row++)
            {
                for (int col = 0; col < colsOfTheField; col++)
                {
                    result.Append(field[row, col]);

                    if (CheckIfAPlaceIsNotInTheEndOfADimension(col, 1))
                    {
                        result.Append(" ");
                    }
                }

                if (CheckIfAPlaceIsNotInTheEndOfADimension(row, 0))
                {
                    result.AppendLine();
                }
            }

            Console.WriteLine(result);
        }

        private static bool CheckIfAPlaceIsNotInTheEndOfADimension(int place, int dimension)
        {
            return place + 1 != field.GetLength(dimension);
        }

        private static void GrowTheSeeds()
        {
            Position currPositionInTheField = new Position();
            for (int row = 0; row < rowsOfTheField; row++)
            {
                for (int col = 0; col < colsOfTheField; col++)
                {
                    currPositionInTheField.Row = row;
                    currPositionInTheField.Col = col;

                    if (!CheckIfThereIsASeedToBeGrown(currPositionInTheField))
                    {
                        continue;
                    }

                    field[currPositionInTheField.Row, currPositionInTheField.Col]++;
                    GrowTheSeedInAllTheFourDirections(currPositionInTheField);
                }
            }
        }

        private static void GrowTheSeedInAllTheFourDirections(Position currPositionInTheField)
        {
            GrowTheSeedInACertainDirection(currPositionInTheField, Position.GetPositionForADirection(Direction.Down));
            GrowTheSeedInACertainDirection(currPositionInTheField, Position.GetPositionForADirection(Direction.Left));
            GrowTheSeedInACertainDirection(currPositionInTheField, Position.GetPositionForADirection(Direction.Right));
            GrowTheSeedInACertainDirection(currPositionInTheField, Position.GetPositionForADirection(Direction.Up));
        }

        private static void GrowTheSeedInACertainDirection(Position currPositionInTheField, Position newPosition)
        {
            while (true)
            {
                currPositionInTheField.Row += newPosition.Row;
                currPositionInTheField.Col += newPosition.Col;

                if (!CheckIfPositionIsValid(currPositionInTheField))
                {
                    break;
                }

                field[currPositionInTheField.Row, currPositionInTheField.Col]++;
            }
        }

        private static bool CheckIfPositionIsValid(Position currPositionInTheField)
        {
            return currPositionInTheField.Row >= 0 &&
                   currPositionInTheField.Row < rowsOfTheField &&
                   currPositionInTheField.Col >= 0 &&
                   currPositionInTheField.Col < colsOfTheField;
        }

        private static bool CheckIfThereIsASeedToBeGrown(Position currPositionInTheField)
        {
            bool toReturn = false;

            foreach (Position seedPosition in allSeedPositions)
            {
                if (PositionsAreTheSame(seedPosition, currPositionInTheField))
                {
                    toReturn = true;

                    break;
                }
            }

            return toReturn;
        }

        private static bool PositionsAreTheSame(Position toBeCompared, Position toCompareWith)
        {
            bool positionAreTheSame = toBeCompared.Row == toCompareWith.Row &&
                                      toBeCompared.Col == toCompareWith.Col;

            return positionAreTheSame;
        }

        private static void ReadTheSeedPositions()
        {
            string stopCommand = "Bloom Bloom Plow";

            string inputLine = Console.ReadLine();
            while (inputLine != stopCommand)
            {
                Position seedPosition = new Position(ConvertStringToIntegerArray(inputLine));

                allSeedPositions.Add(seedPosition);

                inputLine = Console.ReadLine();
            }
        }

        private static int[] ConvertStringToIntegerArray(string toSplit)
        {
            return toSplit.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}
