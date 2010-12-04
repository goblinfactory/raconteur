using System.Collections.Generic;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raconteur;
using Raconteur.IDE;

namespace Features 
{
    public partial class UserSettings
    {
        readonly Project Project = Create.TestObjectFor<Project>();

        readonly List<dynamic> backup = new List<dynamic>();

        [TestInitialize]
        public void SetUp()
        {
            backup.Add(Settings.XUnit);    
            backup.Add(Languages.Current);    
        }

        void Given_the_settings(string Settings)
        {
            Given.That(Project).HasSettingsFile.Is(true);
            Given.That(Project).SettingsFileContent.Is(Settings.ToLower());
        }

        void When_the_project_is_loaded()
        {
            Project.Load();
        }

        void The_Settings_should_be_(string xUnit, string Language)
        {
            Settings.XUnit.ShouldBe(xUnit.ToUpper());
            Languages.Current.Name.ShouldBe(Language);
        }

        [TestCleanup]
        public void TearDown()
        {
            Settings.XUnit = backup[0];
            Languages.Current = backup[1];
        }
    }
}
