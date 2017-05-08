using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

// JSON
using System.IO;
using SimpleJSON;

[Serializable]
public enum SHADER_TYPES
{
    STANDARD,
    TOON_SIMPLE,
    TOON_RAMP,
    TOON_BUMP
};

public class ShadersWindow : EditorWindow {

    private const float MARGIN_SIDES = 10f;
    private const float MARGIN_TOP   = 10f;

    [SerializeField] private SHADER_TYPES currentShader = SHADER_TYPES.STANDARD;

    [SerializeField] private LightSettings lightSettings = new LightSettings(SHADER_TYPES.STANDARD);
    [SerializeField] private PropertiesSettings propertiesSettings = new PropertiesSettings(SHADER_TYPES.STANDARD);

    [MenuItem("Window/Visualization %<")]
    public static void Init()
    {
        EditorWindow windowShaders = EditorWindow.GetWindow<ShadersWindow>("Visualization") as EditorWindow;
        windowShaders.Show();
    }

    void OnGUI()
    {
        Rect s1 = new Rect(MARGIN_SIDES, MARGIN_TOP, position.width - 2.0f * MARGIN_SIDES, position.height * (1.0f / 8.0f));
        Rect s2 = new Rect(MARGIN_SIDES, s1.position.y + s1.height + MARGIN_TOP, position.width - 2.0f * MARGIN_SIDES, position.height * (1.0f / 4.0f));
        Rect s3 = new Rect(MARGIN_SIDES, s2.position.y + s2.height + MARGIN_TOP, position.width - 2.0f * MARGIN_SIDES, position.height * (1.0f / 2.0f));

        // SHADER SECTION
        GUILayout.BeginArea(s1);

        GUILayout.Label("Change shader", EditorStyles.boldLabel);
        SHADER_TYPES newShader = (SHADER_TYPES)EditorGUILayout.EnumPopup(currentShader, new GUILayoutOption[] {});

        GUILayout.EndArea();
        // END SHADER SECTION


        if (GUI.changed && currentShader != newShader)
        { 
            currentShader = newShader;

            lightSettings.Modify(currentShader);
            propertiesSettings.Modify(currentShader);
        }


        // PROPERTIES SECTION
        GUILayout.BeginArea(s2);

        GUILayout.Label("Properties", EditorStyles.boldLabel);

        if (propertiesSettings.GetPropertiesInfo().Count > 0)
        {
            foreach (PropertyInfo property in propertiesSettings.GetPropertiesInfo())
            {
                propertiesSettings.SetFloat(property.pName, EditorGUILayout.Slider(new GUIContent(property.pName), propertiesSettings.GetFloat(property.pName), 0.0f, 25.0f, null));
                propertiesSettings.Apply();
            }
        }
        else
        {
            GUILayout.Label("No Properties", EditorStyles.centeredGreyMiniLabel);
        }

        GUILayout.EndArea();
        // END PROPERTIES SECTION


        // LIGHT SETTINGS SECTION
        GUILayout.BeginArea(s3);

        GUILayout.Label("Light Settings", EditorStyles.boldLabel);
        GUILayout.Label("Directional lights", EditorStyles.centeredGreyMiniLabel);
        lightSettings.directionalIntensity = EditorGUILayout.Slider(new GUIContent("Intensity"), lightSettings.directionalIntensity, 0.0f, 20.0f, null);
        GUILayout.Label("Spot lights", EditorStyles.centeredGreyMiniLabel);
        lightSettings.spotIntensity  = EditorGUILayout.Slider(new GUIContent("Intensity"), lightSettings.spotIntensity, 0.0f, 20.0f, null);
        GUILayout.Label("Point lights", EditorStyles.centeredGreyMiniLabel);
        lightSettings.pointIntensity = EditorGUILayout.Slider(new GUIContent("Intensity"), lightSettings.pointIntensity, 0.0f, 20.0f, null);
        lightSettings.pointRange     = EditorGUILayout.Slider(new GUIContent("  Range  "), lightSettings.pointRange, 0.0f, 25.0f, null);

        lightSettings.Apply();

        GUILayout.EndArea();
        // END LIGHT SETTINGS SECTION
    }

    void OnEnable()
    {
        hideFlags = HideFlags.HideAndDontSave;

        if (lightSettings == null)
        {
            lightSettings = new LightSettings(currentShader);
            lightSettings.Apply();
        }

        // TODO Now the values reset between edit/play mode because 
        // there is no way to serialize the floatProperties dictionary
        propertiesSettings = new PropertiesSettings(currentShader);
        propertiesSettings.Apply();
    }
}

[Serializable]
public class LightSettings
{
    private const float MIN_LIGHT_INTENSITY = 0.0f;
    private const float MAX_LIGHT_INTENSITY = 20.0f;
    private const float MIN_LIGHT_RANGE = 0.0f;
    private const float MAX_LIGHT_RANGE = 50.0f;

    SHADER_TYPES currentShader;    

    [SerializeField] public float directionalIntensity { get; set; }
    [SerializeField] public float pointIntensity       { get; set; }
    [SerializeField] public float pointRange           { get; set; }
    [SerializeField] public float spotIntensity        { get; set; }

    public LightSettings()
    {
        directionalIntensity = 0.0f;
        pointIntensity       = 0.0f;
        pointRange           = 0.0f;
        spotIntensity        = 0.0f;

        currentShader = SHADER_TYPES.STANDARD;
    }

    public LightSettings(SHADER_TYPES shaderType)
    {
        currentShader = shaderType;

        Reset();
    }

    public void Modify(SHADER_TYPES shaderType)
    {
        if (shaderType == currentShader)
            return;

        SetValues(shaderType);

        currentShader = shaderType;
    }

    public void Reset()
    {
        SetValues(currentShader);
    }

    public void Apply()
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        for (int i = 0; i < lights.Length; i++)
        {
            switch (lights[i].type)
            {
                case LightType.Directional:
                    lights[i].intensity = directionalIntensity;
                    break;

                case LightType.Spot:
                    lights[i].intensity = spotIntensity;
                    break;
                
                case LightType.Point:
                    lights[i].intensity = pointIntensity;
                    lights[i].range = pointRange;
                    break;
            }

        }
    }

    private void SetValues(SHADER_TYPES shaderType)
    {
        JSONNode node = UtilsShaders.GetShaderInfo(shaderType);

        if (node != null)
        {
            JSONNode lightSettings = node["LightSettings"];

            if (lightSettings != null)
            {
                directionalIntensity = Mathf.Clamp(lightSettings["Directional"]["Intensity"].AsFloat, MIN_LIGHT_INTENSITY, MAX_LIGHT_INTENSITY);
                spotIntensity = Mathf.Clamp(lightSettings["Spot"]["Intensity"].AsFloat, MIN_LIGHT_INTENSITY, MAX_LIGHT_INTENSITY);
                pointIntensity = Mathf.Clamp(lightSettings["Point"]["Intensity"].AsFloat, MIN_LIGHT_INTENSITY, MAX_LIGHT_INTENSITY);
                pointRange = Mathf.Clamp(lightSettings["Point"]["Range"].AsFloat, MIN_LIGHT_RANGE, MAX_LIGHT_RANGE);
            }
        }
    }

}


[Serializable] public class DictionaryStrFlt : SerializableDictionary<string,float> {}

[Serializable] public class ListPropertyInfo : List<PropertyInfo> {}


[Serializable]
public class PropertyInfo
{
    public string pName;
    public Type pType;
};

[Serializable]
public class PropertiesSettings {

    [SerializeField]
    private SHADER_TYPES currentShader;
    
    [SerializeField]
    private ListPropertyInfo propertiesInfo;

    [SerializeField]
    private DictionaryStrFlt floatProperties;


    public PropertiesSettings()
    {
        propertiesInfo  = new ListPropertyInfo();
        floatProperties = new DictionaryStrFlt();

        currentShader = SHADER_TYPES.STANDARD;
    }

    public PropertiesSettings(SHADER_TYPES shaderType)
    {
        propertiesInfo = new ListPropertyInfo();
        floatProperties = new DictionaryStrFlt();

        currentShader = shaderType;

        SetValues(currentShader);
    }

    // TODO: GENERALIZE
    public float GetFloat(string propertyName)
    {
        if (!floatProperties.ContainsKey(propertyName))
            throw new System.Exception("Cannot get property: " + propertyName + " is not a valid property name (NOT FOUND)");

        return floatProperties[propertyName];
    }

    public void SetFloat(string propertyName, float val)
    {
        if (floatProperties.ContainsKey(propertyName)) {
            floatProperties[propertyName] = val;
        } else {
            floatProperties.Add(propertyName, val);
        }
    }

    public void Modify(SHADER_TYPES shaderType)
    {
        if (shaderType == currentShader)
            return;

        currentShader = shaderType;

        propertiesInfo.Clear();
        floatProperties.Clear();

        SetValues(currentShader);
        ApplyMaterialChange();
    }

    public void Reset()
    {
        SetValues(currentShader);
    }

    public List<PropertyInfo> GetPropertiesInfo()
    {
        return propertiesInfo;
    }

    private void SetValues(SHADER_TYPES shaderType)
    {
        JSONNode node = UtilsShaders.GetShaderInfo(shaderType);

        if (node != null)
        {
            JSONNode properties = node["Properties"];

            if (properties != null)
            {
                foreach (JSONNode property in properties.AsArray)
                {
                    SetFloat(property["Name"], property["DefaultValue"].AsFloat);

                    PropertyInfo info = new PropertyInfo();
                    info.pName = property["Name"];
                    info.pType = Type.GetType(property["Type"]);

                    propertiesInfo.Add(info);
                }
            }
        }
    }

    private void ApplyMaterialChange()
    {
        JSONNode shader = UtilsShaders.GetShaderInfo(currentShader);

        string newMatPath = shader["MaterialFolder"];
        string generalPath = "Materials/general/";
        string excludedPath = "Materials/excluded/";

        Shader newShader = Shader.Find(shader["ShaderName"]);

        List<string> matNames = UtilsResources.GetNamesOfResources<Material>(newMatPath);
        List<string> excludedNames = UtilsResources.GetNamesOfResources<Material>(excludedPath);

        List<GameObject> gameobjects = new List<GameObject>(GameObject.FindObjectsOfType<GameObject>());

        string pathToMaterial = "";

        for (int i = 0; i < gameobjects.Count; i++)
        {
            Renderer meshRenderer;

            if (gameobjects[i].activeInHierarchy && (meshRenderer = gameobjects[i].GetComponent<Renderer>()))
            {
                Material[] materials = meshRenderer.sharedMaterials;

                List<Material> tempMaterials = new List<Material>();

                for (int j = 0; j < materials.Length; j++)
                {
                    if (!materials[j] || excludedNames.IndexOf(materials[j].name) != -1) continue;

                    pathToMaterial = newMatPath + materials[j].name;

                    if (matNames.IndexOf(materials[j].name) != -1) // If the material name is in the folder
                    {
                        Material m = Resources.Load(pathToMaterial) as Material;

                        if (m)
                        {
                            m.name = materials[j].name;

                            gameobjects[i].GetComponent<Renderer>().sharedMaterial = new Material(m);
                        }
                        else continue;
                    }
                    else
                    {
                        pathToMaterial = generalPath + materials[j].name;

                        Material m = Resources.Load(pathToMaterial) as Material;

                        if (m)
                        {
                            m.shader = newShader;

                            foreach (JSONNode texture in shader["Textures"].AsArray)
                            {
                                m = UtilsResources.AddTextureToMaterial(m, texture["PropertyName"], texture["Subfolder"], texture["Name"], texture["HasUnique"].AsBool);
                            }

                            tempMaterials.Add(new Material(m));
                        }
                        else continue;
                        
                    }
                }

                if (tempMaterials.Count == 0) continue;

                gameobjects[i].GetComponent<Renderer>().sharedMaterials = tempMaterials.ToArray();
            }
        }

        Terrain t = GameObject.Find("Terrain").GetComponent<Terrain>();

        t.materialType = Terrain.MaterialType.Custom;
        t.materialTemplate = Resources.Load(newMatPath + "terrain") as Material;
    }

    public void Apply()
    {
        List<string> generalNames = UtilsResources.GetNamesOfResources<Material>("Materials/general/");

        List<GameObject> gameobjects = new List<GameObject>(GameObject.FindObjectsOfType<GameObject>());

        for (int i = 0; i < gameobjects.Count; i++)
        {
            Renderer meshRenderer;

            if (gameobjects[i].activeInHierarchy && (meshRenderer = gameobjects[i].GetComponent<Renderer>()))
            {
                Material[] materials = meshRenderer.sharedMaterials;

                List<Material> tempMaterials = new List<Material>();

                for (int j = 0; j < materials.Length; j++)
                {
                    if (!materials[j] || generalNames.IndexOf(materials[j].name) == -1) continue;

                    var e = floatProperties.GetEnumerator();
                    
                    while (e.MoveNext())
                    {
                        materials[j].SetFloat(e.Current.Key, e.Current.Value);
                    }
                }
            }
        }

    }

}


class UtilsShaders {

    public static JSONNode GetShaderInfo(SHADER_TYPES shaderType)
    {
        string txt;

        FileStream fstream = new FileStream("Assets/Resources/JSON/" + shaderType.ToString().ToLower() + ".json", FileMode.Open, FileAccess.Read);

        using (var streamReader = new StreamReader(fstream, System.Text.Encoding.UTF8))
        {
            txt = streamReader.ReadToEnd();
        }

        return JSON.Parse(txt);
    }

}

class UtilsResources
{
    public static Material AddTextureToMaterial(Material mat, string propertyName, string subfolder, string texName, bool isUnique)
    {
        if (!isUnique)
            texName = mat.name + ((texName == null) ? "" : ("_" + texName));

        Texture tex = Resources.Load("Textures/" + ((subfolder == null) ? "" : (subfolder + "/")) + texName.ToLower()) as Texture;

        if (tex)
        {
            mat.SetTexture(propertyName, tex);
        }

        return mat;
    }

    public static List<string> GetNamesOfResources<T>(string path) where T : UnityEngine.Object
    {
        List<string> names = new List<string>();

        T[] components = Resources.LoadAll<T>(path);

        foreach (T t in components)
        {
            names.Add(t.name);
        }

        return names;
    }
}


[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }
}