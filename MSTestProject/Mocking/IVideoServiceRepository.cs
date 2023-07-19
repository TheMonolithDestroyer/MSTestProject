namespace MSTestProject.Mocking
{
    public interface IVideoServiceRepository
    {
        IEnumerable<int> ListUnprocessedVideoIds();
    }
}