using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class JsonHandler
{
    private JSONNode DB;

    public JsonHandler(string[] files)
    {
        DB = LoadJSON(files[0]);
        for (int i = 1; i < files.Length; i++)
        {
            DB = CombineJSON(DB, LoadJSON(files[i]));
        }
    }

    private JSONNode LoadJSON(string name)
    {
        return JSON.Parse(LoadText("Data/" + name + ".json"));
    }
    public string LoadText(string path)
    {
        //string path = Application.streamingAssetsPath + "/" + filename;
        var fi = new FileInfo(path);
        if (fi != null && fi.Exists)
        {
            return File.ReadAllText(path);
        }
        return "";
    }
    private JSONNode CombineJSON(JSONNode node1, JSONNode node2)
    {
        foreach (var n in node2)
        {
            node1.Add(n.Key, n.Value);
        }
        return node1;
    }

    public JSONNode ByName(string name)
    {
        return DB[name];
    }

    public JSONNode FromPath(string path, string key = "")
    {
        var source = DB;
        var p = path.Split('/');
        var v = p[p.Length - 1];
        for (int i = 0; i < p.Length; i++)
        {
            if (key != "" && i == p.Length - 1)
            {
                foreach (var item in source.AsArray)
                {
                    if (item.Value[v] == key)
                    {
                        return item.Value;
                    }
                }
            }
            else
            {
                source = source[p[i]];
            }
        }
        return source;
    }

    public JSONNode ByID(string setName, string id)
    {
        foreach (var item in FromPath(setName).AsArray)
        {
            if (item.Value["id"] == id)
            {
                return item.Value;
            }
        }
        return null;
    }

    // public T EnumRead<T>(JSONNode node, T dflt) where T : System.Enum
    // {
    //     if (node == null)
    //     {
    //         return dflt;
    //     }
    //     else
    //     {
    //         return (T)Enum.Parse(typeof(T), node);
    //     }
    // }

    // public List<T> EnumListRead<T>(JSONNode node) where T : System.Enum
    // {
    //     var list = new List<T>();
    //     if (node != null)
    //     {
    //         foreach (var en in node.Value.Split(','))
    //         {
    //             if (en != "")
    //             {
    //                 list.Add((T)Enum.Parse(typeof(T), en));
    //             }
    //         }
    //     }
    //     return list;
    // }
}