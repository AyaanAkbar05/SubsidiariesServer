namespace SubsidiariesServer.Data
{
    public class Subsidiaries
    {
        public string Subsidiary { get; set; } = null!;
        public string ParentCompany { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal? RevenueInBillions { get; set; }

    }
}
