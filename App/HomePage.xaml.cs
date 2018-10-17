using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace App
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NewPage());
           
        }
    }
}
