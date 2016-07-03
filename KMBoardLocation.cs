using System;

namespace KnightMoves_CSharp
{
    public class KMBoardLocation {
        private String Name;
        private uint Row;
        private uint Column;
        private uint BoardDimension;

        public KMBoardLocation()
        {
            Row = 0;
            Column = 0;
            BoardDimension = GlobalConstants.DefaultBoardDimensionOnOneSide;
            Name = "";
        }

        public KMBoardLocation(uint row, uint column, uint boardSingleSideDimension) {
            Row = row;
            Column = column;
            BoardDimension = boardSingleSideDimension;
            if (((Row < GlobalConstants.MinimumBoardLocationValue) || (Row > BoardDimension)) ||
                ((Column < GlobalConstants.MinimumBoardLocationValue) || (Column > BoardDimension)))
            {
                ThrowBoundsException("KMBoardLocation::KMBoardLocation");
            }
            MakeName();
        }

        public KMBoardLocation(KMBoardLocation Original)
        {
            Row = Original.Row;
            Column = Original.Column;
            BoardDimension = Original.BoardDimension;
            Name = Original.Name;
        }

        public bool  IsSameLocation(KMBoardLocation BL2) { return (Row == BL2.Row) && (Column == BL2.Column); }

        private bool IsSet() { return ((Row > 0) && (Column > 0)); }
        public void SetRow(uint row) { Row = row; }
        public uint GetRow() { return Row; }
        public void SetColumn(uint column) { Column = column; }
        public uint GetColumn() { return Column; }
        public void SetBoardDimension(uint boardSize) { BoardDimension = boardSize; }
        public uint GetBoardDimension() { return BoardDimension; }
        public void SetName(String name) { Name = name; }
        public String GetName() { return Name; }
        public bool IsRowValid() { return ((Row >= GlobalConstants.MinimumBoardLocationValue) && (Row <= BoardDimension)); }
        public bool IsColumnValid() { return ((Column >= GlobalConstants.MinimumBoardLocationValue) && (Column <= BoardDimension)); }

        public bool IsValid()
        {
            bool ValidLocation = true;
            if (IsSet() == false)
            {
                ValidLocation = false;
            }
            else
            {
                ValidLocation = ((IsRowValid()) && (IsColumnValid()));
            }
    
            return ValidLocation;
        }

        public void MakeName()
        {
            if (((Row < GlobalConstants.MinimumBoardLocationValue)|| (Row > BoardDimension)) ||
                ((Column < GlobalConstants.MinimumBoardLocationValue) || (Column > BoardDimension))) {
                ThrowBoundsException("KMBoardLocation::MakeName()");
            }
            Name = GetRowName((int)(Row - 1));
            Name += Column.ToString();
        }

        /*
         * Thanks to anser by w0lf in http://stackoverflow.com/questions/10373561/convert-a-number-to-a-letter-in-c-sharp-for-use-in-microsoft-excel
         */
        private static string GetRowName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            value += letters[index % letters.Length];

            return value;
        }

        private void ThrowBoundsException(string EMessage)
        {
            EMessage += "value out of range (" + 
                    GlobalConstants.MinimumBoardLocationValue.ToString() + " to " + BoardDimension.ToString() + ")"
                    + "Row = " + Row.ToString() + " Column = " + Column.ToString();

            throw new ArgumentOutOfRangeException(EMessage);
        }

        public void PrintLocation()
        {
            Console.Write(" ");
            Console.Write(Name);
        }
    };
}