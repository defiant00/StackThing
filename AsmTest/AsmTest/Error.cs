namespace AsmTest
{
	public class Error
	{
		public string Value;
		public Position Pos;

		public override string ToString() { return Pos + " " + Value; }

		public Error(string val, Position pos)
		{
			Value = val;
			Pos = pos;
		}
	}
}
