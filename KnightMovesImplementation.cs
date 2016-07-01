using System;
using System.Collections.Generic;

namespace KnightMoves_CSharp
{
    class KnightMovesImplementation {
        KMBoardLocation PointOfOrigin;
        KMBoardLocation Destination;
        uint SingleSideBoardDimension;
        KnightMovesMethodLimitations PathLimitations;
        KMOutputData Results;
        KMMoveFilters MoveFilter;
        KMPath m_Path;
        
        public KnightMovesImplementation(KMBaseData UserInputData)
        {
            SingleSideBoardDimension = UserInputData.DimensionOneSide;
            PathLimitations = UserInputData.LimitationsOnMoves;
            InitPointOfOrigin(UserInputData);
            InitDestination(UserInputData);
            Results = new KMOutputData(PointOfOrigin, Destination, SingleSideBoardDimension, PathLimitations);
            MoveFilter = new KMMoveFilters(PointOfOrigin, SingleSideBoardDimension, PathLimitations);
            m_Path = new KMPath();
        }
        
        public KMOutputData CalculatePaths()
        {
            List<KMMove> PossibleFirstMoves = MoveFilter.GetPossibleMoves(PointOfOrigin);
    
            if (PossibleFirstMoves.Count == 0)
            {
                // Anywhere on the board should have at between 2 and 8 possible moves
                throw new ApplicationException("KnightMovesImplementation::CalculatePaths: No Possible Moves.");
            }
            else
            {
                foreach (KMMove CurrentMove in PossibleFirstMoves)
                {
                    CurrentMove.SetOriginCalculateDestination(PointOfOrigin);
                    if (CurrentMove.IsValid() == true) {
                        CalculatePath(CurrentMove);
                    }
                }
            }
            return Results;
        }

        protected void InitPointOfOrigin(KMBaseData UserInputData)
        {
            PointOfOrigin = new KMBoardLocation(UserInputData.StartRow, UserInputData.StartColumn, SingleSideBoardDimension);
            PointOfOrigin.SetName(UserInputData.StartName);
        }

        protected void InitDestination(KMBaseData UserInputData)
        {
            Destination = new KMBoardLocation(UserInputData.TargetRow, UserInputData.TargetColumn, SingleSideBoardDimension);
            Destination.SetName(UserInputData.TargetName);
        }

        protected void CalculatePath(KMMove CurrentMove)
        {
            KMBoardLocation CurrentLocation = CurrentMove.GetNextLocation();
//            CurrentMove.PrintMove();
            m_Path.AddMoveToPath(CurrentMove);
//            m_Path.PrintPath();
            MoveFilter.PushVisited(CurrentLocation);

                if (Destination.IsSameLocation(CurrentLocation) == true)
                {
                    Results.IncrementAttemptedPaths();
                    Results.AddPath(m_Path);
                }
                else
                {
                    if (CurrentMove.IsValid() == true)
                    {
                        List<KMMove> PossibleMoves = MoveFilter.GetPossibleMoves(CurrentLocation);
                        if (PossibleMoves.Count != 0)
                        {
                            foreach (KMMove NextMove in PossibleMoves)
                            {
                                CalculatePath(NextMove);
                            }
                        }
                        else
                        {
                            Results.IncrementAttemptedPaths();
                        }
                    }
                    else
                    {
                        throw new ApplicationException("In KnightMovesImplementation::CalculatePath CurrentLocation Not Valid");
                    }
                }
                // Backup to previous location
            MoveFilter.PopVisited();
            m_Path.RemoveLastMove();
        }
    };
}