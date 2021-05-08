namespace Senselessly.Foolish.ModTrakt.Wpf.UI.Modules.ModList
{
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Threading.Tasks;
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

        private async Task FindPlugins(FileSystemInfo path)
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
                    modSource.TypeDict = new Dictionary<PluginTypes, int>();
                    if (esl > 0) modSource.TypeDict.Add(key: PluginTypes.Light, value: esl);

                    if (esm > 0) modSource.TypeDict.Add(key: PluginTypes.Master, value: esm);

                    if (esp > 0) modSource.TypeDict.Add(key: PluginTypes.Plugin, value: esp);
                }

                files = files.Where(fi => !new[] {
                                  ".esl",
                                  ".esm",
                                  ".esp"
                              }.Contains(fi.Extension.ToLowerInvariant()))
                             .ToArray();
                var archives = ArchiveTypes.None;
                files.Where(fi => fi.Extension.ToLowerInvariant().Equals(".ba2"))
                     .ToList()
                     .ForEach(fi => {
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("main.ba2")
                                          ? ArchiveTypes.Main
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("materials.ba2")
                                          ? ArchiveTypes.Materials
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("meshes.ba2")
                                          ? ArchiveTypes.Meshes
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("interface.ba2")
                                          ? ArchiveTypes.Interface
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("scripts.ba2")
                                          ? ArchiveTypes.Scripts
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("sound.ba2")
                                          ? ArchiveTypes.Sound
                                          : ArchiveTypes.None;
                          archives ^= fi.Name.ToLowerInvariant().EndsWith("textures.ba2")
                                          ? ArchiveTypes.Textures
                                          : ArchiveTypes.None;
                      });
                modSource.Archives = archives;
                files = files.Where(fi => !fi.Extension.ToLowerInvariant().Equals(".ba2")).ToArray();
                modSource.Files = string.Join(separator: ", ", values: files.Select(fi => $"{fi.Name}"));
                var loose = LooseTypes.None;
                folders.ToList()
                       .ForEach(fo => {
                            loose ^= fo.Name.ToLowerInvariant().Equals("aaf") ? LooseTypes.Aaf : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("f4se") ? LooseTypes.F4Se : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("edit scripts")
                                         ? LooseTypes.Fo4Edit
                                         : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("interface")
                                         ? LooseTypes.Interface
                                         : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("materials")
                                         ? LooseTypes.Materials
                                         : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("music") ? LooseTypes.Music : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("meshes") ? LooseTypes.Meshes : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("mcm") ? LooseTypes.Mcm : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("scripts")
                                         ? LooseTypes.Scripts
                                         : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("sound") ? LooseTypes.Sound : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("textures")
                                         ? LooseTypes.Textures
                                         : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("tools") ? LooseTypes.Tools : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("video") ? LooseTypes.Video : LooseTypes.None;
                            loose ^= fo.Name.ToLowerInvariant().Equals("vis") ? LooseTypes.Vis : LooseTypes.None;
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