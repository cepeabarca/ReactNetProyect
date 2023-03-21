namespace ReactNetProyect.Shared.DTO
{
    public class PagerDTO
    {
        public int Page { get; set; } = 1;
        private int recordsXPage= 10;
        private readonly int MaxrecordsxPage = 50;

        public int RecordsXPage
        {
            get
            {
                return recordsXPage;
            }
            set
            {
                recordsXPage = (value > MaxrecordsxPage) ? MaxrecordsxPage : value;
            }
        }
    }
}