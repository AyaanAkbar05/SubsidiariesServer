namespace SubsidiariesServer.DTO
{
    public class ParentCompanyRevenue
    {
        public required string Name { get; set; }
        public int ParentCompanyId { get; set; }
        public int RevenueInBillions { get; set; }
    }
}
