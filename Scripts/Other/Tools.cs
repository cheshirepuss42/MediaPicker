// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.UI;

// namespace LastWord.Models
// {
//     [Serializable]
//     public static class Tools
//     {
//         public static int TrueRandom() { return (int)System.DateTime.Now.Ticks; }
//         public static int RandomInt() { return UnityEngine.Random.Range(int.MinValue, int.MaxValue); }
//         public static Button MakeButton(Transform transform, string name, Action onclick, bool showing = true)
//         {
//             var a = transform.Find(name).GetComponent<Button>();
//             a.onClick.AddListener(() => { onclick(); });
//             a.gameObject.SetActive(showing);
//             return a;
//         }

//         private static string getStringForHashCode(int hash)
//         {
//             hash -= "AAAAAAA".GetHashCode();
//             long longHash = (long)hash & 0xFFFFFFFFL;
//             char[] c = new char[7];
//             for (int i = 0; i < 7; i++)
//             {
//                 c[6 - i] = (char)('A' + (longHash % 31));
//                 longHash /= 31;
//             }
//             return new string(c);
//         }

//         public static void PutNextTo(GameObject middle, GameObject side, string ori = "above")
//         {
//             var mr = (middle.transform as RectTransform);
//             var sr = (side.transform as RectTransform);
//             var nx = mr.position.x;
//             var ny = mr.position.y;
//             var heightdiff = (mr.rect.height + sr.rect.height) / 2;
//             var widthdiff = (mr.rect.width + sr.rect.width) / 2;
//             switch (ori)
//             {
//                 case "above": side.transform.position = new Vector3(nx, ny + heightdiff, 0); break;
//                 case "under": side.transform.position = new Vector3(nx, ny - heightdiff, 0); break;
//                 case "left": side.transform.position = new Vector3(nx - widthdiff, ny, 0); break;
//                 case "right": side.transform.position = new Vector3(nx + widthdiff, ny, 0); break;
//             }
//         }

//         public static Sprite LoadSprite(string name, bool fromStreaming = false)
//         {
//             Texture2D tex;
//             if (fromStreaming)
//             {
//                 tex = new Texture2D(2, 2);
//                 tex.LoadImage(File.ReadAllBytes(Application.streamingAssetsPath + "/Images/" + name + ".png"));
//             }
//             else
//             {
//                 tex = Resources.Load<Texture2D>("Images/" + name);
//             }
//             return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
//         }

//         public static string LoadText(string filename)
//         {
//             string path = Application.streamingAssetsPath + "/" + filename;
//             var fi = new FileInfo(path);
//             if (fi != null && fi.Exists)
//             {
//                 return File.ReadAllText(path);
//             }
//             return "";
//         }

//         public static Color ChangeColorBrightness(Color color, float correctionFactor)
//         {
//             color.r *= correctionFactor;
//             color.g *= correctionFactor;
//             color.b *= correctionFactor;
//             return color;
//         }
//         public static string Capitalize(string s)
//         {
//             return s.Remove(1).ToUpper() + s.Substring(1);
//         }
//         public static List<string> Split(string list)
//         {
//             if (!string.IsNullOrEmpty(list))
//             {
//                 return list.Split(',').ToList();
//             }
//             else
//             {
//                 return new List<string>();
//             }
//         }

//         public static string RandomFromList(string str, string butnot = "")
//         {
//             return RandomFromList(Split(str), Split(butnot));
//         }

//         public static string RandomFromList(List<string> from, List<string> butnot)
//         {
//             var result = Exclude<string>(from, butnot);
//             if (result.Count > 0)
//             {
//                 return result[UnityEngine.Random.Range(0, result.Count)];
//             }
//             else
//             {
//                 Debug.Log("Too little items in :" + string.Join(",", from.ToArray()));
//                 return null;
//             }
//         }
//         public static string UniqueRandomFromList(string from, int amount = 1, string butnot = null)
//         {
//             return string.Join(",", UniqueRandomListFromList(from, amount, butnot));
//         }

//         public static List<string> UniqueRandomListFromList(string from, int amount = 1, string butnot = null)
//         {
//             var result = new List<string>();
//             for (int i = 0; i < amount; i++)
//             {
//                 var r = butnot == null ? result : result.Concat(Split(butnot)).ToList();
//                 result.Add(RandomFromList(Split(from), r));
//             }
//             return result;
//         }

//         public static List<string> UniqueRandomListFromList(List<string> from, int amount = 1, List<string> butnot = null)
//         {
//             var result = new List<string>();
//             for (int i = 0; i < amount; i++)
//             {
//                 var r = butnot == null ? result : result.Concat(butnot).ToList();
//                 result.Add(RandomFromList(from, r));
//             }
//             return result;
//         }

//         public static List<T> Overlap<T>(List<T> l1, List<T> l2)
//         {
//             var l = new List<T>();
//             foreach (var a in l1)
//             {
//                 foreach (var b in l2)
//                 {
//                     if (a.Equals(b))
//                     {
//                         l.Add(a);
//                     }
//                 }
//             }
//             return l;
//         }

//         public static List<T> Exclude<T>(List<T> l1, List<T> l2)
//         {
//             var l = new List<T>();
//             foreach (var a in l1)
//             {
//                 bool exc = false;
//                 foreach (var b in l2)
//                 {
//                     if (a.Equals(b))
//                     {
//                         exc = true;
//                     }
//                 }
//                 if (!exc)
//                 {
//                     l.Add(a);
//                 }
//             }
//             return l;
//         }

//         public static T RandomItem<T>(this IList<T> list)
//         {
//             int n = list.Count;
//             int k = UnityEngine.Random.Range(0, list.Count);
//             return list[k];
//         }

//         public static void Shuffle<T>(this IList<T> list)
//         {
//             int n = list.Count;
//             while (n > 1)
//             {
//                 n--;
//                 int k = UnityEngine.Random.Range(0, n + 1);
//                 T value = list[k];
//                 list[k] = list[n];
//                 list[n] = value;
//             }
//         }

//         public static T GetRandomEnumValue<T>() where T : Enum => (T)Enum.GetValues(typeof(T)).OfType<Enum>().OrderBy(_ => Guid.NewGuid()).FirstOrDefault();

//         public static void DestroyChildren(this Transform root)
//         {
//             int childCount = root.childCount;
//             for (int i = 0; i < childCount; i++)
//             {
//                 GameObject.DestroyImmediate(root.GetChild(0).gameObject);

//             }
//         }

//         public static void CenterIn(this Transform root)
//         {
//             root.MakeGridIn(1, 1, 0);
//         }

//         public static void MakeWordsIn(this Transform root, Phrase words)
//         {
//             List<int> lengths = new List<int>();
//             foreach (var w in words.Words)
//             {
//                 lengths.Add(w.Count);
//             }
//             root.MakeRowsIn(lengths);
//         }

//         public static void MakeRowsIn(this Transform root, List<int> rowlengths, int padding = 5, string align = "center")
//         {
//             var pos = root.position;
//             var rect = ((RectTransform)root).rect;
//             float centerx = pos.x + rect.center.x;
//             float centery = pos.y + rect.center.y;
//             int amRows = rowlengths.Count;
//             if (amRows > 0 && rowlengths[0] > 0)
//             {
//                 int currChild = 0;
//                 var template = (RectTransform)root.GetChild(currChild).transform;
//                 var itemw = (root.lossyScale.x * template.localScale.x * template.rect.width) + padding;
//                 var itemh = (root.lossyScale.y * template.localScale.y * template.rect.height) + padding;                     
//                 var totalheight = itemh * (amRows + 1);
//                 for (int i = 0; i < amRows; i++)
//                 {
//                     var totalwidth = itemw * (rowlengths[i] - 1);
//                     for (int j = 0; j < rowlengths[i]; j++)
//                     {
//                         var item = (RectTransform)root.GetChild(currChild).transform;
//                         var nx = (centerx - (totalwidth / 2)) + ((j) * itemw);
//                         var ny = (centery - (totalheight / 2)) + (((amRows) - i) * itemh);
//                         item.position = new Vector3(nx, ny, 0);
//                         currChild += 1;
//                     }
//                 }
//             }
//         }
        
//         public static void MakeGridIn(this Transform root, int maxWidth, int maxHeight, int padding = 5, string align = "center")
//         {
//             var pos = root.position;
//             var rect = ((RectTransform)root).rect;
//             int am = root.childCount;
//             float centerx = pos.x + rect.center.x;
//             float centery = pos.y + rect.center.y;
//             maxWidth = maxWidth == 0 ? am : maxWidth;
//             maxHeight = maxHeight == 0 ? am : maxHeight;
//             if (am > 0)
//             {

//                 var template = (RectTransform)root.GetChild(0).transform;              
//                 var itemw = (root.lossyScale.x * template.localScale.x * template.rect.width) + padding;
//                 var itemh = (root.lossyScale.y * template.localScale.y * template.rect.height) + padding;             
//                 var totalwidth = itemw * (maxWidth - 1);
//                 var totalheight = itemh * (maxHeight - 1);
//                 switch (align)
//                 {
//                     case "left": centerx = pos.x + rect.xMin + ((totalwidth + itemw) / 2); break;
//                     case "right": centerx = pos.x + rect.xMax - ((totalwidth + itemw) / 2); break;
//                     case "top": centery = pos.y + (rect.yMax + ((totalheight - itemh) / 2)); break;
//                     case "bottom": centery = pos.y + (rect.yMin - ((totalheight - itemh) / 2)); break;
//                     default: break;
//                 }
//                 for (int i = 0; i < maxHeight; i++)
//                 {
//                     for (int j = 0; j < maxWidth; j++)
//                     {
//                         var grx = (i * maxWidth) + j;
//                         if (grx < am)
//                         {
//                             var item = (RectTransform)root.GetChild(grx).transform;
//                             var nx = (centerx - (totalwidth / 2)) + ((j) * itemw);
//                             var ny = (centery - (totalheight / 2)) + (((maxHeight - 1) - i) * itemh);
//                             item.position = new Vector3(nx, ny, 0);
//                         }
//                     }
//                 }
//             }
//         }
  
//         public static void SetHeight(this RectTransform rt,  float height){
//             rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,  height);
//             rt.ForceUpdateRectTransforms(); //- needed before we adjust pivot a second time?

//         }          
//         public static void SetWidth(this RectTransform rt,  float width){
//             rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,  width);
//             rt.ForceUpdateRectTransforms(); //- needed before we adjust pivot a second time?
//         }              
//     }
// }

