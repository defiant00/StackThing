using System;
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
						listBoxStack.Items.Clear();
					}
				}
			}
		}
		private bool _running = false;

		public MainForm()
		{
			InitializeComponent();
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
			listBoxStack.Items.Add(listBoxStack.Items.Count);
		}
	}
}
