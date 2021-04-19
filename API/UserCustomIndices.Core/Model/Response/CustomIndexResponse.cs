﻿using System.Collections.Generic;
using UserCustomIndices.Core.Model;

namespace UserCustomIndices.Model.Response
{
    public class CustomIndexResponse
    {
        public string UserId { get; set; }
        public List<string> Markets { get; set; }
        public List<Rule> Rules { get; init; } = new List<Rule>();
    }
}
