using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;
namespace Things
{



        // http://answers.unity3d.com/questions/458207/copy-a-component-at-runtime.html
        // http://stackoverflow.com/questions/10261824/how-can-i-get-all-constants-of-a-type-by-reflection
        public class ComponentUtil
        {

            public static void  CopyComponent(Component original, GameObject destination)
            {
                System.Type type = original.GetType();
                Component copy = destination.AddComponent(type);
                // Copied fields can be restricted with BindingFlags
                System.Reflection.FieldInfo[] fields = type.GetFields(); 
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(copy, field.GetValue(original));
                }
               
            }

        }

}