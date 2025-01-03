using Dbtools.models;
using Microsoft.EntityFrameworkCore;

namespace Dbtools.Data;

public class DbContextSteam: DbContext
{
    
    
    
    
    public DbSet<SteamItem> Items { get; set; }
}