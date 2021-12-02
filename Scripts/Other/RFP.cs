using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RandomFilePlayer
{
    public class RFP
    {
        public Settings settings;
        public List<string> FoundFiles, filters, history;
        public string LastFolder, CurrentFile;
        public RFP()
        {
            settings = SettingsManager.Load();
            FoundFiles = new List<string>();
            history = new List<string>();
            setFilter(false, true);
            MakeNewList();
            if (settings.LastPlayed != null)
            {
                setLastPlayed(settings.LastPlayed);
            }
        }

        public List<string> getFiles(string folder, bool recurse = true)
        {
            List<string> list = new List<string>();
            if (recurse)
            {
                foreach (string d in Directory.GetDirectories(folder))
                {
                    list.AddRange(getFiles(d, recurse));
                }
            }
            list.AddRange(Directory.GetFiles(folder));
            return list;
        }

        public string Next()
        {
            var index = FoundFiles.IndexOf(CurrentFile) + 1;
            index = index > FoundFiles.Count ? 0 : index;
            return FoundFiles[index];
        }
        public string Prev()
        {
            var index = FoundFiles.IndexOf(CurrentFile) - 1;
            index = index < 0 ? FoundFiles.Count : index;
            return FoundFiles[index];
        }
        public void setFilter(bool audio, bool video)
        {
            var audioFiles = new List<string> { "mp3", "ogg", "wav" };
            var videoFiles = new List<string> { "avi", "mpg", "mpeg", "mp4", "mkv" };
            filters = new List<string>();
            if (audio)
            {
                filters.AddRange(audioFiles);
            }
            if (video)
            {
                filters.AddRange(videoFiles);
            }
        }

        public void SaveSettings(bool newList = false)
        {
            SettingsManager.Save(settings);
            if (newList)
            {
                MakeNewList();
            }
        }

        public void MakeNewList()
        {
            setFilter(settings.filterAudio, settings.filterVideo);
            FoundFiles.Clear();
            try
            {
                for (var i = settings.selectedFolders.Folders.Count - 1; i >= 0; i--)
                {
                    var files = getFiles(settings.selectedFolders.Folders[i]);
                    if (files.Count > 0)
                    {
                        FoundFiles.AddRange(filterFiles(files));
                    }
                    else
                    {
                        settings.selectedFolders.Folders.RemoveAt(i);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public List<string> filterFiles(List<string> fileList)
        {
            var filtered = fileList.Where(x => filters.Any(x.ToLower().EndsWith)).ToList();// ToList();
            if (filtered.Count == 0)
            {
                filtered = history;
            }
            return filtered;
        }

        public string GetRandom()
        {
            FoundFiles = filterFiles(FoundFiles);
            return FoundFiles[new Random().Next(FoundFiles.Count)];
        }

        public string GetRandomFromFolder(string folder)
        {
            Random rnd = new Random();
            List<string> files = filterFiles(getFiles(folder));
            return files[rnd.Next(files.Count)];
        }

        public void Play(string file, bool history = true)
        {
            Process.Start(file);
            setLastPlayed(file, history);
        }

        public void setLastPlayed(string file, bool addToHistory = true)
        {
            if (addToHistory)
            {
                history.Insert(0, file);
            }
            CurrentFile = file;
            settings.LastPlayed = file;
            SaveSettings();
        }

        public void OpenCurrentFolder()
        {
            Process.Start(Path.GetDirectoryName(CurrentFile));
        }

        public void SaveCurrentCollection()
        {
            settings.AddCollection(settings.selectedFolders.Clone());
            SaveSettings();
        }

        public void ReadCollection(int index)
        {
            settings.selectedFolders.Folders.Clear();
            settings.selectedFolders = settings.SavedCollections[index].Clone();
            settings.LastCollection = index;
            SaveSettings(true);
        }

        public void SetTypeFilter(bool video, bool audio)
        {
            settings.filterAudio = audio;
            settings.filterVideo = video;
            SaveSettings(true);
        }

        public void AddFolderToCurrentCollection(string path)
        {
            if (!settings.selectedFolders.Folders.Contains(path))
            {
                LastFolder = path;
                settings.selectedFolders.Folders.Add(path);
                SaveSettings(true);
            }
        }

        public void RemoveCollection(int index)
        {
            settings.SavedCollections.RemoveAt(index);
            SaveSettings();
        }
        public string listInfo()
        {
            return "\n(" + FoundFiles.IndexOf(CurrentFile).ToString() + "/" + FoundFiles.Count.ToString() + ")";
        }
        public Dictionary<string, string> SearchResult(string key)
        {
            var a = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(key))
            {
                foreach (string file in FoundFiles)
                {
                    if (file.ToLower().Contains(key.ToLower()))
                    {
                        a.Add(file, Path.GetFileName(file));
                    }
                }
            }
            if (a.Count == 0)
            {
                a.Add("", "");
            }
            return a;
        }
    }
}
