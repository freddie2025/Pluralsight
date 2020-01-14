using ConferenceTracker.Entities;
using ConferenceTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConferenceTracker.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly IPresentationRepository _presentationRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly ILogger _logger;

        public PresentationsController(IPresentationRepository presentationRepository, ISpeakerRepository speakerRepository, ILogger<PresentationsController> logger)
        {
            _presentationRepository = presentationRepository;
            _speakerRepository = speakerRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var presentations = _presentationRepository.GetAllPresentations();
            return View(presentations);
        }

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

        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int? id)
        {
            _logger.LogInformation("Getting presentation id:" + id + " for edit.");
            if (id == null)
            {
                _logger.LogError("Presentation id was null.");
                return NotFound();
            }

            var presentation = _presentationRepository.GetPresentation((int)id);
            if (presentation == null)
            {
                _logger.LogWarning("Presentation id," + id + ", was not found.");
                return NotFound();
            }
            _logger.LogInformation("Presentation id," + id + ", was found. Returning 'Edit view'");
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
