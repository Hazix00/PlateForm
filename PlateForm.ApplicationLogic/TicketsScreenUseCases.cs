using PlateForm.Core.Models;
using PlateForm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateForm.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId);
        }
    }
}
