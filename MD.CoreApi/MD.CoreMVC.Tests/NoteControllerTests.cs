using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MD.CoreMVC.Controllers;
using MD.CoreMVC.Models;
using MD.Data;
using MD.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MD.CoreMVC.Tests
{
    public class NoteControllerTests
    {
        [Fact]
        public void NoteControllerInstanceCreationSuccessful()
        {
            var repository = new Mock<IRepository<Note>>().Object;
            var noteController = new NoteController(TestUserManager<AppUser>(), repository);

            Assert.NotNull(noteController);
            Assert.IsType<NoteController>(noteController);
        }

        [Fact]
        public void IndexReturnsAViewResultWithAListOfNotes()
        {
            var userManager = TestUserManager<AppUser>();

            var repoMock = new Mock<IRepository<Note>>();
            repoMock.Setup(x => x.GetAll("1d6w7a")).Returns(GetTestNotes().AsQueryable);
            var repository = repoMock.Object;
            var noteController = new NoteController(userManager, repository);

            var result = noteController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<NoteViewModel>>(viewResult.Model);
            Assert.Equal(GetTestNotes().Count, models.Count());
        }

        [Fact]
        public void CreateGetMethodReturnViewWithEmptyModel()
        {
            var userManager = TestUserManager<AppUser>();
            var repository = new Mock<IRepository<Note>>().Object;
            var noteController = new NoteController(userManager, repository);

            var result = noteController.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void CreatePostMethodSavesNoteSuccessfully()
        {
            var testStore = new List<Note>();
            var userManager = TestUserManager<AppUser>();
            var repoMock = new Mock<IRepository<Note>>();
            repoMock.Setup(x => x.CreateAsync(It.IsAny<Note>())).Callback((Note item) => { testStore.Add(item); });
            repoMock.Setup(x => x.SaveAsync()).Callback(() => { });
            var repository = repoMock.Object;
            var note = new CreateNoteViewModel { Description = "test note" };
            var noteController = new NoteController(userManager, repository);

            var result = noteController.Create(note);

            Assert.Single(testStore);
        }

        [Fact]
        public async void DetailsReturnsPartialViewWithNoteViewModel()
        {
            var note = new Note
            {
                Id = 3,
                Date = new DateTime(),
                Description = "test note"
            };
            var testStore = new List<Note> { note };
            var userManager = TestUserManager<AppUser>();
            var repoMock = new Mock<IRepository<Note>>();
            repoMock.Setup(x => x.GetByIdAsync(3)).Returns(async () => await Task.FromResult(note));
            var repository = repoMock.Object;
            var noteController = new NoteController(userManager, repository);

            var result = await noteController.Details(3);

            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<NoteViewModel>(viewResult.Model);
            Assert.Equal("test note", model.Description);
        }

        [Fact]
        public void DeleteMethodSuccessfullyDeleteNoteWithCorrectParam()
        {
            var testStore = GetTestNotes();
            var userManager = TestUserManager<AppUser>();
            var repoMock = new Mock<IRepository<Note>>();
            repoMock.Setup(x => x.DeleteAsync(It.IsAny<int?>())).Returns(async (int? id) =>
            {
                var noteToDelete = testStore.FirstOrDefault(x => x.Id == id);
                if (noteToDelete != null)
                {
                    testStore.Remove(noteToDelete);
                }
                return noteToDelete;
            });
            repoMock.Setup(x => x.SaveAsync()).Callback(() => { });
            var repository = repoMock.Object;
            var noteController = new NoteController(userManager, repository);

            var result = noteController.Delete(1);

            Assert.Single(testStore);
        }

        [Fact]
        public void DeleteMethodDoesNothingIfParamIsIncorrect()
        {
            var testStore = GetTestNotes();
            var userManager = TestUserManager<AppUser>();
            var repoMock = new Mock<IRepository<Note>>();
            repoMock.Setup(x => x.DeleteAsync(It.IsAny<int?>())).Returns(async (int? id) =>
            {
                var noteToDelete = testStore.FirstOrDefault(x => x.Id == id);
                if (noteToDelete != null)
                {
                    testStore.Remove(noteToDelete);
                }
                return noteToDelete;
            });
            repoMock.Setup(x => x.SaveAsync()).Callback(() => { });
            var repository = repoMock.Object;
            var noteController = new NoteController(userManager, repository);

            var result = noteController.Delete(null);

            Assert.Equal(2, testStore.Count);
        }

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
            var userManager = userManagerMock.Object;

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
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
    }
}