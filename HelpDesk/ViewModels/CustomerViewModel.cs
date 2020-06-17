using HelpDesk.DAL;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.ViewModels
{
    public class CustomerViewModel : BindableBase<Customer>
    {
        private IList<Customer> list;
        public IList<Customer> List
        {
            get { return list; }
            set { list = value; }
        }
        ObservableCollection<Customer> CustList = new ObservableCollection<Customer>();
        public CustomerViewModel()
        {
            using (var Db = new HelpDeskDataContext())
            {
                var query = from c in Db.Customers
                            select c;
                foreach (var c in query)
                {
                    CustList.Add(c);
                    list = CustList;
                }
            }
        
        }
        public string Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

      
        
    }
}
