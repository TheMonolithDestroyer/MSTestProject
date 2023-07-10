namespace MSTestProject.Mocking
{
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            var str = File.ReadAllText(path);
            return str;
        }
    }
}
