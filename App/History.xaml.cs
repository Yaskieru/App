using System;
using System.Collections.Generic;
using App.Model;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace App
{
    public partial class History : ContentPage
    {
        public History()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using(SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Post>();
                var posts = connection.Table<Post>().ToList();
                postListView.ItemsSource = posts;
            }

        }
    }
}
