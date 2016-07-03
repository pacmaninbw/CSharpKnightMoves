    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace KnightMoves_CSharp
    {
        class Program
        {
            /*
             * This program is an experiment in performance. It is my first
             * attempt to write a C# program.
             */
            static void OutputOverAllStatistics(List<double> TestTimes)
            {
                if (TestTimes.Count < 1)    // Prevent Division by 0 in Average
                {
                    Console.WriteLine("No test times to run statistics on!");
                    return;
                }

                TestTimes.Sort();   // Sort once for Min, Median and Max
                Console.WriteLine("\nOverall Results");
                Console.WriteLine("The average execution time is {0} seconds.", TestTimes.Average()); 
                Console.WriteLine("The median execution time is {0} seconds.", TestTimes[TestTimes.Count() / 2]);
                Console.WriteLine("The longest execution time is {0} seconds.", TestTimes[TestTimes.Count - 1]);
                Console.WriteLine("The shortest execution time is {0} seconds.", TestTimes[0]);
            }

            static double ExecutionLoop(KMBaseData UserInputData, bool ShowPathData)
            {
                Stopwatch StopWatch = new Stopwatch();

                KnightMovesImplementation KnightPathFinder = new KnightMovesImplementation(UserInputData);
                StopWatch.Start();
                KMOutputData OutputData = KnightPathFinder.CalculatePaths();
                StopWatch.Stop();
                TimeSpan ts = StopWatch.Elapsed;
                Double ElapsedTimeForOutPut = ts.TotalSeconds;

                Console.Write("finished computation at ");
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("elapsed time: {0} Seconds", ElapsedTimeForOutPut);
                Console.WriteLine();
                Console.WriteLine();

                OutputData.ShowPathData(ShowPathData);
                OutputData.ShowResults();

                return ElapsedTimeForOutPut;
            }

            static void Main(string[] args)
            {
                bool ShowPathData = false;
                Console.WriteLine("Do you want to print each of the resulting paths? (y/n)");
                string ShowPathAnswer = Console.ReadLine();
                ShowPathData = (ShowPathAnswer.ToLower() == "y");

                KMTestData TestData = new KMTestData();
                List<KMBaseData> ListOfTestCases = TestData.LetUserEnterTestCaseNumber();

                try
                {
                    List<double> TestTimes = new List<double>();

                    foreach (KMBaseData TestCase in ListOfTestCases)
                    {
                        TestTimes.Add(ExecutionLoop(TestCase, ShowPathData));
                        GC.Collect();
                    }

                    OutputOverAllStatistics(TestTimes);
                    return;
                }

                catch (ArgumentOutOfRangeException e)
                {
                    Console.Error.Write("A fatal range error occurred in KnightMoves: ");
                    Console.Error.WriteLine(e.ToString());
                    return;
                }
                catch (ArgumentException e)
                {
                    Console.Error.Write("A fatal argument error occurred in KnightMoves: ");
                    Console.Error.WriteLine(e.ToString());
                    return;
                }
                catch (ApplicationException e)
                {
                    Console.Error.Write("A fatal application error occurred in KnightMoves: ");
                    Console.Error.WriteLine(e.ToString());
                    return;
                }
            }
        }
    }
