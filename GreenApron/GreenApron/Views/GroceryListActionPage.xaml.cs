﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GreenApron
{
    public partial class GroceryListActionPage : ContentPage
    {
        public ObservableCollection<GroceryListGroup> groceryItems { get; private set; } = new ObservableCollection<GroceryListGroup>();

        public GroceryListActionPage(ObservableCollection<GroceryListGroup> items)
        {
            InitializeComponent();
            groceryItems = items;
            groceryList.ItemsSource = groceryItems;
        }
        
        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Disable selection
            groceryList.SelectedItem = null;
        }

        public async void Update(object sender, EventArgs e)
        {
            var request = new GroceryRequest();
            request.items = new List<GroceryItem>();
            foreach (GroceryListGroup group in groceryItems)
            {
                foreach (GroceryItem item in group)
                {
                    if (item.Purchased)
                    {
                        item.DateCompleted = DateTime.Now;
                    }
                    request.items.Add(item);
                }
            }
            var response = await App.APImanager.UpdateGroceryItems(request);
            await Navigation.PopAsync();
        }

        public async void Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}