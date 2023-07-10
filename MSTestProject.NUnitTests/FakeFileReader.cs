using MSTestProject.Mocking;

namespace MSTestProject.NUnitTests
{
    public class FakeFileReader : IFileReader
    {
        public string Read(string path)
        {
            return null;
        }
    }
}
