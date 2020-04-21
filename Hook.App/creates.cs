using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;


namespace Hook.App
{
    public class Creates : Common
    {
        public Creates(IData db) : base(db)
        {
        }
        public void Create()
        {
            var changedEntry = new WorkItem();

            Console.WriteLine("Do you want to create a work item? y/n");

            if (Response())
            {
                Console.WriteLine("Do you want to update the work item title? y/n");
                var titleResponse = Response();
                if (titleResponse)
                {
                    changedEntry = getChange(changedEntry, "Title");
                }
                else
                {
                    changedEntry.Title = "Empty";
                }

                Console.WriteLine("Do you want to update the work item product? y/n");
                if (Response())
                {
                    changedEntry = getChange(changedEntry, "Product");
                }
                else
                {
                    changedEntry.Product = "Empty";
                }

                Console.WriteLine("Do you want to update the work item Developer? y/n");
                if (Response())
                {
                    changedEntry = getDevChange(changedEntry);
                }
                else
                {
                    changedEntry.Developer = (DeveloperType)0;
                }
            }


            db.Add(changedEntry);


            Console.WriteLine("Entry successfully created");
            Console.WriteLine("New entry details:");
            Get(changedEntry.Id);

            
        }
    }
}
