using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        /*private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient("http://localhost:5276");
            RestRequest restRequest = new RestRequest("/GetAll/detail/{id}", Method.Get);
            restRequest.AddUrlSegment("id", SIndex.Text);
            RestResponse restResponse = restClient.Execute(restRequest);
            // Console.WriteLine(restResponse.Content);
            Student student = JsonConvert.DeserializeObject<Student>(restResponse.Content);
            SID.Text = student.Id.ToString();
            SName.Text = student.Name;
            SAge.Text = student.Age.ToString();
        }*/

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
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
                // Define the base URL of the API
                RestClient restClient = new RestClient("http://localhost:5276");

                // Create a RestRequest object with the appropriate endpoint and HTTP method
                RestRequest restRequest = new RestRequest("/student/detail/{id}", Method.Get);

                // Add the URL segment for the search query
                restRequest.AddUrlSegment("id", id);

                // Execute the request asynchronously
                RestResponse restResponse = await restClient.ExecuteAsync(restRequest);

                // Check if the response is successful
                if (restResponse.IsSuccessful)
                {
                    // Deserialize the JSON response into a Student object
                    Student student = JsonConvert.DeserializeObject<Student>(restResponse.Content);

                    // Update UI elements with the search result
                    if (student != null)
                    {
                        SID.Text = student.Id.ToString();
                        SName.Text = student.Name;
                        SAge.Text = student.Age.ToString();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
