using System;
using System.Linq;
using NStack;
using Terminal.Gui;
using TestingGuiCS.Models;

namespace TestingGuiCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init ();
            var top = Application.Top;

            // Creates a menubar, the item "New" has a help menu.
            var menu = new MenuBar (new MenuBarItem [] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", () => { top.Running = false; })
                })
            });
            top.Add (menu);

            
            // Creates the top-level window to show
            var win = new Window (new Rect (0, 1, top.Frame.Width, top.Frame.Height-1), "Movie Db");
            top.Add (win);

            // Add some controls
            var txtSearchLbl = new Label(3, 1, "Movie Name: ");
            var txtSearch = new TextField(15, 1, 30, "");
            var forKidsOnly = new CheckBox(3, 3, "For Kids?");
            var minimumRatingLbl = new Label(25, 3, "Minimum Rating: ");
            var minimumRatingTxt = new TextField(41, 3, 10, "");
            var searchBtn = new Button(3, 5, "Filter");
            var allMoviesListView = new ListView(new Rect(4, 8, top.Frame.Width, 200), MovieDataSource.GetList(forKidsOnly.Checked, 0).ToList());
            searchBtn.Clicked = () =>
            {
                
                double rating = 0;
                var isDouble = double.TryParse(minimumRatingTxt.Text.ToString(), out rating);
                if(!string.IsNullOrEmpty(minimumRatingTxt.Text.ToString()) && !isDouble)
                {
                    MessageBox.ErrorQuery(30, 6, "Error", "Rating must be number");
                    minimumRatingTxt.Text = ustring.Empty;
                    return;
                }
                
                win.Remove(allMoviesListView);
                if (string.IsNullOrEmpty(txtSearch.Text.ToString()) || string.IsNullOrEmpty(minimumRatingTxt.Text.ToString()))
                {
                    allMoviesListView = new ListView(new Rect(4, 8, top.Frame.Width, 200),
                        MovieDataSource.GetList(forKidsOnly.Checked, rating).ToList());
                    win.Add(allMoviesListView);
                }
                else
                {
                    win.Remove(allMoviesListView);
                    win.Add(new ListView(new Rect(4, 8, top.Frame.Width, 200), 
                        MovieDataSource.GetList(forKidsOnly.Checked, rating)
                            .Where(x => 
                            x.Name.Contains(txtSearch.Text.ToString(), StringComparison.OrdinalIgnoreCase)
                        ).ToList()));
                }
                
            };
            win.Add (
                txtSearchLbl,
                txtSearch,
                forKidsOnly,
                minimumRatingLbl,
                minimumRatingTxt,
                searchBtn,
                new Label (3, 7, "-------------Search Result--------------")
            );

            Application.Run ();
        }
    }
}