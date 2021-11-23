using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateForm.Core.Models;
using PlateForm.DataStore.EF;

namespace PlateForm.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private readonly BugsContext db;

        public ProjectsController(BugsContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns>List of Projects</returns>
        [HttpGet]
        public async Task<IList<Project>> GetAsync()
        {
           return await db.Projects.ToListAsync();
        }
        /// <summary>
        /// Return Project by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var project = await db.Projects.SingleOrDefaultAsync(p => p.ProjectId == id);
            if (project == null) return NotFound();
            return Ok(project);
        }
        /// <summary>
        /// get all tickets that belong to a project
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public async Task<IActionResult> GetProjectTicketsAsync(int pid)
        {
            var tickets = await db.Tickets.Where(t => t.ProjectId == pid).ToListAsync();
            if (tickets == null || tickets.Count <= 0)
            {
                return NotFound();
            }
            return Ok(tickets);
        }
        /// <summary>
        /// Add new Project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Project project)
        {
            db.Projects.Add(project);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAsync).Replace("Async",""), new { id = project.ProjectId }, project);
        }
        /// <summary>
        /// Update Project
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync( int id, [FromBody] Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            db.Entry(project).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var existingProject = await db.Projects.FindAsync(id);
                if (existingProject == null || ex.GetType() == typeof(DbUpdateConcurrencyException))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }
        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project); 
        }
    }
}
