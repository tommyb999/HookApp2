using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;


namespace Hook.App
{
    public static class creates
    {

        public static void Create()
        {
            IContainer Container = common.ContainerCreation();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();

                var changedEntry = new WorkItem();

                Console.WriteLine("Do you want to create a work item? y/n");

                if (common.Response())
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    var titleResponse = common.Response();
                    if (titleResponse)
                    {
                        changedEntry = common.getChange(changedEntry, "Title");
                    }
                    else
                    {
                        changedEntry.Title = "Empty";
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    if (common.Response())
                    {
                        changedEntry = common.getChange(changedEntry, "Product");
                    }
                    else
                    {
                        changedEntry.Product = "Empty";
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    if (common.Response())
                    {
                        changedEntry = common.getDevChange(changedEntry);
                    }
                    else
                    {
                        changedEntry.Developer = (DeveloperType)0;
                    }
                }


                writer.Add(changedEntry);


                Console.WriteLine("Entry successfully created");
                Console.WriteLine("New entry details:");
                common.Get(changedEntry.Id);

            }
        }
    }
}
