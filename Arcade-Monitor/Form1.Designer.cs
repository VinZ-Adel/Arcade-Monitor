namespace Arcade_Monitor
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			label1 = new Label();
			timer1 = new System.Windows.Forms.Timer(components);
			numericUpDown1 = new NumericUpDown();
			label2 = new Label();
			groupBox1 = new GroupBox();
			checkBox1 = new CheckBox();
			((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(261, 31);
			label1.Name = "label1";
			label1.Size = new Size(38, 15);
			label1.TabIndex = 0;
			label1.Text = "label1";
			// 
			// timer1
			// 
			timer1.Tick += timer1_Tick;
			// 
			// numericUpDown1
			// 
			numericUpDown1.Location = new Point(128, 22);
			numericUpDown1.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
			numericUpDown1.Name = "numericUpDown1";
			numericUpDown1.Size = new Size(120, 23);
			numericUpDown1.TabIndex = 1;
			numericUpDown1.Value = new decimal(new int[] { 5000, 0, 0, 0 });
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(8, 24);
			label2.Name = "label2";
			label2.Size = new Size(114, 15);
			label2.TabIndex = 2;
			label2.Text = "Timer Interval in ms:";
			label2.TextAlign = ContentAlignment.TopRight;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(numericUpDown1);
			groupBox1.Location = new Point(235, 212);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(258, 60);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			// 
			// checkBox1
			// 
			checkBox1.AutoSize = true;
			checkBox1.Location = new Point(298, 160);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new Size(95, 19);
			checkBox1.TabIndex = 4;
			checkBox1.Text = "Timer active?";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += checkBox1_CheckedChanged;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(checkBox1);
			Controls.Add(groupBox1);
			Controls.Add(label1);
			Name = "Form1";
			Text = "Form1";
			((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private System.Windows.Forms.Timer timer1;
		private NumericUpDown numericUpDown1;
		private Label label2;
		private GroupBox groupBox1;
		private CheckBox checkBox1;
	}
}
