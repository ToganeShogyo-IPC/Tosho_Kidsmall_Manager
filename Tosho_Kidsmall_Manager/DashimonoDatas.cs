using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tosho_Kidsmall_Manager
{
    /// <summary>
    /// 出し物データの共通形式
    /// </summary>
    public class DashimonoDatas
    {
        /// <summary>
        /// 学年・クラス
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 出し物の名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 受け入れ可能人数
        /// </summary>
        public int OKNinzu { get; set; }
        /// <summary>
        /// 残り人数(内部処理用)
        /// </summary>
        public int ZanNinzu { get; set; }
    }
}
