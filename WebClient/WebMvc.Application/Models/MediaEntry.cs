using System;

namespace WebMvc.Application.Models;

public class MediaEntry
{
    public string PreviewIconPath { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
}