
// This file has been generated by the GUI designer. Do not modify.
namespace FrontEnd
{
	public partial class ViewLogbook
	{
		private global::Gtk.VBox vbox3;

		private global::Gtk.Calendar calendar1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TextView textview1;

		private global::Gtk.Button btnClose;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget FrontEnd.ViewLogbook
			this.Name = "FrontEnd.ViewLogbook";
			this.Title = global::Mono.Unix.Catalog.GetString ("ViewLogbook");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child FrontEnd.ViewLogbook.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			this.vbox3.BorderWidth = ((uint)(5));
			// Container child vbox3.Gtk.Box+BoxChild
			this.calendar1 = new global::Gtk.Calendar ();
			this.calendar1.CanFocus = true;
			this.calendar1.Name = "calendar1";
			this.calendar1.DisplayOptions = ((global::Gtk.CalendarDisplayOptions)(35));
			this.vbox3.Add (this.calendar1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.calendar1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.textview1 = new global::Gtk.TextView ();
			this.textview1.CanFocus = true;
			this.textview1.Name = "textview1";
			this.textview1.Editable = false;
			this.textview1.WrapMode = ((global::Gtk.WrapMode)(2));
			this.GtkScrolledWindow.Add (this.textview1);
			this.vbox3.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow]));
			w3.Position = 1;
			// Container child vbox3.Gtk.Box+BoxChild
			this.btnClose = new global::Gtk.Button ();
			this.btnClose.CanFocus = true;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseUnderline = true;
			this.btnClose.Label = global::Mono.Unix.Catalog.GetString ("Close");
			this.vbox3.Add (this.btnClose);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.btnClose]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			w4.Padding = ((uint)(5));
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 603;
			this.Show ();
			this.calendar1.DaySelected += new global::System.EventHandler (this.OnDayChange);
			this.calendar1.MonthChanged += new global::System.EventHandler (this.OnMonthChange);
			this.btnClose.Clicked += new global::System.EventHandler (this.OnCloseClick);
		}
	}
}