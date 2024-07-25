namespace AAUG.DomainModels.ViewModels

{
    public class NewsForInsertViewModel
    {
        public string NewsTitle { get; set; } = null!;

        public string NewsDetails { get; set; } = null!;

        public int CreatorUserId { get; set; }
        public int NewsFileId { get; set; }
    }

    public class NewsForEditViewModel
    {
        public int Id { get; set; }

        public string NewsTitle { get; set; } = null!;

        public string NewsDetails { get; set; } = null!;

        public int CreatorUserId { get; set; }
        public int NewsFileId { get; set; }
    }

    public class NewsForShowViewModel
    {
        public int Id { get; set; }

        public string NewsTitle { get; set; } = null!;

        public string NewsDetails { get; set; } = null!;

        public int CreatorUserId { get; set; }
        public int NewsFileId { get; set; }
    }
}