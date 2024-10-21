using System;
using System.Collections.Generic;

public class KBEventViewModel
{
    public string title { get; set; }
    public string presentation_short { get; set; }
    public string presentation_long { get; set; }
    public long unixtime_release { get; set; }
    public decimal price_min { get; set; }
    public decimal price_max { get; set; }
    public Organizer organizer { get; set; }
    public Dictionary<string, string> images { get; set; }
    public Dictionary<string, string> trailers { get; set; }
    public Dictionary<string, EventDate> dates { get; set; }
    public Dictionary<string, Location> locations { get; set; }
    public string url_checkout { get; set; }
    public string url_event_page { get; set; }
    public string url_organizer_serp { get; set; }
}

public class Organizer
{
    public int organizer_id { get; set; }
    public string name { get; set; }
    public string logo { get; set; }
}

public class EventDate
{
    public int date_id { get; set; }
    public int location_id { get; set; }
    public long unixtime_open { get; set; }
    public long unixtime_start { get; set; }
    public int ticket_amount { get; set; }
    public int ticket_available { get; set; }
    public bool date_only { get; set; }
    public string url_checkout { get; set; }
}

public class Location
{
    public int location_id { get; set; }
    public string name { get; set; }
    public string street { get; set; }
    public string vicinity { get; set; }
    public string city { get; set; }
}
