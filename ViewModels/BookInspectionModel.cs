using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHI_InspectionSoftware.ViewModels
{
    public class BookInspectionModel
    {
        private HHIEntities6 db = new HHIEntities6();

        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public Realtor Realtor { get; set; }
        public Inspection Inspection { get; set; }

        public List<Realtor> Realtors { get; set; }
        public List<Template> Templates { get; set; }
        public List<Inspector> Inspectors { get; set; }
        public List<InspectionStatus> InspectionStatuses { get; set; }
    }
}