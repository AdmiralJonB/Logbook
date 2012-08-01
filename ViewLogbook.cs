using System;
using MySql.Data.MySqlClient;
using Gtk;

namespace FrontEnd
{
	public partial class ViewLogbook : Gtk.Window
	{
		public ViewLogbook () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			
			textview1.Editable = false;
			HighlightEntries();
			
			GetLogbook();
		}

		protected void OnDayChange (object sender, System.EventArgs e)
		{
			GetLogbook();
		}
		
		protected void GetLogbook()
		{
			//Create the SQL Query
			string query = "SELECT text FROM log WHERE date = \'";
			
			
			query += calendar1.Date.Year + "-";
			query += (calendar1.Date.Month < 10 ? 0 + calendar1.Date.Month.ToString() : calendar1.Date.Month.ToString()) + "-";
			query += (calendar1.Date.Day < 10 ? 0 + calendar1.Date.Day.ToString() : calendar1.Date.Day.ToString());
			query += "\' ORDER BY id;";
			
			//Clear the textviewer in preperation
			textview1.Buffer.Clear();
			
			//Check whether connection has been opened
			if(!MySQL_Stuff.Connect("logbook","logbook","logbook"))
			{
				string msg = "Connection Failed: " + MySQL_Stuff.Error;
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				return;
			}
			
			Console.WriteLine(query);
			
			//Execute Query
			MySqlDataReader rdr = MySQL_Stuff.ExecuteQuery(query);
			
			//Check if query succeeded
			if(rdr == null)
			{
				string msg = "Query Failed: " + MySQL_Stuff.Error;
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				return;
			}
			
			//Get the text for the day
			string text = "";
			
			for (int i = 1; rdr.Read(); i++) 
			{
				text += i.ToString() + "\n-----\n";
				text += rdr.GetString(0) + "\n\n";
			}
			
			//Close off the readers
			rdr.Close();
			MySQL_Stuff.Close();
			
			textview1.Buffer.Text = text;
			
		}

		protected void OnCloseClick (object sender, System.EventArgs e)
		{
			this.Destroy();
		}

		protected void OnMonthChange (object sender, System.EventArgs e)
		{
			HighlightEntries();
		}
		
		protected void HighlightEntries()
		{
			//Unmark the Dates
			for(uint i = 0; i <= 31; i++)
				calendar1.UnmarkDay(i);
			
			string query = "SELECT date FROM log WHERE MONTH(date)=" + (calendar1.Month + 1).ToString() + " AND YEAR(date)=" + calendar1.Year.ToString() + ";";
			
			//Connect to Database
			if(!MySQL_Stuff.Connect("logbook","logbook","logbook"))
			{
				string msg = "Connection Failed: " + MySQL_Stuff.Error;
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				return;
			}
			
			Console.WriteLine(query);
			
			//Execute Reader
			MySqlDataReader rdr = MySQL_Stuff.ExecuteQuery(query);
			
			//Check if query succeeded
			if(rdr == null)
			{
				string msg = "Query Failed: " + MySQL_Stuff.Error;
				
				MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
				md.Run();
				md.Destroy();
				
				return;
			}
			
			//Mark the dates
			while(rdr.Read())
			{
				DateTime mark = rdr.GetDateTime(0);
				
				calendar1.MarkDay(Convert.ToUInt32(mark.Day));
			}
			
			MySQL_Stuff.Close();
		}
	}
}

