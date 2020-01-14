using ConferenceTracker.Entities;
using ConferenceTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ConferenceTracker.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly IPresentationRepository _presentationRepository;
        private readonly ISpeakerRepository _speakerRepository;

        public PresentationsController(IPresentationRepository presentationRepository, ISpeakerRepository speakerRepository)
        {
            _presentationRepository = presentationRepository;
            _speakerRepository = speakerRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var presentations = _presentationRepository.GetAllPresentations();
            return View(presentations);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = _presentationRepository.GetPresentation((int)id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public IActionResult Create()
        {
            ViewData["SpeakerId"] = new SelectList(_speakerRepository.GetAllSpeakers(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult Create([Bind("Id,Name,StartDateTime,EndDateTime,Description,SpeakerId")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                _presentationRepository.Create(presentation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeakerId"] = new SelectList(_speakerRepository.GetAllSpeakers(), "Id", "Id", presentation?.SpeakerId);
            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = _presentationRepository.GetPresentation((int)id);
            if (presentation == null)
            {
                return NotFound();
            }
            ViewData["SpeakerId"] = new SelectList(_speakerRepository.GetAllSpeakers(), "Id", "Id", presentation?.SpeakerId);
            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int id, [Bind("Id,Name,StartDateTime,EndDateTime,Description,SpeakerId")] Presentation presentation)
        {
            if (id != presentation?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _presentationRepository.Update(presentation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentationExists(presentation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeakerId"] = new SelectList(_speakerRepository.GetAllSpeakers(), "Id", "Id", presentation?.SpeakerId);
            return View(presentation);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = _presentationRepository.GetPresentation((int)id);

            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult DeleteConfirmed(int id)
        {
            var presentation = _presentationRepository.GetPresentation(id);
            _presentationRepository.Delete(presentation);
            return RedirectToAction(nameof(Index));
        }

        private bool PresentationExists(int id)
        {
            return (_presentationRepository.GetPresentation(id) != null);
        }
    }
}
