using ScraperFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ScraperFront.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string SearchString, int? page, string sortBy)
        {

            yahooStocksDataEntities1 dataBase = new yahooStocksDataEntities1();

            ViewBag.SortDateParameter = string.IsNullOrEmpty(sortBy) ? "ScrapeDateTime" : "";
            ViewBag.SortTickerParameter = sortBy == "Ticker" ? "Ticker desc" : "Ticker";

            var data = dataBase.Stocks.AsQueryable();

            

            if (SearchString != null)
            {
                data = data.Where(x => x.Ticker.StartsWith(SearchString));
            }
            
            switch(sortBy)
            {
                case "Ticker desc":
                    data = data.OrderByDescending(x => x.Ticker);
                    break;
                case "Ticker":
                    data = data.OrderBy(x => x.Ticker);
                    break;
                case "ScrapeDateTime":
                    data = data.OrderBy(x => x.ScrapeDateTime);
                    break;
                default:
                    data = data.OrderByDescending(x => x.ScrapeDateTime);
                    break;
            }

            return View(data.ToPagedList(page ?? 1, 14));
        }

        public ActionResult Scrape()
        {
            var timeToScrape = new Scraper();
            timeToScrape.GoScrape();
            return RedirectToAction("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}