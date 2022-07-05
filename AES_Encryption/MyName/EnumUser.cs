namespace AES_Encryption
{
    using System.ComponentModel.DataAnnotations;
    using AES_Encryption.Models;

    public class EnumUser : Enumeration
    {
        public static readonly EnumUser IdentityCard = new EnumUser(1, nameof(IdentityCard));
        public static readonly EnumUser ContractTransferObligations = new EnumUser(2, nameof(ContractTransferObligations));
        public static readonly EnumUser ProfilePicture = new EnumUser(3, nameof(ProfilePicture));
        public static readonly EnumUser DeclarationAML = new EnumUser(4, nameof(DeclarationAML));
        public static readonly EnumUser Report731 = new EnumUser(5, nameof(Report731));
        public static readonly EnumUser OfficialNoticeForProfit = new EnumUser(6, nameof(OfficialNoticeForProfit));

        private EnumUser(int value)
            : this(value, FromValue<EnumUser>(value).Name)
        {
        }

        private EnumUser(int value, string name)
            : base(value, name)
        {
        }
    }
}
