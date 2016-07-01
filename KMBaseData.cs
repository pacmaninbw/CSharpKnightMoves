using System;
namespace KnightMoves_CSharp
{
    class KMBaseData {
        public uint StartRow;
        public uint StartColumn;
        public string StartName;
        public uint TargetRow;
        public uint TargetColumn;
        public string TargetName;
        public uint DimensionOneSide;
        public KnightMovesMethodLimitations LimitationsOnMoves;
        public KMBaseData(uint startRow, uint startColumn, string Name, uint targetRow, uint targetColumn, string targetName, uint boardSize, KnightMovesMethodLimitations limitations)
        {
            StartRow = startRow;
            StartColumn = startColumn;
            StartName = Name;
            TargetRow = targetRow;
            TargetColumn = targetColumn;
            TargetName = targetName;
            DimensionOneSide = boardSize;
            LimitationsOnMoves = limitations;
        }

        public void Print_Input_Data()
        {
            Console.Write("{0}\t{1}\t{2}\t", StartName, TargetName, DimensionOneSide);
            switch (LimitationsOnMoves)
            {
                default:
                    throw new ArgumentException("LetUserEnterTestCaseNumber : Unknown type of Path Limitation.");
                case KnightMovesMethodLimitations.DenyByPreviousLocation:
                    Console.WriteLine("Can't return to previous location");
                    break;
                case KnightMovesMethodLimitations.DenyByPreviousRowOrColumn:
                    Console.WriteLine("Can't return to previous row or column");
                    break;
            }
        }
    };
}