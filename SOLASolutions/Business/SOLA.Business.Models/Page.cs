namespace SOLA.Business.Models
{
    public class Page
    {
        public int PageId { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string StateName { get; set; }

        public string IconUrl { get; set; }

        public bool IsEnabled { get; set; }
    }
}
