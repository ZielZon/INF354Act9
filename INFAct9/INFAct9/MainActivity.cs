using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using System;
using System.Collections.Generic;
using Android.Runtime;

namespace INFAct9
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        /*  protected override void OnCreate(Bundle savedInstanceState)
          {
              base.OnCreate(savedInstanceState);
              Xamarin.Essentials.Platform.Init(this, savedInstanceState);
              // Set our view from the "main" layout resource
              SetContentView(Resource.Layout.activity_main);
          }*/
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        EditText edtTask;
        sqldbClass sqldb;
        adapter adapter;
        ListView lstTask;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    edtTask = new EditText(this);
                    Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Shopping List Item")
                        .SetMessage("Item?")
                        .SetView(edtTask)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    alertDialog.Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        private void ButtonAddClicked (object sender, EventArgs e)
        {

            
                edtTask = new EditText(this);
                Android.Support.V7.App.AlertDialog alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetTitle("Add New Shopping List Item")
                    .SetMessage("Item?")
                    .SetView(edtTask)
                    .SetPositiveButton("Add", OkAction)
                    .SetNegativeButton("Cancel", CancelAction)
                    .Create();
                alertDialog.Show();
          
                    
        
       
        }
        private void CancelAction(object sender, DialogClickEventArgs e)
        {
        }
        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = edtTask.Text;
            sqldb.InsertNewTask(task);
            LoadTaskList();
        }
        public void LoadTaskList()
        {
            List<string> taskList = sqldb.getTaskList();
            adapter = new adapter(this, taskList, sqldb);
            lstTask.Adapter = adapter;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);

            sqldb = new sqldbClass(this);
            lstTask = FindViewById<ListView>(Resource.Id.lstTask);
            Button btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            btnAdd.Click += ButtonAddClicked;
            
            //Load Data  
            LoadTaskList();
        }
    }
}
