// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Int.cs" company="">
//   
// </copyright>
// <summary>
//   Int扩展
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Taf.Utility{
    /// <summary>
    ///     The extensions.
    /// </summary>
    public partial class Extensions{
        /// <summary>
        ///     是否在范围之间
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <param name="max">
        /// </param>
        /// <param name="min">
        /// </param>
        /// <param name="allowEqual">
        ///     是否包含等于
        /// </param>
        /// <returns>
        /// </returns>
        public static bool IsBetween(this int obj, int max, int min, bool allowEqual){
            if(allowEqual){
                return obj >= min && obj <= max;
            }

            return obj > min && obj < max;
        }

        /// <summary>
        ///     获取格式化字符串
        /// </summary>
        /// <param name="number">
        ///     数值
        /// </param>
        /// <param name="defaultValue">
        ///     空值显示的默认文本
        /// </param>
        /// <returns>
        /// </returns>
        public static string Format(this int number, string defaultValue = "") =>
            number == 0 ? defaultValue : number.ToString();

        /// <summary>
        ///     获取格式化字符串
        /// </summary>
        /// <param name="number">
        ///     数值
        /// </param>
        /// <param name="defaultValue">
        ///     空值显示的默认文本
        /// </param>
        /// <returns>
        /// </returns>
        public static string Format(this int? number, string defaultValue = "") =>
            Format(number.SafeValue(), defaultValue);
    }
}
