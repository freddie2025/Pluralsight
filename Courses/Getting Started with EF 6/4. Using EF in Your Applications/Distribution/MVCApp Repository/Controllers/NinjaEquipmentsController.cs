using System.Net;
using System.Web.Mvc;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace MVCApp.Controllers
{
  public class NinjaEquipmentsController : Controller
  {
    private readonly DisconnectedRepository _repo = new DisconnectedRepository();
    // GET: NinjaEquipments/Create
    public ActionResult Create(int ninjaId, string ninjaName)
    {
      ViewBag.NinjaId = ninjaId;
      ViewBag.NinjaName = ninjaName;
      return View();
    }

    // POST: NinjaEquipments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(
      [Bind(Include = "Id,Name,Type,DateCreated,DateModified,NinjaId")] NinjaEquipment ninjaEquipment)
    {
      int ninjaId;
      if (!int.TryParse(Request.Form["NinjaId"], out ninjaId))
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      _repo.SaveNewEquipment(ninjaEquipment, ninjaId);

      return RedirectToAction("Edit", "Ninjas", new {id = ninjaId});
    }

    // GET: NinjaEquipments/Edit/5
    public ActionResult Edit(int? id, int ninjaId, string name)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ViewBag.NinjaId = ninjaId;
      ViewBag.NinjaName = name;
      var ninjaEquipment = _repo.GetEquipmentById(id.Value);
      if (ninjaEquipment == null)
      {
        return HttpNotFound();
      }
      return View(ninjaEquipment);
    }

    // POST: NinjaEquipments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,Type,DateCreated,DateModified")] NinjaEquipment ninjaEquipment)
    {
      int ninjaId;
      if (!int.TryParse(Request.Form["NinjaId"], out ninjaId))
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      _repo.SaveUpdatedEquipment(ninjaEquipment, ninjaId);
      return RedirectToAction("Edit", "Ninjas", new {id = ninjaId});
    }

 
  }
}