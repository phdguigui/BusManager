using BusManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusManager.Model.Interfaces
{
    public interface IStopService
    {
        public bool AddStop(Stop stop);
        public bool AddManyStop(List<Stop> stopList);
        public bool DeleteStop(Stop stop);
        public List<Stop> GetRouteLine(int lineId);
        public Stop? GetStop(Stop stop);
    }
}
