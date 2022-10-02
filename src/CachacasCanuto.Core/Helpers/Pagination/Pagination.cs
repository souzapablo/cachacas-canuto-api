namespace CachacasCanuto.Core.Helpers.Pagination
{
    public class Pagination<T>
    {
        public Pagination(int totalElements, int currentPage, int quantityPerPage)
        {
            CurrentPage = currentPage;
            QuantityPerPage = quantityPerPage;
            TotalElements = totalElements;
        }

        public int CurrentPage { get; set; }
        public int QuantityPerPage { get; set; }
        public int TotalPages
        {
            get { return Convert.ToDouble(TotalElements % QuantityPerPage) == 0 && TotalElements != 0 ? TotalElements / QuantityPerPage : TotalElements / QuantityPerPage + 1; }
        }
        public int TotalElements { get; set; }
        public List<T> Data { get; set; } = new();

        public void AddData(List<T> list)
        {
            var firstIndex = (CurrentPage - 1) * QuantityPerPage;
            var lastIndex = Math.Min(firstIndex + QuantityPerPage - 1, list.Count - 1);

            for (int i = firstIndex; i <= lastIndex; i++)
            {
                Data.Add(list[i]);
            }
        }
    }
}
