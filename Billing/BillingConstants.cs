using System;

namespace InAppBilling.Billing
{
    public sealed class BillingResult
    {
        BillingResult()
        {

        }

    
        public static int OK
        {
            get
            {
                return 0;
            }
        }

        public static int UserCancelled
        {
            get
            {
                return 1;
            }
        }

        public static int BillingUnavailable
        {
            get
            {
                return 3;
            }
        }


        public static int ItemUnavailable
        {
            get
            {
                return 4;
            }
        }

   
        public static int DeveloperError
        {
            get
            {
                return 5;
            }
        }

       
        public static int Error
        {
            get
            {
                return 6;
            }
        }

        public static int ItemAlreadyOwned
        {
            get
            {
                return 7;
            }
        }


        public static int ItemNotOwned
        {
            get
            {
                return 8;
            }
        }
    }


    public sealed class ItemType
    {
    
        public static string InApp
        {
            get
            {
                return "inapp";
            }
        }

  
        public static string Subscription
        {
            get
            {
                return "subs";
            }
        }
    }

    public sealed class Billing
    {
        Billing()
        {
        }

        public static int APIVersion
        {
            get
            {
                return 3;
            }
        }

        public static string BillingIntent
        {
            get
            {
                return "ir.cafebazaar.pardakht.InAppBillingService.BIND";
            }
        }

        public static string BillingPackageName
        {
            get
            {
                return "com.farsitel.bazaar";
            }
        }

        public static string MayketBillingIntent
        {
            get
            {
                return "ir.mservices.market.InAppBillingService.BIND";
            }
        }

        public static string MayketBillingPackageName
        {
            get
            {
                return "ir.mservices.market";
            }
        }


        public static string Sku
        {
            get
            {
                return "direct";
            }
        }

        public static string SkuDetailsList
        {
            get
            {
                return "DETAILS_LIST";
            }
        }

        public static string ItemIdList
        {
            get
            {
                return "ITEM_ID_LIST";
            }
        }
    }

    public static class Response
    {
        public static string Code
        {
            get
            {
                return "RESPONSE_CODE";
            }
        }

        public static string BuyIntent
        {
            get
            {
                return "BUY_INTENT";
            }
        }

        public static string InAppPurchaseData
        {
            get
            {
                return "INAPP_PURCHASE_DATA";
            }
        }

        public static string InAppDataSignature
        {
            get
            {
                return "INAPP_DATA_SIGNATURE";
            }
        }

        public static string InAppDataSignatureList
        {
            get
            {
                return "INAPP_DATA_SIGNATURE_LIST";
            }
        }

        public static string InAppPurchaseItemList
        {
            get
            {
                return "INAPP_PURCHASE_ITEM_LIST";
            }
        }

        public static string InAppPurchaseDataList
        {
            get
            {
                return "INAPP_PURCHASE_DATA_LIST";
            }
        }

        public static string InAppContinuationToken
        {
            get
            {
                return "INAPP_CONTINUATION_TOKEN";
            }
        }
    }

}