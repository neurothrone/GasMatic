using GasMatic.Core.Models;
using IndexedDB.Blazor;
using Microsoft.JSInterop;

namespace GasMatic.Blazor.Wasm.IndexedDb.Data;

public class GasVolumeDb : IndexedDB.Blazor.IndexedDb
{
    public GasVolumeDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version)
    {
    }

    public IndexedSet<GasVolumeEntity> GasVolumeEntities { get; set; }
}