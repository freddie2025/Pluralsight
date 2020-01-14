using ConferenceTracker.Entities;
using ConferenceTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceTracker.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakersController(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
        }

        public IActionResult Index()
        {
            var speakers = _speakerRepository.GetAllSpeakers();
            return View(speakers);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = _speakerRepository.GetSpeaker((int)id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult Create([Bind("Id,FirstName,LastName,Description")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                _speakerRepository.Create(speaker);
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = _speakerRepository.GetSpeaker((int)id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Description")] Speaker speaker)
        {
            if (id != speaker?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _speakerRepository.Update(speaker);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
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
            return View(speaker);
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = _speakerRepository.GetSpeaker((int)id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public IActionResult DeleteConfirmed(int id)
        {
            var speaker = _speakerRepository.GetSpeaker(id);
            _speakerRepository.Delete(speaker);
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return (_speakerRepository.GetSpeaker(id) != null);
        }
    }
}
