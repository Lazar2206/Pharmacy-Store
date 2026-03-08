namespace Domain
{
    public class Bill
    {
        public int IdBill { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int IdPharmacy { get; set; }
        public int IdPatient { get; set; }
        public List<BillItem> BillItems { get; set; }
        public string Status { get; set; } = "Obrada";
    }

}
