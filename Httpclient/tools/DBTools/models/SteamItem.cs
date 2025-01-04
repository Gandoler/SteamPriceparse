using System.ComponentModel.DataAnnotations;
namespace Dbtools.models;

public class SteamItem
{
    [Key]
    public int ItemId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string ItemType { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ItemName { get; set; }  = string.Empty;
    
    [Required]
    public ItemQuality Quality { get; set; } 
    
    public ItemPriceInfo PriceInfo { get; set; } = null!;
}