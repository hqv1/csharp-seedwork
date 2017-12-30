namespace Hqv.Seedwork.Web.Api
{

    /// <summary>
    /// Use by the API Controller to allow paging, searching, ordering, and shapping of resources
    /// </summary>
    public class ResourceParameters
    {        
        private readonly int _maxPageSize;
        private int _pageSize;

        public ResourceParameters()
        {
            _maxPageSize = 20;
            PageSize = 10;
        }

        public ResourceParameters(int maxPageSize, int defaultPageSize)
        {            
            _maxPageSize = maxPageSize;
            PageSize = defaultPageSize;
        }

        public int PageNumber { get; set; } = 1;
        
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; }

        public string Fields { get; set; }
    }
}