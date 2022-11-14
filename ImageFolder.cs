using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellie
{
    class ImageFolder
    {
        public string FolderPath { get; set; }

        public List<string> ImagePaths = new List<string>();

        public ImageFolder(string folderpath)
        {
            FolderPath = folderpath;

            var paths = System.IO.Directory.GetFiles(FolderPath);

            string[] extentions = { "jpg", "png", "bmp", "ico" };

	    var log = System.IO.File.AppendText("debug.log");
            foreach(string path in paths)
            {
                foreach(string extention in extentions)
                {
                    if(path.EndsWith(extention))
                    {
                        ImagePaths.Add(path);
			log.WriteLine(path);
                    }
                }
            }
        }
    }
}
