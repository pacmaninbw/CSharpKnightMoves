    using System;
    using System.Collections.Generic;

    namespace KnightMoves_CSharp
    {
        class KMTestData
        {
            private List<KMBaseData> TestCases;
            /*
             * Provides multiple test cases but does not affect the overall performance of the algorithm.
             * The use is allowed to choose the test cases.
             */
            public KMTestData()
            {
                TestCases = new List<KMBaseData>();
                const uint DefaultBoardSize = GlobalConstants.DefaultBoardDimensionOnOneSide;
                const KnightMovesMethodLimitations DefaultLimitation = KnightMovesMethodLimitations.DenyByPreviousRowOrColumn;
                const uint TestHalfMaximum = GlobalConstants.MaximumBoardDimension / 2;
                const uint TestMaximum = GlobalConstants.MaximumBoardDimension;
                List<KMBaseData> TestData = new List<KMBaseData>();

                TestCases.Add(new KMBaseData(1, 3, "A3", 8, 4, "H4", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 1, "A1", 8, 8, "H8", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 8, "A8", 8, 1, "H1", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(2, 3, "B3", 8, 4, "H4", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(2, 3, "B3", 8, 8, "H8", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(3, 1, "C1", 8, 4, "H4", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 3, "A3", 8, 8, "H8", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 3, "A3", 8, 3, "H4", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(8, 4, "H4", 1, 3, "A3", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(4, 4, "D4", 1, 8, "A8", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(4, 4, "D4", 5, 6, "E6", DefaultBoardSize, DefaultLimitation));
                // Minimum path length should be one move for the following 4 test cases.
                TestCases.Add(new KMBaseData(1, 3, "A3", 2, 5, "B5", DefaultBoardSize, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 3, "A3", 2, 5, "B5", TestHalfMaximum, DefaultLimitation));
                TestCases.Add(new KMBaseData(1, 3, "A3", 2, 5, "B5", DefaultBoardSize, KnightMovesMethodLimitations.DenyByPreviousLocation));
                TestCases.Add(new KMBaseData(1, 3, "A3", 2, 5, "B5", TestMaximum, DefaultLimitation));
            }

            private static bool HasDenyByPreviousLocation(KMBaseData TestCase)
            {
                return (TestCase.LimitationsOnMoves == KnightMovesMethodLimitations.DenyByPreviousLocation);
            }
            private static bool HasMaxBoardSize(KMBaseData TestCase)
            {
                return (TestCase.DimensionOneSide == GlobalConstants.MaximumBoardDimension);
            }

            public List<KMBaseData> LetUserEnterTestCaseNumber()
            {
                int i = 1;
                int Choice = -1;

                Console.WriteLine("Select the number of the test case you want to run.");
                Console.WriteLine("Test Case #\tStart Name\tTarget Name\tBoard Size\tSlicing Method");
                foreach (KMBaseData TestCase in TestCases)
                {
                    Console.Write("{0}\t", i);
                    TestCase.Print_Input_Data();
                    i++;
                }
                Console.WriteLine("{0}\tAll of the above except for {1} and {2}\n", i, (i-1), (i-2));
                Console.WriteLine("{0}\tAll of the above (Go get lunch)\n", ++i);
                Choice = int.Parse(Console.ReadLine());

                if (Choice == (TestCases.Count + 1))
                {
                    TestCases.RemoveAll(HasDenyByPreviousLocation);
                    TestCases.RemoveAll(HasMaxBoardSize);
                }
                else
                {
                    if (Choice <= TestCases.Count)
                    {
                        List<KMBaseData> SingleTestCase = new List<KMBaseData>();
                        SingleTestCase.Add(TestCases[Choice - 1]);
                        return SingleTestCase;
                    }
                }
                return TestCases;
            }

            public void PrintTestCases()
            {
            }
        }
    }
