﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.upload.ueditor
{
    public interface ICrawlerHandler
    {
        Task Process();
    }
}
