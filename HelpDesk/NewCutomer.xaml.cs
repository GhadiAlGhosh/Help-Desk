using EASendMail;
using HelpDesk.DAL;
using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class NewCutomer : Page
    {
        public NewCutomer()
        {
            this.InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var Db = new HelpDeskDataContext())
                {
                    var customer = new Customer();
                    customer.Name = NameField.Text;
                    customer.EmailAddress = EmailField.Text;
                    Db.Add(customer);
                    Db.SaveChanges();
                    OutputTextBlock.Text = customer.Name + " " + "Created.";
                    ClearField();

                }

            }
            catch (Exception)
            {

                var message = new MessageDialog("Unable to Create Ticket");
                await message.ShowAsync();

            }
        }
        private void ClearField()
        {
            NameField.Text = string.Empty;
            EmailField.Text = string.Empty;
            PhoneField.Text = string.Empty;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewTicket));
        }

    }
}
