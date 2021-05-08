using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class CertificationServiceTest
    {
        /// <summary>
        /// Defines the ICertificationExternalService
        /// </summary>
        private Mock<ICertificationExternalService> _certificationExternalService = new Mock<ICertificationExternalService>();

        /// <summary>
        /// The CertificationService
        /// </summary>
        /// <returns></returns>
        private CertificationService CreateCertificationService()
        {
            return new CertificationService(_certificationExternalService.Object);
        }

        [Fact(DisplayName = "Get all Certification")]
        public async Task GetCertificationAsync_IsSuccess()
        {
            var _certificationService = CreateCertificationService();

            IEnumerable<Certification> data = new List<Certification>() { new Certification()
            {
                Id = 1,
                CertificationName = "Certification - 1"
            },
            new Certification()
            {
                Id = 2,
                CertificationName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Certification>> responseData = new ExternalServiceResponse<IEnumerable<Certification>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _certificationExternalService.Setup(x => x.GetCertificationAsync()).ReturnsAsync((responseData));

            var result = await _certificationService.GetCertificationAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Certifications No Records Found")]
        public async Task GetCertificationAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _certificationService = CreateCertificationService();

            IEnumerable<Certification> data = new List<Certification>() { new Certification()
            {
                Id = 1,
                CertificationName = "Certification - 1"
            },
            new Certification()
            {
                Id = 2,
                CertificationName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Certification>> responseData = new ExternalServiceResponse<IEnumerable<Certification>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _certificationExternalService.Setup(x => x.GetCertificationAsync()).ReturnsAsync((responseData));

            var result = await _certificationService.GetCertificationAsync();

            Assert.False(result.IsSuccess);
        }

    }
}