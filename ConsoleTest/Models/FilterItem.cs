using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Models
{
    public class FilterItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FilterNameModel : FilterItem
    {
        public IList<FilterItem> Children { get; set; }
    }
}
