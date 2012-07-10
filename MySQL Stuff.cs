using System;
using Gtk;
using MySql.Data.MySqlClient;

namespace FrontEnd
{
	public static class MySQL_Stuff
	{
		public static MySqlConnection conn = null;
		public static string Error;
		
		public static bool Connect(string User, string Password, string DataBase)
		{
			string conn_string = "server=localhost" + 
									";userid=" + User + 
									";password=" + Password + 
									";database=" + DataBase;
			
			try 
			{
				conn = new MySqlConnection(conn_string);
				conn.Open();
			} 
			catch (MySqlException ex) {
				string msg = "MySql Error: " + ex.Message;
				
				Error = msg;
				
				return false;
			}
			
			return true;
		}
		
		public static void Close()
		{
			if(conn != null)
			{
				conn.Close();
			}
		}
		
		public static bool ExecuteNonQuery(string EditString)
		{
			if(conn == null)
			{
				Error = "Connection has not been established to a MySql Database.";
				return false;
			}
			
			MySqlCommand comm = conn.CreateCommand();
			comm.CommandText = EditString;
			
			try
			{
				comm.ExecuteNonQuery();
			}
			catch(MySqlException ex)
			{
				string msg = "MySql Error: " + ex.Message;
				Error = msg;
				return false;
			}
			
			return true;
		}
		
		public static MySqlDataReader ExecuteQuery(string Query)
		{
			//Error from no connection
			if(conn == null)
			{
				Error = "Connection has not been established to a MySql Database.";
				return null;
			}
			
			//Set up Query
			MySqlCommand comm = conn.CreateCommand();
			comm.CommandText = Query;
			
			MySqlDataReader rdr = null;
			
			//Execute Query
			try
			{
				rdr = comm.ExecuteReader();
			}
			catch(MySqlException ex)
			{
				//On Error
				string msg = "MySql Error: " + ex.Message;
				Error = msg;
				return null;
			}
			
			return rdr;
		}
	}
}

