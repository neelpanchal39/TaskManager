using System.Diagnostics;
using System.Windows.Input;
using SQLite;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.ViewModels
{
    public class AddEditTasksViewModel : BaseViewModel
	{
		public AddEditTasksViewModel()
		{
		}

        SQLiteConnection db;
        public IList<StatusTypes> StatusTypes { get; } = Enum.GetValues(typeof(StatusTypes)).Cast<StatusTypes>().ToList();

        private StatusTypes _selectedStatusType;
        public StatusTypes SelectedStatusType
        {
            get { return _selectedStatusType; }
            set { SetProperty(ref _selectedStatusType, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _saveButtonText = "Add";
        public string SaveButtonText
        {
            get { return _saveButtonText; }
            set { SetProperty(ref _saveButtonText, value); }
        }

        private string _deleteButtonText = "Cancle";
        public string DeleteButtonText
        {
            get { return _deleteButtonText; }
            set { SetProperty(ref _deleteButtonText, value); }
        }

        private TaskModel _taskDetails = new TaskModel();
        public TaskModel TaskDetails
        {
            get { return _taskDetails; }
            set { SetProperty(ref _taskDetails, value); }
        }

        public ICommand UploadDataCommand { get { return new Command(async () => await UploadData()); } }
        private async Task UploadData()
        {
            try
            {
                if(!string.IsNullOrEmpty(TaskDetails.Title) && !string.IsNullOrEmpty(TaskDetails.Description))
                {
                    db = DatabaseServices.DbConnection();
                    if(db != null)
                    {
                        var success = DatabaseServices.InsertTasks(TaskDetails);
                        if (success > 0)
                        {
                            //await _navigationService.NavigateAsync("/LoginPage");
                            await App.Current.MainPage.DisplayAlert("Success", "Task Saved", "Ok");
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please enter details", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception ======" + ex.Message);
            }
        }

        public ICommand DeleteDataCommand { get { return new Command<int>(async (arg) => await DeleteData(arg)); } }
        private async Task DeleteData(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    var success = DatabaseServices.DeleteTask(Id);
                    if (success <= 0)
                    {
                        return;
                    }
                }
                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception ======" + ex.Message);
            }
        }
    }
}

