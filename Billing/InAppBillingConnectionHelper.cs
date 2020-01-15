using System;
using Android.Content;
using Android.App;
using Com.Android.Vending.Billing;

namespace InAppBilling.Billing
{
    public class InAppBillingConnectionHelper : Java.Lang.Object, IServiceConnection
    {
        public InAppBillingConnectionHelper(Activity activity, string publicKey)
        {
            _activity = activity;
            _publicKey = publicKey;
        }

        public IInAppBillingService Service
        {
            get;
            private set;
        }

        public IInAppBillingHelper BillingHelper
        {
            get;
            private set;
        }

        public void Connect()
        {
            var serviceIntent = new Intent(Billing.MayketBillingIntent);
            serviceIntent.SetPackage(Billing.MayketBillingPackageName);
            int intentServicesCount = _activity.PackageManager.QueryIntentServices(serviceIntent, 0).Count;
            if (intentServicesCount != 0)
            {
                _activity.BindService(serviceIntent, this, Bind.AutoCreate);
            }
        }

        public void Disconnected()
        {
            _activity.UnbindService(this);
        }

        #region IServiceConnection implementation

        public void OnServiceConnected(ComponentName name, Android.OS.IBinder service)
        {
            
            Service = IInAppBillingServiceStub.AsInterface(service);
            string packageName = _activity.PackageName;

            try
            {                
                int response = Service.IsBillingSupported(Billing.APIVersion, packageName, ItemType.InApp);
                if (response != BillingResult.OK)
                {
                    Connected = false;
                }
                
                // check for v3 subscriptions support
                response = Service.IsBillingSupported(Billing.APIVersion, packageName, ItemType.Subscription);
                if (response == BillingResult.OK)
                {                    
                    Connected = true;
                    RaiseOnConnected(Connected);

                    return;
                }
                else
                {                    
                    Connected = false;
                }

            }
            catch (Exception ex)
            {                
                Connected = false;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Connected = false;
            Service = null;
            BillingHelper = null;

            RaiseOnDisconnected();
        }

        #endregion

        public bool Connected
        {
            get;
            private set;
        }

        protected virtual void RaiseOnConnected(bool connected)
        {
            if (!connected)
            {
                return;
            }

            BillingHelper = new InAppBillingHelper(_activity, Service, _publicKey);

            var handler = OnConnected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseOnDisconnected()
        {
            var handler = OnDisconnected;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;

        Activity _activity;
        const string Tag = "Iab Helper";
        readonly string _publicKey;
    }
}
