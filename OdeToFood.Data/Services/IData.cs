using Hook.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Data.Services
{
    public interface IData
    {
        IEnumerable<WorkItem> GetAll();
        WorkItem Get(int id);
        void Add(WorkItem workitem); 
    }
}
