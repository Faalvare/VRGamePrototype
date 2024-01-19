using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for any stat, it contains modifiers and delegates that invokes when modifiers are added or removed
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;
    private Dictionary<string, float> modifiers = new Dictionary<string, float>();

    //Delegates
    public delegate void OnModifierAdded(string modifierKey,float modifierValue);
    public OnModifierAdded onModifierAdded;
    public delegate void OnModifierChanged(string modifierKey, float modifierValue);
    public OnModifierChanged onModifierChanged;
    public delegate void OnModifierRemoved(string modifierKey);
    public OnModifierRemoved onModifierRemoved;
    public delegate void OnBaseValueChanged(float newValue);
    public OnBaseValueChanged onBaseValueChanged;

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }

    /// <summary>
    /// Gets the stat final value, after all modifiers
    /// </summary>
    /// <returns>The final value</returns>
    public float GetValue()
    {

        float finalValue = baseValue;
        foreach (KeyValuePair<string, float> mod in modifiers)
        {
            finalValue += mod.Value;
        }
        return finalValue;
    }

    /// <summary>
    /// Sets the base value, before all modifiers
    /// </summary>
    /// <param name="val">The new value</param>
    public void SetBaseValue(float val)
    {
        baseValue = val;
        onBaseValueChanged?.Invoke(val);
    }

    /// <summary>
    /// Adds a modifier to the stat
    /// </summary>
    /// <param name="modifier"></param>
    /// <returns>If it was added or not</returns>
    public bool AddModifier(string modifierKey,float modifierValue)
    {
        if (!modifiers.ContainsKey(modifierKey))
        {
            modifiers.Add(modifierKey, modifierValue);
            onModifierAdded?.Invoke(modifierKey,modifierValue);
            return true;
        }
        else
        {
            Debug.LogErrorFormat("Could not add the modifier {0}, it already exists", modifierKey);
            return false;
        }
    }

    /// <summary>
    /// Removes a modifier
    /// </summary>
    /// <param name="modifierKey">The key of the modifier to remove</param>
    /// <returns>Was it removed?</returns>
    public bool RemoveModifier(string modifierKey)
    {
        if (modifiers.ContainsKey(modifierKey))
        {
            modifiers.Remove(modifierKey);
            onModifierRemoved?.Invoke(modifierKey);
            return true;
        }
        else
        {
            Debug.LogErrorFormat("Could not remove {0}, it doesn't exists", modifierKey);
            return false;
        }
    }

    /// <summary>
    /// Changes a modifier
    /// </summary>
    /// <param name="modifier">The key of the modifier to change</param>
    /// <returns>Was it able to remove it?</returns>
    public bool ChangeModifier(string modifierKey, float modifierValue)
    {
        if (modifiers.ContainsKey(modifierKey))
        {
            modifiers[modifierKey] = modifierValue;
            onModifierChanged?.Invoke(modifierKey,modifierValue);
            return true;
        }
        else
        {
            Debug.LogErrorFormat("Could not change the modifier {0}, it already exists", modifierKey);
            return false;
        }
    }

    /// <summary>
    /// Determines whether the stat contains the specified modifier key.
    /// </summary>
    /// <param name="modifierKey"></param>
    /// <returns></returns>
    public bool ContainsModifier(string modifierKey)
    {
        return modifiers.ContainsKey(modifierKey);
    }
}
