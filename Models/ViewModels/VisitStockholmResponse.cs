using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[NotMapped]
public class VisitStockholmEventResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("meta")]
    public VSMeta Meta { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("results")]
    public List<VSEvent> Results { get; set; }
}

[NotMapped]
public class VSMeta
{
    [JsonPropertyName("categories")]
    public List<VSCategory> Categories { get; set; }
}

[NotMapped]
public class VSCategory
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

[NotMapped]
public class VSEvent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public VSTitle Title { get; set; }

    [JsonPropertyName("description")]
    public VSDescription Description { get; set; }

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
    public VSLocation Location { get; set; }

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
    public List<VSCategory> EventCategories { get; set; }

    [JsonPropertyName("featured_image")]
    public VSFeaturedImage FeaturedImage { get; set; }

    [JsonPropertyName("closest_station")]
    public string ClosestStation { get; set; }
}

[NotMapped]
public class VSTitle
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

[NotMapped]
public class VSDescription
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

[NotMapped]
public class VSLocation
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

[NotMapped]
public class VSFeaturedImage
{
    [JsonPropertyName("title")]
    public VSTitle Title { get; set; }

    [JsonPropertyName("alt")]
    public VSAlt Alt { get; set; }

    [JsonPropertyName("caption")]
    public VSCaption Caption { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}

[NotMapped]
public class VSAlt
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}

[NotMapped]
public class VSCaption
{
    [JsonPropertyName("en")]
    public string En { get; set; }

    [JsonPropertyName("sv")]
    public string Sv { get; set; }
}