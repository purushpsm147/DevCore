using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SGRE.TSA.Api.Controllers;
using System.IO;
using System.Text;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ControllerTest
{
    public class FileUploadControllerTest
    {
        readonly FileUploadController _fileUploadController;


        public FileUploadControllerTest()
        {
            _fileUploadController = new FileUploadController();
        }

        [Fact(DisplayName = "Read AEP LoopUp File Successfully")]
        public async Task ReadAepInputSuccess()
        {
            var file = new Mock<IFormFile>();
           string fakeFileContents = " seqno	Estimation Type	Hub height (m)	AEP increment (%)	AEP estimate (GWh) \n 1	Interpolation	90	89	150";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            var ms = new MemoryStream(fakeFileBytes);
            var writer = new StreamWriter(ms);
            writer.Write(file);

            var fileName = "QQ.csv";
            ms.Position = 0;
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            var result = await _fileUploadController.ReadAepInput(file.Object);

           
            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact(DisplayName = "Read AEP LoopUp File UnSuccessfully")]
        public async Task ReadAepInputNotSuccess()
        {
            var file = new Mock<IFormFile>();
            string fakeFileContents = string.Empty;
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            var ms = new MemoryStream(fakeFileBytes);
            var writer = new StreamWriter(ms);
            writer.Write(file);

            var fileName = "QQ.csv";
            ms.Position = 0;
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            var result = await _fileUploadController.ReadAepInput(file.Object);


            var okresult = new BadRequestObjectResult(result);
            Assert.Equal(400, okresult.StatusCode);
        }

        [Fact(DisplayName = "Read AEP LoopUp File Non CSV")]
        public async Task ReadAepInputNonCSV()
        {
            var file = new Mock<IFormFile>();
            string fakeFileContents = string.Empty;
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            var ms = new MemoryStream(fakeFileBytes);
            var writer = new StreamWriter(ms);
            writer.Write(file);

            var fileName = "QQ.pdf";
            ms.Position = 0;
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            var result = await _fileUploadController.ReadAepInput(file.Object);


            var okresult = new BadRequestObjectResult(result);
            Assert.Equal(400, okresult.StatusCode);
        }

        [Fact(DisplayName = "Read AEP LoopUp File More Than 20MB")]
        public async Task ReadAepInputExceeds()
        {
            var file = new Mock<IFormFile>();
            string fakeFileContents = string.Empty;
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);

            var ms = new MemoryStream(fakeFileBytes);
            var writer = new StreamWriter(ms);
            writer.Write(file);

            var fileName = "QQ.csv";
            ms.Position = 0;
            file.Setup(f => f.FileName).Returns(fileName).Verifiable();
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            file.Setup(f => f.Length).Returns(209715202);
            var result = await _fileUploadController.ReadAepInput(file.Object);


            var okresult = new BadRequestObjectResult(result);
            Assert.Equal(400, okresult.StatusCode);
        }


    }

}