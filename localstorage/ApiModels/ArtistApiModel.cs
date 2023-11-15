namespace localstorage.ApiModels;

public class ArtistApiModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public IList<AlbumApiModel>? Albums { get; set; }
}