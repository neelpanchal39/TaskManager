using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using SQLite;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Views;

namespace TaskManager.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
        SQLiteConnection db;
        public HomeViewModel()
		{
            
        }

		private ObservableCollection<TaskModel> _taskDetails;
        public ObservableCollection<TaskModel> TaskDetails
        {
            get { return _taskDetails; }
            set { SetProperty(ref _taskDetails, value); }
        }

        public async Task LoadData()
        {
            db = DatabaseServices.DbConnection();
            if (db != null)
            {
                var response = DatabaseServices.GetAllData(db);
                if (response != null)
                {
                    var tempList = response.Select(x =>
                    {
                        x.StatusColor = SetStatusColor(x.StatusType);
                        return x;
                    });
                    TaskDetails = new ObservableCollection<TaskModel>(tempList);
                }
            }
        }

        public ICommand SelectedTaskCommand { get { return new Command<TaskModel>(async (arg) => await SelectedTaskData(arg)); } }
        private async Task SelectedTaskData(TaskModel model)
        {
            try
            {
                AddEditTasksPage editTasksPage = new AddEditTasksPage();
                AddEditTasksViewModel addEditTasksView = new AddEditTasksViewModel();
                addEditTasksView.TaskDetails = model;
                addEditTasksView.SaveButtonText = "Update";
                addEditTasksView.DeleteButtonText = "Delete";
                editTasksPage.BindingContext = addEditTasksView;

                await App.Current.MainPage.Navigation.PushAsync(editTasksPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception ======" + ex.Message);
            }
        }

        private static string SetStatusColor(StatusTypes status)
        {
            try
            {
                switch (status)
                {
                    case StatusTypes.Pending:
                        return "#f89e37";
                    case StatusTypes.Inprogress:
                        return "#6677cd";
                    case StatusTypes.Completed:
                        return "#22a455";
                }
                return "#000000";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception => " + ex);
                return "#000000";
            }
        }
    }
}

