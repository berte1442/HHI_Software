using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHI_InspectionSoftware.Models
{
    public class MetricsModel
    {
        public Inspection Inspection { get; set; }
        public Inspector Inspector { get; set; }
        public Realtor Realtor { get; set; }

    }
}