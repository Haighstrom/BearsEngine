using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;

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
                    Thread.Sleep(5);
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
            foreach (string file in Directory.GetFiles(fromDir, "*", so))
                File.Copy(file, Path.Combine(toDir, file.Substring(fromDir.Length + 1)), (options & CopyOptions.Overwrite) > 0);
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
            var dir = Path.GetDirectoryName(filename);

            if (dir != "" && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(filename, contents);
        }

        /// <summary>
        /// Saves lines into a .txt file
        /// </summary>
        public static void SaveTXT(string filename, IEnumerable<string> lines)
        {
            var dir = Path.GetDirectoryName(filename);

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
        public static void SaveCSV<T>(string filename, T[,] data)
            where T : IConvertible
        {
            var csv = new StringBuilder();

            for (int j = 0; j < data.GetLength(1); j++)
            {
                string[] lineData = new string[data.GetLength(0)];

                for (int i = 0; i < lineData.Length; i++)
                    lineData[i] = data[i, j].ToString();

                csv.AppendLine(string.Join(",", lineData));
            }

            File.WriteAllText(filename, csv.ToString());
        }
        #endregion

        #region LoadCSV
        /// <summary>
        /// Loads a .csv file into a 2D array
        /// </summary>
        public static T[,] LoadCSV<T>(string filename)
            where T : IConvertible
        {
            List<string[]> data = new();

            using var reader = new StreamReader(File.OpenRead(filename));

            while (!reader.EndOfStream)
                data.Add(reader.ReadLine().Split(','));

            T[,] ret = new T[data[0].Length, data.Count];

            for (int i = 0; i < ret.GetLength(0); i++)
                for (int j = 0; j < ret.GetLength(1); j++)
                    ret[i, j] = (T)Convert.ChangeType(data[j][i], typeof(T));

            return ret;
        }
        #endregion
        #endregion

        #region BMP
        public static System.Drawing.Bitmap LoadBMP(string filePath) => new(filePath);
        #endregion

        #region XML
        #region SaveXML
        /// <summary>
        /// Saves a struct to a file containing XML 
        /// </summary>
        public static void SaveXML<M>(string fileName, M fileToSave)
            where M : struct
        {
            XmlSerializer writer = new(fileToSave.GetType());

            StreamWriter file = new(fileName);

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
                FileStream loadStream = new(fileName, FileMode.Open);
                XmlSerializer serializer = new(typeof(M));
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
        public static string SerialiseToJSON<M>(M @object, bool indent = true) =>
            JsonSerializer.Serialize(@object, new JsonSerializerOptions
            {
                WriteIndented = indent,
                Converters = { new JSON.Array2DConverter() },
                IncludeFields = true,
            });
        #endregion

        #region SaveJSON
        /// <summary>
        /// Creates a text file with a single line of JSON representing a struct
        /// </summary>
        public static void SaveJSON<M>(string filename, M @object, bool indent = true) => SaveTXT(filename, SerialiseToJSON(@object, indent));
        #endregion

        #region DeserialiseFromJSON
        /// <summary>
        /// Constructs an instance of a struct from its JSON string equivalent
        /// </summary>
        public static M? DeserialiseFromJSON<M>(string json) =>
            JsonSerializer.Deserialize<M>(json, new JsonSerializerOptions
            {
                Converters = { new JSON.Array2DConverter() },
                IncludeFields = true,
            });
        #endregion

        #region LoadJSON
        /// <summary>
        /// Creates a struct from a file containing a single line of JSON
        /// </summary>
        public static M LoadJSON<M>(string filename) => DeserialiseFromJSON<M>(LoadTXTAsString(filename));
        #endregion

        #region LoadJSONFromMultilineTxt
        /// <summary>
        /// Reads lines from a txt file and deserialises each row as an individual json string
        /// </summary>
        public static List<M> LoadJSONFromMultilineTxt<M>(string filename) => LoadTXTAsArray(filename).Select(s => DeserialiseFromJSON<M>(s)).ToList()!; //todo: null check
        #endregion
        #endregion
    }
}