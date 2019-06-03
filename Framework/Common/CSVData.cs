using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// CSVアクセスクラス
	/// </summary>
	public static class CSVData
	{
		/// <summary>
		/// CSVファイルからDataTableにロードします。
		/// <para>・事前にDataTableのインスタンスを生成されている必要があります。</para>
		/// <para>・CSVファイルはShift-JISコードのみ対応します。</para>
		/// </summary>
		/// <param name="tbl">DataTableのインスタンス</param>
		/// <param name="filename">読込むCSVファイル名</param>
		public static void LoadTableData(DataTable tbl, string filename)
		{
			try
			{
				using (StreamReader rdr = new StreamReader(filename, System.Text.Encoding.GetEncoding("Shift_JIS")))
				{
					string line;
					// カラム名データをスキップする
					line = rdr.ReadLine();
					while (!rdr.EndOfStream)
					{
						// データをロードする
						line = rdr.ReadLine();
						DataRow row = tbl.NewRow();
						int idx = 0;
						foreach (string cdat in getcolumns(line))
						{
							row[idx++] = cdat;
						}
						tbl.Rows.Add(row);
					}
				}
			}
			catch (Exception ex)
			{
				throw new CSVException(CommonConst.ErrCSVFile, ex);
			}
		}

		/// <summary>
		/// CSVファイルからDataTableにロードします。
		/// カラム名はCSVファイルの1行目から取得または自動生成します。
		/// </summary>
		/// <param name="filename">CSVファイル名</param>
		/// <param name="tablename">テーブル名</param>
		/// <param name="columnsLine">1行目を項目名として取得するかどうかを指定するフラグ</param>
		/// <returns>取得したCSVデータをDataTableに変換した結果データ</returns>
		public static DataTable LoadTable(string filename, string tablename = null, bool columnsLine = true)
		{
			DataTable tbl = new DataTable(tablename);
			try
			{
				using (StreamReader rdr = new StreamReader(filename, Encoding.UTF8))
				{
					string line;
					if (columnsLine)
					{
						// 1行めからカラム名をロードする
						line = rdr.ReadLine();
						foreach (string cnm in getcolumns(line))
						{
							DataColumn col = new DataColumn(cnm, typeof(string));
							col.AllowDBNull = true;
							tbl.Columns.Add(col);
						}
					}
					while (!rdr.EndOfStream)
					{
						// データをロードする
						line = rdr.ReadLine();
						DataRow row = tbl.NewRow();
						int idx = 0;
						foreach (string cdat in getcolumns(line))
						{
							row[idx++] = cdat;
						}
						tbl.Rows.Add(row);
					}
				}
			}
			catch (Exception ex)
			{
				throw new CSVException(CommonConst.ErrCSVFile, ex);
			}

			return tbl;
		}

		private static string[] getcolumns(string line)
		{
			line.TrimStart(new char[] { '"' }).TrimEnd(new char[] { '"' });
			string[] cnms = line.Split(new string[] { "\t", "\"\t\"" }, StringSplitOptions.None);
			return cnms;
		}


		/// <summary>
		/// DataTableの各RowをCSVファイルに出力します。
		/// </summary>
		/// <param name="data">出力するデータを保持したDataTable</param>
		/// <param name="filename">出力するCSVファイル名</param>
		/// <param name="columnsLine">1行目にカラム名を出力するかどうかを指定</param>
		/// <param name="quate">引用符で囲むかどうかを指定</param>
		/// <param name="numberquate">数値項目を引用符で囲むかどうかを指定</param>
		/// <param name="delimiter">区切り文字</param>
		/// <param name="datetimeSpecial">日時項目の時刻部分出力フラグ</param>
		public static void SaveCSV(DataTable data, string filename, bool columnsLine = true, bool quate = true, bool numberquate = false, char delimiter = '\t', bool datetimeSpecial = false)
		{
			try
			{
				using (StreamWriter wrtr = new StreamWriter(filename, false, System.Text.Encoding.GetEncoding("Shift_JIS")))
				{
					string line;
					if (columnsLine)
					{
						line = makeColumns(data.Columns, quate, delimiter);
						wrtr.WriteLine(line);
					}
					foreach (DataRow row in data.Rows)
					{
						line = makeDataLine(row, quate, numberquate, delimiter, datetimeSpecial);
						wrtr.WriteLine(line);
					}
				}
			}
			catch (Exception ex)
			{
				throw new CSVException(CommonConst.ErrCSVFile, ex);
			}
		}

		private static string makeColumns(DataColumnCollection cols, bool quate, char delimiter)
		{
			string line = string.Empty;
			string qstr = quate ? "\"" : string.Empty;
			string delm = string.Empty;
			foreach (DataColumn col in cols)
			{
				line += delm + qstr + col.ColumnName + qstr;
				delm = string.Empty + delimiter;
			}

			return line;
		}

		private static string makeDataLine(DataRow row, bool quate, bool numberquate, char delimiter, bool datetimeSpecial)
		{
			string line = string.Empty;
			string qstr = quate ? "\"" : string.Empty;
			string delm = string.Empty;
			List<int> datetimecols = new List<int>();
			int idx = 0;
			foreach (DataColumn col in row.Table.Columns)
			{
				if (col.ColumnName.Contains("日時") || col.ColumnName == "削除日付")
				{
					datetimecols.Add(idx);
				}
				idx++;
			}
			idx = 0;
			foreach (object col in row.ItemArray)
			{
				if (numberquate || col is string)
				{
					line += delm + qstr + col.ToString() + qstr;
				}
				else
				{
					if (col is string)
					{
						line += delm + qstr + col.ToString() + qstr;
					}
					else
					{
						if (col is DateTime && datetimeSpecial && datetimecols.Contains(idx) != true)
						{
							line += delm + ((DateTime)col).ToString("yyyy/MM/dd");
						}
						else
						{
							line += delm + col.ToString();
						}
					}
				}
				delm = string.Empty + delimiter;
				idx++;
			}

			return line;
        }

        #region "CSVファイル読み込み CapitalBrain"
        /// <summary>
        /// CSVファイルを読み込み、DataTableで返す
        /// </summary>
        /// <param name="csvFileName">CSVファイル名</param>
        /// <param name="delimiter">区切り文字</param>
        /// <param name="hasHeaderRow">ヘッダー行の有無</param>
        /// <param name="hasFieldsEnclosedInQuotes">フィールドを"で囲み、改行文字、区切り文字を含めることができるか</param>
        /// <param name="trimWhiteSpace">フィールドの前後からスペースを削除か</param>
        /// <returns></returns>
        public static DataTable ReadCsv(string csvFileName, string delimiter, bool hasHeaderRow = true, bool hasFieldsEnclosedInQuotes = true, bool trimWhiteSpace = true)
        {
            DataTable tbl = new DataTable();

            try
            {
                //Shift JISで読み込む
                Microsoft.VisualBasic.FileIO.TextFieldParser tfp = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvFileName, System.Text.Encoding.GetEncoding(932));

                //フィールドが文字で区切られているとする
                tfp.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;

                //区切り文字セット
                string[] delimiters = new string[] { delimiter };
                tfp.Delimiters = delimiters;

                //フィールドを"で囲み、改行文字、区切り文字を含めることができるか
                tfp.HasFieldsEnclosedInQuotes = hasFieldsEnclosedInQuotes;

                //フィールドの前後からスペースを削除する
                tfp.TrimWhiteSpace = trimWhiteSpace;

                if (tfp.EndOfData == true) return tbl;

                string[] fields;

                //ヘッダー行があるならColumn名とする
                if (hasHeaderRow == true)
                {
                    fields = tfp.ReadFields();

                    // 1行めからカラム名をロードする
                    foreach (string cnm in fields)
                    {
						if (string.IsNullOrWhiteSpace(cnm.Trim()))
						{
							continue;
						}
                        DataColumn col = new DataColumn(cnm, typeof(string));
                        col.AllowDBNull = true;
                        tbl.Columns.Add(col);
                    }
                }

                bool bfirst = true;

                //データ読み込み
                while (!tfp.EndOfData)
                {
                    //フィールドを読み込む
                    fields = tfp.ReadFields();

                    //ヘッダー行がない時、データ１行目のカラム数分、カラムを作成する
                    if (hasHeaderRow == false && bfirst == true)
                    {
                        int colNo = 0;
                        foreach (string cnm in fields)
                        {
                            DataColumn col = new DataColumn("clm" + colNo.ToString(), typeof(string));
                            col.AllowDBNull = true;
                            tbl.Columns.Add(col);
                            colNo++;
                        }
                        bfirst = false;
                    }

                    // データをロードする
                    DataRow row = tbl.NewRow();
                    int idx = 0;
                    foreach (string cdat in fields)
                    {
                        row[idx++] = cdat;
						if (idx >= tbl.Columns.Count)
						{
							break;
						}
                    }
                    tbl.Rows.Add(row);
                }

                //後始末
                tfp.Close();
            }
            catch (Exception ex)
            {
                throw new CSVException(CommonConst.ErrCSVFile, ex);
            }

            return tbl;
        }
        #endregion

    }
}
