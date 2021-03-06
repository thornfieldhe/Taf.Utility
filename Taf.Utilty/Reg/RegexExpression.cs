// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexExpression.cs" company="">
//   
// </copyright>
// <summary>
//   正则匹配基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Taf.Utility{
    /// <summary>
    ///     正则匹配基类
    /// </summary>
    public class RegExpressionBase : IRegExpression{
        /// <summary>
        ///     The regex.
        /// </summary>
        protected Regex regex;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegExpressionBase" /> class.
        /// </summary>
        /// <param name="expression">
        ///     The expression.
        /// </param>
        public RegExpressionBase(string expression) =>
            regex = new Regex(expression, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        ///     是否匹配
        /// </summary>
        /// <param name="content">
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool IsMatch(string content) => regex.IsMatch(content);

        /// <summary>
        ///     解析表达式
        /// </summary>
        /// <param name="contex">
        /// </param>
        public void Evaluate(RegexContex contex){
            if(contex == null){
                throw new ArgumentNullException("contex");
            }

            switch(contex.Operator){
                case RegexOperator.Matches:
                    EvaluateMatch(contex);
                    break;
                case RegexOperator.Replace:
                    EvaluateReplace(contex);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        ///     通过Match方式解析表达式
        /// </summary>
        /// <param name="context">
        /// </param>
        protected virtual void EvaluateMatch(RegexContex context){
            context.Matches.Clear();
            context.Groups.Clear();
            var coll = regex.Matches(context.Content);
            if(coll.Count == 0){
                return;
            }

            var groupCount = 0;
            foreach(Match match in coll){
                context.Matches.Add(match.Value);
                GetMaxInt(match.Groups.Count, ref groupCount);
            }

            for(var i = 0; i < groupCount; i++){
                var groupItems =
                    (from Match match in coll where match.Groups[i] != null select match.Groups[i].Value).ToList();
                context.Groups.Add(i, groupItems);
            }
        }

        /// <summary>
        ///     通过Replace方式替换表达式内容
        /// </summary>
        /// <param name="contex">
        /// </param>
        protected virtual void EvaluateReplace(RegexContex contex){
            contex.Content = regex.Replace(contex.Content, contex.Replacement);
        }

        /// <summary>
        ///     The get max int.
        /// </summary>
        /// <param name="resoult">
        ///     The resoult.
        /// </param>
        /// <param name="source">
        ///     The source.
        /// </param>
        private void GetMaxInt(int resoult, ref int source){
            if(resoult > source){
                source = resoult;
            }
        }
    }
}
