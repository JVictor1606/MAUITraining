using System.ComponentModel;

namespace AppTask.Models
{
    public class SubTaskModel : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTimeOffset Delete {  get; set; }
        
        private bool _isCompleted;
        

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; OnPropertyChanged(nameof(IsCompleted)); }
        }

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
