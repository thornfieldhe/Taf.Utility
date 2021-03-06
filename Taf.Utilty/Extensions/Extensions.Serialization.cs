// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Serialization.cs" company="">
//   
// </copyright>
// <summary>
//   序列化扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Taf.Utility{
    /// <summary>
    ///     The extensions.
    /// </summary>
    public partial class Extensions{
        /// <summary>
        ///     The default formatter type.
        /// </summary>
        private const FormatterType DefaultFormatterType = FormatterType.Binary;

        private static Dictionary<string, object> _Dic = new Dictionary<string, object>();

        /// <summary>
        ///     把对象序列化转换为字符串
        /// </summary>
        /// <param name="graph">
        /// </param>
        /// <param name="formatterType">
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string SerializeObjectToString(this object graph, FormatterType formatterType){
            using(var memoryStream = new MemoryStream()){
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, graph);
                var arrGraph = memoryStream.ToArray();
                return Convert.ToBase64String(arrGraph);
            }
        }

        /// <summary>
        ///     把对象序列化转换为字符串
        /// </summary>
        /// <param name="graph">
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string SerializeObjectToString(this object graph) =>
            SerializeObjectToString(graph, DefaultFormatterType);

        /// <summary>
        ///     把已序列化为字符串类型的对象反序列化为指定的类型
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="graph">
        /// </param>
        /// <param name="formatterType">
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T DeserializeStringToObject<T>(this string graph, FormatterType formatterType){
            var arrGraph = Convert.FromBase64String(graph);
            using(var memoryStream = new MemoryStream(arrGraph)){
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        ///     把已序列化为字符串类型的对象反序列化为指定的类型
        /// </summary>
        /// <param name="graph">
        ///     The graph.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T DeserializeStringToObject<T>(this string graph) =>
            DeserializeStringToObject<T>(graph, DefaultFormatterType);

        /// <summary>
        ///     深度克隆
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="t">
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T DeepCopy<T>(this T t){
            using(var stream = new MemoryStream()){
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, t);
                stream.Seek(0, SeekOrigin.Begin);
                var copy = (T)formatter.Deserialize(stream);
                stream.Close();
                return copy;
            }
        }
        /// <summary>
        /// 将源对象所有属性赋值给目标对象
        /// 确保目标对象的属性名称与原对象的属性名称一致，且目标对象属性数量可以少于原对象属性数量
        /// </summary>
        /// <param name="tIn"></param>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <returns></returns>
        public static TOut Copy<TIn, TOut>(this TIn tIn, params string[] skipProperties){
            var skipKey = skipProperties != null ? $"_{string.Join("_", skipProperties)}" : "";
            var key     = $"trans_exp_{typeof(TIn).FullName}_{typeof(TOut).FullName}_{skipKey}";
            if(!_Dic.ContainsKey(key)){
                var parameterExpression = Expression.Parameter(typeof(TIn), "p");
                var memberBindingList   = new List<MemberBinding>();

                foreach(var item in typeof(TOut).GetProperties()){
                    if(!item.CanWrite
                    || skipProperties.Contains(item.Name)){
                        continue;
                    }

                    var property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                var memberInitExpression =
                    Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                var lambda =
                    Expression.Lambda<Func<TIn, TOut>>(memberInitExpression
                                                     , new ParameterExpression[]{parameterExpression});
                var func = lambda.Compile();

                _Dic[key] = func;
            }

            return ((Func<TIn, TOut>) _Dic[key])(tIn);
        }

    #region Xml序列化

        /// <summary>
        ///     序列化实例到Xml
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="obj">
        ///     The obj.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public static string XMLSerializer<T>(this T obj){
            var stream = new MemoryStream();
            var xml    = new XmlSerializer(typeof(T));
            try{
                // 序列化对象
                xml.Serialize(stream, obj);
            } catch(InvalidOperationException ex){
                throw ex;
            }

            stream.Position = 0;
            var sr  = new StreamReader(stream);
            var str = sr.ReadToEnd();
            sr.Dispose();
            stream.Dispose();
            return str;
        }

        /// <summary>
        ///     反序列化Xml到实例
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="xml">
        /// </param>
        /// <returns>
        ///     The <see cref="T" />.
        /// </returns>
        public static T XMLDeserializeFromString<T>(this string xml){
            try{
                if(xml != null){
                    using(var sr = new StringReader(xml)){
                        var xmldes = new XmlSerializer(typeof(T));
                        return (T)xmldes.Deserialize(sr);
                    }
                }

                return default;
            } catch(Exception ex){
                throw ex;
            }
        }

    #endregion


    #region Json序列化

        /// <summary>
        ///     json反序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeSerializesFromString<T>(this string jsonString) where T : class{
            var serializer = new JsonSerializer();
            var sr         = new StringReader(jsonString);
            var o          = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            return o as T;
        }

        /// <summary>
        ///     json序列化（非二进制方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeToString<T>(this T t) => JsonConvert.SerializeObject(t);

    #endregion
    }

    /// <summary>
    ///     The formatter type.
    /// </summary>
    public enum FormatterType{
        /// <summary>
        ///     The soap.
        /// </summary>
        Soap,

        /// <summary>
        ///     The binary.
        /// </summary>
        Binary
    }
}
