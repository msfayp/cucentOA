using System;

using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
//using System.Web.UI.MobileControls.Adapters;

namespace WX.Tool
{
    /// <summary>
    ///EnumMapHelper 的摘要说明
    /// </summary>
    public class EnumMapHelper
    {
        // maps用于保存每种枚举及其对应的EnumMap对象
        private static Dictionary<Type, EnumMap> maps;

        // 由于C#中没有static indexer的概念，所以在这里我们用静态方法
        public static string GetStringFromEnum(Enum item)
        {
            if (maps == null)
            {
                maps = new Dictionary<Type, EnumMap>();
            }

            Type enumType = item.GetType();

            EnumMap mapper = null;
            if (maps.ContainsKey(enumType))
            {
                mapper = maps[enumType];
            }
            else
            {
                mapper = new EnumMap(enumType);
                maps.Add(enumType, mapper);
            }
            return mapper[item];
        }

        private class EnumMap
        {
            private Type internalEnumType;
            private Dictionary<Enum, string> map;

            public EnumMap(Type enumType)
            {
                if (!enumType.IsSubclassOf(typeof(Enum)))
                {
                    throw new InvalidCastException();
                }
                internalEnumType = enumType;
                FieldInfo[] staticFiles = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

                map = new Dictionary<Enum, string>(staticFiles.Length);

                for (int i = 0; i < staticFiles.Length; i++)
                {
                    if (staticFiles[i].FieldType == enumType)
                    {
                        string description = "";
                        object[] attrs = staticFiles[i].GetCustomAttributes(typeof(DescriptionAttribute), true);
                        description = attrs.Length > 0 ?
                            ((DescriptionAttribute)attrs[0]).Description :
                            //若没找到EnumItemDescription标记，则使用该枚举值的名字
                            description = staticFiles[i].Name;

                        map.Add((Enum)staticFiles[i].GetValue(enumType), description);
                    }
                }
            }

            public string this[Enum item]
            {
                get
                {
                    if (item.GetType() != internalEnumType)
                    {
                        throw new ArgumentException();
                    }
                    return map[item];
                }
            }
        }

    }
    //public class EnumItemDescriptionAttribute : DescriptionAttribute
    //{
    //    private bool replaced;

    //    public EnumItemDescriptionAttribute(string description)
    //        : base(description)
    //    {
    //    }
    //    public override string Description
    //    {
    //        get
    //        {
    //            if (!this.replaced)
    //            {
    //                this.replaced = true;
    //                base.DescriptionValue = SR.GetString(base.Description);
    //            }
    //            return base.Description;
    //        }
    //    }
    //}
}
