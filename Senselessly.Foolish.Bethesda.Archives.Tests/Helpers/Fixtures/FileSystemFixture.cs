namespace Senselessly.Foolish.Bethesda.Archives.Tests.Helpers.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions.TestingHelpers;

    public sealed class FileSystemFixture : IDisposable
    {
        public const string ModRoot = "C:\\Mods\\All";
        public const string ModRootSub01 = "C:\\Mods\\All\\Plugin Two - 2 - 1-0-1";
        public const string ModRootSub02 = "C:\\Mods\\All\\Plugin06";
        public const string ModArchivesRoot = "C:\\Mods\\Archives";
        public const string ModEmptyRoot = "C:\\Mods\\None";
        public const string ModPluginsRoot = "C:\\Mods\\Plugins";
        public const string ModPlugin01 = "Plugin01.esm";
        public const string ModPlugin02 = "Plugin02.esl";
        public const string ModPlugin03 = "Plugin03.esl";
        public const string ModPlugin04 = "Plugin04.esp";
        public const string ModPlugin05 = "Plugin05.esp";
        public const string ModPlugin06 = "Plugin06.esp";
        public const string ModArchive01 = "Plugin01 - Main.ba2";
        public const string ModArchive02 = "Plugin01 - Sounds.ba2";
        public const string ModArchive03 = "Plugin01 - Textures.ba2";
        public const string ModArchive04 = "Plugin02 - Main.ba2";
        public const string ModArchive05 = "Plugin04 - Main.ba2";
        public const string ModArchive06 = "Plugin05 - Main.ba2";
        public const string ModArchive07 = "Plugin05 - Textures.ba2";
        public const string ModArchive08 = "Plugin06 - Textures.ba2";

        public FileSystemFixture()
        {
            var storage = new MockFileSystem(new Dictionary<string, MockFileData> {
                {ModRoot, new MockDirectoryData()},
                {ModArchivesRoot, new MockDirectoryData()},
                {ModEmptyRoot, new MockDirectoryData()},
                {ModPluginsRoot, new MockDirectoryData()},
                {ModRootSub01, new MockDirectoryData()},
                {ModRootSub02, new MockDirectoryData()},
                {$"{ModRoot}\\{ModPlugin01}", new MockFileData(ModPlugin01)},
                {$"{ModRootSub01}\\{ModPlugin02}", new MockFileData(ModPlugin02)},
                {$"{ModRoot}\\{ModPlugin03}", new MockFileData(ModPlugin03)},
                {$"{ModRoot}\\{ModPlugin04}", new MockFileData(ModPlugin04)},
                {$"{ModRoot}\\{ModPlugin05}", new MockFileData(ModPlugin05)},
                {$"{ModRootSub02}\\{ModPlugin06}", new MockFileData(ModPlugin06)},
                {$"{ModRoot}\\{ModArchive01}", new MockFileData(ModArchive01)},
                {$"{ModRoot}\\{ModArchive02}", new MockFileData(ModArchive02)},
                {$"{ModRoot}\\{ModArchive03}", new MockFileData(ModArchive03)},
                {$"{ModRootSub01}\\{ModArchive04}", new MockFileData(ModArchive04)},
                {$"{ModRoot}\\{ModArchive05}", new MockFileData(ModArchive05)},
                {$"{ModRoot}\\{ModArchive06}", new MockFileData(ModArchive06)},
                {$"{ModRootSub02}\\{ModArchive07}", new MockFileData(ModArchive07)},
                {$"{ModRoot}\\{ModArchive08}", new MockFileData(ModArchive08)},
                {$"{ModPluginsRoot}\\{ModPlugin01}", new MockFileData(ModPlugin01)},
                {$"{ModPluginsRoot}\\{ModPlugin02}", new MockFileData(ModPlugin02)},
                {$"{ModPluginsRoot}\\{ModPlugin04}", new MockFileData(ModPlugin04)},
                {$"{ModArchivesRoot}\\{ModArchive01}", new MockFileData(ModArchive01)},
                {$"{ModArchivesRoot}\\{ModArchive02}", new MockFileData(ModArchive02)},
                {$"{ModArchivesRoot}\\{ModArchive03}", new MockFileData(ModArchive03)}
            });
            Storage = storage;
        }

        public MockFileSystem Storage { get; private set; }

        public void Dispose()
        {
            Storage = null;
        }
    }
}