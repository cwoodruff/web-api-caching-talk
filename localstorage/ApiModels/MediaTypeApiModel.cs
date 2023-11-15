namespace localstorage.ApiModels;

public class MediaTypeApiModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IList<TrackApiModel>? Tracks { get; set; }
}