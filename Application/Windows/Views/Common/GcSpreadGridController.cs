using GrapeCity.Windows.SpreadGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KyoeiSystem.Application.Windows.Views.Common
{
    /// <summary>
    /// スプレッドグリッドコントローラークラス
    /// </summary>
    public class GcSpreadGridController
    {
        private GcSpreadGrid _grid;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gr">対象のスプレッドグリッドコントロール</param>
        public GcSpreadGridController(GcSpreadGrid gr)
        {
            _grid = gr;

            _grid.InputBindings.Add(new KeyBinding(_grid.NavigationCommands.MoveNext, Key.Enter, ModifierKeys.None));

            _grid.PreviewKeyDown += GcSpreadGrid_PreviewKeyDown;

        }
        #endregion

        #region << プロパティ定義 >>

        /// <summary>
        /// 対象のスプレッドグリッド
        /// </summary>
        public GcSpreadGrid SpreadGrid
        {
            get { return _grid; }
            private set { _grid = value; }
        }

        /// <summary>
        /// GrapeCity.Windows.SpreadGrid.GcSpreadGrid 上のアクティブセルの列インデックスを取得します。
        /// </summary>
        public int ActiveColumnIndex
        {
            get { return _grid.ActiveColumnIndex; }
        }

        /// <summary>
        /// GrapeCity.Windows.SpreadGrid.GcSpreadGrid 上のアクティブセルの行インデックスを取得します。
        /// </summary>
        public int ActiveRowIndex
        {
            get { return _grid.ActiveRowIndex; }
        }

        /// <summary>
        /// セルがロックされているかどうかを取得または設定します
        /// GrapeCity.Windows.SpreadGrid.GcSpreadGridが保護されている場合、ロックされたセルは編集できません
        /// </summary>
        public bool? CellLocked
        {
            get { return SpreadGrid.Cells[this.ActiveRowIndex, this.ActiveColumnIndex].Locked; }
            set { SpreadGrid.Cells[this.ActiveRowIndex, this.ActiveColumnIndex].Locked = value; }
        }
        #endregion


        #region << セル値設定 >>

        /// <summary>
        /// 指定セルに値を設定する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <param name="value">設定値</param>
        public void SetCellValue(int rIdx, int cIdx, object value)
        {
            _grid.Cells[rIdx, cIdx].Value = value;
        }

        /// <summary>
        /// 指定セルに値を設定する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <param name="value">設定値</param>
        public void SetCellValue(int cIdx, object value)
        {
            SetCellValue(this.ActiveRowIndex, cIdx, value);
        }

        /// <summary>
        /// 指定セルに値を設定する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <param name="drow">データ行</param>
        /// <param name="colName">データ行の列名</param>
        public void SetCellValue(int rIdx, int cIdx, DataRow drow, string colName)
        {
            _grid.Cells[rIdx, cIdx].Value = drow[colName];
        }

        /// <summary>
        /// 選択中のセルに値を設定する
        /// </summary>
        /// <param name="value"></param>
        public void SetCellValue(object value)
        {
            SetCellValue(this.ActiveRowIndex, this.ActiveColumnIndex, value);
        }

        #endregion

        #region << セル値取得 >>

        #region 指定セル値取得(object)
        /// <summary>
        /// 指定セルから値を取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>object value</returns>
        public object GetCellValue(int rIdx, int cIdx)
        {
            return _grid.Cells[rIdx, cIdx].Value;
        }

        /// <summary>
        /// 指定セルから値を取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>object value</returns>
        public object GetCellValue(int cIdx)
        {
            return GetCellValue(this.ActiveRowIndex, this.ActiveColumnIndex);
        }

        /// <summary>
        /// 選択セルから値を取得する
        /// </summary>
        /// <returns>object value</returns>
        public object GetCellValue()
        {
            return GetCellValue(this.ActiveColumnIndex);
        }
        #endregion

        #region 指定セル値取得(int)
        /// <summary>
        /// 指定セルから値をInt型に変換して取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>int value</returns>
        public int? GetCellValueToInt(int rIdx, int cIdx)
        {
            var val = GetCellValue(rIdx, cIdx);

            if (val == null)
                return null;

            try
            {
                int ival;
                return int.TryParse(val.ToString(), out ival) ? ival : (int?)null;

            }
            catch
            {
                // 例外時はモミってnullを返す
                return null;
            }

        }

        /// <summary>
        /// 指定セルから値をInt型に変換して取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>int value</returns>
        public int? GetCellValueToInt(int cIdx)
        {
            return GetCellValueToInt(this.ActiveRowIndex, cIdx);
        }

        /// <summary>
        /// 選択セルから値をInt型に変換して取得する
        /// </summary>
        /// <returns>int value</returns>
        public int? GetCellValueToInt()
        {
            return GetCellValueToInt(this.ActiveRowIndex, this.ActiveColumnIndex);
        }

        #endregion

        #region 指定セル値取得(string)
        /// <summary>
        /// 指定セルから値をString型に変換して取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>string value</returns>
        public string GetCellValueToString(int rIdx, int cIdx)
        {
            var val = GetCellValue(rIdx, cIdx);

            if (val == null)
                return null;

            return val.ToString();

        }

        /// <summary>
        /// 指定セルから値をString型に変換して取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>string value</returns>
        public string GetCellValueToString(int cIdx)
        {
            return GetCellValueToString(this.ActiveRowIndex, cIdx);
        }

        /// <summary>
        /// 選択セルから値をString型に変換して取得する
        /// </summary>
        /// <returns>string value</returns>
        public string GetCellValueToString()
        {
            return GetCellValueToString(this.ActiveRowIndex, this.ActiveColumnIndex);
        }
        #endregion

        #region 指定セル値取得(DateTime)
        /// <summary>
        /// 指定セルから値をDateTime型に変換して取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>DateTime value</returns>
        public DateTime? GetCellValueToDateTime(int rIdx, int cIdx)
        {
            var val = GetCellValue(rIdx, cIdx);

            if (val == null)
                return null;

            try
            {
                DateTime dval;
                return DateTime.TryParse(val.ToString(), out dval) ? dval : (DateTime?)null;
            }
            catch
            {
                // 例外時はモミってnullを返す
                return null;
            }

        }

        /// <summary>
        /// 指定セルから値をDateTime型に変換して取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>DateTime value</returns>
        public DateTime? GetCellValueToDateTime(int cIdx)
        {
            return GetCellValueToDateTime(this.ActiveRowIndex, cIdx);
        }
        #endregion

        #region 指定セル値取得(decimal)
        /// <summary>
        /// 指定セル値をDecimal型に変換して取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>decimal value</returns>
        public decimal? GetCellValueToDecimal(int rIdx, int cIdx)
        {
            var val = GetCellValue(rIdx, cIdx);

            if (val == null)
                return null;

            try
            {
                decimal ival;
                return decimal.TryParse(val.ToString(), out ival) ? ival : (decimal?)null;

            }
            catch
            {
                // 例外時はモミってnullを返す
                return null;
            }

        }

        /// <summary>
        /// 指定セル値をDecimal型に変換して取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>decimal value</returns>
        public decimal? GetCellValueToDecimal(int cIdx)
        {
            return GetCellValueToDecimal(this.ActiveRowIndex, cIdx);
        }

        /// <summary>
        /// 選択セル値をDecimal型に変換して取得する
        /// </summary>
        /// <returns>decimal value</returns>
        public decimal? GetCellValueToDecimal()
        {
            return GetCellValueToDecimal(this.ActiveRowIndex, this.ActiveColumnIndex);
        }

        #endregion


        #endregion

        #region <<  >>

        #region フォーカス設定
        /// <summary>
        /// 指定セルにフォーカスを設定する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        public void SetCellFocus(int rIdx, int cIdx = 0)
        {
            _grid.Focus();
            _grid.ActiveCellPosition = new CellPosition(rIdx, cIdx);
        }
        #endregion

        #region 指定セルのスクロール表示
        /// <summary>
        /// 指定セルが表示されるようスクロールさせる
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        public void ScrollShowCell(int rIdx = 0, int cIdx = 0)
        {
            _grid.Focus();
            _grid.ActiveCellPosition = new CellPosition(rIdx, cIdx);
            _grid.ShowCell(rIdx, cIdx, VerticalPosition.Bottom);
        }
        #endregion

        #region 検証エラー初期化
        /// <summary>
        /// 指定行の検証エラーを初期化する
        /// </summary>
        /// <param name="rIdx"></param>
        public void ClearValidationErrors(int rIdx)
        {
            _grid.Rows[rIdx].ValidationErrors.Clear();
        }
        #endregion

        #region 検証エラー追加
        /// <summary>
        /// 指定行に検証エラー情報を追加します
        /// </summary>
        /// <param name="rIdx"></param>
        /// <param name="cIdx"></param>
        /// <param name="errMessage"></param>
        public void AddValidationError(int rIdx, int cIdx, string errMessage)
        {
            _grid.Rows[rIdx].ValidationErrors.Add(new SpreadValidationError(errMessage, null, rIdx, cIdx));
        }
        #endregion

        #region セルのロック状態を設定
        /// <summary>
        /// セルのロック状態を設定する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <param name="isChecked">チェックステータス</param>
        public void SetCellLocked(int rIdx, int cIdx, bool isChecked)
        {
            SpreadGrid.Cells[rIdx, cIdx].Locked = isChecked;
        }

        /// <summary>
        /// セルのロック状態を設定する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <param name="isChecked">チェックステータス</param>
        public void SetCellLocked(int cIdx, bool isChecked)
        {
            SpreadGrid.Cells[this.ActiveRowIndex, cIdx].Locked = isChecked;
        }
        #endregion

        #region セルのロック状態を取得
        /// <summary>
        /// セルがロックされているかどうかを取得する
        /// </summary>
        /// <param name="rIdx">行インデックス</param>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>ロック状態</returns>
        public bool? GetCellLocked(int rIdx, int cIdx)
        {
            return SpreadGrid.Cells[rIdx, cIdx].Locked;
        }

        /// <summary>
        /// セルがロックされているかどうかを取得する
        /// </summary>
        /// <param name="cIdx">列インデックス</param>
        /// <returns>ロック状態</returns>
        public bool? GetCellLocked(int cIdx)
        {
            return GetCellLocked(this.ActiveRowIndex, cIdx);
        }
        #endregion

        #region グリッドの使用可否設定
        /// <summary>
        /// グリッドの使用可否を設定する
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetEnabled(bool isEnabled)
        {
            _grid.IsEnabled = isEnabled;
        }
        #endregion






        #endregion


        #region << スプレッドグリッドイベント群 >>

        #region PreviewKeyDown
        /// <summary>
        /// グリッド上でキーが押された時のイベント処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GcSpreadGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Delete押下時の処理
            // REMARKS:編集状態でない場合のDeleteキーは無視する
            if (e.Key == Key.Delete && !this.SpreadGrid.Cells[this.ActiveRowIndex, this.ActiveColumnIndex].IsEditing)
                e.Handled = true;

        }
        #endregion

        #endregion




    }

}
