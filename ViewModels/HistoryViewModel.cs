using System.Collections.ObjectModel;
using CalorieClient.Models;
using CalorieClient.Services.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalorieClient.ViewModels;

public partial class HistoryViewModel(IHistoryService historyService) : ObservableObject
{
    public ObservableCollection<HistoryEntry> Entries { get; } = [];

    [ObservableProperty]
    private bool _isEmpty;

    [RelayCommand]
    private async Task LoadEntries()
    {
        var entries = await historyService.GetAllAsync();
        Entries.Clear();
        foreach (var entry in entries)
            Entries.Add(entry);

        IsEmpty = Entries.Count == 0;
    }

    [RelayCommand]
    private async Task DeleteEntry(HistoryEntry entry)
    {
        await historyService.DeleteAsync(entry);
        Entries.Remove(entry);
        IsEmpty = Entries.Count == 0;
    }
}
