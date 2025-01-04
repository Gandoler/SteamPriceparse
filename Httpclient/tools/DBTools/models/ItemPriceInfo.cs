using System.ComponentModel.DataAnnotations;

namespace Dbtools.models;

public class ItemPriceInfo
{
    [Key]
    public int PriceInfoId { get; set; }
    
    [Required]
    public int ItemId { get; set; } // Внешний ключ
    
    [Required]
    public decimal MinPrice { get; set; } // Минимальная цена
    
    [Required]
    public int SalesVolume { get; set; } // Объем продаж
    
    [Required]
    public decimal AvgPrice { get; set; } // Средняя цена
    
    // Навигационное свойство
    public SteamItem SteamItem { get; set; } = null!;
}