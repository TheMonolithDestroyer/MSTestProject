using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace MSTestProject.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;
        IVideoServiceRepository _repository;

        public VideoService(
            IFileReader fileReader = null, 
            IVideoServiceRepository repository = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _repository = repository ?? new VideoServiceRepository();
        }

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var ids = _repository.ListUnprocessedVideoIds();

            videoIds.AddRange(ids);

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
