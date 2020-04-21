using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;


namespace Hook.App
{
    public class Deletes : Common
    {
        public Deletes(IData db) : base(db)
        {

        }

        public void Delete(int id)
        {
            

            var entry = db.Get(id);

            Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
            Console.WriteLine("xxxxxxxxxxxxxxx");

            Console.WriteLine("Do you want to delete this work item? y/n");

            if (Response())
            {
                db.Delete(entry.Id);
            }

            Console.WriteLine("Entry successfully deleted");
            Console.WriteLine("Current list of work items:");
            GetAll();

            
        }
    }
}
