namespace Senselessly.Foolish.Bethesda.Archives.Tests.Tests.Models.Files
{
    using System.Collections.Generic;
    using Archives.Models.Files;
    using Enums;
    using FluentAssertions;
    using Helpers.Fixtures;
    using Xunit;

    [Collection("PluginInit")]
    [Trait("Category", "PluginInit")]
    public class PluginInitTests
    {
        [Fact]
        public void Plugin_Accepts_FolderName()
        {
            const string expected = FileSystemFixture.ModRootSub01;
            var plugin = new Plugin(expected);
            plugin.FolderName.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Can_Set_Archives()
        {
            const ArchiveTypes expected = ArchiveTypes.Main & ArchiveTypes.Textures;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Archives = expected};
            plugin.Archives.Should().Be(expected);
        }

        [Fact]
        public void Plugin_Can_Set_Folders()
        {
            const string expected = "FoldersIsSet";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Folders = expected};
            plugin.Folders.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Can_Set_Files()
        {
            const string expected = "FilesIsSet";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Files = expected};
            plugin.Files.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Can_Set_Loose_Types()
        {
            const LooseTypes expected = LooseTypes.Meshes & LooseTypes.Materials & LooseTypes.Textures;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Loose = expected};
            plugin.Loose.Should().Be(expected);
        }

        [Fact]
        public void Plugin_Can_Set_Type_Dictionary()
        {
            const int expectedCount = 2;
            IDictionary<PluginTypes, int> expected = new Dictionary<PluginTypes, int> {
                {PluginTypes.Plugin, 1}, {PluginTypes.Light, 0}
            };
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {TypeDict = expected};
            plugin.TypeDict.Should()
                  .NotBeEmpty()
                  .And.HaveCount(expectedCount)
                  .And.SatisfyRespectively(result => result.Key.Should().Be(PluginTypes.Plugin),
                       result => result.Key.Should().Be(PluginTypes.Light));
        }

        [Fact]
        public void Plugin_Sets_ExtraFolders_None()
        {
            const string expected = "None";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            plugin.ExtraFolders.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_ExtraFolders()
        {
            const string expected = "FoldersIsSet";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Folders = expected};
            plugin.ExtraFolders.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_ExtraFiles_None()
        {
            const string expected = "None";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            plugin.ExtraFiles.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_ExtraFiles()
        {
            const string expected = "FilesIsSet";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {Files = expected};
            plugin.ExtraFiles.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_ModId_Found()
        {
            const int expected = 2;
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            plugin.ModId.Should<int>().Be(expected);
        }

        [Fact]
        public void Plugin_Sets_ModId_Missing()
        {
            const int expected = -1;
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            plugin.ModId.Should<int>().Be(expected);
        }

        [Fact]
        public void Plugin_Sets_ModName_Found()
        {
            const string expected = "Plugin Two";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            plugin.ModName.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_ModName_Missing_Folder_Name()
        {
            const string expected = "Plugin06";
            var plugin = new Plugin(FileSystemFixture.ModRootSub02);
            plugin.ModName.Should().Match(expected);
        }

        [Fact]
        public void Plugin_Sets_Types_None()
        {
            const string expected = "None";
            var plugin = new Plugin(FileSystemFixture.ModRootSub01);
            plugin.Types.Should().Be(expected);
        }

        [Fact]
        public void Plugin_Sets_Types()
        {
            const string expected = "Light: 0, Plugin: 1";
            IDictionary<PluginTypes, int> types = new Dictionary<PluginTypes, int> {
                {PluginTypes.Plugin, 1}, {PluginTypes.Light, 0}
            };
            var plugin = new Plugin(FileSystemFixture.ModRootSub01) {TypeDict = types};
            plugin.Types.Should().Match(expected);
        }
    }
}