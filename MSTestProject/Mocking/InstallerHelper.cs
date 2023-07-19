using System.IO;
using System;
using System.Net;

namespace MSTestProject.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;
        public InstallerHelper(IFileDownloader fileDownloader = null)
        {
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    $"http://example.com/{customerName}/{installerName}", 
                    _setupDestinationFile);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
