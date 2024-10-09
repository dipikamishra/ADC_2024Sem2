using System.Drawing;
using System.ServiceModel;

namespace BusinessTier
{
    public delegate bool SearchDelegate(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

        [OperationContract]
        bool SearchLastName(string lastName, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    }
}
