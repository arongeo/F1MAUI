using MobilProj.Model;
using MobilProj.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.ViewModel
{
    public class DriverViewModel
    {
        public ObservableCollection<Driver> Drivers { get; set; } = new();
        private DriverService DriverService { get; set; } = new DriverService();

        public DriverViewModel()
        {
            Drivers = new ObservableCollection<Driver>(DriverService.LoadDrivers().Result);
        }
    }
}
