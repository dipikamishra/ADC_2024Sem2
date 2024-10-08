using System;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Reflection;
//using BusinessWebAPI;

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
        }

        private void IndexButton_OnClick(object sender, RoutedEventArgs e)
        {

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
                // Define the base URL of the API
                RestClient restClient = new RestClient("http://localhost:5159");
                // restRequest.AddUrlSegment("lastname", SearchLastNameText.Text);
                // Create a RestRequest object with the appropriate endpoint and HTTP method
                RestRequest restRequest = new RestRequest($"/search/lastName?name={lastName}", Method.Get);

                // Add the URL segment for the search query
               restRequest.AddUrlSegment("lastName", SearchLastNameText.Text);

                // Execute the request asynchronously
                RestResponse restResponse = await restClient.ExecuteAsync(restRequest);

                // Check if the response is successful
                if (restResponse.IsSuccessful)
                {
                    // Deserialize the JSON response to a dynamic object
                    var result = JsonConvert.DeserializeObject<dynamic>(restResponse.Content);

                    // Update UI elements with the search result
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

        /*private async Task SearchByLastNameAsync(string lastName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define the API URL (replace with the correct port for your API)
                    string apiUrl = $"http://localhost:5159/api/BusinesssWebAPI/search/{lastName}";

                    // Send the GET request
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();

                        // Update UI with search results
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

       
    }
}*/
