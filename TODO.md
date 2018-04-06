- Error: RSS.xaml.cs file, ArticleListItem_Clicked event
	This event is triggered when a feed is clicked (left box of UI).
	Because this event is triggered at the wrong time, the sender object in
	the parameter is not an actual (article) list view item.  So, the object doesn't
	have a description property.  An error occurs when trying to access the description property.
	
	Potential Fix: Figure out why the event is being triggered at the wrong moment and correct this.