using System;

namespace WebMvc.Models.MediaEntry;

public interface IMediaEntry
{
    public string PreviewIconPath { get; set; } // ?
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
}