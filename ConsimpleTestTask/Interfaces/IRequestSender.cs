using ConsimpleTestTask.Models;
using System.Threading.Tasks;

namespace ConsimpleTestTask.Interfaces
{
    interface IRequestSender
    {
        Task<ResultModel> Get(string uri);
    }
}
