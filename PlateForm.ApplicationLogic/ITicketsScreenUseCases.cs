using PlateForm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlateForm.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<IEnumerable<Ticket>> SearchTicketsAsync(string filter);
        Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId);
    }
}