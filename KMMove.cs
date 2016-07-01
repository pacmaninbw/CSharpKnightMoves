using System;
using System.Diagnostics;

namespace KnightMoves_CSharp
{
    public class KMMove {
        private int RowTransition;
        private int ColumnTransition;
        private int BoardDimension;
        private KMBoardLocation Origin;
        private KMBoardLocation Destination;

        public KMMove() {
            Origin = new KMBoardLocation();
            Destination = new KMBoardLocation();
            RowTransition = 0;
            ColumnTransition = 0;
            BoardDimension = (int)(GlobalConstants.DefaultBoardDimensionOnOneSide);
        }

        public KMMove(int rowTransition, int columnTransition, uint boardDimension)
        {
            Origin = new KMBoardLocation();
            Destination = new KMBoardLocation();
            RowTransition = rowTransition;
            ColumnTransition = columnTransition;
            BoardDimension = (int)boardDimension;
        }

        public KMMove(KMMove Original)
        {
            Origin = new KMBoardLocation(Original.Origin);
            Destination = new KMBoardLocation(Original.Destination);
            RowTransition = Original.RowTransition;
            ColumnTransition = Original.ColumnTransition;
            BoardDimension = Original.BoardDimension;
        }

        private void CalculateDestination()
        {
            Destination.SetRow((uint)(RowTransition + Origin.GetRow()));
            Destination.SetColumn((uint)(ColumnTransition + Origin.GetColumn()));
            Destination.SetBoardDimension(Origin.GetBoardDimension());
        }

        public void SetOriginCalculateDestination(KMBoardLocation OriginSource)
        {
            if (OriginSource.IsValid() == true)
            {
                Origin = new KMBoardLocation(OriginSource);
                CalculateDestination();
            }
            else
            {
                throw new ApplicationException("KMMove::SetOriginCalculateDestination : Parameter OriginSource is not valid!\n");
            }
        }

        public KMBoardLocation GetNextLocation() { return Destination; }
        public void SetBoardDimension(uint boardDimension) { BoardDimension = (int)(boardDimension); }
        public int GetBoardDimension() { return BoardDimension; }
        public bool IsValid() { return ((((RowTransition == 0) || (ColumnTransition == 0)) || (!Destination.IsValid())) ? false : true); }

        public void SetTransitionsAndDimension(int rowTransition,
            int columnTransition, uint boardDimension) {
            RowTransition = rowTransition;
            ColumnTransition = columnTransition;
            BoardDimension = (int)(boardDimension);
        }
        
        public void MakeNamesForOutPut()
        {
            if (Origin.IsValid() == true) {
                Origin.MakeName();
            }
            if (Destination.IsValid() == true) {
                Destination.MakeName();
            }
        }

        public void PrintMove()
        {
            Console.Write("Move from ");
            Origin.PrintLocation();
            Console.Write(" To ");
            Destination.PrintLocation();
            Console.WriteLine();
        }
    };

}