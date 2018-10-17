using System;
using System.Collections.Generic;
using System.Linq;
using App.Logic;
using App.Model;
using Plugin.Geolocator;
using SQLite;

using Xamarin.Forms;

namespace App
{
    public partial class NewPage : ContentPage
    {
        public NewPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue.categories.FirstOrDefault();
            try
            {
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name
                };

                if (experienceEntry.Text != null)
                {
                    SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation);
                    connection.CreateTable<Post>();
                    int rows = connection.Insert(post);
                    connection.Close();

                    if (rows > 0)
                    {
                        DisplayAlert("Success", rows.ToString(), "Ok");
                    }
                    else
                    {
                        DisplayAlert("Failed", rows.ToString(), "Ok");
                    }

                    experienceEntry.Text = "";
                }
            }
            catch (NullReferenceException nre)
            {
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
