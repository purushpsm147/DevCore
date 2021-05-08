using SGRE.TSA.Models.Extensions;

namespace SGRE.TSA.Models
{
    public class BlobUpload
    {
        private string _id;
        private string _fileClass;
        private string _fileDescriptor;
        private string _filedName;


        public string ID
        {
            get { return _id.RemoveSpace(); }
            set { _id = value.RemoveSpace(); }
        }

        public string FileClass
        {
            get { return _fileClass.RemoveSpace(); }
            set { _fileClass = value.RemoveSpace(); }
        }


        public string FileDescriptor
        {
            get { return _fileDescriptor.RemoveSpace(); }
            set { _fileDescriptor = value.RemoveSpace(); }
        }

        public string FieldName
        {
            get { return _filedName.RemoveSpace(); }
            set { _filedName = value.RemoveSpace(); }
        }

    }
}
