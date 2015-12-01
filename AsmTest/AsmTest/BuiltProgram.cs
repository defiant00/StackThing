using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsmTest
{
	public enum Status
	{
		Running,
		Success,
		Error
	}

	public class BuiltProgram
	{
		public BindingList<int> Stack, Registers, Input, Expected, Output;
		public BindingList<Error> Errors;
		public Status Status;
		private Parser Parser;
		private int InputCounter, ProgramCounter;
		private List<Command> Commands;

		public bool CanRun { get { return Errors.Count == 0; } }

		private bool InputAvailable { get { return InputCounter < Input.Count; } }
		private int NextInput { get { return Input[InputCounter++]; } }

		public void Init(IEnumerable<int> input, IEnumerable<int> expected, string inputText)
		{
			Stack.Clear();
			Registers.Clear();
			for (int i = 0; i < 4; i++) { Registers.Add(0); }
			Input.Clear();
			Expected.Clear();
			Output.Clear();
			foreach (int i in input) { Input.Add(i); }
			foreach (int i in expected) { Expected.Add(i); }
			Status = Status.Running;
			Build(inputText);
		}

		public BuiltProgram()
		{
			Stack = new BindingList<int>();
			Registers = new BindingList<int>();
			Input = new BindingList<int>();
			Expected = new BindingList<int>();
			Output = new BindingList<int>();
			Errors = new BindingList<Error>();
			Parser = new Parser(Errors);
		}

		public void Reset()
		{
			InputCounter = 0;
			ProgramCounter = 0;
		}

		public void Build(string input)
		{
			Errors.Clear();
			Commands = Parser.Parse(input.ToUpper());
			Reset();
		}

		public void Tick()
		{
			ExecuteCurrent();

			if (Errors.Count > 0) { Status = Status.Error; }

			bool eq = true;
			if (Expected.Count == Output.Count)
			{
				for (int i = 0; i < Expected.Count && eq; i++)
				{
					if (Expected[i] != Output[i]) { eq = false; }
				}
			}
			else { eq = false; }

			if (eq) { Status = Status.Success; }
		}

		private int Peek { get { return Stack[Stack.Count - 1]; } }

		private int Pop()
		{
			int val = Stack[Stack.Count - 1];
			Stack.RemoveAt(Stack.Count - 1);
			return val;
		}

		private void JumpToLabel(Token arg)
		{
			for (int i = 0; i < Commands.Count; i++)
			{
				if (Commands[i].Labels.Contains(arg.Val))
				{
					ProgramCounter = i;
					return;
				}
			}
			Errors.Add(new Error("Couldn't locate label '" + arg.Val + "' while running", arg.Pos));
			ProgramCounter--;
		}

		private void ExecuteCurrent()
		{
			if (ProgramCounter < Commands.Count)
			{
				var cmd = Commands[ProgramCounter];
				ProgramCounter++;
				if (cmd.Type.Type == TokenType.Add)
				{
					if (Stack.Count > 1)
					{
						int first = Pop();
						int second = Pop();
						Stack.Add(first + second);
					}
					else
					{
						Errors.Add(new Error("Add requires two values", cmd.Type.Pos));
						ProgramCounter--;
					}
				}
				else if (cmd.Type.Type == TokenType.Duplicate)
				{
					if (Stack.Count > 0) { Stack.Add(Peek); }
					else
					{
						Errors.Add(new Error("Duplicate requires a value", cmd.Type.Pos));
						ProgramCounter--;
					}
				}
				else if (cmd.Type.Type == TokenType.Negate)
				{
					if (Stack.Count > 0) { Stack.Add(-Pop()); }
					else
					{
						Errors.Add(new Error("Negate requires a value", cmd.Type.Pos));
						ProgramCounter--;
					}
				}
				else if (cmd.Type.Type == TokenType.Push)
				{
					switch (cmd.Arg.Type)
					{
						case TokenType.Nil:
							Stack.Add(0);
							break;
						case TokenType.Input:
							if (InputAvailable) { Stack.Add(NextInput); }
							else
							{
								Errors.Add(new Error("Input is empty", cmd.Arg.Pos));
								ProgramCounter--;
							}
							break;
						case TokenType.Register1:
							Stack.Add(Registers[0]);
							break;
						case TokenType.Register2:
							Stack.Add(Registers[1]);
							break;
						case TokenType.Register3:
							Stack.Add(Registers[2]);
							break;
						case TokenType.Register4:
							Stack.Add(Registers[3]);
							break;
						case TokenType.Number:
							Stack.Add(Convert.ToInt32(cmd.Arg.Val));
							break;
						default:
							Errors.Add(new Error(cmd.Arg.Type + " is not valid for a Push", cmd.Arg.Pos));
							ProgramCounter--;
							break;
					}
				}
				else if (cmd.Type.Type == TokenType.Pop)
				{
					if (Stack.Count > 0)
					{
						switch (cmd.Arg.Type)
						{
							case TokenType.Nil:
								Pop();
								break;
							case TokenType.Output:
								Output.Add(Pop());
								break;
							case TokenType.Register1:
								Registers[0] = Pop();
								break;
							case TokenType.Register2:
								Registers[1] = Pop();
								break;
							case TokenType.Register3:
								Registers[2] = Pop();
								break;
							case TokenType.Register4:
								Registers[3] = Pop();
								break;
							default:
								Errors.Add(new Error(cmd.Arg.Type + " is not valid for a Pop", cmd.Arg.Pos));
								break;
						}
					}
					else
					{
						Errors.Add(new Error("Pop requires a value", cmd.Type.Pos));
						ProgramCounter--;
					}
				}
				else if (cmd.Type.IsJump)
				{
					if (cmd.Type.Type == TokenType.Jump) { JumpToLabel(cmd.Arg); }
					else if (Stack.Count > 0)
					{
						switch (cmd.Type.Type)
						{
							case TokenType.JumpLessThanZero:
								if (Peek < 0) { JumpToLabel(cmd.Arg); }
								break;
							case TokenType.JumpEqualsZero:
								if (Peek == 0) { JumpToLabel(cmd.Arg); }
								break;
							case TokenType.JumpGreaterThanZero:
								if (Peek > 0) { JumpToLabel(cmd.Arg); }
								break;
						}
					}
					else
					{
						Errors.Add(new Error("Conditional jumps require a value", cmd.Type.Pos));
						ProgramCounter--;
					}
				}
				else { Errors.Add(new Error(cmd.Type.Type + " is not known", cmd.Type.Pos)); }

				while (ProgramCounter >= Commands.Count) { ProgramCounter -= Commands.Count; }
				while (ProgramCounter < 0) { ProgramCounter += Commands.Count; }
			}
		}
	}
}
