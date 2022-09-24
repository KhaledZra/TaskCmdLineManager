using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskCmdLineManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            if (!(args.Length == 0))
            {
                switch (args[0].ToLower())
                {
                    case "help":
                        Help(args);
                        break;

                    case "init":
                        Init(args);
                        break;
                }
            }
        }

        public static void Help(string[] args)
        {
            Console.WriteLine("Worked!");
            Console.WriteLine($"Input: {args[0]}");
        }

        public static void Init(string[] args)
        {
            Console.WriteLine("Worked!");
            Console.WriteLine($"Input: {args[0]}");

            string sFileName = "TaskList.json";
            string sJsonString = JsonSerializer.Serialize(new Task("Do homework", false));
            File.WriteAllText(sFileName, sJsonString);

            Console.WriteLine(File.ReadAllText(sFileName));
        }
    }
}