using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SubtitleStyles : MonoBehaviour
{
    [SerializeField] private List<style> styles = new List<style>();
    private Dictionary<string, style> _styleMap = new Dictionary<string, style>();
    private style _defaultStyle;

    [System.Serializable]
    public struct style
    {
        public string key;
        public TMP_FontAsset font;
        public Color fontColor;
        public Color outlineColor;
    }

    private void Awake()
    {
        _defaultStyle = styles[0];
        foreach (var style in styles)
        {
            _styleMap[style.key] = style;
        }
    }

    public style GetStyle(string key)
    {
        if( _styleMap.ContainsKey(key))
        {
            return _styleMap[key];
        }
        return _defaultStyle;
    }

}
