namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public override char Identifier => 'R';

        private static Point DefaultPoint(int x, Team team)
            => new Point(x, team == Team.White ? 0 : 7);

        public Rook(Point point, Team team) : base(point, team) { }

        /// <summary>
        /// Use default y position by team
        /// </summary>
        public Rook(int x, Team team) : base(DefaultPoint(x, team), team) { }

        protected override bool DoesDirectionCorrect(Point delta)
            => (crossUnitLength == delta.Unit.Abs);
    }
}