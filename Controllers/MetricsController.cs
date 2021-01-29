using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HHI_InspectionSoftware.Controllers
{
    public class MetricsController : Controller
    {
        private HHIEntities5 db = new HHIEntities5();

        // GET: Metrics
        public ActionResult Index()
        {
            return View();
        }

        // returns top 3 realtors
        public ActionResult RealtorMetrics()
        {
            Realtor realtor;
            string rName1 = null;
            string rName2 = null;
            string rName3 = null;

            int rCount1 = 0;
            int rCount2 = 0;
            int rCount3 = 0;

            int counter = 0;
            foreach (var i in db.Inspections)
            {
                if (db.Realtors.Find(i.RealtorID).FirstName + " " + db.Realtors.Find(i.RealtorID).LastName != rName1 &&
                    db.Realtors.Find(i.RealtorID).FirstName + " " + db.Realtors.Find(i.RealtorID).LastName != rName2 &&
                    db.Realtors.Find(i.RealtorID).FirstName + " " + db.Realtors.Find(i.RealtorID).LastName != rName3)
                {

                    foreach (var n in db.Inspections)
                    {
                        if (i.RealtorID == n.RealtorID)
                        {
                            counter++;
                        }
                    }

                    realtor = db.Realtors.Find(i.RealtorID);
                    string name = realtor.FirstName + " " + realtor.LastName;

                    if (counter > rCount1)
                    {
                        rCount3 = rCount2;
                        rCount2 = rCount1;
                        rCount1 = counter;

                        rName3 = rName2;
                        rName2 = rName1;

                        rName1 = name;

                    }
                    else if (counter > rCount2)
                    {
                        rCount3 = rCount2;
                        rCount2 = counter;

                        rName3 = rName2;

                        rName2 = name;
                    }
                    else if (counter > rCount3)
                    {
                        rCount3 = counter;

                        rName3 = name;
                    }

                    counter = 0;
                }
            }

            ViewBag.rName1 = rName1;
            ViewBag.rCount1 = rCount1;
            ViewBag.rName2 = rName2;
            ViewBag.rCount2 = rCount2;
            ViewBag.rName3 = rName3;
            ViewBag.rCount3 = rCount3;

            return View();
        }
    }
}