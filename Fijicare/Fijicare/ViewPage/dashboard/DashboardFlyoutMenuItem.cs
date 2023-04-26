using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fijicare.ViewPage.dashboard
{
    public class DashboardFlyoutMenuItem
    {
        public DashboardFlyoutMenuItem()
        {
            TargetType = typeof(DashboardFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}