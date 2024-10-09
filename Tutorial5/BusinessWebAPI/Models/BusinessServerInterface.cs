using System.Drawing;
using System.ServiceModel;

namespace BusinessWebAPI.Models 
{
    public delegate bool SearchDelegate(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);


    public interface BusinessServerInterface
    {
       
        int GetNumEntries();

       
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

        bool SearchLastName(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    }
}
