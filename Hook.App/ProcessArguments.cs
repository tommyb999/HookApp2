using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hook.Data.Services;
using Hook.Data.Models;

namespace Hook.App
{
    class ProcessArguments
    {


        public WorkItem Process_args(string[] args, string mode)
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
                        if (pos == 1) { entry.Id = Convert.ToInt32(i); }
                        if (pos == 2) { entry.Title = i; }
                        if (pos == 3) { entry.Product = i; }
                        if (pos == 4)
                        {
                            int devNum = DevMatch(i);
                            entry = DevChange(devNum, entry);
                        }
                        if (pos > 4) { throw new System.ArgumentException("Too many arguments entered, see README for guidance"); }
                    }
                }
                pos += 1;
            }

            return entry;
        }

        public WorkItem Update_entry(WorkItem entry, int pos, string v)
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

        public string[] explicit_arg_process(string i)
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

        public string[] split_argument(string[] sep, string i)
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


        public int DevMatch(string name)
        {
            name.ToUpper();
            if (name == "NONE") { return 0; }
            else if (name == "TOM") { return 1; }
            else if (name == "MANISH") { return 2; }
            else if (name == "JOSH") { return 3; }
            else
            {
                throw new System.ArgumentException("Developer name doesn't match Developer list; Please enter either None, Tom, Manish or Josh");
            }
        }

        public WorkItem DevChange(int Dev, WorkItem workitem)
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

    }
}
