﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVUploadService.Model
{
    public class FileStore
    {
        public string FileName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public bool Status { get; set; }
        public string TableName { get; set; }
    }
}
