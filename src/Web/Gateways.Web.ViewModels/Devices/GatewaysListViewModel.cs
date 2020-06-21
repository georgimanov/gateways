namespace Gateways.Web.ViewModels.Devices
{
    using System.Collections.Generic;

    public class GatewaysListViewModel
    {
        public GatewaysListViewModel()
        {
            this.Gateways = new List<GatewayViewModel>();
        }

        public IEnumerable<GatewayViewModel> Gateways { get; set; }
    }
}
