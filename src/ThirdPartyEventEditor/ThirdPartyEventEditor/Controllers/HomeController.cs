using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThirdPartyEventEditor.BusinessLogic.Services;
using ThirdPartyEventEditor.DataAccess.Models;

namespace ThirdPartyEventEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<ThirdPartyEvent, int> _thirdPartyEventService;

        public HomeController(IService<ThirdPartyEvent, int> thirdPartyEventService)
        {
            _thirdPartyEventService = thirdPartyEventService ?? throw new ArgumentNullException(nameof(thirdPartyEventService));
        }

        public ActionResult SaveEventList()
        {
            var pathToFolder = Server.MapPath("~/App_Data/");

            var pathToFile = Path.Combine(pathToFolder, "ThirdPartyEvents.json");

            var content = System.IO.File.ReadAllBytes(pathToFile);

            return File(content, "application/json");
        }

        public ActionResult Index()
        {
            var models = _thirdPartyEventService.GetAll();

            return View(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ThirdPartyEvent model)
        {
            if (ModelState.IsValid)
            {
                _thirdPartyEventService.Insert(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _thirdPartyEventService.GetAll().FirstOrDefault(m => m.Id == id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ThirdPartyEvent model)
        {
            if (ModelState.IsValid)
            {
                _thirdPartyEventService.Update(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _thirdPartyEventService.Delete(id);

            return RedirectToAction("Index");
        }

        private async Task<string> UploadSampleImage()
        {
            var path = Path.Combine(Server.MapPath("~/App_Data/"), "poster_circus.png");
            using (var memoryStream = new MemoryStream())
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                await fileStream.CopyToAsync(memoryStream);
                return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}