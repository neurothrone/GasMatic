namespace GasMatic.Maui.Core.Services.Database;

public static class DatabaseConstants
{
    private const string DatabaseFileName = "GasMaticDB";

    // Flags
    // - read/write mode
    // - create db if it doesn't exist
    // - enable multithreaded database access
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
}