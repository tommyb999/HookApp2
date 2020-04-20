using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;

namespace Hook.App
{
    public static class updates
    {
        public static void Update(int id)
        {
            IContainer Container = common.ContainerCreation();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);

                var changedEntry = new WorkItem();

                changedEntry.Id = entry.Id;

                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");

                Console.WriteLine("Do you want to update this work item? y/n");

                if (common.Response())
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    if (common.Response())
                    {
                        changedEntry = common.getChange(changedEntry, "Title");
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    if (common.Response())
                    {
                        changedEntry = common.getChange(changedEntry, "Product");
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    if (common.Response())
                    {
                        changedEntry = common.getDevChange(changedEntry);
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
