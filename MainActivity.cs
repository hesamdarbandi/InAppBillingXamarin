using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using InAppBilling.Billing;

namespace InAppBilling
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        InAppBillingConnectionHelper _connectionHelper;
        IInAppBillingHelper _billingHelper;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            try
            {
                _connectionHelper = new InAppBillingConnectionHelper(this, Config.BazarKey);
                _connectionHelper.OnConnected += async (sender, e) =>
                {
                    _billingHelper = _connectionHelper.BillingHelper;
                };

                _connectionHelper.Connect();
            }

            catch (Exception er)
            {
                var d = er.Message;
            }
        }

        private async Task<bool> CheckPurchase()
        {
            try
            {

                if (_billingHelper == null)
                {
                    Toast.MakeText(this, "عدم اتصال به کافه بازار و یا مایکت لطفا برنامه را مجددا اجرا کنید", ToastLength.Long);
                    return false;
                }

                var products = await _billingHelper.QueryInventoryAsync(new List<string>() { Billing.Billing.Sku }, ItemType.Subscription);
                if (products == null || !products.Any())
                {
                    Toast.MakeText(this, "لطفا در اپلیکیشن کافه بازار و یا مایکت وارد اکانت شوید ", ToastLength.Long);
                    return false;
                }

                var isPurchase = _billingHelper.GetPurchases(ItemType.InApp);
                if (isPurchase != null && isPurchase.Any(en => en.ProductId == products.FirstOrDefault().ProductId))
                {
                    return true;
                }

                _billingHelper.LaunchPurchaseFlow(Billing.Billing.Sku, ItemType.Subscription, "");
                return false;

            }
            catch (Exception er)
            {
                var msg = er.Message;
                return false;
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

     
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

