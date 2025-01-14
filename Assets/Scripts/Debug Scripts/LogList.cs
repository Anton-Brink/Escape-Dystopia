using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LogList
{
    public static void LogItems<T>(List<T> list)
    {
        foreach (T item in list)
        {
            LogItem(item);
        }
    }

    // Method to log an individual item using reflection
    private static void LogItem<T>(T item)
    {
        if (item == null)
        {
            Debug.Log("null");
            return;
        }

        Type itemType = item.GetType();

        // Check if the item is a primitive type or a string
        if (itemType.IsPrimitive || itemType == typeof(string))
        {
            Debug.Log(item.ToString());
        }
        else
        {
            // Use reflection to log properties and fields
            FieldInfo[] fields = itemType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string logMessage = itemType.Name + " { ";

            foreach (FieldInfo field in fields)
            {
                logMessage += field.Name + ": " + field.GetValue(item) + ", ";
            }

            foreach (PropertyInfo property in properties)
            {
                logMessage += property.Name + ": " + property.GetValue(item, null) + ", ";
            }

            logMessage = logMessage.TrimEnd(',', ' ') + " }";
            Debug.Log(logMessage);
        }
    }
}