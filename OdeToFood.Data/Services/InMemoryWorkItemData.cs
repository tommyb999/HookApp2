using Hook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Hook.Data.Services
{
    public class InMemoryWorkItemData : IData
    {
        List<WorkItem> WorkItems;

        public InMemoryWorkItemData()
        {
            WorkItems = new List<WorkItem>();
            //{
                //new WorkItem { Id = 4, Title = "Hook", Product = "Some Product", Developer = "Tom"},
                //new WorkItem { Id = 5, Title = "Something", Product = "Some Product", Developer = "Manish"},
                //new WorkItem { Id = 6, Title = "Something else", Product = "Some Product", Developer = "Josh"},

                //Create a file to write to
            //};

            string path = @"/Users/MartynBale/dev/HookApp2/json_output.json";
            string line;
            using (StreamReader file = new StreamReader(path))
            {
                // Adding the json output
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    //Console.WriteLine(JsonConvert.DeserializeObject(line));
                    WorkItems.Add(JsonConvert.DeserializeObject<WorkItem>(line));
                }

            }

        }

        public WorkItem Get(int id)
        {
            return WorkItems.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<WorkItem> GetAll()
        {
            return WorkItems.OrderBy(r => r.Id);
        }

        public void Add(WorkItem workitem)
        {
            WorkItems.Add(workitem);
            workitem.Id = WorkItems.Max(r => r.Id) + 1; 
        }

        public void Update(WorkItem workitem)
        {
            var existing = Get(workitem.Id);
            if(existing != null)
            {
                existing.Title = workitem.Title;
                existing.Product = workitem.Product;
                existing.Developer = workitem.Developer;
            }
        }


    }
}
