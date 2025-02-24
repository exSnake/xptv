using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using xptv.Core.Application.Channels.Services.Files;
using xptv.Core.Application.Channels.Services.Groups;
using xptv.Core.Application.Channels.Services.Parsing;
using xptv.Core.Application.Common.Pagination;
using xptv.Core.Domain.Channels;
using xptv.Extensions;
using xptv.Presentation.Common.Components.Loading.ViewModels;
using xptv.Presentation.Player.Views;

// Aggiungi questo in cima

namespace xptv.Presentation.Channels.ViewModels;

public partial class ChannelsViewModel : ObservableObject
{
    private readonly IM3UService _m3UService;
    private readonly IM3UFileService _fileService;
    private readonly IPaginationService _paginationService;
    private readonly IChannelGroupService _groupService;
    public ILoadingOverlayViewModel LoadingOverlayViewModel { get; set; }

    private const string lastM3UPathKey = "LastM3UPath";
    private const int pageSize = 50;
    private List<Channel> _allChannels = [];

    [ObservableProperty] private bool _isFirstLoad;
    [ObservableProperty] private List<Channel> _channels = [];
    [ObservableProperty] private List<string> _groups = ["All"];
    [ObservableProperty] private ObservableCollection<Channel> _filteredChannels = [];
    [ObservableProperty] private string? _selectedGroup = "All";
    [ObservableProperty] private Channel? _selectedChannel;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _loadingMessage = "";
    [ObservableProperty] private bool _isLoadingMore;
    [ObservableProperty] private string? _selectedChannelUrl;
    [ObservableProperty] private bool _isChannelSelected;

    public ChannelsViewModel(
        IM3UService m3UService,
        IM3UFileService fileService,
        IPaginationService paginationService,
        IChannelGroupService groupService,
        ILoadingOverlayViewModel loadingOverlayViewModel
    )
    {
        _m3UService = m3UService;
        _fileService = fileService;
        _paginationService = paginationService;
        _groupService = groupService;
        IsChannelSelected = false;
        IsFirstLoad = true;
        IsLoading = false; // Non iniziamo più in loading
        LoadingMessage = "";

        LoadingOverlayViewModel = loadingOverlayViewModel;
    }


    // Nuovo metodo pubblico per l'inizializzazione
    public async Task InitializeAsync()
    {
        var lastPath = await _fileService.GetLastPathAsync();

        if (!string.IsNullOrEmpty(lastPath) && File.Exists(lastPath))
        {
            await LoadM3UFile(lastPath);
        }
    }

    [RelayCommand]
    private async Task LoadM3UFileAsync()
    {
        var filePath = await _fileService.PickFileAsync();
        if (filePath != null)
        {
            await LoadM3UFile(filePath);
            Preferences.Set(lastM3UPathKey, filePath);
        }
    }

    private async Task LoadM3UFile(string filePath)
    {
        try
        {
            IsLoading = true;
            LoadingMessage = $"Loading {Path.GetFileName(filePath)}...";

            _allChannels = (await _m3UService.ReadM3UFileAsync(filePath)).ToList(); // Memorizza tutti i canali

            Groups = _groupService.GetGroups(_allChannels);
            IsFirstLoad = false;
            UpdateFilteredList(); // Ora caricherà solo la prima pagina
        }
        catch (Exception ex)
        {
            await ShowAlert("Error", $"Error loading M3U file: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
            LoadingMessage = "";
        }
    }

    [RelayCommand]
    private void LoadMoreItems()
    {
        if (IsLoadingMore)
            return;

        try
        {
            IsLoadingMore = true;
            Debug.WriteLine($"Loading more items. Current count: {FilteredChannels.Count}");

            var currentCount = FilteredChannels.Count;
            var itemsToAdd = GetNextBatch(currentCount, pageSize);

            if (itemsToAdd.Count != 0)
            {
                // Usiamo AddRange invece di aggiungere uno per uno
                FilteredChannels.AddRange(itemsToAdd);
                Debug.WriteLine($"New total count: {FilteredChannels.Count}");
            }
        }
        finally
        {
            IsLoadingMore = false;
        }
    }

    [RelayCommand]
    private async Task Tap(Channel channel)
    {
        var arguments = new Dictionary<string, object> { [nameof(Channel)] = channel };
        await Shell.Current.GoToAsync(nameof(VideoPlayerPage), arguments);
    }

    private List<Channel> GetNextBatch(int skip, int take)
    {
        if (string.IsNullOrEmpty(SelectedGroup) || SelectedGroup == "All")
        {
            return _allChannels.Skip(skip).Take(take).ToList();
        }

        return _allChannels
            .Where(c => c.GroupTitle == SelectedGroup)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    partial void OnSelectedGroupChanged(string? value)
    {
        UpdateFilteredList();
    }

    partial void OnSelectedChannelChanged(Channel? value)
    {
        if (value != null) { }
    }

    public void UpdateFilteredList()
    {
        if (_allChannels.Count == 0)
        {
            FilteredChannels.Clear();
            return;
        }

        var filtered = _groupService.FilterChannelsByGroup(_allChannels, SelectedGroup ?? "All");
        var pageItems = _paginationService.Paginate(filtered, 1, pageSize).ToList();

        FilteredChannels.Clear();
        FilteredChannels.AddRange(pageItems);
    }

    private async Task ShowAlert(string title, string message)
    {
        var window = Application.Current?.Windows[0];
        if (window?.Page != null)
        {
            await window.Page.DisplayAlert(title, message, "OK");
        }
    }
}