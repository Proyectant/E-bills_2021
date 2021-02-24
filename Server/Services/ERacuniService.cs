using ERacuniNovi.Shared.Models;
using ERacuniProtoNameSpace;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using ERacuniNovi.Server.Context;

namespace ERacuniNovi.Server.Services
{
    public class ERacuniService : ERacuniProtoServis.ERacuniProtoServisBase
    {
        private readonly ILogger<ERacuniService> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public ERacuniService(ILogger<ERacuniService> log, UserManager<IdentityUser> umanager, SignInManager<IdentityUser> simanager)
        {
            _logger = log;
            _userManager = umanager;
            _signInManager = simanager;

        }

        public override async Task<StandardReplyMsg> Registration(RegistrationMsg request, ServerCallContext context)
        {
            _logger.LogInformation("usao u Registraciju");
            IdentityUser iuser;
            iuser = new User
            {
                UserName = request.Username,
                firstName = request.UserProto.FirstNameProto,
                lastName = request.UserProto.LastNameProto
            };
            _logger.LogInformation($"iz rekvesta smoo napunili iuser-a sa usernejmom {iuser.UserName}");
            var rezultat = await _userManager.CreateAsync(iuser, request.Password);
            _logger.LogInformation($"napunio rezultat i ako je true uspeo je >>>> {rezultat.Succeeded}<<<<");

            //ako je uspesna registracija vraca u poruci da je uspesno
            if (rezultat.Succeeded)
            {
                _logger.LogInformation("ovaj je uspeo da ga registruje");
                return new StandardReplyMsg { Success = true };
            }
            else
            {
                _logger.LogInformation("nije uspela registracije sad ce da napise greske");
                //ako nije uspesna onda vraca Uspeh da je false
                //i pise sto nece, tj sta je greska
                return new StandardReplyMsg
                {
                    Success = false,
                    Error = rezultat.Errors
                    .Select(e => e.Description)
                    .Aggregate((sveGreske, greska) => sveGreske += greska + System.Environment.NewLine)
                };
            }
        }

        public override async Task<StandardReplyMsg> LogIn(RegistrationMsg request, ServerCallContext context)
        {
            Console.WriteLine(request.Login);
            //ako bool promenljiva Login u requestu
            //je false onda vraca ovaj odgovor
            if (!request.Login)
                return new StandardReplyMsg { Success = false, Error = "ovo nije za login, verovatno je za registraciju :/" };

            //u bazi trazimo juzera kog smo dobili u requestu
            //preko username
            //ovde treba biti oprezan
            IdentityUser iuser = await _userManager.FindByNameAsync(request.Username);
            if (iuser is not null)
            {
                //rezultat je tipa SignInResult
                //pogledati konstruktor i analizirati ovo bool isPersistent
                var rezultat = await _signInManager.PasswordSignInAsync(iuser, request.Password, false, false);
                if (rezultat.Succeeded)

                    return new StandardReplyMsg { Success = true, Error = "Uspesno ste loginovani" };
                else
                    return new StandardReplyMsg { Success = false, Error = "Bad user/pass" };
            }
            else
            {
                return new StandardReplyMsg { Success = false, Error = "nema ga u bazi" };
            }
        }

        public override async Task<StandardReplyMsg> LogOut(EmptyMsg request, ServerCallContext context)
        {
            await _signInManager.SignOutAsync();
            return new StandardReplyMsg { Error = "Uspesno ste izlogovani" };
        }

        public override async Task<StandardReplyMsg> ProveriKorisnika(EmptyMsg request, ServerCallContext context)
        {
            Console.WriteLine("hjgfds");
            var s = _userManager.Users;
            Console.WriteLine(_userManager.Users.Count());
            try
            {
                if (_userManager.Users.Count() == 0)
                {
                    return new StandardReplyMsg { Success = true, Error = "nema" };
                }
                else
                {
                    return new StandardReplyMsg { Success = false, Error = "nema" };

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}