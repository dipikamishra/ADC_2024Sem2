using System.Drawing;
using System.IO;
using APICoreClasses;

namespace ServerWebAPI.Models
{
    public class DataGen
    {

        private readonly Random _rand = new Random();

        private readonly string[] _fNameList = {
            "Robert", "Jack", "John", "Jane", "Michael", "William", "David", "Stefan", "Nelson", "Richard", "Charlie", "Mary", "Linda", "Susan", "Jessica", "Kathleen", "Ann"
        };

        private readonly string[] _lNameList = {
            "Smith", "Johnson", "Williams", "Jones", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "Citizen", "Doe"
        };

        private readonly List<Bitmap> _icons;

        //Initialize the icons int he constructor
        public DataGen()
        {
            _icons = new List<Bitmap>(); // Initialize the list of icons for image
            // Generate a few really basic icons
            // Probably not the best way to do it, but it works :)
            for (var i = 0; i < 10; i++)
            {
                var image = new Bitmap(64, 64);
                for (var x = 0; x < 64; x++)
                {
                    for (var y = 0; y < 64; y++)
                    {
                        image.SetPixel(x, y, Color.FromArgb(_rand.Next(256), _rand.Next(256), _rand.Next(256)));
                    }
                }
                _icons.Add(image);
            }
        }

       

        // Convert the Bitmap to a base64 string
        public static string ConvertBitmapToBase64(Bitmap bitmap)
        {
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private string GetFirstName() => _fNameList[_rand.Next(_fNameList.Length)];

        private string GetLastName() => _lNameList[_rand.Next(_lNameList.Length)];

        private uint GetPIN() => (uint)_rand.Next(9999);

        private uint GetAcctNo() => (uint)_rand.Next(100000000, 999999999);

        private int GetBalance() => _rand.Next(-10000, 10000);

        private Bitmap GetRandomIcon() => _icons[_rand.Next(_icons.Count)];

       // private Bitmap GetIcon() => _icons[_rand.Next(_icons.Count)];

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance, out string icon)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();
            // Get a random Bitmap icon and convert it to base64 string
            Bitmap randomIcon = GetRandomIcon();
            icon = APICoreClasses.Imagefn.ConvertBitmapToBase64(randomIcon);
            //icon = GetIcon();
        }

        //public int NumOAccts() => _rand.Next(100000, 999999);
        public int NumOAccts() => 10;

    }
}
