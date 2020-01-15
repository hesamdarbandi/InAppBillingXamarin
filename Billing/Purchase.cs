using System;
namespace InAppBilling.Billing
{

    public class Purchase
    {
        public string PackageName { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public string DeveloperPayload { get; set; }
        public string PurchaseTime { get; set; }
        public int PurchaseState { get; set; }
        public string PurchaseToken { get; set; }
    }
}
