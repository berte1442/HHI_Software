using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HHI_InspectionSoftware;


namespace HHI_InspectionSoftware.ViewModels
{
    public class TemplateModel
    {
        public Template Template { get; set; }
        public Area Area { get; set; }
        public HomeSystem HomeSystem { get; set; }
        public CheckItem CheckItem { get; set; }
        public Image Image { get; set; }
        public Comment Comment { get; set; }
        public Limitation Limitation { get; set; }
        public Category Category { get; set; }

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