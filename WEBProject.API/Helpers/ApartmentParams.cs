namespace WEBProject.API.Helpers
{
    public class ApartmentParams
    {
        private const int MaxPageSize = 30;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int minPrice { get; set; } = 0;
        public int maxPrice { get; set; } = 99;
        public int minRooms { get; set; } = 1;
        public int maxRooms { get; set; } = 10;
        public int guests { get; set; } = 1;
        public string city { get; set; } = "";
        public string country { get; set; } = "";
        public string orderby { get; set; }
        public string startDate { get; set; } = "";
        public string endDate { get; set; } = "";
        public string filtertype { get; set; } = "";
        public string filterstatus { get; set; } = "";
        public string filteramenities { get; set; } = "";

    }
}