/*
 * Created by SharpDevelop.
 * User: nmo
 * Date: 11.02.2012
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace mikudown
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.statusbox = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(290, 71);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(93, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Download!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(103, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(181, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "http://safebooru.org/index.php?page=dapi&s=post&q=index&tags=hatsune_miku+-comic&" +
			"limit=100&pid=";
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(103, 45);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(152, 20);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "Downloaded";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(12, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(389, 105);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(28, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 23);
			this.label4.TabIndex = 13;
			this.label4.Text = "Source:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(198, 75);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(85, 24);
			this.checkBox2.TabIndex = 12;
			this.checkBox2.Text = "Folder 01-10";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(103, 75);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(90, 24);
			this.checkBox1.TabIndex = 11;
			this.checkBox1.Text = "Year-Month";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(4, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "Output location:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.textBox4);
			this.groupBox3.Controls.Add(this.textBox5);
			this.groupBox3.Location = new System.Drawing.Point(290, 10);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(93, 54);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Pages";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(31, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 18);
			this.label2.TabIndex = 9;
			this.label2.Text = "to";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Click += new System.EventHandler(this.Label2Click);
			// 
			// textBox4
			// 
			this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox4.Location = new System.Drawing.Point(6, 19);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(19, 20);
			this.textBox4.TabIndex = 3;
			this.textBox4.Text = "0";
			// 
			// textBox5
			// 
			this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox5.Location = new System.Drawing.Point(54, 19);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(33, 20);
			this.textBox5.TabIndex = 4;
			this.textBox5.Text = "260";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 21);
			this.label1.TabIndex = 8;
			this.label1.Text = "Folder structure:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(255, 45);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(28, 20);
			this.button2.TabIndex = 6;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.progressBar1);
			this.groupBox2.Controls.Add(this.statusbox);
			this.groupBox2.Controls.Add(this.textBox3);
			this.groupBox2.Location = new System.Drawing.Point(12, 113);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(525, 329);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Info";
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(6, 312);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(511, 11);
			this.progressBar1.TabIndex = 2;
			// 
			// statusbox
			// 
			this.statusbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.statusbox.Location = new System.Drawing.Point(6, 286);
			this.statusbox.Name = "statusbox";
			this.statusbox.ReadOnly = true;
			this.statusbox.Size = new System.Drawing.Size(512, 20);
			this.statusbox.TabIndex = 1;
			// 
			// textBox3
			// 
			this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox3.Location = new System.Drawing.Point(6, 19);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox3.Size = new System.Drawing.Size(512, 261);
			this.textBox3.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
			this.pictureBox1.Location = new System.Drawing.Point(406, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(130, 100);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(549, 454);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 250);
			this.Name = "MainForm";
			this.Text = "MikuDown";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox statusbox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
	}
}
