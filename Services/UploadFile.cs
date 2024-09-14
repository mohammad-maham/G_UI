using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace G_APIs.Services
{

    public class UploadFile
    {
        public List<string> Upload(HttpFileCollectionBase files)   //target= company or employee or ...
        {
            try
            {
                var uploadList = new List<string>();
                var validFiles = new[] { ".jpg", ".jpeg", ".png", ".pdf" };

                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            file.InputStream.CopyTo(memoryStream);
                            var fileBytes = memoryStream.ToArray();
                            var base64String = Convert.ToBase64String(fileBytes);
                            uploadList.Add(base64String);
                        }
                    }
                }
                return uploadList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}