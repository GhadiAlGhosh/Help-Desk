using HelpDesk.DAL;
using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HelpDesk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTicket : Page
    {
        public NewTicket()
        {
            this.InitializeComponent();
            Customer = new CustomerViewModel();
            
            
        }
        public CustomerViewModel Customer;

        private void WarrantySwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggle = sender as ToggleSwitch;
            if (toggle!=null)
            {
                if (toggle.IsOn==true)
                {
                    ExpirationDate.IsEnabled = true;
                }
                else
                {
                    ExpirationDate.IsEnabled = false;
                }
            }
        }

        private void AddCustomer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewCutomer));
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home));
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var ticket = new Ticket();
            var customer = new Customer();

            using (var Db = new HelpDeskDataContext())
            {
                try
                {
                    customer.Name = CustomerList.Text;
                    ticket.DeviceType = DeviceType.Text;
                    ticket.Customer = customer;
                    ticket.SerialNumber = SerialNumberField.Text;
                    ticket.PartNumber = PartNumberField.Text;
                    Db.Add(ticket);
                    Db.SaveChanges();
                    OutputTextBlock.Text = ticket.Id + " " + "Created.";
                }
                catch (Exception)
                {

                    var message = new MessageDialog("Error Ticket Not Created");
                    await message.ShowAsync();
                }
             
            }
            SubmitButton.IsEnabled = false;
            var send = new Send();
            await send.Send_Email(ticket);
            SubmitButton.IsEnabled = true;
            ClearField();

        }

        private void ClearField()
        {
            DeviceType.SelectedIndex = -1;
            CustomerList.SelectedIndex = -1;
            SerialNumberField.Text = string.Empty;
            PartNumberField.Text = string.Empty;



        }
    }
}
