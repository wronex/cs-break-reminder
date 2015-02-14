
namespace BreakReminder
{
	partial class FormBreakReminder
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
			this.components = new System.ComponentModel.Container();
			this.labelReminder = new System.Windows.Forms.Label();
			this.timerUpdate = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// labelReminder
			// 
			this.labelReminder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelReminder.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelReminder.Location = new System.Drawing.Point(0, 0);
			this.labelReminder.Name = "labelReminder";
			this.labelReminder.Size = new System.Drawing.Size(284, 262);
			this.labelReminder.TabIndex = 0;
			this.labelReminder.Text = "Take a break";
			this.labelReminder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// timerUpdate
			// 
			this.timerUpdate.Enabled = true;
			this.timerUpdate.Interval = 500;
			this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdateTick);
			// 
			// FormBreakReminder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.ControlBox = false;
			this.Controls.Add(this.labelReminder);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FormBreakReminder";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Break Reminder";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Timer timerUpdate;
		private System.Windows.Forms.Label labelReminder;
	}
}
