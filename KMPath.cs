using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace KnightMoves_CSharp
    {
        public class KMPath {
            /*
             *  This class maintains a history of the current path for reporting purposes
             *  as well as for the search algorithm.
             */
            private Stack<KMMove> RecordOfMoves;

            public void AddMoveToPath(KMMove Move) { RecordOfMoves.Push(Move); }

            public KMPath()
            {
                RecordOfMoves = new Stack<KMMove>();
            }

            public KMPath(KMPath Original)
            {
                RecordOfMoves = Original.GetRecordOfMoves();
            }

            public int GetLength() { return RecordOfMoves.Count; }

            /*
             * C# does not provide a default copy constructor so copy RecordOfMoves
             */
            public Stack<KMMove> GetRecordOfMoves()
            {
                Stack<KMMove> CopyOfRecordOfMoves = new Stack<KMMove>();
                foreach (KMMove Record in RecordOfMoves)
                {
                    CopyOfRecordOfMoves.Push(new KMMove(Record));
                }
                return CopyOfRecordOfMoves;
            }

            /*
             * Print the path in the order in which the move were made.
             */
            public void PrintPath()
            {
                Console.WriteLine("This path contains {0} moves", RecordOfMoves.Count);
                List<KMMove> MoveListReorder = new List<KMMove>();

                foreach (KMMove CurrentMove in RecordOfMoves)
                {
                    MoveListReorder.Add(CurrentMove);
                }

                MoveListReorder.Reverse();

                foreach (KMMove CurrentMove in MoveListReorder)
                {
                    CurrentMove.MakeNamesForOutPut();
                    CurrentMove.PrintMove();
                    Console.WriteLine();
                }
            }

            public KMMove GetLastMove()
            {
                KMMove LastMove;
                if (RecordOfMoves.Count > 0) {
                    LastMove = RecordOfMoves.Peek();
                }
                else
                {
                    throw new ArgumentException("In KMPath::GetLastMove() : There was no last move");
                }
    
                return LastMove;
            }

            public void RemoveLastMove()
            {
                if (RecordOfMoves.Count > 0)
                {
                    RecordOfMoves.Pop();
                }
            }

        };
    }
