using System;
using Autofac;
using Hook.Data.Models;
using Hook.Data.Services;

namespace Hook.App
{
    public class Common
    {

        public IData db;

        public Common(IData db)
        {
            this.db = db;

        }

        public bool Response()
        {
            var ans = Console.ReadLine().ToString();

            while (ans != "y" && ans != "n")
            {
                Console.WriteLine(ans);
                Console.WriteLine("Please type either y or n to respond!");
                Console.WriteLine("Do you want to update this work item? y/n");
                ans = Console.ReadLine();

            }

            return ans=="y";
        }

        public void Get(int id)
        {
             var entry = db.Get(id);
            Console.WriteLine($"Id={entry.Id}, Title={entry.Title}, Product={entry.Product}, Developer={entry.Developer}");
            Console.WriteLine("xxxxxxxxxxxxxxx");
        }

        public void GetAll()
        {
            var entries = db.GetAll();
            foreach (var item in entries)
            {
                Console.WriteLine($"Id={item.Id}, Title={item.Title}, Product={item.Product}, Developer={item.Developer}");
                Console.WriteLine("xxxxxxxxxxxxxxx");
            }
            
        }

        public WorkItem getChange(WorkItem item, string type)
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
            if (!Response())
            {
                goto startEntry;
            }

            return item;
        }

        public WorkItem getDevChange(WorkItem item)
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
                Console.WriteLine($"Developer set to {item.Developer}, is this correct? y/n");
                if (Response())
                {
                    return item;
                }
                else
                {
                    goto DevStart;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return item;
            }
        }

        public int DevMatch(string name)
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
            else if (name == "TOM")
            {
                return 2;
            }
            else if (name == "TOM")
            {
                return 3;
            }
            else
            {
                throw new System.ArgumentException("Developer name doesn't match Developer list; Please enter either None, Tom, Manish or Josh");
            }
        }

        public void DevChange(int Dev, WorkItem workitem)
        {
            try
            {

                if (Dev != 0 && Dev != 1 && Dev != 2 && Dev != 3)
                {
                    Console.WriteLine("Invalid entry, Developer set to [None]");
                    workitem.Developer = (DeveloperType)0;
                }
                else
                {
                    workitem.Developer = (DeveloperType)Dev;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
            }

        }

    }
}
