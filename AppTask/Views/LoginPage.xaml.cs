using AppTask.DataBase.Repositories;
using AppTask.Libraries.Authentications;
using AppTask.Libraries.Validations;
using AppTask.Models;
using AppTask.Services;

namespace AppTask.Views;

public partial class LoginPage : ContentPage
{
    private IUserService _service;
    private IUserModelRepository _repository;
    private StartPage _startPage;

    public LoginPage(IUserService service, IUserModelRepository repository, StartPage startPage)
    {
        InitializeComponent();
        _service = service;
        _repository = repository;
        _startPage = startPage;
    }

    private async void NextAction(object sender, EventArgs e)
    {
        //RESET STATUS
        LblEmailValidateMessage.IsVisible = false;
        var email = EntryEmail.Text.Trim().ToLower();

        //VALIDAR E-MAIL
        if (!EmailValidate.IsValidEmail(email))
		{
            await _service.GetAcessToken(email);
			LblEmailValidateMessage.IsVisible = true;
            return;
		}

        EntryEmail.IsEnabled = false;
        BtnNext.IsVisible = false;
        Step2.IsVisible = true;
    }

    private async void AcessAction(object sender, EventArgs e)
    {
        var email = EntryEmail.Text.Trim().ToLower();
        var acessToken = EntryAcessToken.Text.Trim();


        LblAcessTokenValidateMessage.IsVisible = false;


        UserModel userAPI = await _service.ValidateAcessToken(new UserModel {Email = email, AcessToken = acessToken});
        if(userAPI != null)
        {
            var userDb = _repository.GetByEmail(userAPI.Email);
            if(userDb == null)
                _repository.Add(userAPI);

            else
                _repository.Update(userAPI);


            UserAuth.SetUserLogged(userAPI);

            App.Current.MainPage = new NavigationPage(_startPage);

        }
        else
        {
            LblAcessTokenValidateMessage.IsVisible = true;
        }
    }
}