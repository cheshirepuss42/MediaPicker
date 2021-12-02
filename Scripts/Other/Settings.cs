using System.Collections.Generic;

using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace RandomFilePlayer
{
    public static class Log
    {
        public static void Write(string txt, string filename = "bla.txt")
        {
            string path = Environment.CurrentDirectory + "/" + filename;
            File.AppendAllText(path, txt + Environment.NewLine);
        }
    }
    [Serializable]
    public class FolderCollection
    {
        public List<string> Folders;
        public FolderCollection(List<string> folders)
        {
            Folders = folders;
        }
        public string UIName()
        {
            string str = "";
            foreach (var folder in Folders)
            {
                str += Path.GetFileName(folder);
                if (Folders.IndexOf(folder) < Folders.Count - 1)
                {
                    str += ", ";
                }
            }
            return str;
        }
        public FolderCollection Clone()
        {
            var f = new List<string>();
            foreach (var folder in Folders)
            {
                f.Add(folder);
            }
            return new FolderCollection(f);
        }
    }
    [Serializable]
    public class Settings
    {
        public FolderCollection selectedFolders;
        public List<FolderCollection> SavedCollections;
        public bool filterAudio, filterVideo;
        public string LastPlayed;
        public int LastCollection;

        public Settings()
        {
            SavedCollections = new List<FolderCollection>();
            selectedFolders = new FolderCollection(new List<string>());
            filterAudio = false;
            filterVideo = true;
            LastPlayed = "";
            LastCollection = -1;
        }
        public void AddCollection(FolderCollection fc)
        {
            foreach (var sc in SavedCollections)
            {
                if (fc.UIName() == sc.UIName())
                {
                    return;
                }
            }
            SavedCollections.Add(fc);
            LastCollection = SavedCollections.Count - 1;
        }
    }

    public class SettingsManager
    {
        public static Settings Load()
        {
            return new Settings();
        }
        public static void Save(Settings s)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, s);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                // Properties.Settings.Default.data = Convert.ToBase64String(buffer);
                // Properties.Settings.Default.Save();
            }
        }

        public static Settings LoadFile()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SettingsManager.Path(),
                                      FileMode.OpenOrCreate,
                                      FileAccess.Read,
                                      FileShare.Read);
            Settings result;
            try
            {
                result = (Settings)formatter.Deserialize(stream);

            }
            catch (Exception e)
            {
                result = new RandomFilePlayer.Settings();
            }
            stream.Close();
            return result;
        }
        public static void SaveFile(Settings s)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(SettingsManager.Path(),
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, s);
            stream.Close();
        }
        public static string Path()
        {
            return System.IO.Path.GetTempPath() + "rfpdata.dat";
        }
    }
}
