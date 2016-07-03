    using System;
    using System.Collections.Generic;

    namespace KnightMoves_CSharp
    {
        class KnightMovesImplementation {
            /*
             * This class provides the search for all the paths a Knight on a chess
             * board can take from the point of origin to the destination. It
             * implements a modified Knights Tour. The classic knights tour problem
             * is to visit every location on the chess board without returning to a
             * previous location. That is a single path for the knight. This
             * implementation returns all possible paths from point a to point b.
             *      
             * The current implementation is a Recursive Breadth First Search. Conceptually
             * the algorithm implements a B+ tree with a maximum of 8 possible branches
             * at each level. The root of the tree is the point of origin. A particular
             * path terminates in a leaf. A leaf is the result of either reaching the
             * destination, or reaching a point where there are no more branches to
             * traverse.
             * 
             * The public interface CalculatePaths establishes the root and creates
             * the first level of branching. The protected interface CalculatePath
             * performs the recursive depth first search, however, the
             * MoveFilters.GetPossibleMoves() function it calls performs a breadth
             * first search of the current level.
             */
            private KMBoardLocation PointOfOrigin;
            private KMBoardLocation Destination;
            private uint SingleSideBoardDimension;
            private KnightMovesMethodLimitations PathLimitations;
            private KMOutputData Results;
            private KMMoveFilters MoveFilter;
            private KMPath m_Path;
        
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
            /*
             * Recursive algorith that performs a depth search.
             * The call to CurrentMove.GetNextLocation() implements the breadth-first portion
             * of the search.
             */
            protected void CalculatePath(KMMove CurrentMove)
            {
                KMBoardLocation CurrentLocation = CurrentMove.GetNextLocation();
                m_Path.AddMoveToPath(CurrentMove);
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
