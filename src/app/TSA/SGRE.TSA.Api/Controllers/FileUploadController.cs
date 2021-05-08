using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class FileUploadController : ControllerBase
    {
        private readonly string[] permittedExtensions = { ".txt", ".pdf", ".csv", ".xls", ".xlsx", ".ppt", ".doc", ".docx", ".pptx", ".eof" };
        private readonly long _fileSizeLimit = 20971520;


        [HttpPut, Route("Upload")]
        public async Task<IActionResult> UploadFiles(IFormFile file, [FromForm] string blobText, [FromServices] IFileUploadService fileUpload)
        {
            if (string.IsNullOrWhiteSpace(blobText))
            {
                ModelState.AddModelError("BlobJsonFileMissing", "Blob Json file is missing");
                return BadRequest(ModelState);
            }

            BlobUpload jsonObject = JsonConvert.DeserializeObject<BlobUpload>(blobText);

            if (string.IsNullOrWhiteSpace(jsonObject.ID) || string.IsNullOrWhiteSpace(jsonObject.FieldName) || string.IsNullOrWhiteSpace(jsonObject.FileClass) || string.IsNullOrWhiteSpace(jsonObject.FileDescriptor))
            {
                ModelState.AddModelError("ModelValidationError", "ID, FileName, FileClass, FileDescriptor cannot be null or zero or empty");
                return BadRequest(ModelState);
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                ModelState.AddModelError("InvalidFileFormat", $"Uploaded File format should be of type .txt, .pdf, .csv, .xls, .xlsx, .ppt, .doc, .docx, .pptx, .eof");
                return BadRequest(ModelState);
            }
            if (file.Length > _fileSizeLimit)
            {
                ModelState.AddModelError("FileSizeExccedsError", "Uploaded file size cannot exceed 20Mb");
                return BadRequest(ModelState);
            }

            var result = await fileUpload.Upload(file, jsonObject);
            if (result.IsSuccess)
            {
                return Ok(new Uri(result.SendUploadResults));
            }

            return BadRequest(result.SendUploadResults);
        }

        [HttpPut, Route("AepInput")]
        public async Task<IActionResult> ReadAepInput(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || ext != ".csv")
            {
                ModelState.AddModelError("InvalidFileFormat", $"Uploaded File format should be of type .csv");
                return BadRequest(ModelState);
            }
            if (file.Length > _fileSizeLimit)
            {
                ModelState.AddModelError("FileSizeExccedsError", "Uploaded file size cannot exceed 20Mb");
                return BadRequest(ModelState);
            }

            var stringBuilder = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    stringBuilder.Append(string.Concat(';', await reader.ReadLineAsync()));
            }

            string[] resultCSV = stringBuilder.ToString().Split(';');

            var res = resultCSV.Skip(2)
                              .Select(s => s.Split(','))
                              .Select(a => new
                              {
                                  Id = Convert.ToInt32(a[0]),
                                  EstimationType = a[1],
                                  HubHeight = Convert.ToDecimal(a[2]),
                                  AEPIncrement = Convert.ToDecimal(a[3]),
                                  AEPestimateGWh = Convert.ToDecimal(a[4])
                              });



            if (!res.Any())
            {
                ModelState.AddModelError("EmptyFile", "There is no data in the file!");
                return BadRequest(ModelState);
            }
            return Ok(res);
        }

    }
}
