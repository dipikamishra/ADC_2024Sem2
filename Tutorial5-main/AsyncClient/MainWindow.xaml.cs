using System;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using BusinessTier;
using DBInterface;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Linq;

namespace AsyncClient
{
    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;

        public MainWindow()
        {
            InitializeComponent();
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8200/BusinessService";
            var chanFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = chanFactory.CreateChannel();
            NoItems.Content = "Total Items: " + foob.GetNumEntries();
            LoadData(0);
            IndexBox.Text = "0";
        }

        private void IndexButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IndexBox.Text, out var index))
            {
                LoadData(index);
            }
            else
            {
                MessageBox.Show($"\"{IndexBox.Text}\" is not a valid integer...");
            }
        }

        private void LoadData(int index)
        {
            try
            {
                foob.GetValuesForEntry(index, out var accNo, out var pin, out var bal, out var fName, out var lName, out var icon);
                FirstName.Text = fName;
                LastName.Text = lName;
                Balance.Text = bal.ToString("C");
                AcctNo.Text = accNo.ToString();
                Pin.Text = pin.ToString("D4");
                // Convert to image source
                UserIcon.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                icon.Dispose();
            }
            catch (FaultException<IndexOutOfRangeFault> exception)
            {
                MessageBox.Show(exception.Detail.Issue);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchLastNameText.Text;
            // Validate input
            if (string.IsNullOrEmpty(searchText) || !searchText.All(char.IsLetter))
            {
                MessageBox.Show("Please enter a valid name. Special characters and numbers are not allowed.");
                return;
            }

            // Disable UI elements and show the progress bar
            SearchLastNameText.IsReadOnly = true;
            SearchButton.IsEnabled = false;
            ProgressBar.IsIndeterminate = true;

            await SearchByLastNameAsync(SearchLastNameText.Text);

            // Re-enable the UI after search is complete
            SearchLastNameText.IsReadOnly = false;
            SearchButton.IsEnabled = true;
            ProgressBar.IsIndeterminate = false;
        }

        private async Task SearchByLastNameAsync(string lastName)
        {
            try
            {
                // Search for the last name on a background thread
                var result = await Task.Run(() =>
                {
                    // Call SearchLastName method on the server
                    bool success = foob.SearchLastName(lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
                    return new { success, acctNo, pin, bal, fName, lName };
                });

                // Check if the search was successful
                if (result.success)
                {
                    // Update the UI with the search result
                    FirstName.Text = result.fName;
                    LastName.Text = result.lName;
                    Balance.Text = result.bal.ToString("C");
                    AcctNo.Text = result.acctNo.ToString();
                    Pin.Text = result.pin.ToString("D4");
                }
                else
                {
                    MessageBox.Show("No records found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
