using System;
using Hook.Data.Models;
using Hook.Data.Services;
using Autofac;


namespace Hook.App
{
    public static class deletes
    {
        private static IContainer Container { get; set; }

        public static void Delete(int id)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IData>();
                var entry = writer.Get(id);

                Console.WriteLine($"{entry.Id}:{entry.Title}:{entry.Product}:{entry.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");

                Console.WriteLine("Do you want to delete this work item? y/n");

                var workitemResponse = common.Response();

                if (workitemResponse == "y")
                {
                    writer.Delete(entry.Id);
                }


                Console.WriteLine("Entry successfully deleted");
                Console.WriteLine("Current list of work items:");
                common.GetAll();

            }
        }
    }
}
