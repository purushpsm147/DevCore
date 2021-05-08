using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class ProjectConstraintsController : ControllerBase
    {
        private readonly IProjectConstraintsService projectConstraintsService;

        public ProjectConstraintsController(IProjectConstraintsService projectConstraintsService)
        {
            this.projectConstraintsService = projectConstraintsService;
        }

        [HttpGet, Route("{projectId:int}"), EnableQuery()]
        public async Task<IEnumerable<ProjectConstraint>> GetProjectConstraints(int projectId)
        {
            var result = await projectConstraintsService.GetProjectConstraintsAsync(projectId);
            if (result.IsSuccess)
            {
                return result.ConstraintResults;
            }

            return new List<ProjectConstraint>();
        }

        //redundand API
        [HttpGet, Route("ConstraintsId={id:int}"), EnableQuery()]
        public async Task<IEnumerable<ProjectConstraint>> GetProjectConstraintsById(int id)
        {
            var result = await projectConstraintsService.GetProjectConstraintsByIdAsync(id);
            if (result.IsSuccess)
            {
                return result.ConstraintResults;
            }

            return new List<ProjectConstraint>();
        }

        //Validations Pending
        [HttpPut]
        public async Task<IActionResult> PutProjectConstraints(ProjectConstraint projectConstraint)
        {
            string error = DoBasicValidations(projectConstraint);
            if (!string.IsNullOrEmpty(error))
            {
                return Conflict(error);
            }
            var result = await projectConstraintsService.PutProjectConstraintsAsync(projectConstraint);
           
            if (result.IsSuccess)
            {
                return Ok(result.ConstraintResults);
            }

            return NotFound(result.ConstraintResults);
        }

        private string DoBasicValidations(ProjectConstraint projectConstraint)
        {
            //Should be handled by frontend
            if (projectConstraint.ProjectId <= 0)
            {
                return "ProjectID cannot be Zero or empty";
            }
            if (projectConstraint?.LogisticConstraint?.LogisticStatusId <= 0)
            {
                return "LogisticStatusId cannot be Zero or empty";
            }
          
            return string.Empty;
        }

        [HttpPatch, Route("{id:int}")]
        public async Task<IActionResult> PatchProjectConstraintsAsync(int id, ProjectConstraint projectConstraint)
        {
            string error = DoBasicValidations(projectConstraint);
            if (!string.IsNullOrEmpty(error))
            {
                return Conflict(error);
            }
            var result = await projectConstraintsService.PatchProjectConstraintsAsync(id, projectConstraint);

            if (result.IsSuccess)
            {
                return Ok(result.ConstraintResults);
            }

            return NotFound(result.ConstraintResults);
        }



    }

}
