using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
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
using System.Web.UI;

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

                            // Convert the base64 string back to BitmapImage
                            if (!string.IsNullOrEmpty(customer.Icon))
                            {
                                BitmapImage image = ConvertBase64ToBitmapImage(customer.Icon);
                                UserIcon.Source = image;  // Set the image to the WPF Image control
                            }
                            else
                            {
                                MessageBox.Show("No image available for this customer.");
                            }
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
                            // Convert the base64 string back to BitmapImage
                            if (!string.IsNullOrEmpty(customer.Icon))
                            {
                                BitmapImage image = ConvertBase64ToBitmapImage(customer.Icon);
                                UserIcon.Source = image;  // Set the image to the WPF Image control
                            }
                            else
                            {
                                MessageBox.Show("No image available for this customer.");
                            }
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

        public BitmapImage ConvertBase64ToBitmapImage(string base64String)
        {
            // Decode the base64 string into byte array
            byte[] binaryData = Convert.FromBase64String(base64String);

            // Create a MemoryStream from the byte array
            using (var stream = new System.IO.MemoryStream(binaryData))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // Load the image immediately
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze(); // Freeze the image to make it usable across threads
                return image;
            }
        }

       /* private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RestClient restClient = new RestClient("http://localhost:5076");
            RestRequest restRequest = new RestRequest("/customer/details", Method.Post);
            APICoreClasses.Customer customer = new APICoreClasses.Customer();
            customer.FirstName = firstName.Text;
            customer.LastName = lastName.Text;
            customer.AcctNo = UInt32.Parse(acctNo.Text);
            customer.Pin = UInt32.Parse(pin.Text);
            customer.Balance = Int32.Parse(balance.Text);

            // Convert the image to base64 and set the Icon property
            Bitmap bitmap = ConvertImageSourceToBitmap(UserIcon.Source);
            string base64Icon = Imagefn.ConvertBitmapToBase64(bitmap);
            customer.Icon = base64Icon;

            restRequest.RequestFormat = RestSharp.DataFormat.Json;
            restRequest.AddBody(customer);

            RestResponse restResponse = restClient.Execute(restRequest);
            // Console.WriteLine(restResponse.Content);
            MessageBox.Show(restResponse.Content);

        }*/



        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RestClient restClient = new RestClient("http://localhost:5203");
                RestRequest restRequest = new RestRequest("/customerA/details/data", Method.Post);
                APICoreClasses.Customer customer = new APICoreClasses.Customer
                {
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    AcctNo = UInt32.Parse(acctNo.Text),
                    Pin = UInt32.Parse(pin.Text),
                    Balance = Int32.Parse(balance.Text)
                };

                // Convert the image to base64 and set the Icon property
                Bitmap bitmap = ConvertImageSourceToBitmap(UserIcon.Source);
                string base64Icon = Imagefn.ConvertBitmapToBase64(bitmap);
                customer.Icon = base64Icon;

                // Set request format and add the customer object as body
                restRequest.RequestFormat = RestSharp.DataFormat.Json;
                restRequest.AddBody(customer);

                // Execute the request asynchronously
                RestResponse restResponse = await restClient.ExecuteAsync(restRequest);

                // Ensure UI updates happen on the UI thread using Dispatcher.Invoke
                Dispatcher.Invoke(() =>
                {
                    // Update UI (e.g., show message or update text fields)
                    MessageBox.Show(restResponse.Content);
                });
            }
            catch (Exception ex)
            {
                // Handle the exception if any
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        


        private Bitmap ConvertImageSourceToBitmap(ImageSource imageSource)
        {
            BitmapImage bitmapImage = imageSource as BitmapImage;
            if (bitmapImage != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder(); // Use the correct encoder based on your image type
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    memoryStream.Position = 0;
                    return new Bitmap(memoryStream); // Convert to System.Drawing.Bitmap
                }
            }
            return null;
        }
    }
}





//Object reference not set to an instance of an object.
