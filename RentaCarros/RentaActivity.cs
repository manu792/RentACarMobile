using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RentaCarros
{
    [Activity(Label = "RentaActivity")]
    public class RentaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Renta);

            // Add Tabs
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            ActionBar.SetIcon(Resource.Drawable.icon);
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.DisplayOptions = ActionBarDisplayOptions.HomeAsUp | ActionBarDisplayOptions.ShowTitle | ActionBarDisplayOptions.ShowHome;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}