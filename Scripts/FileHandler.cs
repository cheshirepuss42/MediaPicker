using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileHandler
{
    public List<string> VideoExtensions = new List<string> { "avi", "mpg", "mpeg", "mp4", "mkv" };
    public string DataPath = "user://mediapicker.data";   

    public List<string> GetFiles(string folder, bool recurse = true)
    {
        List<string> list = new List<string>();
        if (recurse)
        {
            foreach (string d in Directory.GetDirectories(folder))
            {
                list.AddRange(GetFiles(d, recurse));
            }
        }
        foreach (var filepath in Directory.GetFiles(folder))
        {
            bool filtered = true;
            foreach (var suffix in VideoExtensions)
            {
                if (filepath.EndsWith(suffix))
                {
                    filtered = false;
                }
            }
            if (!filtered)
            {
                list.Add(filepath);
            }
        }
        return list;
    }

    public void SaveSettings(MediaPickerSettings settings)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(DataPath);
        bf.Serialize(file, settings);
        file.Close();
    }

    public MediaPickerSettings LoadSettings()
    {
        MediaPickerSettings settings;
        if (File.Exists(DataPath))
        { 
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(DataPath, FileMode.Open);
            settings = (MediaPickerSettings)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            settings = new MediaPickerSettings();
        }
        return settings;
    }     
}