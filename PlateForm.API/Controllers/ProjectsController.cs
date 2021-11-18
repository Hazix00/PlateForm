using Microsoft.AspNetCore.Mvc;

namespace PlateForm.API.Controllers
{
    [ApiController]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns>List of Projects</returns>
        [HttpGet]
        public object Get()
        {
            return new { Response = "Hello from api Projects" };
        }
        /// <summary>
        /// Return Project by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project</returns>
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return new { Response = $"Hello from api Id = {id}" };
        }
        /// <summary>
        /// Add new Project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post()
        {
            var result = new { Response = "Adding new Project" };
            return Ok(result);
        }
        /// <summary>
        /// Update Project
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put()
        {
            var result = new { Response = "Updating Project" };
            return Ok(result);
        }
        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = new { Response = $"Delete Project Id = {id}" };
            return Ok(result);
        }
    }
}
