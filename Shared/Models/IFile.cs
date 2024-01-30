
namespace Shared.Models;
/// <summary>
/// Ett gränssnitt som definierar filåtgärder.
/// </summary>
public interface IFile
{
    bool SaveContentToFile(string content);
    string GetContentFormFile();
    bool Exists(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string contents);
}
