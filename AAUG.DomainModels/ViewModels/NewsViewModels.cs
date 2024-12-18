using AAUG.DomainModels.Dtos.Media;
using Microsoft.AspNetCore.Http;

namespace AAUG.DomainModels.ViewModels

{
    public class NewsForInsertViewModel
    {
        public string NewsTitle { get; set; } = null!;
        public string NewsDetails { get; set; } = null!;
        public IFormFile? NewsFile { get; set; }
    }

    public class NewsForEditViewModel
    {
        public int Id { get; set; }

        public string NewsTitle { get; set; } = null!;

        public string NewsDetails { get; set; } = null!;
        public IFormFile? NewsFile { get; set; }
    }

    public class NewsForShowViewModel
    {
        public int Id { get; set; }

        public string NewsTitle { get; set; } = null!;

        public string NewsDetails { get; set; } = null!;

        public int CreatorUserId { get; set; }
        public int? NewsFileId { get; set; }
        public MediaFileGetDto? NewsFile { get; set; }
    }

    #region teaser
    public class NewTeaserGetViewModel
    {
        public int Id { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string NewsDetails { get; set; } = null!;
        public int CreaterUserId { get; set; }
        public int? NewsFileId { get; set; }

    }
    #endregion
}