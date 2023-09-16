using System;

namespace WebMvc.Application.Models;

public class MediaEntryBuilder
{
    private MediaEntry _mediaEntry = new();

    public MediaEntryBuilder()
    {
        _mediaEntry.PreviewIconPath = string.Empty;
        _mediaEntry.Name = string.Empty;
        _mediaEntry.CreationDate = DateTime.MinValue;
        _mediaEntry.LastModificationDate = DateTime.MinValue;
    }

    public MediaEntryBuilder SetPreviewIconPath(string previewIconPath)
    {
        _mediaEntry.PreviewIconPath = previewIconPath;
        return this;
    }

    public MediaEntryBuilder SetName(string name)
    {
        _mediaEntry.Name = name;
        return this;
    }

    public MediaEntryBuilder SetCreationDate(DateTime creationDate)
    {
        _mediaEntry.CreationDate = creationDate;
        return this;
    }

    public MediaEntryBuilder SetLastModificationDate(DateTime lastModificationDate)
    {
        _mediaEntry.LastModificationDate = lastModificationDate;
        return this;
    }

    public MediaEntry Build()
    {
        return _mediaEntry;
    }
}