using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ControllerTest
{
    public class SupportControllerTest
    {
        SupportController _supportController;

        private readonly Mock<ISupportService> _supportService = new Mock<ISupportService>();

        private readonly Mock<IConfiguration> _configService = new Mock<IConfiguration>();

        public SupportControllerTest()
        {
            _supportController = new SupportController(_supportService.Object);
        }

        [Fact(DisplayName = "Send Mail Successfully")]
        public async Task SendMailIsSuccess()
        {
            MailBody mailBody = new MailBody()
            {
                CustomerProject = "Random Project - 1",
                TowerScenario  = "1",
                Workflow = "Preliminary Logistic Assessment",
                Requestedby = "Test User 1",
                ToRole = "Logistic Pre Sales Road Engineer",
                To = "Bharath.M@siemensgamesa.com"
            };

            var SendMailResults = "";

            _supportService.Setup(x => x.SendMail(It.IsAny<MailBody>())).ReturnsAsync((true, SendMailResults));

            var SectionMock = new Mock<IConfigurationSection>();
            SectionMock.Setup(s => s.Value).Returns("https://20.73.195.228/");
            _configService.Setup(x => x.GetSection(It.IsAny<string>())).Returns(SectionMock.Object);

            var result = await _supportController.SendMail(mailBody, _configService.Object);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
        }

        [Fact(DisplayName = "Send Mail Failed")]
        public async Task SendMailIsFailure()
        {
            MailBody mailBody = new MailBody()
            {
                CustomerProject = "Random Project - 1",
                TowerScenario = "1",
                Workflow = "Preliminary Logistic Assessment",
                Requestedby = "Test User 1",
                ToRole = "Logistic Pre Sales Road Engineer",
                To = "Bharath.M@siemensgamesa.com"
            };

            var SendMailResults = "";

            _supportService.Setup(x => x.SendMail(It.IsAny<MailBody>())).ReturnsAsync((false, SendMailResults));

            var SectionMock = new Mock<IConfigurationSection>();
            SectionMock.Setup(s => s.Value).Returns("https://20.73.195.228/");
            _configService.Setup(x => x.GetSection(It.IsAny<string>())).Returns(SectionMock.Object);

            var result = await _supportController.SendMail(mailBody, _configService.Object);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }
    }

}