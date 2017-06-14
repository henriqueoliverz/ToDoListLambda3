using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListLambda3.Models
{
    public class ToDo : BaseDataObject
    {
        private string _title = string.Empty;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _done = false;

        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }

    }
}
