﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RequestModel
{
    public class PitchDeckModel
    {
        public IFormFile File { get; set; }
    }
}
