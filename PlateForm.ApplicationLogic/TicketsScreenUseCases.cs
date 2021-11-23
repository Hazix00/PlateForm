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
        private readonly ITicketRepository ticketRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            this.projectRepository = projectRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId);
        }

        public async Task<IEnumerable<Ticket>> SearchTicketsAsync(string filter)
        {
            return await ticketRepository.GetAsync(filter);
        }
    }
}
