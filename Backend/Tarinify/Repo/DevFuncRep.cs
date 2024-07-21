
using MediaInfo.DotNetWrapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Xabe.FFmpeg;
using System.IO;
using ImageMagick;
using Azure.Core;
using Trainify.Entities;
using Trainify.Context;
namespace Trainify.Repo
{
    public class DevFuncRep : IDevFuncRep
    {
        private readonly DbContainer db;

        public DevFuncRep(DbContainer db)
        {
            this.db = db;
        }
      

        public void ConvertVideoToGif(IFormFile file)
        {

            //convert input_video.mp4 output.gif
            string TempFilePath = Path.Combine("wwwroot", "v.mp4");
            string gifpath = Path.Combine("wwwroot", "gif.gif");
            using (FileStream fs = new FileStream(TempFilePath, FileMode.Create))
            {
                file.CopyTo(fs);
            }
            //srcLink = Session["videoPath"].ToString();
            System.Diagnostics.Process grabInfoProcess;
            grabInfoProcess = new System.Diagnostics.Process();
            grabInfoProcess.StartInfo.UseShellExecute = false;
            grabInfoProcess.StartInfo.RedirectStandardOutput = true;
            grabInfoProcess.StartInfo.RedirectStandardError = true;
            grabInfoProcess.StartInfo.CreateNoWindow = true;
            grabInfoProcess.StartInfo.FileName = @"C:\ffmpeg\\bin\\ffmpeg.exe";
            grabInfoProcess.StartInfo.Arguments = "ffmpeg -ss 5 -i "+TempFilePath+" -t 10 -pix_fmt rgb24 "+gifpath;
            //ffmpeg - i source_video.avi animated_gif.gif
            grabInfoProcess.Start();
            string output = grabInfoProcess.StandardOutput.ReadToEnd();
            string newoutput = grabInfoProcess.StandardError.ReadToEnd();
            grabInfoProcess.WaitForExit();

        }















        public FormFile CreateEmptyFile()
        {
            var stream = new MemoryStream();

            // Create a new instance of FormFile
            var file = new FormFile(stream, 0, stream.Length, null, "no")
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream" // You may need to adjust the content type
            };

            return file;
        }

        public bool IsVideo(IFormFile file)
        {
            int indx = file.FileName.Split('.').Length - 1;
            string extension = file.FileName.Split('.')[indx];

            // List of video file extensions
            string[] videoExtensions = { "mp4", "avi", "mov", "wmv", "flv", "mkv", "webm" }; // Add more if needed

            // Check if the file extension matches any video extension
            foreach (string videoExtension in videoExtensions)
            {
                if (extension.Equals(videoExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
