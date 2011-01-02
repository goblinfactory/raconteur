using System.Collections.Generic;
using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_grouping_tests
    {
        [Test]
        public void should_include_tags_in_definitions()
        {
            var Sut = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Tags

                    @tag
                    Scenario: With Tag
                "
            };

            Sut.ScenarioDefinitions[0]
                .Count.ShouldBe(2);
        }

        [Test]
        public void should_include_every_line_after_the_first_tag()
        {
            var Sut = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Tags

                    @tag
                    line
                    line
                    Scenario: With Tag
                "
            };

            Sut.ScenarioDefinitions[0]
                .Count.ShouldBe(4);
        }

        [Test]
        public void should_read_tags_of_Scenarios()
        {
            var Sut = new ScenarioParserClass();

            Sut.ScenarioFrom(new List<string>
            {
                "@a_tag dud @another_tag",
                "@a_tag dud",
                "Scenario: With Tag"
            })
            .Tags.ShouldBe("a_tag", "another_tag");
        }

        readonly Scenario Scenario = new Scenario { Tags = { "tag" } };

        [Test]
        public void should_add_Scenario_Tags_to_tests()
        {
            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[TestCategory(""tag"")]");
        }

        [Test]
        public void should_use_propper_xUnit_attribute()
        {
            var backup = Settings.XUnit;
            Settings.XUnit = XUnits.Framework["mbunit"];

            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[Category(""tag"")]");

            Settings.XUnit = backup;
        }
    }
}