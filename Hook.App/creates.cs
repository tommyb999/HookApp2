using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;


namespace Hook.App
{
    public static class creates
    {
        private static IContainer Container { get; set; }

        public static void Create()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();

                var changedEntry = new WorkItem();

                Console.WriteLine("Do you want to create a work item? y/n");

                var workitemResponse = Common.Response();

                if (workitemResponse == "y")
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    var titleResponse = Common.Response();
                    if (titleResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Title");
                    }
                    else
                    {
                        changedEntry.Title = "Empty";
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    var productResponse = Common.Response();
                    if (productResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Product");
                    }
                    else
                    {
                        changedEntry.Product = "Empty";
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    var devResponse = Common.Response();
                    if (devResponse == "y")
                    {
                        changedEntry = Common.getChange(changedEntry, "Developer");
                    }
                    else
                    {
                        changedEntry.Developer = "Empty";
                    }

                }


                writer.Add(changedEntry);


                Console.WriteLine("Entry successfully created");
                Console.WriteLine("New entry details:");
                Common.Get(changedEntry.Id);

            }
        }
    }
}
