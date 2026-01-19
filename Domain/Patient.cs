namespace Domain
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public override string? ToString()
        {
            return $"{IdPatient} {FirstName}  {LastName}";
        }
    }

}
