namespace EF.DataProtection.Sample.Entities
{
    public class UserQuery
    {
        public long Id { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }
        
        public string SensitiveData { get; set; }
        
        public string PhoneNumberHash { get; protected set; }
    }
}