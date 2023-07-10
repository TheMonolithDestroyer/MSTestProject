using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MSTestProject.Mocking
{
    public class VideoService
    {
        public string ReadVideoTitle()
        {
            var str = new FileReader().Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            using var context = new VideoContext();
            var videos = context.Videos.Where(i => !i.IsProcessed).Select(i => i.Id).ToList();

            videoIds.AddRange(videos);

            return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
