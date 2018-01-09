using System.Web;

namespace SPCF.ViewModels
{
    public class ImageArray
    {
        public static byte[] ToByteArray(HttpPostedFileBase posted)
        {
            if (posted == null)
                return null;

            var array = new byte[posted.ContentLength];
            posted.InputStream.Position = 0;
            posted.InputStream.Read(array, 0, posted.ContentLength);
            return array;
        }
    }
}