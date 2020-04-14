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

                
        public IEnumerable<WorkItem> GetAll()
        {
            return WorkItems.OrderBy(r => r.Id);
        }
    }
}
