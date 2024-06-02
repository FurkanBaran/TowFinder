using System.Collections.Generic;
using TowFinder.Models;

namespace TowFinder.ViewModels
{
    public class HomeViewModel
    {
        public string City { get; set; }
        public string District { get; set; }
        public List<string> Cities { get; set; }
        public List<TowOperator> TowOperators { get; set; }
    }
}
