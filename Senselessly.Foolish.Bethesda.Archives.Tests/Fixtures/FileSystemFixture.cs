namespace Senselessly.Foolish.Bethesda.Archives.Tests.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions.TestingHelpers;

    public sealed class FileSystemFixture : IDisposable
    {
        private const string ModRoot = "C:\\Mods";

        public FileSystemFixture()
        {
            var storage = new MockFileSystem(new Dictionary<string, MockFileData> {{ModRoot, new MockDirectoryData()}});
            Storage = storage;
        }

        public MockFileSystem Storage { get; private set; }

        public void Dispose()
        {
            Storage = null;
        }
    }
}