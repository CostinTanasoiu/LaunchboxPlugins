using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchboxPluginsTests.MockedClasses
{
    public class GameMock : IGame
    {
        public GameMock()
        {
            AdditionalApplications = new List<IAdditionalApplication>();
            CustomFields = new List<ICustomField>();
        }

        public Image RatingImage => null;

        public string ScreenshotImagePath => null;

        public string FrontImagePath => null;

        public string MarqueeImagePath => null;

        public string BackImagePath => null;

        public string Box3DImagePath => null;

        public string BackgroundImagePath => null;

        public string Cart3DImagePath => null;

        public string CartFrontImagePath => null;

        public string CartBackImagePath => null;

        public string ClearLogoImagePath => null;

        public string DetailsWithPlatform => null;

        public string DetailsWithoutPlatform => null;

        public string PlatformClearLogoImagePath => null;

        public string ApplicationPath { get; set; }
        public string CommandLine { get; set; }
        public bool Completed { get; set; }
        public string ConfigurationCommandLine { get; set; }
        public string ConfigurationPath { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Developer { get; set; }
        public string DosBoxConfigurationPath { get; set; }
        public string EmulatorId { get; set; }
        public bool Favorite { get; set; }

        public string Id => Guid.NewGuid().ToString();

        public DateTime? LastPlayedDate { get; set; }
        public string ManualPath { get; set; }
        public string MusicPath { get; set; }
        public string Notes { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Rating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleaseYear { get; set; }
        public string RootFolder { get; set; }
        public bool ScummVmAspectCorrection { get; set; }
        public bool ScummVmFullscreen { get; set; }
        public string ScummVmGameDataFolderPath { get; set; }
        public string ScummVmGameType { get; set; }
        public bool ShowBack { get; set; }
        public string SortTitle { get; set; }
        public string Source { get; set; }
        public bool OverrideDefaultStartupScreenSettings { get; set; }
        public bool UseStartupScreen { get; set; }
        public bool HideAllNonExclusiveFullscreenWindows { get; set; }
        public int StartupLoadDelay { get; set; }
        public bool HideMouseCursorInGame { get; set; }
        public bool DisableShutdownScreen { get; set; }
        public bool AggressiveWindowHiding { get; set; }
        public int StarRating { get; set; }

        public float CommunityOrLocalStarRating => 0;

        public float StarRatingFloat { get; set; }
        public float CommunityStarRating { get; set; }
        public int CommunityStarRatingTotalVotes { get; set; }
        public string Status { get; set; }
        public int? LaunchBoxDbId { get; set; }
        public int? WikipediaId { get; set; }
        public string WikipediaUrl { get; set; }
        public string Title { get; set; }
        public bool UseDosBox { get; set; }
        public bool UseScummVm { get; set; }
        public string Version { get; set; }
        public string Series { get; set; }
        public string PlayMode { get; set; }
        public string Region { get; set; }
        public int PlayCount { get; set; }
        public bool Portable { get; set; }
        public string VideoPath { get; set; }
        public string ThemeVideoPath { get; set; }
        public bool Hide { get; set; }
        public bool Broken { get; set; }
        public string CloneOf { get; set; }
        public string GenresString { get; set; }

        public BlockingCollection<string> Genres { get; set; }

        public string[] PlayModes { get; set; }

        public string[] Developers => new string[] { };

        public string[] Publishers => new string[] { };

        public string[] SeriesValues => new string[] { };

        public string SortTitleOrTitle => Title;

        public List<ICustomField> CustomFields { get; set; }

        #region Additional Applications

        public List<IAdditionalApplication> AdditionalApplications { get; set; }
        public bool? Installed { get; set; }

        public IAdditionalApplication AddNewAdditionalApplication()
        {
            var app = new AdditionalApplicationMock();
            AdditionalApplications.Add(app);
            return app;
        }

        public IAdditionalApplication[] GetAllAdditionalApplications()
        {
            return AdditionalApplications.ToArray();
        }

        public bool TryRemoveAdditionalApplication(IAdditionalApplication additionalApplication)
        {
            return AdditionalApplications.Remove(additionalApplication);
        }

        #endregion

        public ICustomField AddNewCustomField()
        {
            throw new NotImplementedException();
        }

        public IMount AddNewMount()
        {
            throw new NotImplementedException();
        }

        public string Configure()
        {
            throw new NotImplementedException();
        }

        public ICustomField[] GetAllCustomFields()
        {
            return CustomFields.ToArray();
        }

        public ImageDetails[] GetAllImagesWithDetails()
        {
            throw new NotImplementedException();
        }

        public ImageDetails[] GetAllImagesWithDetails(string imageType)
        {
            throw new NotImplementedException();
        }

        public IMount[] GetAllMounts()
        {
            throw new NotImplementedException();
        }

        public string GetBigBoxDetails(bool showPlatform)
        {
            throw new NotImplementedException();
        }

        public string GetManualPath()
        {
            throw new NotImplementedException();
        }

        public string GetMusicPath()
        {
            throw new NotImplementedException();
        }

        public string GetNewManualFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewMusicFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewThemeVideoFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewVideoFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNextAvailableImageFilePath(string extension, string imageType, string region)
        {
            throw new NotImplementedException();
        }

        public string GetThemeVideoPath()
        {
            throw new NotImplementedException();
        }

        public string GetVideoPath(bool prioritizeThemeVideos = false)
        {
            throw new NotImplementedException();
        }

        public string OpenFolder()
        {
            throw new NotImplementedException();
        }

        public string OpenManual()
        {
            throw new NotImplementedException();
        }

        public string Play()
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveCustomField(ICustomField customField)
        {
            return CustomFields.Remove(customField);
        }

        public bool TryRemoveMount(IMount mount)
        {
            throw new NotImplementedException();
        }
    }
}
