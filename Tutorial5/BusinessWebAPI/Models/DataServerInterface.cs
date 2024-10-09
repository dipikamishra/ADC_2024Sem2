// Alex Starling - Distributed Computing - 2021
using System.Drawing;
using System.ServiceModel;

namespace BusinessWebAPI.Models
{
    
    public interface DataServerInterface
    {
     
        int GetNumEntries();

        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    }
}
