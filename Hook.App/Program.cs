using System;
using Hook.Data.Services;
using Hook.Data.Models;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.CommandLineUtils;



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


            //If no arguments are provided then go through the user input promting route
            if (args.Length == 0)
            {
                No_input();
            }
            else
            {
                //Get arg[0] and use it to select the work item operation
                var mode = args[0].ToUpper();

                // Go through the subsequent args and use them to select the parameters for the work item operation
                WorkItem entry = Process_args(args, mode);

                if (mode == "UPDATE")      {db.Update(entry);}
                else if (mode == "CREATE") {db.Add(entry);}
                else if (mode == "DELETE") {db.Delete(entry.Id);}
                else if (mode == "VIEW")   {common.GetAll();}
                else {throw new System.ArgumentException("First argument must be either update, create, delete or view");}
            }
            Console.ReadLine();

        }

        private static WorkItem Process_args(string[] args, string mode)
        {
            WorkItem entry = new WorkItem();
            bool explicit_arg_used = false; 
            int pos = 0;
            foreach (var i in args)
            {
                if (pos > 0)
                {
                    if (i[0] == '-')
                    {
                        explicit_arg_used = true;
                        string[] explicit_arg = explicit_arg_process(i);
                        pos = Convert.ToInt32(explicit_arg[0]);
                        entry = Update_entry(entry, pos, explicit_arg[1]);
                    }
                    else
                    {
                        if (explicit_arg_used == true)
                        {
                            throw new System.ArgumentException("Cannot use a positional argument after an explicit argument has been used");
                        }
                        if (pos == 1) {entry.Id = Convert.ToInt32(i);}
                        if (pos == 2) {entry.Title = i;}
                        if (pos == 3) {entry.Product = i;}
                        if (pos == 4)
                        {
                            int devNum = DevMatch(i);
                            entry = DevChange(devNum, entry);
                        }
                        if (pos > 4) {throw new System.ArgumentException("Too many arguments entered, see README for guidance");}
                    }
                }
                pos += 1;
            }

            return entry;
        }

        private static WorkItem Update_entry(WorkItem entry, int pos, string v)
        {
            if (pos == 1)
            {
                entry.Id = Convert.ToInt32(v);
                return entry;
            }
            else if (pos == 2)
            {
                entry.Title = v;
                return entry;
            }
            else if (pos == 3)
            {
                entry.Product = v;
                return entry;
            }
            else if (pos == 4)
            {
                // Add method to take developer string input and return a developer
                int devNum = DevMatch(v);
                entry = DevChange(devNum, entry);
                return entry;
            }
            else
            {
                throw new System.ArgumentException("Argument position exceeds the maximum number of arguments for the app");
            }
            
        }

        private static string[] explicit_arg_process(string i)
        {
            var contains_colon = i.Contains(":");
            var contains_equals = i.Contains("=");

            if (contains_colon == true && contains_equals == true)
            {
                throw new System.ArgumentException("Parameter cannot contain ; and =");
            }
            else if (contains_colon == false && contains_equals == false)
            {
                throw new System.ArgumentException("Explicit parameter must contain either ; or =");
            }
            else if (contains_colon == true)
            {
                string[] separator = { ":" };
                return split_argument(separator, i);
            }
            else
            {
                string[] separator = { "=" };
                return split_argument(separator, i);
            }

        }

        private static string[] split_argument(string[] sep, string i)
        {
            String[] strlist = i.Split(sep, 2, StringSplitOptions.RemoveEmptyEntries);
            var workItem_param = strlist[0].ToUpper().Trim();
            var param = strlist[1].ToUpper().Trim();
            if (workItem_param == "-PRODUCT")
            {
                string[] result = { "3", param };
                return result;
            }
            else if (workItem_param == "-ID")
            {
                string[] result = { "1", param };
                return result;
            }
            else if (workItem_param == "-TITLE")
            {
                string[] result = { "2", param };
                return result;
            }
            else if (workItem_param == "-DEVELOPER")
            {
                string[] result = { "4", param };
                return result;
            }
            else
            {
                throw new System.ArgumentException("Explicit parameter must begin with Product, Id, Title or Developer");
            }
        }


        public static int DevMatch(string name)
        {
            name.ToUpper();
            if (name == "NONE")
            {
                return 0;
            }
            else if (name == "TOM")
            {
                return 1;
            }
            else if (name == "MANISH")
            {
                return 2;
            }
            else if (name == "JOSH")
            {
                return 3;
            }
            else
            {
                throw new System.ArgumentException("Developer name doesn't match Developer list; Please enter either None, Tom, Manish or Josh");
            }
        }

        public static WorkItem DevChange(int Dev, WorkItem workitem)
        {
            try
            {

                if (Dev != 0 && Dev != 1 && Dev != 2 && Dev != 3)
                {
                    Console.WriteLine("Invalid entry, Developer set to [None]");
                    workitem.Developer = (DeveloperType)0;
                    return workitem;
                }
                else
                {
                    workitem.Developer = (DeveloperType)Dev;
                    return workitem;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                throw new System.ArgumentException("Developer name doesn't match Developer list; Please enter either None, Tom, Manish or Josh");
            }

        }

        public static void No_input()
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
    

