using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlateForm.Repository;
using PlateForm.Core.Models;

namespace PlateForm.ApplicationLogic
{
    public class ProjectsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            return await projectRepository.GetAsync();
        }
    }
}
