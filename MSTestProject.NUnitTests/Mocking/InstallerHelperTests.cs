using Moq;
using MSTestProject.Mocking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp] 
        public void SetUp() 
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper( _fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_SuccessfulDownloading_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("Nurdaulet", "Installer");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_UnsuccessfulDownloading_ReturnsFalse()
        {
            _fileDownloader
                .Setup(fds => fds.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("Nurdaulet", "Installer");

            Assert.That(result, Is.False);
        }
    }
}
