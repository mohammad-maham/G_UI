using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace G_APIs.Services { 

public class UploadFile
{
    public List<string> Upload(HttpFileCollectionBase files)   //target= company or employee or ...
    {
        try
        {
            var uploadList = new List<string>();
            var uploadFolder = @"UserUploads\";
            var validFiles =new[]  { ".jpg", ".jpeg", ".png", ".pdf" };

            foreach (var key in files.AllKeys)
            {
                var posteFile = files.Get(key);
                if (posteFile != null)
                {
                    if (!validFiles.Contains(Path.GetExtension(posteFile.FileName)))
                        throw new Exception("فقط تصاویر و فایل های pdf قابل آپلود میباشند.");

                    var fileName = Guid.NewGuid().ToString().Substring(0, 5) + "-" + posteFile.FileName;
                    var savePath = AppDomain.CurrentDomain.BaseDirectory + uploadFolder  +fileName;

                    posteFile.SaveAs(savePath);
                    uploadList.Add(fileName);
                }
            }

            return uploadList;
            //if (file.FileAsBase64.Contains(","))
            //    file.FileAsBase64 = file.FileAsBase64.Substring(file.FileAsBase64.IndexOf(",") + 1);

            //file.FileAsByteArray = Convert.FromBase64String(file.FileAsBase64);
            //using (var fs = new FileStream(filePathName, FileMode.CreateNew))
            //{
            //    fs.Write(file.FileAsByteArray, 0, file.FileAsByteArray.Length);
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}}