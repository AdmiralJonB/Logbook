using System;

namespace FrontEnd
{
	public partial class View_Logbook : Gtk.Window
	{
		public View_Logbook () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

