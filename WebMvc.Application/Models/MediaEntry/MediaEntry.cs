using System;

namespace WebMvc.Application.Models.MediaEntry;

public class MediaEntry : IMediaEntry
{
    public string PreviewIconPath { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastModificationDate { get; set; }
}