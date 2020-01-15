
namespace InAppBilling.Billing
{
    public class Receipt
    {
        
        public string Id { get; set; }

        public string BundleId { get; set; }

        public string TransactionId { get; set; }

        public string DeveloperPayload { get; set; }

        public string PurchaseToken { get; set; }
    }

    public class Product
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
    }

    public class Order
    {
        public string PackageName { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string DeveloperPayload { get; set; }
        public long PurchaseTime { get; set; }
        public int PurchaseState { get; set; }
        public string PurchaseToken { get; set; }
        public byte[] Signature { get; set; }
    }
}
