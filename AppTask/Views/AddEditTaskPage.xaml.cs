using AppTask.Models;
using AppTask.DataBase.Repositories;
using System.Text;

namespace AppTask.Views;

public partial class AddEditTaskPage : ContentPage
{
    private ITaskModelRepository _repository;
    private TaskModel _task;
	public AddEditTaskPage()
	{
		InitializeComponent();
        _task = new TaskModel();
        _repository = new TaskModelRepository();
        BindableLayout.SetItemsSource(BindableLayout_Steps, _task.SubTasks);
	}

    public AddEditTaskPage(TaskModel task)
    {
        _repository = new TaskModelRepository();

        InitializeComponent();
        _task = task;
        FillFilds();

        BindableLayout.SetItemsSource(BindableLayout_Steps, _task.SubTasks);
    }

    private void FillFilds()
    {
        Entry_TaskName.Text = _task.Name;
        Editor_TaskDescription.Text = _task.Description;
        TaskDatePicker.Date = _task.PrevisionDate.Date;
    }
    private void CloseModal(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
    private void SaveData(object sender, EventArgs e)
    {
        //Carregar os dados
        GetDataFromForm();

        //Validar os Dados
        bool valid = ValidData();
        //Save date
        if(valid) 
        {
            SaveInDataBase();
        // Close Page
        Navigation.PopModalAsync();
        //Update List of previous page
        UpdateListInStartPage();
        }


    }

    private void GetDataFromForm()
    {
        _task.Name = Entry_TaskName.Text;
        _task.Description = Editor_TaskDescription.Text;
        _task.PrevisionDate = TaskDatePicker.Date;
        _task.PrevisionDate = _task.PrevisionDate.AddHours(23);
        _task.PrevisionDate = _task.PrevisionDate.AddMinutes(59);
        _task.PrevisionDate = _task.PrevisionDate.AddSeconds(59);

        _task.Created = DateTime.Now;
        _task.IsCompleted = false;

    }

    private bool ValidData()
    {
        Lbl_TaskName_Required.IsVisible = false;
        Lbl_TaskDescription_Required.IsVisible = false;

        bool validResult = true;

        if (string.IsNullOrWhiteSpace(_task.Name))
        {
            Lbl_TaskName_Required.IsVisible = true;
            validResult = false;
        }
        if (string.IsNullOrWhiteSpace(_task.Description))
        {
            Lbl_TaskDescription_Required.IsVisible = true;
            validResult = false;
        }



        return validResult;
    }

    private void SaveInDataBase()
    {
        if(_task.Id == default(Guid))
        {
            _repository.Add(_task);
        }
        else
            _repository.Update(_task);
        
    }

    private void UpdateListInStartPage() 
    {
        var navPage = (NavigationPage)App.Current.MainPage;
        var startPage = (StartPage)navPage.CurrentPage;
        startPage.LoadData();
    }

    private async void AddStep(object sender, EventArgs e)
    {
        var stepName = await DisplayPromptAsync("Etapa", "Digite o nome da etapa ou subtarefa:", "Adicionar", "Cancelar");
        if(!string.IsNullOrWhiteSpace(stepName))
        {
            _task.SubTasks.Add(new SubTaskModel { Name = stepName, IsCompleted = false});
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        TaskDatePicker.WidthRequest = width - 39;
    }

    private void OnTapToDelete(object sender, TappedEventArgs e)
    {
        _task.SubTasks.Remove((SubTaskModel)e.Parameter);
    }
}