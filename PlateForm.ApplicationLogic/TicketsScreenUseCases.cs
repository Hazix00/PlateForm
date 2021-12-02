using PlateForm.Core.Models;
using PlateForm.Repository;
using System.Collections.Generic;
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
            if (int.TryParse(filter, out int ticketId))
            {
                var ticket = await ticketRepository.GetByIdAsync(ticketId);
                return new List<Ticket>() { ticket };
            }
            return await ticketRepository.GetAsync(filter);
        }
        public async Task<IEnumerable<Ticket>> ViewOwnerTicketsAsync(int projectId, string filter)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId, filter);
        }
        public async Task<Ticket> ViewTicketByIdAsync(int ticketId)
        {
            return await ticketRepository.GetByIdAsync(ticketId);
        }
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await ticketRepository.UpdateAsync(ticket);
        }
        public async Task AddTicketAsync(Ticket ticket)
        {
            await ticketRepository.CreateAsync(ticket);
        }

    }
}
