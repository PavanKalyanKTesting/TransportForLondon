using Newtonsoft.Json;


namespace TransportForLondon.Context
{
    public class DataReaderClass
    {
        /// <summary>
        /// Capturing file path of data file
        /// </summary>
        /// <param name="filestring"></param>
        /// <returns></returns>
        public static string ReadFile(string filestring)
        {
            string pathToProject = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            string filePath = Path.Combine(pathToProject, "DataFiles/" + filestring);
            return filePath;
        }
        /// <summary>
        /// Reading the Data file: TestDataFile.json
        /// </summary>
        /// <returns></returns>
        public FileDataTest ReadFileData()
        {
            string FilePath = ReadFile(@"TestDataFile.json");
            string jsonText = File.ReadAllText(FilePath);
            FileDataTest data = JsonConvert.DeserializeObject<FileDataTest>(jsonText);
            return data;
        }
    }
    public class FileDataTest
    {
        public string URL { get; set; } = string.Empty;
        public _ValidLocation ValidLocation { get; set; } = new _ValidLocation();
        public _InvalidLocation InvalidLocation { get; set; } = new _InvalidLocation();
        public _EditLocation EditLocation { get; set; } = new _EditLocation();
    }
    public class _InvalidLocation
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
    public class _ValidLocation
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
    public class _EditLocation
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
    }
}

