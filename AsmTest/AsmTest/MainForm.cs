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
					if (value) { Init(); }
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

		private BuiltProgram Program;

		public MainForm()
		{
			InitializeComponent();
			Program = new BuiltProgram();
			listBoxStack.DataSource = Program.Stack;
			listBoxRegisters.DataSource = Program.Registers;
			listBoxInput.DataSource = Program.Input;
			listBoxExpected.DataSource = Program.Expected;
			listBoxOutput.DataSource = Program.Output;
			listBoxErrors.DataSource = Program.Errors;
			Init();
		}

		private void Init()
		{
			Program.Init(new[] { 1, 2, 3, 4, 5 }, new[] { 2, 4, 6, 8, 10 }, richTextBoxInstructions.Text);
		}

		private void buttonStop_Click(object sender, EventArgs e) { Stop(); }

		private void Stop()
		{
			Running = false;
			timer.Stop();
			Program.Reset();
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

		private void timer_Tick(object sender, EventArgs e) { Tick(); }

		private void Tick()
		{
			Program.Tick();
			if (Program.Status != Status.Running)
			{
				Stop();
				if (Program.Status == Status.Success)
				{
					MessageBox.Show("Success!");
				}
			}
		}

		private void richTextBoxInstructions_TextChanged(object sender, EventArgs e)
		{
			Program.Build(richTextBoxInstructions.Text);
			CanRun = Program.CanRun;
		}
	}
}
