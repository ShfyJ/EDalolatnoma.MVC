using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using EDalolatnoma.MVC.Constants;
using EDalolatnoma.MVC.Data;
using EDalolatnoma.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace EDalolatnoma.MVC.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Администратор")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly INotyfService _notyf;
        private readonly ApplicationDbContext _context;
        public RegisterModel(
            INotyfService notyf,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
             ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _notyf = notyf;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Введите Email")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Введите имя пользователя")]
            [Display(Name = "Ф.И.О. пользователя")]
            public string FioName { get; set; } 

            [Required(ErrorMessage = "Введите должность пользователя")]
            [Display(Name = "Должность")]
            public string Position { get; set; }



            [Required(ErrorMessage = "Выберите организацию")]
            [Display(Name = "Организация")]
            public int Company_id { get; set; }


            [Required(ErrorMessage = "Поле Пароль обязательно для заполнения.")]
            [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} и не более {1} символов.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            
            [DataType(DataType.Password)]
            [Display(Name = "Подтвердите пароль")]
            [Compare("Password", ErrorMessage = "Пароль и пароль подтверждения не совпадают.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["Company_id"] = new SelectList(_context.Company, "Id", "CompanyFullName");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var emails = _userManager.Users.Select(u => u.Email).ToList();

            if (emails.Contains(Input.Email))
            {
               
                _notyf.Error("Зарегистрированная Email.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email, Email = Input.Email, 
                    Company_id=Input.Company_id,FioName = Input.FioName,  
                    Position = Input.Position,EmailConfirmed=true,   
                 };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _notyf.Success("Учетный запись успешно создан!");
                    _logger.LogInformation("Пользователь создал новую учетную запись с паролем.");
                    return RedirectToAction("Index", "Users");
                }
                foreach (var error in result.Errors)
                {
                    _notyf.Error(error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

           
            return Page();
        }
    }
}
