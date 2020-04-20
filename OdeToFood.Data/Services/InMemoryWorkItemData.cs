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

            string path = @"H:\HookApp2\json_output.json";

            string text = File.ReadAllText(path);
            WorkItems = JsonConvert.DeserializeObject<List<WorkItem>>(text);

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
            SaveToFile();
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
            SaveToFile();
        }

        public void Delete(int id)
        {
            var workitem = Get(id);
            if (workitem != null)
            {
                WorkItems.Remove(workitem);
            }
            SaveToFile();

        }

        public void SaveToFile()
        {
            //Create a file to write to
            string path = @"H:\HookApp2\json_output.json";

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(WorkItems));
        }

    }
}
