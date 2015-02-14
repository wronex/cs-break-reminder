
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BreakReminder
{
	/// <summary>
	/// Description of FormBreakReminder.
	/// </summary>
	public partial class FormBreakReminder : Form
	{
		private TimeSpan breakDuration;
		private DateTime startOfBreak;
		
		public FormBreakReminder(TimeSpan breakDuration)
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			this.breakDuration = breakDuration;
			this.startOfBreak = DateTime.Now;
			
			Cursor.Hide();
			this.UpdateTimeLeft();
		}
		
		private void UpdateTimeLeft()
		{
			TimeSpan elapsedTime = DateTime.Now - this.startOfBreak;
			bool isBreakOver = elapsedTime > this.breakDuration;
			
			if (isBreakOver) {
				this.Close();
				return;
			}
			
			TimeSpan timeLeftOnBreak = this.breakDuration - elapsedTime;			
			this.labelReminder.Text = timeLeftOnBreak.ToString(@"mm\:ss");
		}
		
		private void TimerUpdateTick(object sender, EventArgs e)
		{			
			UpdateTimeLeft();
		}
	}
}
