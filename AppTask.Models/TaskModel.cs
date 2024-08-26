using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AppTask.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset PrevisionDate { get; set;}

        public Guid UserId { get; set; }

        public UserModel? User { get; set; }

        public DateTimeOffset Created {  get; set; }
        public DateTimeOffset Update {  get; set; }
        public DateTimeOffset Deleted {  get; set; }
        
        private bool _isCompleted;

        public bool IsCompleted { 
            get {  return _isCompleted; }
            set { _isCompleted = value; OnPropertyChanged(nameof(IsCompleted)); }
        }

        public ObservableCollection<SubTaskModel> SubTasks { get; set;} = new ObservableCollection<SubTaskModel>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
