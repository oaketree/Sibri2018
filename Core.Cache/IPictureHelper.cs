using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utility
{
    public interface IPictureHelper
    {
        //void ProcessByStream(Stream s, string thumbnailPath, Size size);
        //void ProcessByFile(string originalImagePath, string thumbnailPath, Size size);

        void ProcessByStream(Stream s, PictureSize size);
        MemoryStream Ms { get; set; }
    }
}
