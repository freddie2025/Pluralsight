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

		[HttpGet]
		[Authorize(Roles = "Administrators")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		[ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Description,EmailAddress,PhoneNumber")]Speaker speaker)
        {
			if (ModelState.IsValid)
			{
				_speakerRepository.Create(speaker);
				return RedirectToAction(nameof(Index));
			}
			else
				return View(speaker);
        }

        [HttpGet]
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
        [Authorize(Roles = "Administrators")]
        public IActionResult Edit(int id, Speaker speaker)
        {
            if (id != speaker?.Id)
            {
                return NotFound();
            }

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

        [HttpGet]
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
