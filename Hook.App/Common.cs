using System;
using Autofac;
using Hook.Data.Models;
using Hook.Data.Services;

namespace Hook.App
{
    public static class Common
    {
        private static IContainer Container { get; set; }

        public static string Response()
        {
            var ans = Console.ReadKey().ToString();

            while (ans != "y" || ans != "n")
            {
                Console.WriteLine("Please type either y or n to respond!");
                Console.WriteLine("Do you want to update this work item? y/n");
            }

            return ans;
        }

        public static void Get(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);
                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");


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
            else if (type == "Developer")
            {
                item.Developer = Console.ReadLine();

                Console.WriteLine($"New title will be {item.Developer}");
                Console.WriteLine("Are you happy with the new title? y/n");
            }

            var confirmationResponse = Common.Response();

            if (confirmationResponse == "n")
            {
                goto startEntry;
            }

            return item;
        }
    }
}
