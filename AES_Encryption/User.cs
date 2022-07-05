namespace AES_Encryption
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Encrypted]
        public string? Email { get; set; }
        [Encrypted]
        public string? IdentityNumber { get; set; }

        public string? PetName { get; set; }
    }
}
