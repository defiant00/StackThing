using System.Collections.Generic;
using System.ComponentModel;

namespace AsmTest
{
	public class Lexer
	{
		private string input;
		private int start, pos;
		private List<Token> tokens = new List<Token>();
		private BindingList<Error> errors;

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

		public Lexer(BindingList<Error> errors)
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
						emit(TokenType.Label);
						pos++;
						discard();
					}
					else { emit(TokenType.Identifier); }
				}
				else if (char.IsDigit(c) || c == '-')
				{
					while (char.IsDigit(peek)) { pos++; }
					if (curVal == "-")
					{
						errors.Add(new Error("'-' with no number", curPos));
						discard();
					}
					else { emit(TokenType.Number); }
				}
				else if (char.IsWhiteSpace(c)) { discard(); }
				else
				{
					backup();
					errors.Add(new Error("Unknown char '" + peek + "'", curPos));
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

		private void emit(TokenType type)
		{
			tokens.Add(new Token(type, curPos, curVal, errors));
			start = pos;
		}
	}

	public enum TokenType
	{
		Identifier,
		Label,
		commands_start,
		noargs_start,
		Add,
		Negate,
		noargs_end,
		Pop,
		Push,
		commands_end,
		jumps_start,
		Jump,
		JumpLessThanZero,
		JumpEqualsZero,
		JumpGreaterThanZero,
		jumps_end,
		inputs_start,
		Number,
		Input,
		outputs_start,
		Register1,
		Register2,
		Register3,
		Register4,
		inputs_end,
		Output,
		outputs_end,
	}

	public class Token
	{
		public TokenType Type;
		public Position Pos;
		public string Val;

		public bool IsInput { get { return Type > TokenType.inputs_start && Type < TokenType.inputs_end; } }
		public bool IsOutput { get { return Type > TokenType.outputs_start && Type < TokenType.outputs_end; } }
		public bool IsCommand { get { return Type > TokenType.commands_start && Type < TokenType.commands_end; } }
		public bool IsJump { get { return Type > TokenType.jumps_start && Type < TokenType.jumps_end; } }
		public bool IsNoArg { get { return Type > TokenType.noargs_start && Type < TokenType.noargs_end; } }

		public Token(TokenType type, Position pos, string val, BindingList<Error> errors)
		{
			Type = type;
			string lVal = val.ToLower();
			if (Keywords.ContainsKey(lVal)) { Type = Keywords[lVal]; }
			if (type == TokenType.Label && Type != TokenType.Label)
			{
				errors.Add(new Error("Cannot use '" + val + "' as a label", pos));
			}
			Pos = pos;
			Val = val;
		}

		public readonly static Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
		{
			{"add",     TokenType.Add},
			{"in",      TokenType.Input},
			{"jmp",     TokenType.Jump},
			{"jlz",     TokenType.JumpLessThanZero},
			{"jez",     TokenType.JumpEqualsZero},
			{"jgz",     TokenType.JumpGreaterThanZero},
			{"neg",     TokenType.Negate},
			{"out",     TokenType.Output},
			{"pop",     TokenType.Pop},
			{"push",    TokenType.Push},
			{"r1",      TokenType.Register1},
			{"r2",      TokenType.Register2},
			{"r3",      TokenType.Register3},
			{"r4",      TokenType.Register4},
		};
	}

	public class Position
	{
		public int Line, Char;
		public override string ToString() { return Line + ":" + Char; }
	}
}
