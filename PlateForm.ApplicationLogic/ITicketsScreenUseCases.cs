using PlateForm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlateForm.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<int> AddTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
        Task<IEnumerable<Ticket>> SearchTicketsAsync(string filter);
        Task UpdateTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> ViewOwnerTicketsAsync(int projectId, string filter);
        Task<Ticket> ViewTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId);
    }
}