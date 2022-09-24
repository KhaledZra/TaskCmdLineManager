using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskCmdLineManager
{
    internal class Program
    {
        private static List<JsonFileHandler> _files = LoadKnownFiles();
        private static List<Task> _tasks = new();
        //private static Task _task = Load();

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(_files.Count);
            if (!(args.Length == 0))
            {
                Console.WriteLine($"Input: {args[0]}");
                Console.WriteLine("Output: ");
                switch (args[0].ToLower())
                {
                    case "help":
                        Help();
                        break;

                    case "init":
                        if (args.Length == 2)
                        {
                            Init(args[1]);
                        }
                        else
                        {
                            Console.WriteLine("Error! Do this: <dotnet run init filename>. (Without <>)");
                        }
                        break;

                    case "show":
                        if (args.Length == 2)
                        {
                            ShowTaskList(args[1]);
                        }
                        else if (args.Length == 1)
                        {
                            ShowFileList();
                        }
                        else
                        {
                            Console.WriteLine("Error!");
                        }

                        break;

                    case "add":
                        if (args.Length >= 3)
                        {
                            AddTask(args);
                        }
                        else
                        {
                            Console.WriteLine("ERROR! Type <dotnet run add listname 'task description text'> (without '')");
                        }
                        break;

                    default:
                        Console.WriteLine("ERROR! Type <dotnet run help> to find out how to get started!");
                        break;

                }
            }
            else
            {
                Console.WriteLine("No command used! Enter this to get started: <dotnet run help>");
            }
        }

        public static List<JsonFileHandler> LoadKnownFiles()
        {
            string sFileName = "FileList.json";
            string sJsonString = File.ReadAllText(sFileName);
            if (string.IsNullOrWhiteSpace(sJsonString))
            {
                return new List<JsonFileHandler>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<JsonFileHandler>>(sJsonString)!;
            }
        }

        public static void SaveFileList()
        {
            string sFileName = "FileList.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string sJsonString = JsonSerializer.Serialize<List<JsonFileHandler>>(_files, options);
            File.WriteAllText(sFileName, sJsonString);
        }

        public static void Help()
        {

        }

        public static void Init(string listName)
        {
            if (ConfirmFilePath(listName + ".json") == false)
            {
                _files.Add(new(listName + ".json"));
                var options = new JsonSerializerOptions { WriteIndented = true };
                string sJsonString = JsonSerializer.Serialize<List<Task>>(new List<Task>(), options);
                File.WriteAllText(_files[^1].FileName, sJsonString);
                SaveFileList();
            }
            else
            {
                Console.WriteLine("Error! List already exists!");
            }

            Console.WriteLine($"Successfully created new List with name: {listName}!");
        }

        public static void ShowTaskList(string sListName)
        {
            if (_files.Count == 0)
            {
                Console.WriteLine("No todo lists to add to! use <dotnet run init filename>.");
            }
            else
            {
                LoadTask(sListName);
                if (!(_tasks.Count == 0))
                {
                    for (int i = 0; i < _tasks.Count; i++)
                    {
                        Console.WriteLine("---------------- ");
                        Console.WriteLine($"Task: {i + 1}");
                        _tasks[i].ShowTask();
                    }
                    Console.WriteLine("---------------- ");
                }
                //else
                //{
                //    Console.WriteLine("No tasks added yet! use <dotnet> <run> <add> <filename> <task description>.");
                //}
            }

        }

        public static void ShowFileList()
        {
            if (_files.Count == 0)
            {
                Console.WriteLine("No lists available! use <dotnet run init filename>.");
            }
            else
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    Console.WriteLine("---------------- ");
                    Console.WriteLine($"List: {i + 1}");
                    _files[i].ShowFile();
                }
                Console.WriteLine("---------------- ");
            }
        }

        public static void SaveTask(string sListName)
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-6-0
            // The following example uses synchronous code to create a JSON file:
            sListName = sListName + ".json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string sJsonString = JsonSerializer.Serialize<List<Task>>(_tasks, options);
            File.WriteAllText(sListName, sJsonString);

            // The following example uses asynchronous code to create a JSON file:
            //string fileName = "WeatherForecast.json";
            //using FileStream createStream = File.Create(fileName);
            //await JsonSerializer.SerializeAsync(createStream, weatherForecast);
            //await createStream.DisposeAsync();

            //Console.WriteLine(File.ReadAllText(sFileName));
        }

        public static void LoadTask(string listName)
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-6-0
            // To deserialize from a file by using synchronous code
            string sFileName = listName + ".json";
            if (ConfirmFilePath(sFileName))
            {
                string sJsonString = File.ReadAllText(sFileName);
                _tasks = JsonSerializer.Deserialize<List<Task>>(sJsonString)!;
            }
            else
            {
                Console.WriteLine($"Filename '{listName}' does not exist.");
            }
            //return JsonSerializer.Deserialize<Task>(sJsonString)!;
        }

        public static bool ConfirmFilePath(string fileName)
        {
            foreach (JsonFileHandler item in _files)
            {
                if (item.FileName == fileName)
                {
                    return true;
                }
            }

            return false;
        }

        public static void AddTask(string[] cmdStrings)
        {
            string taskDesc = string.Empty;
            for (int i = 2; i < cmdStrings.Length-1; i++)
            {
                taskDesc += (cmdStrings[i] + " ");
            }
            taskDesc += cmdStrings[^1];

            if (_files.Count == 0)
            {
                Console.WriteLine("No lists available! use <dotnet run init filename>.");
            }
            else
            {
                LoadTask(cmdStrings[1]);
                if (!DupeTask(taskDesc))
                {
                    _tasks.Add(new Task(taskDesc, false));
                    SaveTask(cmdStrings[1]);
                    Console.WriteLine($"Task added! To view use <dotnet run show {cmdStrings[1]}>.");
                }

            }
        }

        public static bool DupeTask(string taskDesc)
        {
            foreach (Task item in _tasks)
            {
                if (item.TaskDescription == taskDesc)
                {
                    Console.WriteLine("Error! Dupe/Already added task!");
                    return true;
                }
            }

            return false;
        }

    }
}