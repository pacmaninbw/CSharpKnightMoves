using System;
using System.Collections.Generic;

namespace KnightMoves_CSharp
{
    public class KMMoveFilters {
        private struct LocationBase { public uint Row; public uint Column; }
        // The 8 possible moves the knight can make.
        public static int MAIXMUM_POSSIBLE_MOVES = 8;
        private KMMove Left1Up2;
        private KMMove Left2Up1;
        private KMMove Left2Down1;
        private KMMove Left1Down2;
        private KMMove Right1Up2;
        private KMMove Right2Up1;
        private KMMove Right2Down1;
        private KMMove Right1Down2;

        const int Left1 = -1;
        const int Left2 = -2;
        const int Down1 = -1;
        const int Down2 = -2;
        const int Right1 = 1;
        const int Right2 = 2;
        const int Up1 = 1;
        const int Up2 = 2;
    
        List<KMMove> AllPossibleMoves = new List<KMMove>();

        private uint BoardDimension;
        public uint GetBoardDimension() { return BoardDimension; }

        KnightMovesMethodLimitations PathLimitations;
        Stack<LocationBase> VisitedLocations = new Stack<LocationBase>();
        Stack<uint> VisitedRows = new Stack<uint>();
        Stack<uint> VisitedColumns = new Stack<uint>();

        public KMMoveFilters(KMBoardLocation Origin, uint SingleSideBoardDimension, KnightMovesMethodLimitations VisitationLimitations)
        {
            PathLimitations = VisitationLimitations;
            BoardDimension = SingleSideBoardDimension;

            Left1Up2 = new KMMove(Left1, Up2, BoardDimension);
            Left2Up1 = new KMMove(Left2, Up1, BoardDimension);
            Left2Down1 = new KMMove(Left2, Down1, BoardDimension);
            Left1Down2 = new KMMove(Left1, Down2, BoardDimension);
            Right1Up2 = new KMMove(Right1, Up2, BoardDimension);
            Right2Up1 = new KMMove(Right2, Up1, BoardDimension);
            Right2Down1 = new KMMove(Right2, Down1, BoardDimension);
            Right1Down2 = new KMMove(Right1, Down2, BoardDimension);

            AllPossibleMoves.Add(Left1Up2);
            AllPossibleMoves.Add(Left2Up1);
            AllPossibleMoves.Add(Left2Down1);
            AllPossibleMoves.Add(Left1Down2);
            AllPossibleMoves.Add(Right1Up2);
            AllPossibleMoves.Add(Right2Up1);
            AllPossibleMoves.Add(Right2Down1);
            AllPossibleMoves.Add(Right1Down2);

            // Record the initial location so we never return
            LocationBase PtOfOrigin = new LocationBase();
            PtOfOrigin.Row = Origin.GetRow();
            PtOfOrigin.Column = Origin.GetColumn();
            VisitedLocations.Push(PtOfOrigin);
        }
        /*
         * C# does not seem to provide a default copy constructor so copy AllPossibleMoves
         */
        private List<KMMove> GetAllPossibleMoves() { return CopyAllPossibleMoves(); }

        private List<KMMove> CopyAllPossibleMoves()
        {
            List<KMMove> PublicCopy = new List<KMMove>();
            foreach (KMMove PossibleMove in AllPossibleMoves)
            {
                PublicCopy.Add(new KMMove(PossibleMove));
            }

            return PublicCopy;
        }

        public List<KMMove> GetPossibleMoves(KMBoardLocation Origin)
        {
            if (Origin.IsValid() == false)
            {
                throw new ArgumentException("KMMoveFilters::GetPossibleMoves : Parameter Origin is not valid!\n");
            }

            List<KMMove> TempAllPossibleMoves = GetAllPossibleMoves();
            List<KMMove> PossibleMoves = new List<KMMove>();
            foreach (KMMove PossibeMove in TempAllPossibleMoves)
            {
                PossibeMove.SetOriginCalculateDestination(Origin);
                if ((PossibeMove.IsValid() == true) && (IsNotPreviouslyVisited(PossibeMove.GetNextLocation()) == true))
                {
                    PossibleMoves.Add(PossibeMove);
                }
            }
            return PossibleMoves;
        }

        protected bool IsNotPreviouslyVisited(KMBoardLocation PossibleDestination)
        {
            bool NotPrevioslyVisited = true;
            LocationBase PossibleLocation;
            
            PossibleLocation.Row = PossibleDestination.GetRow();
            PossibleLocation.Column = PossibleDestination.GetColumn(); 
            // We can't ever go back to a previously visited location
            if (VisitedLocations.Count != 0)
            {
                if (VisitedLocations.Contains(PossibleLocation) == true)
                {
                    NotPrevioslyVisited = false;
                }
            }
            switch (PathLimitations)
            {
                default:
                    throw new ArgumentException("KMPath::CheckMoveAgainstPreviousLocations : Unknown type of Path Limitation.");
                case KnightMovesMethodLimitations.DenyByPreviousLocation:
                    // Handled above by VisitedLocations.Contains().
                    break;
                case KnightMovesMethodLimitations.DenyByPreviousRowOrColumn:
                    if ((VisitedRows.Count != 0) && (VisitedColumns.Count != 0))
                    {
                        if (VisitedRows.Contains(PossibleDestination.GetRow()) == true)
                        {
                            NotPrevioslyVisited = false;
                            break;
                        }
                        if (VisitedColumns.Contains(PossibleDestination.GetColumn()) == true)
                        {
                            NotPrevioslyVisited = false;
                            break;
                        }
                    }
                    break;
            }

            return NotPrevioslyVisited;
        }

        public void PushVisited(KMBoardLocation Location)
        {
            LocationBase TestLocation = new LocationBase();

            TestLocation.Row = Location.GetRow();
            TestLocation.Row = Location.GetRow();

            VisitedLocations.Push(TestLocation);
            VisitedRows.Push(Location.GetRow());
            VisitedColumns.Push(Location.GetColumn());
        }

        public void PopVisited()
        {
            VisitedRows.Pop();
            VisitedColumns.Pop();
            VisitedLocations.Pop();
        }

    };

}
