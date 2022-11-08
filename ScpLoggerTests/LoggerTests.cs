using NUnit.Framework;
using Moq;
using ScpLogger;

namespace ScpLoggerTests
{
    [TestFixture]
    public class LoggerTests
    {
        private Mock<Logger> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<Logger>(MockBehavior.Strict);
            _loggerMock.Verify(x=>x.UploadLog(It.IsAny<Logger>()));
        }

        [Test]
        public void MainTest()
        {
            int result = 1+1;
            Assert.AreEqual(2,result);
        }
        [Test]
        public void UploadLog_SuccessTest()
        {
            Assert.DoesNotThrow(()=>_loggerMock.Object.UploadLog(new Logger()));
        }
    }
}
