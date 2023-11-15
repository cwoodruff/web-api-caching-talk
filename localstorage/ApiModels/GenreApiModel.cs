namespace localstorage.ApiModels;

public class GenreApiModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IList<TrackApiModel>? Tracks { get; set; }
}