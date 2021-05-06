namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Modules.ModList
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Bethesda.Archives.Enums;
    using Bethesda.Archives.Models.Files;
    using Shared;

    internal sealed class ModListViewModel : Module
    {
        private readonly IFileSystem _storage;

        private IEnumerable<Plugin> _modSources;

        public ModListViewModel() { }

        public ModListViewModel(IFileSystem storage) => _storage = storage;

        public IEnumerable<Plugin> Sources
        {
            get => _modSources;
            private set => SetProperty(field: ref _modSources, newValue: value);
        }

        private async void FindPlugins(FileSystemInfo path)
        {
            var pathInfo = _storage.DirectoryInfo.FromDirectoryName(path.FullName);
            var parent = pathInfo.GetDirectories();
            var sources = new List<Plugin>();
            await foreach (var folder in parent.OrderBy(folder => folder.Name).ToAsyncEnumerable())
            {
                var folders = folder.GetDirectories();
                var files = folder.GetFiles();
                var modSource = new Plugin(folder.Name);
                var esl = files.Count(fi => fi.Extension.ToLowerInvariant().EndsWith("esl"));
                var esm = files.Count(fi => fi.Extension.ToLowerInvariant().EndsWith("esm"));
                var esp = files.Count(fi => fi.Extension.ToLowerInvariant().EndsWith("esp"));
                if (esl + esm + esp > 0)
                {
                    modSource.TypeDict = new Dictionary<PluginType, int>();
                    if (esl > 0) modSource.TypeDict.Add(key: PluginType.Light, value: esl);

                    if (esm > 0) modSource.TypeDict.Add(key: PluginType.Master, value: esm);

                    if (esp > 0) modSource.TypeDict.Add(key: PluginType.Plugin, value: esp);
                }

                files = files.Where(fi => !new[] {
                                  ".esl",
                                  ".esm",
                                  ".esp"
                              }.Contains(fi.Extension.ToLowerInvariant()))
                             .ToArray();
                var archives = ArchiveType.None;
                files.Where(fi => fi.Extension.ToLowerInvariant().Equals(".ba2"))
                     .ToList()
                     .ForEach(fi => {
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("main.ba2")
                                          ? ArchiveType.Main
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("materials.ba2")
                                          ? ArchiveType.Materials
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("meshes.ba2")
                                          ? ArchiveType.Meshes
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("interface.ba2")
                                          ? ArchiveType.Interface
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("scripts.ba2")
                                          ? ArchiveType.Scripts
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("sound.ba2")
                                          ? ArchiveType.Sound
                                          : ArchiveType.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("textures.ba2")
                                          ? ArchiveType.Textures
                                          : ArchiveType.None;
                      });
                modSource.Archives = archives;
                files = files.Where(fi => !fi.Extension.ToLowerInvariant().Equals(".ba2")).ToArray();
                modSource.Files = string.Join(separator: ", ", values: files.Select(fi => $"{fi.Name}"));
                var loose = LooseType.None;
                folders.ToList()
                       .ForEach(fo => {
                            loose ^= fo.Name.ToLowerInvariant().Equals("aaf") ? LooseType.Aaf : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("f4se") ? LooseType.F4Se : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("edit scripts")
                                         ? LooseType.Fo4Edit
                                         : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("interface")
                                         ? LooseType.Interface
                                         : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("materials")
                                         ? LooseType.Materials
                                         : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("music") ? LooseType.Music : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("meshes") ? LooseType.Meshes : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("mcm") ? LooseType.Mcm : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("scripts") ? LooseType.Scripts : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("sound") ? LooseType.Sound : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("textures")
                                         ? LooseType.Textures
                                         : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("tools") ? LooseType.Tools : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("video") ? LooseType.Video : LooseType.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("vis") ? LooseType.Vis : LooseType.None;
                        });
                modSource.Loose = loose;
                folders = folders.Where(fo => !new[] {
                                      "aaf",
                                      "edit scripts",
                                      "f4se",
                                      "interface",
                                      "materials",
                                      "music",
                                      "meshes",
                                      "mcm",
                                      "scripts",
                                      "sound",
                                      "textures",
                                      "tools",
                                      "video",
                                      "vis"
                                  }.Contains(fo.Name.ToLowerInvariant()))
                                 .ToArray();
                modSource.Folders = string.Join(separator: ", ", values: folders.Select(fo => $"{fo.Name}"));
                sources.Add(modSource);
            }

            Sources = sources;
        }
    }
}