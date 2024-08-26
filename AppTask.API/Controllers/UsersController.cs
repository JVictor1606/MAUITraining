using AppTask.API.Libraries.Constants;
using AppTask.API.Libraries.Emails;
using AppTask.API.Libraries.Text;
using AppTask.DataBase.Repositories;
using AppTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserModelRepository _repository;
        private EmailAcessToken _email;
        private int valueToken = 4;
        public UsersController(IUserModelRepository repository, EmailAcessToken email)
        {
            _repository = repository;
            _email = email;
        }
        [HttpGet]
        public IActionResult GetAcessToken(string email)
        {
           var user =  _repository.GetByEmail(email);
            if (user == null) 
            {
                // Fazer o vadastro do usuario e criar token
                
              user = new UserModel()
                {
                    Email = email,
                    AcessToken = String.Empty.GenerateHash(valueToken),
                    AcessTokenCreated = DateTimeOffset.Now,
                    Created = DateTimeOffset.Now,
                };

                _repository.Add(user);
            }
            else
            {
                // Gerar um novo token de acesso 
                user.AcessToken = String.Empty.GenerateHash(valueToken);
                user.AcessTokenCreated = DateTimeOffset.Now;

                _repository.Update(user);
            }

            //Enivar o email
            _email.Send(user);

            return Ok(user);
        }
        [HttpPost]
        public IActionResult ValidateAcessToken(UserModel userModel) 
        {
            var user =_repository.GetByEmail(userModel.Email);
            if (userModel == null)
                return NotFound();

            if(user.AcessToken == userModel.AcessToken)
            {
               var serverHours = DateTimeOffset.Now.Add(Config.LimitAcessTokenCreated);
               var tokenLimitHours = user.AcessTokenCreated.Add(Config.LimitAcessTokenCreated);
                if (serverHours <= tokenLimitHours) 
                {
                    return Ok(user);
                }
            }
            else
            {
                return BadRequest("AcessToken inválido ou vencido");
            }
            return Ok();
        }
    }
}
