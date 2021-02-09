using System.Collections.Generic;

namespace Inventory_Management_System.ViewModels
{
    public class TrendAnalysisChartViewModel
    {
        public List<string> category { get; set; }
        public List<TrendAnalysisChartDetails> data { get; set; }


    }

    public class TrendAnalysisChartDetails
    {
        public string name { get; set; }
        public List<int> data { get; set; }

    }
}
