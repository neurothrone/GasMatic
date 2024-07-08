using Microsoft.EntityFrameworkCore;

namespace GasMatic.Blazor.Wasm.Data;

public class GasMaticDbContext : DbContext
{
    public GasMaticDbContext(DbContextOptions<GasMaticDbContext> contextOptions) : base(contextOptions)
    {
    }

    public DbSet<GasVolumeEntity> GasVolumeEntities { get; set; }
}