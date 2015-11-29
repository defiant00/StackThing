﻿namespace AsmTest
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBoxLegend = new System.Windows.Forms.GroupBox();
			this.groupBoxInstructions = new System.Windows.Forms.GroupBox();
			this.richTextBoxInstructions = new System.Windows.Forms.RichTextBox();
			this.groupBoxStack = new System.Windows.Forms.GroupBox();
			this.listBoxStack = new System.Windows.Forms.ListBox();
			this.groupBoxInOut = new System.Windows.Forms.GroupBox();
			this.listBoxOutput = new System.Windows.Forms.ListBox();
			this.listBoxInput = new System.Windows.Forms.ListBox();
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonStep = new System.Windows.Forms.Button();
			this.buttonRun = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.buttonFast = new System.Windows.Forms.Button();
			this.groupBoxInstructions.SuspendLayout();
			this.groupBoxStack.SuspendLayout();
			this.groupBoxInOut.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxLegend
			// 
			this.groupBoxLegend.Location = new System.Drawing.Point(8, 8);
			this.groupBoxLegend.Name = "groupBoxLegend";
			this.groupBoxLegend.Size = new System.Drawing.Size(184, 496);
			this.groupBoxLegend.TabIndex = 0;
			this.groupBoxLegend.TabStop = false;
			this.groupBoxLegend.Text = "Legend";
			// 
			// groupBoxInstructions
			// 
			this.groupBoxInstructions.Controls.Add(this.richTextBoxInstructions);
			this.groupBoxInstructions.Location = new System.Drawing.Point(200, 8);
			this.groupBoxInstructions.Name = "groupBoxInstructions";
			this.groupBoxInstructions.Size = new System.Drawing.Size(288, 496);
			this.groupBoxInstructions.TabIndex = 1;
			this.groupBoxInstructions.TabStop = false;
			this.groupBoxInstructions.Text = "Instructions";
			// 
			// richTextBoxInstructions
			// 
			this.richTextBoxInstructions.DetectUrls = false;
			this.richTextBoxInstructions.Location = new System.Drawing.Point(8, 24);
			this.richTextBoxInstructions.Name = "richTextBoxInstructions";
			this.richTextBoxInstructions.Size = new System.Drawing.Size(272, 464);
			this.richTextBoxInstructions.TabIndex = 0;
			this.richTextBoxInstructions.Text = "";
			// 
			// groupBoxStack
			// 
			this.groupBoxStack.Controls.Add(this.listBoxStack);
			this.groupBoxStack.Location = new System.Drawing.Point(528, 8);
			this.groupBoxStack.Name = "groupBoxStack";
			this.groupBoxStack.Size = new System.Drawing.Size(80, 480);
			this.groupBoxStack.TabIndex = 2;
			this.groupBoxStack.TabStop = false;
			this.groupBoxStack.Text = "Stack";
			// 
			// listBoxStack
			// 
			this.listBoxStack.FormattingEnabled = true;
			this.listBoxStack.Location = new System.Drawing.Point(8, 24);
			this.listBoxStack.Name = "listBoxStack";
			this.listBoxStack.Size = new System.Drawing.Size(64, 446);
			this.listBoxStack.TabIndex = 0;
			// 
			// groupBoxInOut
			// 
			this.groupBoxInOut.Controls.Add(this.listBoxOutput);
			this.groupBoxInOut.Controls.Add(this.listBoxInput);
			this.groupBoxInOut.Location = new System.Drawing.Point(616, 8);
			this.groupBoxInOut.Name = "groupBoxInOut";
			this.groupBoxInOut.Size = new System.Drawing.Size(160, 480);
			this.groupBoxInOut.TabIndex = 3;
			this.groupBoxInOut.TabStop = false;
			this.groupBoxInOut.Text = "Input / Output";
			// 
			// listBoxOutput
			// 
			this.listBoxOutput.FormattingEnabled = true;
			this.listBoxOutput.Location = new System.Drawing.Point(88, 24);
			this.listBoxOutput.Name = "listBoxOutput";
			this.listBoxOutput.Size = new System.Drawing.Size(64, 446);
			this.listBoxOutput.TabIndex = 1;
			// 
			// listBoxInput
			// 
			this.listBoxInput.FormattingEnabled = true;
			this.listBoxInput.Location = new System.Drawing.Point(8, 24);
			this.listBoxInput.Name = "listBoxInput";
			this.listBoxInput.Size = new System.Drawing.Size(64, 446);
			this.listBoxInput.TabIndex = 0;
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(432, 520);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(80, 32);
			this.buttonStop.TabIndex = 4;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonStep
			// 
			this.buttonStep.Location = new System.Drawing.Point(520, 520);
			this.buttonStep.Name = "buttonStep";
			this.buttonStep.Size = new System.Drawing.Size(80, 32);
			this.buttonStep.TabIndex = 5;
			this.buttonStep.Text = "|| Step";
			this.buttonStep.UseVisualStyleBackColor = true;
			this.buttonStep.Click += new System.EventHandler(this.buttonStep_Click);
			// 
			// buttonRun
			// 
			this.buttonRun.Location = new System.Drawing.Point(608, 520);
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.Size = new System.Drawing.Size(80, 32);
			this.buttonRun.TabIndex = 6;
			this.buttonRun.Text = "|> Run";
			this.buttonRun.UseVisualStyleBackColor = true;
			this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
			// 
			// timer
			// 
			this.timer.Interval = 10;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// buttonFast
			// 
			this.buttonFast.Location = new System.Drawing.Point(696, 520);
			this.buttonFast.Name = "buttonFast";
			this.buttonFast.Size = new System.Drawing.Size(80, 32);
			this.buttonFast.TabIndex = 7;
			this.buttonFast.Text = ">> Fast";
			this.buttonFast.UseVisualStyleBackColor = true;
			this.buttonFast.Click += new System.EventHandler(this.buttonFast_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.buttonFast);
			this.Controls.Add(this.buttonRun);
			this.Controls.Add(this.buttonStep);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.groupBoxInOut);
			this.Controls.Add(this.groupBoxStack);
			this.Controls.Add(this.groupBoxInstructions);
			this.Controls.Add(this.groupBoxLegend);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Stack Asm Thing";
			this.groupBoxInstructions.ResumeLayout(false);
			this.groupBoxStack.ResumeLayout(false);
			this.groupBoxInOut.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxLegend;
		private System.Windows.Forms.GroupBox groupBoxInstructions;
		private System.Windows.Forms.RichTextBox richTextBoxInstructions;
		private System.Windows.Forms.GroupBox groupBoxStack;
		private System.Windows.Forms.ListBox listBoxStack;
		private System.Windows.Forms.GroupBox groupBoxInOut;
		private System.Windows.Forms.ListBox listBoxOutput;
		private System.Windows.Forms.ListBox listBoxInput;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonStep;
		private System.Windows.Forms.Button buttonRun;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Button buttonFast;
	}
}
