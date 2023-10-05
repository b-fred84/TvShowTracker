using DataAccess.Data;
using ImdbScraper;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using System;
using System.Security.Claims;
using TvShowTracker.Models;

namespace ShowTrackerApp.Pages;

public partial class Shows
{
    [Inject] public ShowData ShowData { get; set; }
    [Inject] public IImdbRatingScraper RatingScraper { get; set; }

    public List<Show> ShowsList { get; set; }
    private List<ItemModel> ToolBarOptions { get; set; } = new List<ItemModel>();
  

    private SfDialog _dialogBoxAdd;
    private SfDialog _dialogBoxEdit;
    private SfDialog _dialogBoxSelectRow;
    private SfDialog _dialogBoxDelete;
    

    private IImdbRatingScraper _ratingScraper;

    private bool _userAuthenticated;

    private Show _showToModify = new Show();

    private string _headerText = string.Empty;
    private string _userId = string.Empty;

    private bool _isChecked = true;
    public int SelectedShowId { get; set; } = 0;
    private bool _showSelectRowMessage { get; set; } = false;

    string[] GenreArr = { "Comedy", "Fantasy", "Sci Fi", "Crime", "Drama", "Action", 
                          "Thriller", "Horror", "Western", "Documentary", "Animation"  };

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
       
        _userId = authState.User.Identity.Name;
        ShowsList = await ShowData.GetShows(_userId);
        
        
        _ratingScraper = RatingScraper;

        ToolBarOptions.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new show", PrefixIcon = "e-add" });
        ToolBarOptions.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit a show", PrefixIcon = "e-edit" });
        ToolBarOptions.Add(new ItemModel() { Text = "Delete", TooltipText = "Delete a show", PrefixIcon = "e-delete" });
    }

    public void RowSelectHandler(RowSelectEventArgs<Show> args)
    {
        SelectedShowId = args.Data.Id;
        Console.WriteLine(SelectedShowId);
    }
    public async Task ToolbarClickHandler(ClickEventArgs args)
    {
        if (args.Item.Text == "Add") 
        {
            _showToModify = new Show();
            _headerText = "Add a new show";
            await this._dialogBoxAdd.Show();
           
        }
        if (args.Item.Text == "Edit")
        {
            if (SelectedShowId == 0)
            {
                await this._dialogBoxSelectRow.Show();
            }
            else
            {
                _headerText = "Edit show details";
                _showToModify = await ShowData.GetShowById(SelectedShowId);
                await this._dialogBoxEdit.Show();
            }

        }
        if (args.Item.Text == "Delete")
        {
            if (SelectedShowId == 0)
            {
                await this._dialogBoxSelectRow.Show();
            }
            else
            {
                _headerText = "Remove Show From List";
                _showToModify = await ShowData.GetShowById(SelectedShowId);
                await this._dialogBoxDelete.Show();
            }
        }
    }
    
    //imdb scraper slows down process of saving a new show, bad functionality
    //is the imdb rating needed anyway? if keeping it look to change it so it doesn't slow down saving process,
    //probably remove imdb rating tho, not really needed if you are already watching the show
    protected async Task SaveNewShow()
    {
        _showToModify.UserId = _userId;

        _showToModify.ImdbRating = _ratingScraper.GetShowRatings(_showToModify.Name);
        await ShowData.InsertShow(_showToModify);
  
        // Clear the form and refresh the list of shows
        _showToModify = new Show();
        ShowsList = await ShowData.GetShows(_userId); // Refresh the list

        await CloseDialogBox();

    }

    protected async Task SaveEdits()
    {
        await ShowData.UpdateShow(_showToModify);
        ShowsList = await ShowData.GetShows(_userId);
        await CloseDialogBox();
    }
    private async Task CloseDialogBox()
    {
        await this._dialogBoxAdd.HideAsync();
        await this._dialogBoxEdit.HideAsync();
        await this._dialogBoxDelete.HideAsync();
    }

    private async Task DeleteShow()
    {
        await ShowData.DeleteShow(SelectedShowId);

        ShowsList = await ShowData.GetShows(_userId);

        await CloseDialogBox();
    }

  

    

}





