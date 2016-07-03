    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace KnightMoves_CSharp
    {
        class KMOutputData {
            /*
             * This class reports the data after the test is complete. Each successful
             * path is stored for output. Statistics on all the successful paths are
             * always provided, printing all the moves in the successful paths is
             * optional. A count of all attempted paths is maintained and reported.
             * The current configuration is a command line program with text output,
             * this class could be converted to providing graphic output.
             */
            protected KMBoardLocation m_Origin;
            protected KMBoardLocation m_Destination;
            protected uint m_BoardDimension;
            protected uint m_AttemptedPaths;
            protected KnightMovesMethodLimitations m_LimitationsOnMoves;
            protected bool m_ShowPathData;
            protected List<KMPath> m_PathRecords;

            public void IncrementAttemptedPaths() { m_AttemptedPaths++; }
            public int GetPathCount() { return m_PathRecords.Count; }
            public List<KMPath> GetAllPaths() { return m_PathRecords; }
            public uint GetAttempts()  { return m_AttemptedPaths; }
            public KMBoardLocation GetPointOfOrigin() { return m_Origin; }
            public KMBoardLocation GetDestination() { return m_Destination; }
            public uint GetBoardDimension() { return m_BoardDimension; }
            // Statistics on the returned paths.
            public KnightMovesMethodLimitations GetSlicingMethod() { return m_LimitationsOnMoves; }
            public void DontShowPathData() { m_ShowPathData = false; }
            public void ShowPathData() { m_ShowPathData = true; }
            public void ShowPathData(bool Enabled) { m_ShowPathData = Enabled; }
            public bool GetShowPathData() { return m_ShowPathData; }

            public KMOutputData(KMBoardLocation Origin, KMBoardLocation Destination, uint BoardDimension, KnightMovesMethodLimitations SliceFilterType)
            {
                m_BoardDimension = BoardDimension;
                m_AttemptedPaths = 0;
                m_LimitationsOnMoves = SliceFilterType;
                m_ShowPathData = false;
                m_PathRecords = new List<KMPath>();
                m_Origin = new KMBoardLocation(Origin);
                m_Destination = new KMBoardLocation(Destination);
            }

            public void AddPath(KMPath PathData)
            {
                m_PathRecords.Add(new KMPath(PathData));
            }

            public void ShowStats()
            {
                List<int> PathLengths = new List<int>();

                if (m_PathRecords.Count < 1) {  // prevent division by zero
                    return;
                    throw new ApplicationException("KMOutputData::ShowStats : No paths were recorded!\n");
                }

                foreach (KMPath PathsIter in m_PathRecords)
                {
                    PathLengths.Add(PathsIter.GetLength());
                }
    
                PathLengths.Sort();
                Console.WriteLine("The average path length is {0}", PathLengths.Average());
                Console.WriteLine("The median path length is {0}", PathLengths[PathLengths.Count/2]);
                Console.WriteLine("The longest path is {0} moves.", PathLengths.Last()); //PathLengths[PathLengths.Count-1]);
                Console.WriteLine("The shortest path is {0} moves.", PathLengths[0]);
            }

            private void EndOfTestCase()
            {
                for (int i = 0; i < 25; i++) Console.Write("#");
                Console.Write(" End of Test Case");
                m_Origin.PrintLocation();
                Console.Write(" to ");
                m_Destination.PrintLocation();
                for (int i = 0; i < 25; i++) Console.Write("#");

                Console.WriteLine();
                Console.WriteLine();
            }

            public void ShowResults()
            {
                Console.Write("The point of origin for all path searches was ");
                m_Origin.PrintLocation();
                Console.WriteLine();
                Console.Write("The destination point for all path searches was ");
                m_Destination.PrintLocation();
                Console.WriteLine();
                Console.WriteLine("The number of squares on each edge of the board is {0}", m_BoardDimension);
                OutputSlicingMethodlogy();
                Console.WriteLine("There are {0} Resulting Paths", m_PathRecords.Count);
                Console.WriteLine("There were {0} attempted paths", m_AttemptedPaths);
                ShowStats();    

                if (m_ShowPathData)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Here is the listing of the paths");
                    Console.WriteLine();
                    foreach (KMPath Path in m_PathRecords)
                    {
                        Path.PrintPath();
                    }
                }
                EndOfTestCase();
            }
       
            protected void OutputSlicingMethodlogy()
            {
                switch (m_LimitationsOnMoves) {
                default :
                    throw new ApplicationException("KnightMovesTIO::OutputSlicingMethodlogy : Unknown type of Path Limitation.");
                case KnightMovesMethodLimitations.DenyByPreviousLocation :
                    Console.WriteLine("The slicing methodology used to further limit searches was no repeat visits to squares on the board.");
                    break;
                case KnightMovesMethodLimitations.DenyByPreviousRowOrColumn:
                    Console.WriteLine("The slicing methodology used to further limit searches was no repeat visits to any rows or columns.");
                    break;
                }
    
            }
        };
    }
