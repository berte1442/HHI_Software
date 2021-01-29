using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HHI_InspectionSoftware;


namespace HHI_InspectionSoftware.Models
{
    public class TemplateModel
    {
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        public IEnumerable<HomeSystem> HomeSystems { get; set; }
        public IEnumerable<CheckItem> CheckItems { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Limitation> Limitations { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}