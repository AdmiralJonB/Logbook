using System;
using Gtk;

namespace FrontEnd
{
	public partial class LogEntry : Gtk.Window
	{
		public LogEntry () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		protected void OnSaveClick (object sender, System.EventArgs e)
		{			
			if(!MySQL_Stuff.Connect("logbook","logbook","logbook"))
			{
				string msg = "Connection Failed: " + MySQL_Stuff.Error;
				Console.WriteLine(msg);
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				return;
			}
			
			/*Gtk.TextIter start, end;
			textview1.Buffer.GetBounds(out start, out end);
			
			string text = textview1.Buffer.GetText(start,end,true);*/
			
			string text = textview1.Buffer.Text;
			
			text = text.Replace("\'","\\'");
			text = text.Replace("\n","\\n");
			
			string statement = "INSERT INTO log(text) VALUES (\'" + text + "\');";
			Console.WriteLine(statement);
			
			if(!MySQL_Stuff.ExecuteNonQuery(statement))
			{
				string msg = "Save Failed: " + MySQL_Stuff.Error;
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				MySQL_Stuff.Close();
				
				return;
			}
			
			MySQL_Stuff.Close();
			
			this.Destroy();
		}

		protected void OnCancelClick (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
	}
}

