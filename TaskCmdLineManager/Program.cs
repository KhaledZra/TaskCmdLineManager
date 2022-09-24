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
                Console.WriteLine("Worked!");
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
                            Console.WriteLine("Error! Do this: dotnet run init <filename>. (Without <>)");
                        }
                        break;

                    case "show":
                        Show();
                        break;

                    case "add":
                        AddDebug();
                        break;

                    case "adddebug":
                        AddDebug();
                        break;

                    default:
                        Console.WriteLine("ERROR! Type 'dotnet run help' to find out how to get started!");
                        break;

                }
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
            _files.Add(new(listName + ".json"));
            var options = new JsonSerializerOptions { WriteIndented = true };
            string sJsonString = JsonSerializer.Serialize(new Task(), options);
            File.WriteAllText(_files[^1].FileName, sJsonString);
            SaveFileList();

            Console.WriteLine("Success!");
        }

        public static void Show()
        {
            Load();
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

        public static void Load()
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-6-0
            // To deserialize from a file by using synchronous code
            string sFileName = "TaskList.json";
            string sJsonString = File.ReadAllText(sFileName);
            _tasks = JsonSerializer.Deserialize<List<Task>>(sJsonString)!;
            //return JsonSerializer.Deserialize<Task>(sJsonString)!;
        }

        public static void AddDebug()
        {
            Load();
            _tasks.Add(new Task());
            //Save();
        }

    }
}