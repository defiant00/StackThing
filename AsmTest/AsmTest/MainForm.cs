using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AsmTest
{
	public partial class MainForm : Form
	{
		private bool Running
		{
			get { return _running; }
			set
			{
				if (value != _running)
				{
					_running = value;
					richTextBoxInstructions.ReadOnly = value;
					if (value)
					{
						Init();
					}
				}
			}
		}
		private bool _running = false;

		private bool CanRun
		{
			set
			{
				buttonStep.Enabled = value;
				buttonRun.Enabled = value;
				buttonFast.Enabled = value;
			}
		}

		private BindingList<int> Stack = new BindingList<int>();
		private BindingList<int> In = new BindingList<int>();
		private BindingList<int> Out = new BindingList<int>();
		private BindingList<string> Errors = new BindingList<string>();
		private Lexer Lexer;

		public MainForm()
		{
			InitializeComponent();
			listBoxStack.DataSource = Stack;
			listBoxInput.DataSource = In;
			listBoxOutput.DataSource = Out;
			listBoxErrors.DataSource = Errors;
			Lexer = new Lexer(Errors);
			Init();
		}

		private void Init()
		{
			Stack.Clear();
			In.Clear();
			foreach (int i in new[] { 1, 2, 3, 4, 5 }) { In.Add(i); }
			Out.Clear();
			Errors.Clear();
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			Running = false;
			timer.Stop();
		}

		private void buttonStep_Click(object sender, EventArgs e)
		{
			Running = true;
			timer.Stop();
			Tick();
		}

		private void buttonRun_Click(object sender, EventArgs e)
		{
			Running = true;
			timer.Interval = 100;
			timer.Start();
		}

		private void buttonFast_Click(object sender, EventArgs e)
		{
			Running = true;
			timer.Interval = 10;
			timer.Start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			Tick();
		}

		private void Tick()
		{
		}

		private void richTextBoxInstructions_TextChanged(object sender, EventArgs e)
		{
			Build();
			CanRun = Errors.Count == 0;
		}

		private void Build()
		{
			Errors.Clear();
			var toks = Lexer.Lex(richTextBoxInstructions.Text);
			foreach(var t in toks)
			{
				Errors.Add(t.Pos + " (" + t.Type + ") " + t.Val);
			}
		}
	}
}
