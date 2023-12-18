using Core.Entities;

public class FileStorage : IEntity
{
    public int Id { get; set; }
    public int? State { get; set; }
    public string? Collection { get; set; }
    public int OwnerId { get; set; }
    public string? RealFileName { get; set; }
    public string? FileName { get; set; }
    public long FileSize { get; set; }
    public string? FileType { get; set; }
    public string? DataType { get; set; }
    public DateTime? AddedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public int? AddedUserId { get; set; }
    public int? ModifiedUserId { get; set; }
}