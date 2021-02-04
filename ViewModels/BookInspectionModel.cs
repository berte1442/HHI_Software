using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHI_InspectionSoftware.ViewModels
{
    public class BookInspectionModel
    {
        public BookInspectionModel()
        {
            Realtors = db.Realtors;
            Templates = db.Templates;
            Inspectors = db.Inspectors;
            InspectionStatuses = db.InspectionStatus;
        }
        private HHIEntities6 db = new HHIEntities6();

        public Address Address { get; set; }
        public Customer Customer { get; set; }
        public Realtor Realtor { get; set; }
        public Inspection Inspection { get; set; }
        public int TemplateID { get; set; }
        public int InspectionStatusID { get; set; }
        public int InspectorID { get; set; }

        public IEnumerable<Realtor> Realtors { get; set; }
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<Inspector> Inspectors { get; set; }
        public IEnumerable<InspectionStatus> InspectionStatuses { get; set; }
    }

}