using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HHI_InspectionSoftware.Controllers
{
    public class MetricsController : Controller
    {
        private HHIEntities6 db = new HHIEntities6();

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

            decimal rRev1 = 0;
            decimal rRev2 = 0;
            decimal rRev3 = 0;

            int counter = 0;
            decimal revTotal = 0;
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
                            revTotal += n.Price??0;
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

                        rRev1 = revTotal;

                    }
                    else if (counter > rCount2)
                    {
                        rCount3 = rCount2;
                        rCount2 = counter;

                        rName3 = rName2;

                        rName2 = name;

                        rRev2 = revTotal;
                    }
                    else if (counter > rCount3)
                    {
                        rCount3 = counter;

                        rName3 = name;

                        rRev3 = revTotal;
                    }

                    counter = 0;
                    revTotal = 0;
                }
            }

            ViewBag.rName1 = rName1;
            ViewBag.rCount1 = rCount1;
            ViewBag.rRev1 = rRev1;
            ViewBag.rName2 = rName2;
            ViewBag.rCount2 = rCount2;
            ViewBag.rRev2 = rRev2;
            ViewBag.rName3 = rName3;
            ViewBag.rCount3 = rCount3;
            ViewBag.rRev3 = rRev3;

            return View();
        }

        public ActionResult InspectorMetrics()
        {
            Inspector inspector;
            string iName1 = null;
            string iName2 = null;
            string iName3 = null;

            int iCount1 = 0;
            int iCount2 = 0;
            int iCount3 = 0;

            decimal iRev1 = 0;
            decimal iRev2 = 0;
            decimal iRev3 = 0;

            int counter = 0;
            decimal revTotal = 0;

            foreach (var i in db.Inspections)
            {
                if (db.Inspectors.Find(i.InspectorID).FirstName + " " + db.Inspectors.Find(i.InspectorID).LastName != iName1 &&
                    db.Inspectors.Find(i.InspectorID).FirstName + " " + db.Inspectors.Find(i.InspectorID).LastName != iName2 &&
                    db.Inspectors.Find(i.InspectorID).FirstName + " " + db.Inspectors.Find(i.InspectorID).LastName != iName3)
                { 
                    foreach (var n in db.Inspections)
                    {
                        if (i.InspectorID == n.InspectorID)
                        {
                            counter++;
                            revTotal += n.Price ?? 0;
                        }
                    }

                    inspector = db.Inspectors.Find(i.InspectorID);
                    string name = inspector.FirstName + " " + inspector.LastName;

                    if (counter > iCount1)
                    {
                        iCount3 = iCount2;
                        iCount2 = iCount1;
                        iCount1 = counter;

                        iName3 = iName2;
                        iName2 = iName1;

                        iName1 = name;

                        iRev1 = revTotal;

                    }
                    else if (counter > iCount2)
                    {
                        iCount3 = iCount2;
                        iCount2 = counter;

                        iName3 = iName2;

                        iName2 = name;

                        iRev2 = revTotal;

                    }
                    else if (counter > iCount3)
                    {
                        iCount3 = counter;

                        iName3 = name;

                        iRev3 = revTotal;

                    }

                    counter = 0;
                    revTotal = 0;
                }
            }

            ViewBag.iName1 = iName1;
            ViewBag.iCount1 = iCount1;
            ViewBag.iRev1 = iRev1;
            ViewBag.iName2 = iName2;
            ViewBag.iCount2 = iCount2;
            ViewBag.iRev2 = iRev2;
            ViewBag.iName3 = iName3;
            ViewBag.iCount3 = iCount3;
            ViewBag.iRev3 = iRev3;

            return View();
        }
    }
}