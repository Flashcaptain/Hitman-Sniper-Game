﻿﻿﻿﻿﻿﻿﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

public class XmlNodule : IEnumerable<XmlNodule>
{
    private XmlDocument _doc;
    private XmlNode _xml;

    public XmlNodule()
    {
    }

    public static XmlNodule Load(string file)
    {
        var returnNodule = new XmlNodule();
        returnNodule.LoadXml(Resources.Load<TextAsset>(file));

        return returnNodule;
    }

    public XmlNodule(XmlNode n)
    {
        _xml = n;
    }

    public XmlNodule Get(string nodeName)
    {
        return new XmlNodule(_xml.SelectSingleNode(nodeName));
    }

    public void Set(object value)
    {
        _xml.InnerText = value.ToString();
    }

    public void LoadXmlFile(string path)
    {
        _doc = new XmlDocument();
        _doc.Load(path);
        _xml = _doc.FirstChild;
    }

    public void LoadXml(TextAsset text)
    {
        _doc = new XmlDocument();
        _doc.LoadXml(text.text);
        _xml = _doc.FirstChild;
    }

    public void Save(string path)
    {
        _doc.Save(path);
    }

    private static string StripSpecialCharacters(string text)
    {
        return Regex.Replace(text, @"\s+", "");
    }

    public int ToInt()
    {
        return int.Parse(StripSpecialCharacters(_xml.InnerText));
    }

    public override string ToString()
    {
        return StripSpecialCharacters(_xml.InnerText);
    }

    public bool ToBool()
    {
        return bool.Parse(StripSpecialCharacters(_xml.InnerText));
    }

    public Vector3 ToVector3()
    {
        var x = float.Parse(StripSpecialCharacters(_xml.SelectSingleNode("x").InnerText));
        var y = float.Parse(StripSpecialCharacters(_xml.SelectSingleNode("y").InnerText));
        var z = float.Parse(StripSpecialCharacters(_xml.SelectSingleNode("z").InnerText));

        return new Vector3(x, y, z);
    }

    public string ToXml()
    {
        return _xml.InnerXml;
    }

    public IEnumerator<XmlNodule> GetEnumerator()
    {
        return (from XmlNode x in _xml select new XmlNodule(x)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   