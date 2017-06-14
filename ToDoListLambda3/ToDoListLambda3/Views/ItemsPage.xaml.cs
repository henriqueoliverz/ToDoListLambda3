using System;

using ToDoListLambda3.Models;
using ToDoListLambda3.ViewModels;

using Xamarin.Forms;

namespace ToDoListLambda3.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            await DisplayAlert("Oops", "Faltou um evento para eu dar UPDATE! kkkkkkkk", "Ok, iremos fazer!");
        }
    }
}
