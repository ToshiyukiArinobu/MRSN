using System;
using System.Web;

namespace KyoeiSystem.Application.Windows.Views.SRY
{
    public class MyModule1 : IHttpModule
    {
        /// <summary>
        /// モジュールを使用するには、Web の Web.config ファイルでこの
        /// モジュールを設定し、IIS に登録する必要があります。詳細については、
        /// 次のリンクを参照してください: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //後処理用コードはここに追加します。
        }

        public void Init(HttpApplication context)
        {
            // LogRequest イベントの処理方法とそれに対するカスタム ログの 
            // 実装方法の例を以下に示します
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //カスタム ログのロジックはここに挿入します
        }
    }
}
