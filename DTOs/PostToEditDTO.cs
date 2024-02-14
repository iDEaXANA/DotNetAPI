namespace DotnetAPI.DTOs
{

    public partial class PostToEditDTO
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; } = string.Empty;
        public string PostContent { get; set; } = string.Empty;
    }
}