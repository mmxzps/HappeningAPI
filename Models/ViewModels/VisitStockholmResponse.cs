using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class VisitStockholmEventResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public List<Event> Results { get; set; }
}

public class Meta
{
    [JsonPropertyName("categories")]
    public List<Category> Categories { get; set; }
}

public class Category
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

public class Event
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public Title Title { get; set; }

    [JsonPropertyName("description")]
    public Description Description { get; set; }

    [JsonPropertyName("external_website_url")]
    public string ExternalWebsiteUrl { get; set; }

    [JsonPropertyName("call_to_action_url")]
    public string CallToActionUrl { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("venue_name")]
    public string VenueName { get; set; }

    [JsonPropertyName("zip_code")]
    public string ZipCode { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("modified_at")]
    public DateTime ModifiedAt { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("start_date")]
    public string StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public string EndDate { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string EndTime { get; set; }

    [JsonPropertyName("categories")]
    public List<Category> EventCategories { get; set; }

    [JsonPropertyName("featured_image")]
    public FeaturedImage FeaturedImage { get; set; }

    [JsonPropertyName("closest_station")]
    public string ClosestStation { get; set; }
}

public class Title
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

public class Description
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

public class Location
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

public class FeaturedImage
{
    [JsonPropertyName("title")]
    public Title Title { get; set; }

    [JsonPropertyName("alt")]
    public Alt Alt { get; set; }

    [JsonPropertyName("caption")]
    public Caption Caption { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public class Alt
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

public class Caption
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}