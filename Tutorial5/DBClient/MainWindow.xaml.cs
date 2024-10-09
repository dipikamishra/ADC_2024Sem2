// Alex Starling - Distributed Computing - 2021
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using DBInterface;
using BusinessTier;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Linq;

namespace DBClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate bool SearchDelegate(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    public partial class MainWindow : Window
    {
        //private DataServerInterface foob;
        private BusinessServerInterface foob;
        private SearchDelegate searchDelegate;

        public MainWindow()
        {
            InitializeComponent();

            // This is a factory that generates remote connections to our remote class. This 
            // is what hides the RPC stuff!
            var tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            var URL = "net.tcp://localhost:8200/BusinessService";
            var chanFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
            foob = chanFactory.CreateChannel();
            // Also, tell me how many entries are in the DB.
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

        private bool SearchDB(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon)
        {
            return foob.SearchLastName(lastName, out acctNo, out pin, out bal, out fName, out lName, out icon);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchLastNameText.Text;
            // Validate input
            if (string.IsNullOrEmpty(searchText) || !searchText.All(char.IsLetter))
            {
                MessageBox.Show("Please enter a valid name. Special characters and numbers are not allowed.");
                return;
            }
            // Disable UI elements and show progress bar
            Dispatcher.Invoke(() =>
            {
                SearchLastNameText.IsReadOnly = true;
                SearchButton.IsEnabled = false;
                ProgressBar.IsIndeterminate = true;
            });

            // Create delegate for searching and set callback method
            searchDelegate = SearchDB;
            AsyncCallback callback = this.OnSearchCompletion;

            // Begin asynchronous search
            IAsyncResult result = searchDelegate.BeginInvoke(SearchLastNameText.Text, out uint _, out uint _, out int _, out string _, out string _, out Bitmap _, callback, null);
        }

        private void OnSearchCompletion(IAsyncResult asyncResult)
        {
            // Get the AsyncResult object
            AsyncResult asyncobj = (AsyncResult)asyncResult;
            // Get the delegate that was used to call the method
            SearchDelegate search = (SearchDelegate)((AsyncResult)asyncResult).AsyncDelegate;
            bool success = false;
            uint acctNo = 0;
            uint pin = 0;
            int bal = 0;
            string fName = "";
            string lName = "";
            Bitmap icon = null;

            try
            {
                // Complete the asynchronous operation and get the result
                success = search.EndInvoke(out acctNo, out pin, out bal, out fName, out lName, out icon, asyncResult);

                Dispatcher.Invoke(() =>
                {
                    // If found match
                    if (success)
                    {
                        // Update the UI with the search result
                        FirstName.Text = fName;
                        LastName.Text = lName;
                        Balance.Text = bal.ToString("C");
                        AcctNo.Text = acctNo.ToString();
                        Pin.Text = pin.ToString("D4");
                        UserIcon.Source = Imaging.CreateBitmapSourceFromHBitmap(icon.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                    else
                    {
                        MessageBox.Show("No records found.");
                    }
                });
            }
            catch (Exception ex)
            {
                // Show error message
                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show($"Error: {ex.Message}");
                });
            }
            finally
            {
                // Reset UI and progess bar
                Dispatcher.Invoke(() =>
                {
                    SearchLastNameText.IsReadOnly = false;
                    SearchButton.IsEnabled = true;
                    ProgressBar.IsIndeterminate = false;

                    // Dispose of the icon if it was created
                    icon?.Dispose();
                });
            }

            // Close the wait handle
            asyncobj.AsyncWaitHandle.Close();
        }
    }
}
