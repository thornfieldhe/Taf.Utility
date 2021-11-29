// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidRequiredAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   GUID必填
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Taf.Utility;

namespace System.ComponentModel.DataAnnotations{
    /// <summary>
    ///     The guid required attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GuidRequiredAttribute : DataTypeAttribute{
        private Guid Value;

        public GuidRequiredAttribute(string message) : this() => ErrorMessage = message;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GuidRequiredAttribute" /> class.
        /// </summary>
        public GuidRequiredAttribute()
            : base(DataType.Date){ }

        /// <summary>
        ///     The format error message.
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
                ErrorMessage = "值不能为空";
            }

            return base.FormatErrorMessage(name);
        }

        /// <summary>
        ///     The is valid.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public override bool IsValid(object value){
            if(value == null){
                return false;
            }

            Guid.TryParse(value.ToString(), out Value);
            return !Value.IsEmpty();
        }
    }
}
