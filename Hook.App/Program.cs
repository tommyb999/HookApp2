using System;
using Hook.Data.Services;
using Hook.Data.Models;
using System.Collections.Generic;
using Autofac;


namespace Hook.App
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InMemoryWorkItemData>().As<IData>();
            builder.RegisterType<Common>();

            Container = builder.Build();

            var scope = Container.BeginLifetimeScope();
            var db = scope.Resolve<IData>();
            var common = scope.Resolve<Common>();

            var finished = false;

            string[] options = { "Title", "Product" };

            while (finished == false)
            {
                common.GetAll();

                // Ask about updates
                Console.WriteLine("Do you want to update any work items? y/n");
                if (common.Response())
                {
                    Console.WriteLine("Enter the id of the work item you would like to update.");
                    var entry = db.Get(Convert.ToInt32(Console.ReadLine()));

                    var changedEntry = new WorkItem();

                    changedEntry.Id = entry.Id;

                    Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                    Console.WriteLine("xxxxxxxxxxxxxxx");

                    Console.WriteLine("Do you want to update this work item? y/n");

                    if (common.Response())
                    {
                        foreach (var option in options)
                        {
                            Console.WriteLine($"Do you want to update the work item {option}? y/n");
                            if (common.Response())
                            {
                                changedEntry = common.getChange(changedEntry, option);
                            }
                        }
                        Console.WriteLine("Do you want to update the work item Developer? y/n");
                        if (common.Response())
                        {
                            changedEntry = common.getDevChange(changedEntry);
                        }

                    }

                    db.Update(changedEntry);


                    Console.WriteLine("Entry successfully updated");
                    Console.WriteLine("New entry details:");
                    db.Get(changedEntry.Id);
                }

                // Ask about deletes
                Console.WriteLine("Do you want to delete any work items? y/n");
                if (common.Response())
                {
                    Console.WriteLine("Enter the id of the work item you would like to delete.");
                    var entry = db.Get(Convert.ToInt32(Console.ReadLine()));

                    Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                    Console.WriteLine("xxxxxxxxxxxxxxx");

                    Console.WriteLine("Do you want to delete this work item? y/n");

                    if (common.Response())
                    {
                        db.Delete(entry.Id);
                    }

                    Console.WriteLine("Entry successfully deleted");
                    Console.WriteLine("Current list of work items:");
                    common.GetAll();
                }

                // Ask about creating a new entry
                Console.WriteLine("Do you want to create a work item? y/n");
                if (common.Response())
                {
                    var changedEntry = new WorkItem();

                    Console.WriteLine("Do you want to create a work item? y/n");

                    if (common.Response())
                    {
                        foreach (var option in options)
                        {
                            Console.WriteLine($"Do you want to add a work item {option}? y/n");
                            if (common.Response())
                            {
                                changedEntry = common.getChange(changedEntry, option);
                            }
                        }

                        Console.WriteLine("Do you want to set the work item Developer? y/n");
                        if (common.Response())
                        {
                            changedEntry = common.getDevChange(changedEntry);
                        }
                        else
                        {
                            changedEntry.Developer = (DeveloperType)0;
                        }
                    }


                    db.Add(changedEntry);


                    Console.WriteLine("Entry successfully created");
                    Console.WriteLine("New entry details:");
                    common.Get(changedEntry.Id);
                }

                common.GetAll();

                // Ask whether user wants to make any more changes
                Console.WriteLine("Do you want to make further changes? y/n");
                var moreChanges = !common.Response();

                finished = moreChanges;
            }
        }
    }

}
    

