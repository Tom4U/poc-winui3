using Microsoft.Graph.Models;
using System.Threading.Tasks;

namespace Ohms.Kopf.Desktop.Core.Contracts
{
    internal interface IGraph
    {
        Task<User> ActiveUser { get; }
    }
}
