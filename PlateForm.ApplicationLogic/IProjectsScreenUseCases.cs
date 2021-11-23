using PlateForm.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlateForm.ApplicationLogic
{
    public interface IProjectsScreenUseCases
    {
        Task<IEnumerable<Project>> ViewProjectsAsync();
    }
}