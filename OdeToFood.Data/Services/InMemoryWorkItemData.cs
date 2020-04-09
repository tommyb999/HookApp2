using Hook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hook.Data.Services
{
    public class InMemoryWorkItemData : IData
    {
        List<WorkItem> WorkItems;

        public InMemoryWorkItemData()
        {
            WorkItems = new List<WorkItem>()
            {
                new WorkItem { Id = 1, Title = "Hook", Product = "Some Product", Developer = "Tom"},
                new WorkItem { Id = 2, Title = "Something", Product = "Some Product", Developer = "Manish"},
                new WorkItem { Id = 3, Title = "Something else", Product = "Some Product", Developer = "Josh"},
            };
        }

                
        public IEnumerable<WorkItem> GetAll()
        {
            return WorkItems.OrderBy(r => r.Id);
        }
    }
}
