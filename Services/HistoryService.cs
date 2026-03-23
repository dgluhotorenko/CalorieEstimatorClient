using CalorieClient.Models;
using CalorieClient.Services.Abstract;
using SQLite;

namespace CalorieClient.Services;

public class HistoryService : IHistoryService
{
    private readonly SQLiteAsyncConnection _db;

    public HistoryService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "calorie_history.db");
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<HistoryEntry>().Wait();
    }

    public async Task<List<HistoryEntry>> GetAllAsync() =>
        await _db.Table<HistoryEntry>().OrderByDescending(e => e.AnalyzedAt).ToListAsync();

    public async Task<HistoryEntry?> FindByHashAsync(string hash) =>
        await _db.Table<HistoryEntry>().FirstOrDefaultAsync(e => e.ImageHash == hash);

    public async Task SaveAsync(HistoryEntry entry) =>
        await _db.InsertAsync(entry);

    public async Task DeleteAsync(HistoryEntry entry) =>
        await _db.DeleteAsync(entry);
}
