using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Common.Models.Search
{
    public class QuickDetails
    {
        private string threesMmr;
        private string twosMMr;
        public string ThreesMmr { get => threesMmr?.Length == 0 ? "N/A" : threesMmr; set => threesMmr = value; }
        public string TwosMmr { get => twosMMr?.Length == 0 ? "N/A" : twosMMr; set => twosMMr = value; }
}
}
