using System.Collections.Generic;
using System.ComponentModel;

namespace AsmTest
{
	public class Lexer
	{
		private string input;
		private int start, pos;
		private List<Token> tokens = new List<Token>();
		private BindingList<string> errors;

		private Position curPos
		{
			get
			{
				int l = 1, c = 1;
				bool cl = true;
				for (var i = start - 1; i >= 0; i--)
				{
					if (input[i] == '\n')
					{
						l++;
						cl = false;
					}
					else if (cl) { c++; }
				}
				return new Position { Char = c, Line = l };
			}
		}

		private string curVal { get { return input.Substring(start, pos - start); } }

		public Lexer(BindingList<string> errors)
		{
			this.errors = errors;
		}

		public List<Token> Lex(string input)
		{
			this.input = input + '\0';
			start = 0;
			pos = 0;
			tokens.Clear();
			while (true)
			{
				char c = next();
				if (c == '\0')
				{
					break;
				}
				else if (c == ';')
				{
					while (peek != '\n' && peek != '\0') { pos++; }
					discard();
				}
				else if (char.IsLetter(c))
				{
					while (char.IsLetterOrDigit(peek)) { pos++; }
					if (peek == ':')
					{
						emit(Token.TType.Label);
						pos++;
						discard();
					}
					else { emit(Token.TType.Identifier); }
				}
				else if (char.IsDigit(c) || c == '-')
				{
					while (char.IsDigit(peek)) { pos++; }
					if (curVal == "-")
					{
						errors.Add(curPos + " '-' with no number");
						discard();
					}
					else { emit(Token.TType.Number); }
				}
				else if (char.IsWhiteSpace(c)) { discard(); }
				else
				{
					backup();
					errors.Add(curPos + " Unknown char '" + peek + "'");
					next();
					discard();
				}

			}
			return tokens;
		}

		private char peek { get { return input[pos]; } }

		private char next()
		{
			char c = peek;
			pos++;
			return c;
		}

		private void backup() { pos--; }

		private void discard() { start = pos; }

		private void emit(Token.TType type)
		{
			tokens.Add(new Token(type, curPos, curVal));
			start = pos;
		}
	}

	public class Token
	{
		public enum TType
		{
			Duplicate,
			Identifier,
			Label,
			Number,
			Push,
			Return
		}

		public TType Type;
		public Position Pos;
		public string Val;

		public Token(TType type, Position pos, string val)
		{
			if (Keywords.ContainsKey(val)) { Type = Keywords[val]; }
			else { Type = type; }
			Pos = pos;
			Val = val;
		}

		public readonly static Dictionary<string, TType> Keywords = new Dictionary<string, TType>
		{
			{"dup",     TType.Duplicate},
			{"push",    TType.Push},
			{"ret",     TType.Return},
		};
	}

	public class Position
	{
		public int Line, Char;
		public override string ToString() { return Line + ":" + Char; }
	}
}
