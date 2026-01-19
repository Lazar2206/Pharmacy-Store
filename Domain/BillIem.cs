namespace Domain
{
    public class BillItem
    {
        public int IdBill { get; set; }
        public int Rb { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int IdMedicine { get; set; }
    }

}
