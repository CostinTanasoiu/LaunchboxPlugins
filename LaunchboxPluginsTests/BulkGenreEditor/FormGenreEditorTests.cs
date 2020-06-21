using AutoFixture;
using BulkGenreEditor;
using LaunchboxPluginsTests.MockedClasses;
using NSubstitute;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins.RetroAchievements;
using Xunit;

namespace LaunchboxPluginsTests.BulkGenreEditor
{
    public class FormGenreEditorTests
    {
        private Fixture _fixture = new Fixture();
        private IDataManager _launchboxDataManagerMock = Substitute.For<IDataManager>();

        public FormGenreEditorTests()
        {
            _fixture.Customize<GameMock>(x => x
                .Without(y => y.AdditionalApplications)
                .Without(y => y.CustomFields)
            );
        }

        [Theory]
        [InlineData("Adventure")]
        [InlineData("Adventure", "Multiplayer")]
        [InlineData("Hello")]
        [InlineData("Hello World")]
        [InlineData("Hello", "Hello World", "Lorem Ipsum Dolor")]
        [InlineData("Adventure", "Hello", "Hello World", "Lorem Ipsum Dolor")]
        public void CanSelectExistingAndNewGenres(params string[] genres)
        {
            var dummyGames = GetListOfDummyGames();
            var selectedGames = dummyGames.Take(4).ToArray();

            _launchboxDataManagerMock.GetAllGames()
                .Returns(dummyGames);

            // Creating a temporary thread to run the form.
            // Implementation based on: https://ourcodeworld.com/articles/read/890/how-to-solve-csharp-exception-current-thread-must-be-set-to-single-thread-apartment-sta-mode-before-ole-calls-can-be-made-ensure-that-your-main-function-has-stathreadattribute-marked-on-it
            Thread t = new Thread((ThreadStart)(() => {
                var sutForm = new FormGenreEditor(_launchboxDataManagerMock, selectedGames);
                foreach (var genre in genres)
                    sutForm.SelectGenres(genre);
                sutForm.AddSelectedGenres();
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            // Check that all our selected games now have all the genres we've selected
            foreach (var game in selectedGames)
            {
                var gameGenres = game.GenresString.Split(';');
                Assert.Equal(genres.Length, gameGenres.Intersect(genres).Count());
            }

            // Now check that these genres were not also applied to the other games.
            var otherGames = dummyGames.Except(selectedGames);
            foreach (var game in otherGames)
            {
                var gameGenres = game.GenresString.Split(';');
                Assert.NotEqual(genres.Length, gameGenres.Intersect(genres).Count());
            }
        }

        [Theory]
        [InlineData("Action")]
        [InlineData("Action", "Multiplayer")]
        public void CanRemoveGenres(params string[] genres)
        {
            var dummyGames = GetListOfDummyGames();
            var selectedGames = dummyGames.Take(2).ToArray();

            _launchboxDataManagerMock.GetAllGames()
                .Returns(dummyGames);

            // Creating a temporary thread to run the form.
            Thread t = new Thread((ThreadStart)(() => {
                var sutForm = new FormGenreEditor(_launchboxDataManagerMock, selectedGames);
                foreach (var genre in genres)
                    sutForm.SelectGenres(genre);
                sutForm.RemoveSelectedGenres();
            }));

            // Run your code from a thread that joins the STA Thread
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            // Check that none of our selected games have the genres that we've removed
            foreach (var game in selectedGames)
            {
                var gameGenres = game.GenresString.Split(';');
                Assert.Empty(gameGenres.Intersect(genres));
            }

            // Now check that these genres were not also applied to the other 2 games for which we have known values.
            var otherGames = dummyGames.Except(selectedGames);
            foreach (var game in selectedGames.Skip(2).Take(2))
            {
                var gameGenres = game.GenresString.Split(';');
                Assert.NotEmpty(gameGenres.Intersect(genres));
            }
        }

        #region Private Methods

        public IGame[] GetListOfDummyGames()
        {
            // Setting up a list of mocked games, with the first four having specific values
            var dummyGames = new List<GameMock>
{
                new GameMock
                {
                    Title = "Death and Return of Superman, The",
                    Genres = new BlockingCollection<string> { "Beat' Em Up" },
                    PlayModes = new string[] {"Single Player"}
                },
                new GameMock
                {
                    Title = "Aladdin",
                    Genres = new BlockingCollection<string>{"Action", "Adventure"},
                    PlayModes = new string[] {"Single Player"}
                },
                new GameMock
                {
                    Title = "The Ghoul Patrol",
                    Genres = new BlockingCollection<string>(),
                    PlayModes = new string[] {"Cooperative", "Multiplayer"}
                },
                new GameMock
                {
                    Title = "Dragon View",
                    Genres = new BlockingCollection<string>{ "Action", "RPG" },
                    PlayModes = new string[] {"Single Player"}
                }
};

            // Adding 50 more games to the list
            dummyGames.AddRange(_fixture.CreateMany<GameMock>(50));

            return dummyGames.ToArray();
        }

        #endregion
    }
}
