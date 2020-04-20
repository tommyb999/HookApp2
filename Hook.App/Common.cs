using System;
using Autofac;
using Hook.Data.Models;
using Hook.Data.Services;

namespace Hook.App
{
    public static class common
    {
        private static IContainer Container { get; set; }

        public static IContainer ContainerCreation()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InMemoryWorkItemData>().As<IData>();
            Container = builder.Build();
            return Container;
        }

        public static bool Response()
        {
            var ans = Console.ReadKey().ToString();

            while (ans != "y" || ans != "n")
            {
                Console.WriteLine("Please type either y or n to respond!");
                Console.WriteLine("Do you want to update this work item? y/n");
            }

            return ans=="y";
        }

        public static void Get(int id)
        {
            IContainer Container = ContainerCreation();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);
                Console.WriteLine($"Id={entry.Id}, Title={entry.Title}, Product={entry.Product}, Developer={entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");


            }
        }

        public static void GetAll()
        {
            IContainer Container = ContainerCreation();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entries = writer.GetAll();
                foreach (var item in entries)
                {
                    Console.WriteLine($"Id={item.Id}, Title={item.Title}, Product={item.Product}, Developer={item.Developer}");
                    Console.WriteLine("xxxxxxxxxxxxxxx");

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

            //Confirm happy with the new entry
            if (!common.Response())
            {
                goto startEntry;
            }

            return item;
        }

        public static WorkItem getDevChange(WorkItem item)
        {
            DevStart:
            Console.WriteLine("Select the developer for the task");
            Console.WriteLine("Type 0 for None");
            Console.WriteLine("Type 1 for Tom");
            Console.WriteLine("Type 2 for Manish");
            Console.WriteLine("Type 3 for Josh");

            try
            {
                int newDev = Convert.ToInt32(Console.ReadLine());
                
                if (newDev != 0 && newDev != 1 && newDev != 2 && newDev != 3)
                {
                    Console.WriteLine("Invalid entry");
                    goto DevStart;
                }
                
                item.Developer = (DeveloperType)newDev;

                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return item;
            }
            
        }

    }
}
