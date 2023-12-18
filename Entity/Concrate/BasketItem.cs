
using Core.Entities;
using Entity.Concrate;

public class BasketItem : IEntity
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public int? ProductCount { get; set; }
    public Product? Product { get; set; }
    public DateTime? AddedDate { get; set; }
}