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

                var workitemResponse = common.Response();

                if (workitemResponse)
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    var titleResponse = common.Response();
                    if (titleResponse)
                    {
                        changedEntry = common.getChange(changedEntry, "Title");
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    var productResponse = common.Response();
                    if (productResponse)
                    {
                        changedEntry = common.getChange(changedEntry, "Product");
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    var devResponse = common.Response();
                    if (devResponse)
                    {
                        changedEntry = common.getChange(changedEntry, "Developer");
                    }

                }

                writer.Update(changedEntry);


                Console.WriteLine("Entry successfully updated");
                Console.WriteLine("New entry details:");
                common.Get(changedEntry.Id);

            }
        }


    }
}
