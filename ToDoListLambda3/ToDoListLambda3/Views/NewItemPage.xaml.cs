using System;

using ToDoListLambda3.Models;

using Xamarin.Forms;

namespace ToDoListLambda3.Views
{
    public partial class NewItemPage : ContentPage
    {
        public ToDo Item;

        public NewItemPage()
        {
            InitializeComponent();
            Item = new ToDo();
            BindingContext = Item;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}