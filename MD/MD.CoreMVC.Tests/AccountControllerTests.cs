using MD.Data;
using MD.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using MD.CoreMVC.Controllers;
using System.Linq;
using MD.CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MD.CoreMVC.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void AccountControllerInstanceCreationSuccessful()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);

            Assert.NotNull(accountController);
            Assert.IsType<AccountController>(accountController);
        }

        [Fact]
        public async void ManageMethodReturnViewWithCorrectData()
        {
            var repoMock = new Mock<IRepository<Note>>();
            var repository = repoMock.Object;
            repoMock.Setup(x => x.GetAll("1d6w7a")).Returns(GetTestNotes().AsQueryable);
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);

            var result = await accountController.Manage();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("test user", accountController.ViewBag.UserName);
            Assert.Equal(2, accountController.ViewBag.NoteCount);
        }

        [Fact]
        public void LoginGetMethodReturnsViewWithNotEmptyModel()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);

            var result = accountController.Login("~/Home/Index");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<LoginViewModel>(viewResult.Model);
            Assert.Equal("~/Home/Index", model.ReturnUrl);
        }

        [Fact]
        public async void LoginPostMethodRedirectToHomeIndexViewIfSuccessfulResult()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);
            var loginViewModel = new LoginViewModel
            {
                Email = "test@test.com",
                Password = "testPassword@#12"
            };

            var result = await accountController.Login(loginViewModel);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void LogOffMethodReturnsRedirectToHomeIndexAction()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);

            var result = await accountController.LogOff();

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RegisterGetMethodReturnsViewWithEmptyModel()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);

            var result = accountController.Register();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public async void RegisterPostMethodRedirectToHomeIndexActionIfSuccessful()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var accountController = new AccountController(TestUserManager<AppUser>(), TestSignInManager<AppUser>(), repository);
            var registerViewModel = new RegisterViewModel
            {
                Email = "test@test.com",
                Password = "testPassword@#12",
                ConfirmPassword = "testPassword@#12"
            };

            var result = await accountController.Register(registerViewModel);

            Assert.IsType<RedirectToActionResult>(result);
        }

        #region private_methods
        private static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());

            var userManagerMock = new Mock<UserManager<TUser>>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            userManagerMock.Setup(x => x.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(() => "1d6w7a");
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .Returns(async () => await Task.FromResult(IdentityResult.Success));

            userManagerMock.Setup(x => x.GetUserName(It.IsAny<ClaimsPrincipal>())).Returns(() => "test user");

            var userManager = userManagerMock.Object;

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManagerMock.Object;
        }

        private static SignInManager<TUser> TestSignInManager<TUser>() where TUser : class
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>().Object;
            var userClaimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>().Object;
            var identityOptions = new Mock<IOptions<IdentityOptions>>().Object;
            var logger = new Mock<ILogger<SignInManager<TUser>>>().Object;
            var authSchemeProvider = new Mock<IAuthenticationSchemeProvider>().Object;

            var fakeSignInManagerMock = new Mock<SignInManager<TUser>>(TestUserManager<TUser>(), httpContextAccessor,
                userClaimsFactory, identityOptions, logger, authSchemeProvider);

            // Test login
            fakeSignInManagerMock.Setup(x =>
                    x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(async () =>
                {
                    return await Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success);
                });

            return fakeSignInManagerMock.Object;
        }

        private List<Note> GetTestNotes()
        {
            var notesList = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Date = DateTime.Parse("20.08.2018"),
                    Description = "Birthday",
                    Photos = new List<Photo>(),
                    UserId = "1d6w7a"
                },
                new Note
                {
                    Id = 2,
                    Date = DateTime.Parse("19.08.2018"),
                    Description = "Today",
                    Photos = new List<Photo>(),
                    UserId = "1d6w7a"
                },
            };
            return notesList;
        }
        #endregion
    }
}