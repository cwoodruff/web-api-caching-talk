namespace localstorage.ApiModels;

public class PlaylistApiModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IList<TrackApiModel>? Tracks { get; set; }
}