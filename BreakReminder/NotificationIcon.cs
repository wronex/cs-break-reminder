using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BreakReminder
{
	public sealed class NotificationIcon
	{
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		
		private Timer updateTimer; // A timer for periodically checking if it is time for a break.
		private DateTime lastBreak; // Date and time at the beginning of the most recent break.
		private Bitmap currentIcon; // A refrence to the current notification icon.
		
		
		private readonly TimeSpan BreakInterval = TimeSpan.FromMinutes(60); // Time between the beginning of each break.
		private readonly TimeSpan BreakDuration = TimeSpan.FromMinutes(15); // Duration of a break.
		
		#region Initialize icon and menu
		public NotificationIcon()
		{
			this.lastBreak = DateTime.Now + BreakDuration - BreakInterval;
			
			this.notificationMenu = new ContextMenu(InitializeMenu());
			this.notifyIcon = new NotifyIcon();
			this.notifyIcon.DoubleClick += this.IconDoubleClick;
			this.notifyIcon.ContextMenu = this.notificationMenu;
			
			this.updateTimer = new Timer();
			this.updateTimer.Tick += TimerTick;
			this.updateTimer.Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
			this.updateTimer.Start();
			
			#if DEBUG
				this.TakeBreak();
			#endif
			
			this.UpdateNotificationIcon();
		}
		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("Take break", MenuTakeBreakClick),
				new MenuItem("-"),
				new MenuItem("About", MenuAboutClick),
				new MenuItem("Exit", MenuExitClick)
			};
			return menu;
		}
		#endregion
		
		#region Main - Program entry point
		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (var mtx = new System.Threading.Mutex(true, "BreakReminder", out isFirstInstance)) {
				if (isFirstInstance) {
					NotificationIcon notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
				} else {
					// The application is already running
					MessageBox.Show("An instance of BreakReminder is already running. This program will now close.", 
					                "BreakReminder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			} // releases the Mutex
		}
		#endregion
		
		/// <summary>
		/// Shows a break reminder form on all screens. The reminders will stay
		/// visible for the supplied duration.
		/// </summary>
		/// <param name="breakDuration">Duration of break.</param>
		private void ShowBreakReminderOnAllScreens(TimeSpan breakDuration)
		{
			foreach (var screen in Screen.AllScreens) {
				var form = new FormBreakReminder(breakDuration);
				form.Location = screen.WorkingArea.Location;
				form.Show();
			}
		}
		
		/// <summary>
		/// Draws a notification icon for showing the number of minutes until
		/// the next break.
		/// </summary>
		/// <param name="minutes">Minutes until next break.</param>
		/// <returns>An icon.</returns>
		private Bitmap DrawIconWithTimeLeft(int minutes)
		{
			Bitmap icon = new Bitmap(16, 16);
			
			// Center the text.
			StringFormat format = new StringFormat();
			format.FormatFlags = StringFormatFlags.NoWrap;
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			
			using (var gfx = Graphics.FromImage(icon)) {
				gfx.FillRectangle(Brushes.Black, 0, 0, icon.Width, icon.Height);
				gfx.DrawString(minutes.ToString(), SystemFonts.DefaultFont, Brushes.White, icon.Width / 2.0f, icon.Height / 2.0f, format);
			}
			
			return icon;
		}
		
		/// <summary>
		/// Updates the notification icon to show the time until the next break.
		/// </summary>
		private void UpdateNotificationIcon()
		{
			TimeSpan timeToNextBreak = DateTime.Now - this.lastBreak;
			
			Bitmap icon = DrawIconWithTimeLeft((int)timeToNextBreak.TotalMinutes);
			this.currentIcon = icon;
			this.notifyIcon.Icon = Icon.FromHandle(icon.GetHicon());
			this.notifyIcon.Text = timeToNextBreak.ToString("mm") + " minutes until the next break";
		}
		
		/// <summary>
		/// Take a break now.
		/// </summary>
		private void TakeBreak()
		{
			this.ShowBreakReminderOnAllScreens(BreakDuration);
			this.lastBreak = DateTime.Now;
		}
		
		#region Event Handlers
		private void TimerTick(object sender, EventArgs e)
		{
			bool takeBreak = DateTime.Now - this.lastBreak > BreakInterval;
			bool isBreak = DateTime.Now - this.lastBreak < BreakDuration;
			
			if (takeBreak) {
				this.TakeBreak();
			}
				
			this.UpdateNotificationIcon();
		}
		
		private void MenuTakeBreakClick(object sender, EventArgs e)
		{
			this.TakeBreak();
			this.UpdateNotificationIcon();
		}
		
		private void MenuAboutClick(object sender, EventArgs e)
		{
			MessageBox.Show("Your hourly reminder to take a break, by Wronex.\r\n\r\n" +
			                "Version: " + Application.ProductVersion,
			                "BreakReminder", MessageBoxButtons.OK, MessageBoxIcon.None);
		}
		
		private void MenuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void IconDoubleClick(object sender, EventArgs e)
		{
		}
		#endregion
	}
}
