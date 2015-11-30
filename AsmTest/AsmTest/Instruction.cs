namespace AsmTest
{
	public class Command
	{
		public enum IType
		{
			Push,
			Return
		}

		public string Label;
		public IType Type;
		public Arg Argument;

		public class Arg
		{
			public enum AType
			{
				Input,
				Number
			}

			public AType Type;
			public int Val;
		}
	}
}
