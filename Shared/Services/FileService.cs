using Shared.Models;

namespace Shared.Services
{
    public class FileService : IFile
    {
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

