namespace WebMvc.Models;

public class File
{
    public File(string filename)
    {
        FileName = filename;
    }
    
    public string FileName { get; set; }

    public override string ToString()
    {
        return FileName;
    }
}