using Microsoft.EntityFrameworkCore;
using RocketseatAuction.API.Entites;

namespace RocketseatAuction.API.Repositories;

public class RocketseatAuctionDbContext: DbContext
{
    public DbSet<Auction> Auctions { get; set; }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/home/dluccas/RiderProjects/RocketseatAuction/leilaoDbNLW.db");
    }
}