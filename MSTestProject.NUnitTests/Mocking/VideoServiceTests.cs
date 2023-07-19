using Moq;
using MSTestProject.Mocking;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoServiceRepository> _videoServiceRepository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoServiceRepository = new Mock<IVideoServiceRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoServiceRepository.Object);   
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_VideoServiceTableContainsRows_ReturnsJoinedIds()
        {
            _videoServiceRepository
                .Setup(vsr => vsr.ListUnprocessedVideoIds())
                .Returns(new List<int> { 1, 2, 3 });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_EmptyVideoServiceTable_ReturnsEmptyString()
        {
            _videoServiceRepository
                .Setup(vsr => vsr.ListUnprocessedVideoIds())
                .Returns(new List<int>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.Empty);
        }
    }
}
