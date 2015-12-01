using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AsmTest
{
	public class Command
	{
		public List<string> Labels;
		public Token Type;
		public Token Arg;

		public override string ToString()
		{
			var sb = new StringBuilder();
			if (Labels != null && Labels.Count > 0)
			{
				sb.Append("[");
				sb.Append(string.Join(", ", Labels));
				sb.Append("] ");
			}
			sb.Append(Type.Type);
			if (Arg != null)
			{
				sb.Append(" ");
				if (Arg.Type == TokenType.Number || Arg.Type == TokenType.Identifier) { sb.Append(Arg.Val); }
				else { sb.Append(Arg.Type); }
			}
			return sb.ToString();
		}
	}

	public class Parser
	{
		private Lexer Lexer;
		private BindingList<Error> Errors;
		private List<Token> Tokens;

		public Parser(BindingList<Error> errors)
		{
			Errors = errors;
			Lexer = new Lexer(errors);
		}

		public List<Command> Parse(string input)
		{
			var cmds = new List<Command>();
			Tokens = Lexer.Lex(input);
			var lbls = new List<string>();
			while (Tokens.Count > 0)
			{
				var toks = GetCurLineTokens();
				bool cmd = false;
				for (int i = 0; i < toks.Count; i++)
				{
					if (toks[i].Type == TokenType.Label)
					{
						if (!cmd) { lbls.Add(toks[i].Val); }
						else
						{
							Errors.Add(new Error("Labels must be at the start of a line", toks[i].Pos));
						}
					}
					else if (!cmd)
					{
						if (toks[i].IsJump)
						{
							if (i + 1 < toks.Count && toks[i + 1].Type == TokenType.Identifier)
							{
								cmds.Add(new Command { Labels = lbls, Type = toks[i], Arg = toks[i + 1] });
								i++;
							}
							else { Errors.Add(new Error("Jump missing label", toks[i].Pos)); }
						}
						else if (toks[i].IsNoArg)
						{
							cmds.Add(new Command { Labels = lbls, Type = toks[i] });
						}
						else if (toks[i].Type == TokenType.Push)
						{
							if (i + 1 < toks.Count && toks[i + 1].IsInput)
							{
								cmds.Add(new Command { Labels = lbls, Type = toks[i], Arg = toks[i + 1] });
								i++;
							}
							else { Errors.Add(new Error("Push missing input", toks[i].Pos)); }
						}
						else if (toks[i].Type == TokenType.Pop)
						{
							if (i + 1 < toks.Count && toks[i + 1].IsOutput)
							{
								cmds.Add(new Command { Labels = lbls, Type = toks[i], Arg = toks[i + 1] });
								i++;
							}
							else { Errors.Add(new Error("Pop missing output", toks[i].Pos)); }
						}
						else { Errors.Add(new Error("Invalid token " + toks[i].Type, toks[i].Pos)); }

						cmd = true;
						lbls = new List<string>();
					}
					else { Errors.Add(new Error("One command per line.", toks[i].Pos)); }
				}
			}
			// Add any labels from the end back on to the beginning.
			if (cmds.Count > 0) { cmds[0].Labels.AddRange(lbls); }

			SanityCheck(cmds);

			return cmds;
		}

		private void SanityCheck(List<Command> cmds)
		{
			// duplicate labels
			for (int i = 0; i < cmds.Count; i++)
			{
				foreach (string l in cmds[i].Labels)
				{
					for (int j = i + 1; j < cmds.Count; j++)
					{
						if (cmds[j].Labels.Contains(l))
						{
							Errors.Add(new Error("Duplicate label '" + l + "'", cmds[j].Type.Pos));
						}
					}
				}
			}
			// jmp labels exist
			foreach (var c in cmds)
			{
				if (c.Type.IsJump)
				{
					bool exists = false;
					for (int i = 0; i < cmds.Count && !exists; i++)
					{
						if (cmds[i].Labels.Contains(c.Arg.Val)) { exists = true; }
					}
					if (!exists)
					{
						Errors.Add(new Error("Missing label '" + c.Arg.Val + "'", c.Arg.Pos));
					}
				}
			}
		}

		private List<Token> GetCurLineTokens()
		{
			var toks = new List<Token>();
			int line = Tokens[0].Pos.Line;
			for (int i = 0; i < Tokens.Count && Tokens[i].Pos.Line == line; i++)
			{
				toks.Add(Tokens[i]);
			}
			Tokens.RemoveRange(0, toks.Count);
			return toks;
		}
	}
}
