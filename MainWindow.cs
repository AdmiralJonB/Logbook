using System;
using Gtk;
using MySql.Data.MySqlClient;

using FrontEnd;

public partial class MainWindow: Gtk.Window
{	
	Gtk.Window log;
	Gtk.Window view;
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnLBookEntryClicked (object sender, System.EventArgs e)
	{
		log = new FrontEnd.LogEntry();
	}

	protected void OnViewLogbookClick (object sender, System.EventArgs e)
	{
		view = new FrontEnd.ViewLogbook();
	}

	protected void OnBtnSaveClicked (object sender, System.EventArgs e)
	{
		System.IO.StreamWriter file = new System.IO.StreamWriter("SavedLogBook.tex");
		
		string line = "\\documentclass[a4paper,10pt]{article}\n\\usepackage[hmargin=2cm,vmargin=2.5cm]{geometry}\n\\title{LOG}\n\\begin{document}\n\\maketitle\n\\tableofcontents\n\\clearpage\n";
		
		file.WriteLine(line);
		
		//Connect to Database
		if(!MySQL_Stuff.Connect("logbook","logbook","logbook"))
		{
			string msg = "Connection Failed: " + MySQL_Stuff.Error;
			
			MessageDialog md = new MessageDialog(this,DialogFlags.Modal,MessageType.Error,ButtonsType.Ok,msg);
			md.Run();
			md.Destroy();
			
			return;
		}
		
		string query = "SELECT DATE(CONVERT_TZ(date,'UTC','GMT')) , text FROM log ORDER BY date, id;";
		
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
		
		bool first = true;
		DateTime currentDate = DateTime.UtcNow;
		int sectionCounter = 1;
		
		while(rdr.Read())
		{
			if(first)
			{
				currentDate = rdr.GetDateTime(0);
				first = false;
				
				line = "\\section{" + currentDate.Day.ToString() + "/" + 
					currentDate.Month.ToString() + "/" + currentDate.Year.ToString() + "}";
				
				file.WriteLine(line);
			}
			else
			{
				if(rdr.GetDateTime(0) != currentDate)
				{
					currentDate = rdr.GetDateTime(0);
					
					line = "\\section{" + currentDate.Day.ToString() + "/" + 
						currentDate.Month.ToString() + "/" + currentDate.Year.ToString() + "}";
				
					file.WriteLine("");
					file.WriteLine(line);
					
					sectionCounter = 1;
				}
			}
			
			file.WriteLine("");
			
			file.WriteLine("\\subsection{Entry " + sectionCounter.ToString() + "}\n");
			
			file.WriteLine(rdr.GetString(1));
			
			sectionCounter++;
		}
		
		file.WriteLine("\\end{document}");
		
		MySQL_Stuff.Close();
		file.Close();
	}
}
