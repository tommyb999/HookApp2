using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;

namespace Hook.App
{
    public class Updates : Common
    {
        public Updates(IData db) : base(db)
        {

        }

        public void Update(int id)
        {
            
            var entry = db.Get(id);

            var changedEntry = new WorkItem();

            changedEntry.Id = entry.Id;

            Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
            Console.WriteLine("xxxxxxxxxxxxxxx");

            Console.WriteLine("Do you want to update this work item? y/n");

            if (Response())
            {
                Console.WriteLine("Do you want to update the work item title? y/n");
                if (Response())
                {
                    changedEntry = getChange(changedEntry, "Title");
                }

                Console.WriteLine("Do you want to update the work item product? y/n");
                if (Response())
                {
                    changedEntry = getChange(changedEntry, "Product");
                }

                Console.WriteLine("Do you want to update the work item Developer? y/n");
                if (Response())
                {
                    changedEntry = getDevChange(changedEntry);
                }

            }

            db.Update(changedEntry);


            Console.WriteLine("Entry successfully updated");
            Console.WriteLine("New entry details:");
            Get(changedEntry.Id);

            
        }


    }
}
