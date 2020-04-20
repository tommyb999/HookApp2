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
            Container = builder.Build();

            var finished = false;

            while (finished == false)
            {
                common.GetAll();

                // Ask about updates
                Console.WriteLine("Do you want to update any work items? y/n");
                if (common.Response())
                {
                    Console.WriteLine("Enter the id of the work item you would like to update.");
                    var workitemID = Convert.ToInt32(Console.ReadLine());

                    updates.Update(workitemID);
                }

                // Ask about deletes
                Console.WriteLine("Do you want to delete any work items? y/n");
                if (common.Response())
                {
                    Console.WriteLine("Enter the id of the work item you would like to delete.");
                    var workitemID = Convert.ToInt32(Console.ReadLine());

                    deletes.Delete(workitemID);
                }

                // Ask about creating a new entry
                Console.WriteLine("Do you want to create a work item? y/n");
                if (common.Response())
                {
                    Console.WriteLine("Enter the id of the work item you would like to delete.");
                    var workitemID = Convert.ToInt32(Console.ReadLine());

                    creates.Create();
                }

                common.GetAll();

                // Ask whether user wants to make any more changes
                Console.WriteLine("Do you want to make further changes? y/n");
               var moreChanges = !common.Response();

                finished = moreChanges;
            }



        }


        /*
        //COMMON
        public static bool Response()
        {
            var ans = Console.ReadLine().ToString();
            Console.WriteLine(ans);

            while (ans != "y" && ans != "n")
            {
                Console.WriteLine("Please type either y or n to respond!");
                Console.WriteLine("Do you want to update this work item? y/n");
                ans = Console.ReadLine().ToString();
            }

            return (ans == "y") ;
        }

        public static void Get(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);
                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");


            }
        }

        public static void GetAll()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entries = writer.GetAll();
                foreach (var item in entries)
                {
                    Console.WriteLine($"{item.Id}:{item.Title}");

                }
            }
        }

        public static WorkItem getChange(WorkItem item, string type)
        {
        startEntry:
            Console.WriteLine($"Enter the new {type} required");
            if (type == "Title")
            {
                item.Title = Console.ReadLine();

                Console.WriteLine($"New title will be {item.Title}");
                Console.WriteLine("Are you happy with the new title? y/n");
            }
            else if (type == "Product")
            {
                item.Product = Console.ReadLine();

                Console.WriteLine($"New title will be {item.Product}");
                Console.WriteLine("Are you happy with the new title? y/n");
            }
            else if (type == "Developer")
            {
                item.Developer = Console.ReadLine();

                Console.WriteLine($"New title will be {item.Developer}");
                Console.WriteLine("Are you happy with the new title? y/n");
            }

            var confirmationResponse = Response();

            if (!confirmationResponse)
            {
                goto startEntry;
            }

            return item;
        }





        //CREATES
        public static void Create()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();

                var changedEntry = new WorkItem();

                Console.WriteLine("Do you want to create a work item? y/n");

                var workitemResponse = Response();

                if (workitemResponse)
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
                    var productResponse = Response();
                    if (productResponse)
                    {
                        changedEntry = getChange(changedEntry, "Product");
                    }
                    else
                    {
                        changedEntry.Product = "Empty";
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    var devResponse = Response();
                    if (devResponse)
                    {
                        changedEntry = getChange(changedEntry, "Developer");
                    }
                    else
                    {
                        changedEntry.Developer = "Empty";
                    }

                }


                writer.Add(changedEntry);


                Console.WriteLine("Entry successfully created");
                Console.WriteLine("New entry details:");
                common.Get(changedEntry.Id);

            }
        }



        //DELETES
        public static void Delete(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);

                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");

                Console.WriteLine("Do you want to delete this work item? y/n");

                var workitemResponse = Response();

                if (workitemResponse)
                {
                    writer.Delete(entry.Id);
                }


                Console.WriteLine("Entry successfully deleted");
                Console.WriteLine("Current list of work items:");
                common.GetAll();

            }
        }



        //UPDATES
        public static void Update(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);

                var changedEntry = new WorkItem();

                changedEntry.Id = id;

                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");

                Console.WriteLine("Do you want to update this work item? y/n");

                var workitemResponse = Response();

                if (workitemResponse)
                {
                    Console.WriteLine("Do you want to update the work item title? y/n");
                    var titleResponse = Response();
                    if (titleResponse)
                    {
                        changedEntry = getChange(changedEntry, "Title");
                    }

                    Console.WriteLine("Do you want to update the work item product? y/n");
                    var productResponse = Response();
                    if (productResponse)
                    {
                        changedEntry = getChange(changedEntry, "Product");
                    }

                    Console.WriteLine("Do you want to update the work item Developer? y/n");
                    var devResponse = Response();
                    if (devResponse)
                    {
                        changedEntry = getChange(changedEntry, "Developer");
                    }

                }

                writer.Update(changedEntry);


                Console.WriteLine("Entry successfully updated");
                Console.WriteLine("New entry details:");
                Get(changedEntry.Id);

            }
        }
        */



    }

}
    

