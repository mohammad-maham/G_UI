using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G_APIs.Services
{

    public class UploadFile
    {
        public List<string> Upload(HttpFileCollectionBase files)   //target= company or employee or ...
        {
            try
            {
                var uploadList = new List<string>();
                string[] validFiles = { "image/jpeg", "image/jpg" };

                if (files.Count > 2)
                    throw new Exception("تعداد مجاز برای آپلود تنها دو عدد عکس میباشد");


                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!validFiles.Contains(file.ContentType))
                            throw new Exception(" فرمت فایل ارسالی باید jpg باشد.");

                        if (file.ContentLength > 300 * 1024)
                            throw new Exception("سایز عر عکس نباید بیشتر از 300 کیلوبایت باشد");

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