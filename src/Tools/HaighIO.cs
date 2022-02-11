using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using HaighFramework;

namespace BearsEngine
{
    public static class HaighIO
    {
        #region enum CopyOptions
        [Flags]
        public enum CopyOptions : uint
        {
            None = 0,
            /// <summary>
            /// Whether to overwrite files in the target directory
            /// </summary>
            Overwrite = 1,
            /// <summary>
            /// Bring top level folders
            /// </summary>
            BringFolders = 2,
            /// <summary>
            /// Bring all subdirectories and folders within them
            /// </summary>
            BringAll = 6,
        }
        #endregion

        #region File Operations
        public static bool FileExists(string filePath) => File.Exists(filePath);

        public static bool DirectoryExists(string directoryPath) => Directory.Exists(directoryPath);

        #region DeleteFile
        public static void DeleteFile(string filePath)
        {
            if (!FileExists(filePath))
                throw new HException("file does not exist {0}", filePath);

            File.Delete(filePath);
        }
        #endregion

        #region DeleteDirectory
        public static void DeleteDirectory(string directoryPath)
        {
            if (!DirectoryExists(directoryPath))
                throw new HException("folder does not exist {0}", directoryPath);

            int retries = 5;
            while (retries > 0)
            {
                try
                {
                    Directory.Delete(directoryPath, true);
                    retries = 0;
                }
                catch
                {
                    System.Threading.Thread.Sleep(5);
                    retries--;
                }
            }
        }
        #endregion

        #region GetDirectories
        public static List<string> GetDirectories(string path, bool includeSubDirectories) => Directory.GetDirectories(path, "*", includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();

        public static List<string> GetDirectories(string path, bool includeSubDirectories, string searchPattern) => Directory.GetDirectories(path, searchPattern, includeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
        #endregion

        #region CopyFiles
        /// <summary>
        /// Copies all files and optionally folders from one directory to another
        /// </summary>
        /// <param name="fromDir">The directory to copy from</param>
        /// <param name="toDir">The directory to copy to</param>
        /// <param name="options">Various [Flags] options for copying</param>
        public static void CopyFiles(string fromDir, string toDir, CopyOptions options)
        {
            if (!Directory.Exists(toDir))
                Directory.CreateDirectory(toDir);

            if (fromDir.Last() == '/')
                fromDir = fromDir.Remove(fromDir.Length - 1);

            if (toDir.Last() != '/')
                toDir += '/';

            var so = (options & CopyOptions.BringAll) > 0 ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            //copy subfolders first (if applicable)
            if ((options & CopyOptions.BringFolders) > 0)
                foreach (string dir in Directory.GetDirectories(fromDir, "*", so))
                    Directory.CreateDirectory(Path.Combine(toDir, dir.Substring(fromDir.Length + 1)));

            //copy files
            foreach (string file in System.IO.Directory.GetFiles(fromDir, "*", so))
                File.Copy(file, System.IO.Path.Combine(toDir, file.Substring(fromDir.Length + 1)), (options & CopyOptions.Overwrite) > 0);
        }
        #endregion
        #endregion

        #region .txt
        #region SaveTXT
        /// <summary>
        /// Saves a single line of text into a .txt file
        /// </summary>
        public static void SaveTXT(string filename, string contents)
        {
            var dir = System.IO.Path.GetDirectoryName(filename);

            if (dir != "" && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(filename, contents);
        }

        /// <summary>
        /// Saves lines into a .txt file
        /// </summary>
        public static void SaveTXT(string filename, IEnumerable<string> lines)
        {
            var dir = System.IO.Path.GetDirectoryName(filename);

            if (dir != "" && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllLines(filename, lines);
        }
        #endregion

        #region TxtToArray
        /// <summary>
        /// Puts the contents of a .txt file into an array
        /// </summary>
        public static string[] LoadTXTAsArray(string filename) => File.ReadAllLines(filename);
        #endregion

        #region TxtToString
        /// <summary>
        /// Puts the contents of a .txt file into a string
        /// </summary>
        public static string LoadTXTAsString(string filename) => File.ReadAllText(filename);
        #endregion
        #endregion

        #region .csv
        #region SaveCSV
        /// <summary>
        /// Saves a 2D array to a .csv file
        /// </summary>
        public static void SaveCSV(string filename, string[,] data)
        {
            var csv = new StringBuilder();
            string line;

            for (int j = 0; j < data.GetLength(1); j++)
            {
                string[] lineData = new string[data.GetLength(0)];
                for (int i = 0; i < lineData.Length; i++)
                {
                    lineData[i] = data[i, j];
                }
                line = string.Join(",", lineData);
                csv.AppendLine(line);
            }

            File.WriteAllText(filename, csv.ToString());
        }
        #endregion

        #region LoadCSV
        /// <summary>
        /// Loads a .csv file into a 2D array
        /// </summary>
        public static string[,] LoadCSV(string filename)
        {
            List<string[]> data = new List<string[]>();

            using (var reader = new StreamReader(File.OpenRead(filename)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    data.Add(line);
                }
                string[,] ret = new string[data[0].Length, data.Count];
                for (int i = 0; i < ret.GetLength(0); i++)
                    for (int j = 0; j < ret.GetLength(1); j++)
                        ret[i, j] = data[j][i];
                return ret;
            }
        }
        #endregion
        #endregion

        #region BMP
        public static System.Drawing.Bitmap LoadBMP(string filePath) => new System.Drawing.Bitmap(filePath);
        #endregion

        #region XML
        #region SaveXML
        /// <summary>
        /// Saves a struct to a file containing XML 
        /// </summary>
        public static void SaveXML<M>(string fileName, M fileToSave)
            where M : struct
        {
            XmlSerializer writer = new XmlSerializer(fileToSave.GetType());

            StreamWriter file = new StreamWriter(fileName);

            writer.Serialize(file, fileToSave);
            file.Close();
        }
        #endregion

        #region LoadXML
        /// <summary>
        /// Creates a struct from a file containing an XML
        /// </summary>
        public static M LoadXML<M>(string fileName)
            where M : struct
        {
            if (File.Exists(fileName))
            {
                FileStream loadStream = new FileStream(fileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(M));
                M fileLoaded = (M)serializer.Deserialize(loadStream);
                loadStream.Close();
                return fileLoaded;
            }
            else throw new HException("File not found: {0}", fileName);
        }
        #endregion
        #endregion

        #region JSON
        #region SerialiseToJSON
        /// <summary>
        /// Converts a struct to its JSON string equivalent
        /// </summary>
        public static string SerialiseToJSON<M>(M @object, bool indent = false) => JsonSerializer.Serialize(@object, new JsonSerializerOptions { WriteIndented = indent });
        #endregion

        #region SaveJSON
        /// <summary>
        /// Creates a text file with a single line of JSON representing a struct
        /// </summary>
        public static void SaveJSON<M>(string filename, M @object, bool indent = false) => SaveTXT(filename, SerialiseToJSON(@object, indent));
        #endregion

        #region DeserialiseFromJSON
        /// <summary>
        /// Constructs an instance of a struct from its JSON string equivalent
        /// </summary>
        public static M DeserialiseFromJSON<M>(string json, bool indented = false) => JsonSerializer.Deserialize<M>(json, new JsonSerializerOptions { WriteIndented = indented });
        #endregion

        #region LoadJSON
        /// <summary>
        /// Creates a struct from a file containing a single line of JSON
        /// </summary>
        public static M LoadJSON<M>(string filename, bool indented = false) => DeserialiseFromJSON<M>(LoadTXTAsString(filename), indented);
        #endregion
        #endregion
    }
}