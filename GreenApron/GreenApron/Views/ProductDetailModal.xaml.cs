﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GreenApron
{
    public partial class ProductDetailModal : ContentPage
    {
        private string _context { get; set; }

        public ProductDetailModal(Ingredient item, string ctx)
        {
            InitializeComponent();
            this.BindingContext = item;
            _context = ctx;
            foreach (string unit in UnitOptions)
            {
                unitPicker.Items.Add(unit);
            }
        }

        public string[] UnitOptions = new string[] { "tablespoons", "teaspoons", "cups", "pints", "quarts", "gallons", "lbs", "oz" };

        public async void OnAddClicked(object sender, EventArgs e)
        {
            var item = (Ingredient)BindingContext;
            item.amount = Convert.ToDouble(qtyEntry.Text);
            if (unitPicker.SelectedIndex != -1)
            {
                item.unit = UnitOptions[unitPicker.SelectedIndex];
            }
            // TODO: Call WebAPI endpoint that adds a GroceryItem
            if (_context == "grocery")
            {
                var response = App.APImanager.AddGroceryItem(item);
            } else if (_context == "inventory")
            {
                var response = App.APImanager.AddInventoryItem(item);
            }
            await Navigation.PopModalAsync();
        }

        public async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
