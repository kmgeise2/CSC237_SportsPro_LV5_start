using System.Collections.Generic;

namespace CSC237_SportsPro_LV5_start.Models
{
    public class TechIncidentViewModel
    {
        public Technician Technician { get; set; }
        public Incident Incident { get; set; }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}
