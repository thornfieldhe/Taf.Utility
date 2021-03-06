// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRegValidateAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   正则验证属性基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text.RegularExpressions;
using Taf.Utility;

namespace System.ComponentModel.DataAnnotations{
    /// <summary>
    ///     The base reg validate attribute.
    /// </summary>
    public class RegValidateAttribute : ValidationAttribute{
        /// <summary>
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="properityName">属性名称，可以为空</param>
        /// <param name="message"></param>
        public RegValidateAttribute(string pattern, string message, string properityName){
            Pattern       = pattern;
            ErrorMessage  = message;
            ProperityName = properityName;
        }

        /// <summary>
        ///     Gets the pattern.
        /// </summary>
        protected string Pattern{ get; }

        /// <summary>
        ///     Gets the pattern.
        /// </summary>
        protected string ProperityName{ get; }

        /// <summary>
        ///     格式化错误消息
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string FormatErrorMessage(string name){
            if(ErrorMessage             == null
            && ErrorMessageResourceName == null){
                ErrorMessage = $"正则验证失败:{name}";
            }

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString);
        }

        /// <summary>
        ///     是否验证通过
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="validationContext">
        ///     The validation Context.
        /// </param>
        /// <returns>
        ///     The <see cref="ValidationResult" />.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            if(value.ToStr().IsEmpty()){
                return null;
            }

            return Regex.IsMatch(value.ToStr(), Pattern)
                ? null
                : new ValidationResult(FormatErrorMessage(ProperityName ?? string.Empty));
        }
    }
}
