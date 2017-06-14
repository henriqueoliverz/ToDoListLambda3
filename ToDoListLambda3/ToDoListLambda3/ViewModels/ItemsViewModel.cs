using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ToDoListLambda3.Helpers;
using ToDoListLambda3.Models;
using ToDoListLambda3.Services;
using ToDoListLambda3.Views;

using Xamarin.Forms;

namespace ToDoListLambda3.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ToDo> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private ToDoService ToDoService;

        public ItemsViewModel()
        {
            ToDoService = new ToDoService();
            Title = "ToDo List";
            Items = new ObservableRangeCollection<ToDo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, ToDo>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as ToDo;
                Items.Add(_item);
                await ToDoService.SalvarItem(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ToDoService.ObterListaAsync();
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}