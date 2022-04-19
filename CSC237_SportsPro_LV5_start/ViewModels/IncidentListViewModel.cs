using System.Collections.Generic;

namespace CSC237_SportsPro_LV5_start.Models
{
    public class IncidentListViewModel
    {
        public string Filter { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
