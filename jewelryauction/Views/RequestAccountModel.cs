namespace jewelryauction.Views
{
    public class RequestAccountModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public string AccountPhone { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
