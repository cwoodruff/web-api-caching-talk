namespace localstorage.ApiModels;

public class AlbumApiModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }

    public ArtistApiModel Artist { get; set; }

    public IList<TrackApiModel> Tracks { get; set; }
}