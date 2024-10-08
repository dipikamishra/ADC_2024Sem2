using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using APICoreClasses;

namespace SimpleClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           // OpenConsole();
        }

       

        private async void SearchLNButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable UI elements and show the progress bar
            SearchLastName.IsReadOnly = true;
            SearchButton2.IsEnabled = false;
            ProgressBar.IsIndeterminate = true;

            await SearchByLNAsync(SearchLastName.Text);

            // Re-enable the UI after search is complete
            SearchLastName.IsReadOnly = false;
            SearchButton2.IsEnabled = true;
            ProgressBar.IsIndeterminate = false;

        }


        private async Task SearchByLNAsync(string LN)
        {
            try
            {
                // Validate if the input string is null or empty
                if (string.IsNullOrWhiteSpace(LN))
                {
                    MessageBox.Show("Please enter a valid search string.");
                    return;
                }

                

                // Define the base URL of the API
                RestClient restClient = new RestClient("http://localhost:5203");

                // Create a RestRequest object with the appropriate endpoint and HTTP method
                RestRequest restRequest = new RestRequest("/customerLN/detail/{LN}", Method.Get);

                // Add the URL segment for the search query (passing string as 'id')
                restRequest.AddUrlSegment("LN", LN);

                // Execute the request asynchronously
                RestResponse restResponse = await restClient.ExecuteAsync(restRequest);

                // Check if the response is successful
                if (restResponse.IsSuccessful)
                {
                    // Deserialize the JSON response into a Customer object
                    APICoreClasses.Customer customer = JsonConvert.DeserializeObject<APICoreClasses.Customer>(restResponse.Content);

                    // Update UI elements with the search result
                    if (customer != null)
                    {
                        // Use Dispatcher.Invoke to update the UI from the correct thread
                        Dispatcher.Invoke(() =>
                        {
                            firstName.Text = customer.FirstName ?? "N/A";
                            lastName.Text = customer.LastName ?? "N/A";
                            acctNo.Text = customer.AcctNo.ToString();
                            pin.Text = customer.Pin.ToString();
                            balance.Text = customer.Balance.ToString();
                        });
                    }
                    else
                    {
                        MessageBox.Show("No records found.");
                    }
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




        private async void SearchIButton_Click(object sender, RoutedEventArgs e)
        {
            // Disable UI elements and show the progress bar
            SIndex.IsReadOnly = true;
            SearchButton.IsEnabled = false;
            ProgressBar.IsIndeterminate = true;

            await SearchByIdAsync(SIndex.Text);

            // Re-enable the UI after search is complete
            SIndex.IsReadOnly = false;
            SearchButton.IsEnabled = true;
            ProgressBar.IsIndeterminate = false;

        }




        
        
        private async Task SearchByIdAsync(string id)
        {
            try
            {

                // Parse the input string into an integer
                if (!int.TryParse(id, out int parsedId))
                {
                    MessageBox.Show("Please enter a valid integer.");
                    return;
                }

                // Define the base URL of the API
                RestClient restClient = new RestClient("http://localhost:5203");

                // Create a RestRequest object with the appropriate endpoint and HTTP method
                RestRequest restRequest = new RestRequest("/customer/detail/{id}", Method.Get);

                // Add the URL segment for the search query
                restRequest.AddUrlSegment("id", parsedId);

                // Execute the request asynchronously
                RestResponse restResponse = await restClient.ExecuteAsync(restRequest);

                // Check if the response is successful
                if (restResponse.IsSuccessful)

                {                   
                    // Deserialize the JSON response into a Student object
                    
                    APICoreClasses.Customer customer = JsonConvert.DeserializeObject<APICoreClasses.Customer>(restResponse.Content);
                    //Console.WriteLine($"Response from WPFClient: {restResponse.StatusCode} - {Content}");
                    // Update UI elements with the search result
                    if (customer != null)
                    {


                        // Console.WriteLine($"Customer data: {customer.FirstName} {customer.LastName}, AcctNo: {customer.AcctNo}");

                        // Use Dispatcher.Invoke to update the UI from the correct thread

                        Dispatcher.Invoke(() =>
                        {
                            firstName.Text = customer.FirstName.ToString() ?? "N/A";
                            lastName.Text = customer.LastName ?? "N/A";
                            acctNo.Text = customer.AcctNo.ToString();
                            pin.Text = customer.Pin.ToString();
                            balance.Text = customer.Balance.ToString();
                        });
                        
                    }
                    else
                    {
                        MessageBox.Show("No records found.");
                    }
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

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}





//Object reference not set to an instance of an object.
