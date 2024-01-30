using Shared.Models;
using System.Diagnostics;

namespace Shared.Services
{
    public class FileService(string filePath) : IFile
    {
        private readonly string _filePath = filePath;
        public bool SaveContentToFile(string content)
        {
            try
            {
                using (var sw = new StreamWriter(_filePath))
                {
                    sw.WriteLine(content);
                }
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
        public string GetContentFormFile()
        {
            try
            {
                if(File.Exists(_filePath))
                {
                    using var sr = new StreamReader(_filePath);
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        /// <summary>
        /// Kontrollerar om en fil existerar.
        /// </summary>
        /// <param name="path">Sökvägen till filen.</param>
        /// <returns>True om filen existerar, annars false.</returns>
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Läser all text från en fil.
        /// </summary>
        /// <param name="path">Sökvägen till filen.</param>
        /// <returns>Texten i filen.</returns>
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Skriver all text till en fil.
        /// </summary>
        /// <param name="path">Sökvägen till filen.</param>
        /// <param name="contents">Texten som ska skrivas till filen.</param>
        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}

