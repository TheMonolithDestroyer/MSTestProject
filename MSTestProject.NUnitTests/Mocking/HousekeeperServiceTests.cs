using Moq;
using MSTestProject.Mocking;

namespace MSTestProject.NUnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IFileManager> _fileManagerMock;
        private Mock<IEmailSender> _emailSenderMock;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private IHousekeeperService _housekeeperService;
        private Housekeeper _housekeeper;
        private DateTime _statementDate = new DateTime(2023, 7, 23);
        private string _generatedFilename;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Oid = 1,
                Email = "a",
                FullName = "b",
                StatementEmailBody = "c"
            };
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>() { _housekeeper }.AsQueryable());

            _generatedFilename = "filename";
            _fileManagerMock = new Mock<IFileManager>();
            _fileManagerMock
                .Setup(fm => fm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)))
                .Returns(() => _generatedFilename);

            _emailSenderMock = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _housekeeperService = new HousekeeperService(_unitOfWorkMock.Object, _fileManagerMock.Object, _emailSenderMock.Object, _xtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GeneratesStatements()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            _fileManagerMock.Verify(fm => fm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HousekeepersEmailIsNullOrEmptyOrWhiteSpace_ShouldNotGeneratesStatements(string emailValue)
        {
            _housekeeper.Email = emailValue;

            _housekeeperService.SendStatementEmails(_statementDate);

            _fileManagerMock.Verify(fm => fm.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, (_statementDate)), Times.Never);
        }

        [Test]
        public void SendStatementEmails_GeneratesStatementFile_EmailTheFile()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_GeneratedStatementFileNameIsEmptyNullWhiteSpace_ShouldNotEmailFile(string statementFilename)
        {
            _housekeeper.FullName = null;
            _generatedFilename = statementFilename;

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailFileFailed_ShowsMessagebox()
        {
            _emailSenderMock
                .Setup(esm => esm.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _generatedFilename, It.IsAny<string>()))
                .Throws<Exception>();

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplayed();
        }

        private void VerifyEmailSent()
        {
            _emailSenderMock.Verify(es => es.EmailFile(
                _housekeeper.Email, 
                _housekeeper.StatementEmailBody, 
                _generatedFilename, 
                It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _emailSenderMock.Verify(es => es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _generatedFilename,
                    It.IsAny<string>()),
                Times.Never);
        }

        private void VerifyMessageBoxDisplayed()
        {
            _xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
