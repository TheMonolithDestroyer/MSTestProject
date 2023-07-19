namespace MSTestProject.Mocking
{
    public class VideoServiceRepository : IVideoServiceRepository
    {
        public IEnumerable<int> ListUnprocessedVideoIds()
        {
            using var context = new VideoContext();
            var videos = context.Videos
                .Where(i => !i.IsProcessed)
                .Select(i => i.Id)
                .ToList();

            return videos;
        }
    }
}
