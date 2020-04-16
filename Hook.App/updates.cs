using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;

namespace Hook.App
{
    public static class updates
    {
        private static IContainer Container { get; set; }

        public static void Update(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);

                var changedEntry = new WorkItem();

                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");

                Console.WriteLine("Do you want to update this work item? y/n");

                var workitemResponse = Common.Response();

                if (workitemResponse == "y")
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    var titleResponse = Common.Response();
                    if (titleResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Title");
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    var productResponse = Common.Response();
                    if (productResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Product");
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    var devResponse = Common.Response();
                    if (devResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Developer");
                    }

                }

                writer.Update(changedEntry);


                Console.WriteLine("Entry successfully updated");
                Console.WriteLine("New entry details:");
                Common.Get(changedEntry.Id);

            }
        }


    }
}
