using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Enums;
using AAUG.DomainModels.Media;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AAUG.Service.Implementations.Media;

public class MediaFIleService : IMediaFileService
{
    private readonly IMapper mapper;
    private readonly IAaugUnitOfWork unitOfWork;
    public MediaFIleService(IAaugUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<MediaFileInsertViewModel> InsertMediaFilesAsync(MediaFileInsertViewModel insertEntity)
    {
        await unitOfWork.MediaFileRepository.AddMediaFileAsync(
           mapper.Map<MediaFileInsertDto>(insertEntity)
       );

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return insertEntity;
    }

    public async Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file)
    {
        var eventFolder = await unitOfWork.MediaFolderRepository.GetEventsFolder().FirstAsync();

        if (!Directory.Exists(eventFolder.Name))
        {
            Directory.CreateDirectory(eventFolder.Name);
        }

        var fileId = Guid.NewGuid();
        var fileName = $"{fileId}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(eventFolder.Name, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var mediaFileInsertDto = new MediaFileInsertDto
        {
            Gid = fileId,
            MediaFolderId = eventFolder.Id,
            Name = Path.GetFileNameWithoutExtension(file.FileName),
            Extension = Path.GetExtension(file.FileName),
            Size = file.Length / 1024.0, // Size in KB
            IsOmit = false,
            Date = DateTime.UtcNow
        };

        var mediaFile = await unitOfWork.MediaFileRepository.AddMediaFileAsync(mediaFileInsertDto);
        await unitOfWork.SaveChangesAsync();

        var mediaFileResult = mapper.Map<MediaFileGetDto>(mediaFileInsertDto);
        mediaFileResult.Id = mediaFile.Id;

        return mediaFileResult;

    }

    public async Task<FileResultDto> DownloadMediaFileAsync(int fileId)
    {
        var mediaFile = await unitOfWork.MediaFileRepository.GetMediaFile(fileId).FirstOrDefaultAsync();
        if (mediaFile == null)
        {
            throw new FileNotFoundException("File not found");
        }
        var folderPath = mediaFile.MediaFolder.Name;
        var filePath = Path.Combine(folderPath, $"{mediaFile.Gid}{mediaFile.Extension}");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found on disk.");
        }
        var fileBytes = await File.ReadAllBytesAsync(filePath);

        return new FileResultDto
        {
            FileName = $"{mediaFile.Name}{mediaFile.Extension}",
            ContentType = GetContentType(mediaFile.Extension),
            FileBytes = fileBytes
        };
        throw new ArgumentException("Invalid media file path.");
    }
    private string GetContentType(string extension)
    {
        // Simple content type mapping
        return extension.ToLower() switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream",
        };
    }
}
