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

namespace INFAct9
{
    class adapter : BaseAdapter
    {
        private MainActivity mainActivity;
        private List<string> taskList;
        private sqldbClass dbHelper;

        public adapter(MainActivity mainActivity, List<string> taskList, sqldbClass dbHelper)
        {
            this.mainActivity = mainActivity;
            this.taskList = taskList;
            this.dbHelper = dbHelper;
        }
        public override int Count { get { return taskList.Count; } }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = inflater.Inflate(Resource.Layout.row, null);
            TextView txtTask = view.FindViewById<TextView>(Resource.Id.task_title);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);
            Button btnTaskTitle = view.FindViewById<Button>(Resource.Id.task_title);
          
            txtTask.Text = taskList[position];
           
            btnTaskTitle.LongClick += delegate
            {
                string task = taskList[position];
                dbHelper.deleteTask(task);
                mainActivity.LoadTaskList(); // Reload Data 
            };
            btnDelete.Click += delegate
            {
                string task = taskList[position];
                dbHelper.deleteTask(task);
                mainActivity.LoadTaskList(); // Reload Data  
            };
            return view;
        }

       
    }
}
