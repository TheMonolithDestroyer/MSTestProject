using System.Net;

namespace MSTestProject.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }

    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string path)
        {
            using var client = new HttpClient();
            client.DownloadFile(url, path);
        }
    }

    public static class HttpClientExtension
    {
        public static void DownloadFile(this HttpClient client, string url, string filePath)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = client.Send(request);
            response.EnsureSuccessStatusCode();

            using var stream = response.Content.ReadAsStream();
            using var fileStream = new FileStream(filePath, FileMode.Create);
            stream.CopyTo(fileStream);
        }
    }
}
