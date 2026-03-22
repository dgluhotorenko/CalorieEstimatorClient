using CalorieClient.Models;

namespace CalorieClient.Services.Abstract;

public interface IHistoryService
{
    Task<List<HistoryEntry>> GetAllAsync();
    Task SaveAsync(HistoryEntry entry);
    Task DeleteAsync(HistoryEntry entry);
}
