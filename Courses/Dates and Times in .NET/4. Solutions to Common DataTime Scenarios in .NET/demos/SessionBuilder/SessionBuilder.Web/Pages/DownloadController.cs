using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SessionBuilder.Core;

namespace SessionBuilder.Web.Pages
{
    public class DownloadController : Controller
    {
        private readonly ISpeakerRepository speakerRepository;

        public DownloadController(ISpeakerRepository speakerRepository)
        {
            this.speakerRepository = speakerRepository;
        }

        public FileContentResult Index(Guid id)
        {
            var speaker = speakerRepository.Get(id);

            var csv = "Title,Speaker,Length,ScheduledAt" + Environment.NewLine;

            var offset = TimeSpan.FromHours(-11);

            DateTimeOffset.UtcNow.ToUnixTimeSeconds()

            foreach (var session in speaker.Sessions)
            {
                csv += $"{session.Title},{speaker.Name},{session.Length},{session.ScheduledAt.ToOffset(offset).ToString("o")}{Environment.NewLine}";
            }

            return File(Encoding.UTF8.GetBytes(csv), "text/csv", $"sessions for {speaker.Name}.csv");
        }
    }
}