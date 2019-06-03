using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace KyoeiSystem.Application.WCFService
{
    /// <summary>
    /// 運転日報関連機能
    /// </summary>
    public class DLY09010
    {

        public class DLY09010_Member
        {
            [DataMember] public int 明細番号 { get; set; }
            [DataMember] public int 明細行 { get; set; }
            [DataMember] public int 明細区分 { get; set; }
            [DataMember] public int 得意先ID { get; set; }
            [DataMember] public int 得意先KEY { get; set; }
            [DataMember] public DateTime? 入出金日付 { get; set; }
            [DataMember] public int 出金区分 { get; set; }
            [DataMember] public int 出金金額 { get; set; }
            [DataMember] public decimal d出金金額 { get; set; }
            [DataMember] public int? 摘要ID { get; set; }
            [DataMember] public string 摘要 { get; set; }
            [DataMember] public DateTime? 手形期日 { get; set; }
            [DataMember] public int? 入力者ID { get; set; }
            [DataMember] public string Str手形期日 { get; set; }
        }

        public class DLY09010_NData
        {
            [DataMember] public decimal? 予定出金金額 { get; set; } 
            [DataMember] public decimal? 既出金額 { get; set; }
            [DataMember] public decimal? 入金相殺 { get; set; }
        }

        public class DLY09010_OData
        {
            [DataMember] public string 請求年月 { get; set; }
            [DataMember] public int 年月 { get; set; }
            [DataMember] public decimal 出金金額 { get; set; }
            [DataMember] public decimal 調整他 { get; set; }
            [DataMember] public decimal 出金差額 { get; set; }
            [DataMember] public decimal 支払金額 { get; set; }
            [DataMember] public decimal 立替通行料 { get; set; }
            [DataMember] public decimal 消費税 { get; set; }
            [DataMember] public decimal 支払合計額 { get; set; }
            [DataMember] public decimal 繰越金額 { get; set; }
            [DataMember] public decimal 前月残高　 { get; set; }
        }

        public class T04_TEKIYO_NAME
        {
            public int row { get; set; }
            public int 摘要ID { get; set; }
            public string 摘要 { get; set; }
        }

        #region データ取得

        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="p明細番号"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p入金日付"></param>
        /// <returns></returns>
        public List<DLY09010_Member> DLY09010_GetData(int? p明細番号)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                DLY09010_Member result = new DLY09010_Member();
                var NKubun = (from cmb in context.M99_COMBOLIST
                              where cmb.分類 == "日次" && cmb.機能 == "入金伝票入力" && cmb.カテゴリ == "入金区分"
                             select cmb);
                var query = (from t04 in context.T04_NYUK
                             from m01 in context.M01_TOK.Where(c => c.得意先KEY == t04.取引先KEY)
                             where t04.明細区分 == 3 && t04.明細番号 == p明細番号
                             select new DLY09010_Member
                             {
                                 得意先ID = m01.得意先ID,
                                 得意先KEY = m01.得意先KEY,
                                 入出金日付 = t04.入出金日付,
                                 出金区分 = t04.入出金区分,
                                 出金金額 = t04.入出金金額 == null ? 0 : t04.入出金金額,
                                 d出金金額 = t04.入出金金額 == null ? 0 : t04.入出金金額,
                                 摘要ID = t04.摘要ID,
                                 摘要 = t04.摘要名,
                                 手形期日 = t04.手形日付,
                                 明細番号 = t04.明細番号,
                                 明細行 = t04.明細行,
                                 入力者ID = t04.入力者ID,
                             }).ToList();
                foreach (var rec in query)
                {
                    rec.Str手形期日 = rec.手形期日 == null ? "" : ((DateTime)(rec.手形期日)).ToString("yyyy/MM/dd");
                }

                return query;
            }
        }

        #endregion

        #region GET明細番号

        public int? GetMeisaiNo(int pMeisaiNo, int vector)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var ret = (from t04 in context.T04_NYUK
                           where t04.明細区分 == 3
                           select t04.明細番号);

                //データが1件もない状態で<< < > >>を押された時の処理
                if (pMeisaiNo == 0 && ret.Count() == 0)
                {
                    return null;
                }

                int? No = 0;
                switch (vector)
                {
                    case 0:		// 最小値
                        No = (from t04 in context.T04_NYUK
                              where t04.明細区分 == 3
                              select t04.明細番号).Min();
                        break;
                    case 1:		// ひとつ前
                        No = (from t04 in context.T04_NYUK
                              where t04.明細区分 == 3 && t04.明細番号 < pMeisaiNo
                              select t04.明細番号).DefaultIfEmpty().Max();
                        break;
                    case 2:		// ひとつ後
                        No = (from t04 in context.T04_NYUK
                              where t04.明細区分 == 3 && t04.明細番号 > pMeisaiNo
                              select t04.明細番号).DefaultIfEmpty().Min();
                        break;
                    default:	// 最大
                        No = (from t04 in context.T04_NYUK
                              where t04.明細区分 == 3
                              select t04.明細番号).Max();
                        break;
                }

                return No;
            }
        }

        #endregion

        #region 登録件数

        /// <summary>
        /// 現在の登録件数取得
        /// </summary>
        /// <returns></returns>
        public int GetMaxMeisaiCount()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //context.Connection.Open();
                int query = (from t04 in context.T04_NYUK where t04.明細区分 == 3 select t04.明細番号).Distinct().Count();
                return query;
            }
        }

        #endregion

        #region 入金予定額取得

        //DLY08010_NData
        /// <summary>
        /// 入金予定額取得
        /// </summary>
        /// <param name="p明細番号"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p入金日付"></param>
        /// <returns></returns>
        public List<DLY09010_NData> DLY09010_GETNData(int p得意先ID, DateTime p入出金日付 , int p明細番号)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {

                context.Connection.Open();
                DLY09010_Member result = new DLY09010_Member();
                var ret = from m01 in context.M01_TOK
                          where m01.得意先ID == p得意先ID
                          select m01;
                var qm01 = ret.FirstOrDefault();
                int i得意先KEY;
                i得意先KEY = qm01.得意先KEY;

                #region 日付計算(入金予定額)

                //***日付計算(入金予定額)***//
                //得意先の締日を取得
                int x締日 = qm01.Ｓ締日;
                int x日付 = Convert.ToInt32(p入出金日付.Day.ToString());
                int xi出金日付;
                DateTime x出金日付, x出金予定日;

                //サイトの考え方　サイトが1の場合　締日が翌月でも翌月の末日までがサイトの領域になります　20160517対応
                //if (x締日 < x日付)
                //{
                //    //締日が20締に対して入金日付が31日等だった場合　月に1を足して来月のデータとしてカウント
                //    x出金日付 = p入出金日付.AddMonths(+1);
                //    //入金予定額を求めるためにサイトを引いた集計年月を作成
                //    x出金予定日 = x出金日付.AddMonths(-qm01.Ｔサイト日);
                //    xi出金日付 = AppCommon.IntParse(x出金予定日.Month.ToString().Length == 1 ? x出金予定日.Year.ToString() + "0" + x出金予定日.Month.ToString() : x出金予定日.Year.ToString() + x出金予定日.Month.ToString());  
                //}
                //else
                //{
                    x出金日付 = p入出金日付;
                    //入金予定額を求めるためにサイトを引いた集計年月を作成
                    x出金予定日 = x出金日付.AddMonths(-qm01.Ｔサイト日);
                    xi出金日付 = AppCommon.IntParse(x出金予定日.Month.ToString().Length == 1 ? x出金予定日.Year.ToString() + "0" + x出金予定日.Month.ToString() : x出金予定日.Year.ToString() + x出金予定日.Month.ToString());   
                //}

                 var query = (from s02 in context.S02_YOSS
                             where s02.支払先KEY == i得意先KEY && s02.集計年月 == xi出金日付
                             group s02 by s02.締日売上金額 into Group
                             select new DLY09010_NData
                             {
                                 予定出金金額 = Group.Sum(c => c.締日売上金額 + c.締日消費税 + c.締日通行料) == null ? 0 : Group.Sum(c => c.締日売上金額 + c.締日消費税 + c.締日通行料)
                             }).AsQueryable();

                #endregion

                #region 日付計算(既入金額)

                 //***日付計算(既入金額)***//
                int y締日 = qm01.Ｓ締日;
				if (y締日 == 0)
				{
					return null;
				};

                int y日付 = AppCommon.IntParse(p入出金日付.Day.ToString());
                DateTime y出金日付From , y出金日付To;
                if (y締日 < 31)
                {
                    if (y締日 < y日付)
                    {
                        y出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + (y締日 + 1).ToString());
                        y出金日付To = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + 01);
                        y出金日付To = y出金日付To.AddMonths(+1);
                        y出金日付To = Convert.ToDateTime(y出金日付To.Year.ToString() + "/" + y出金日付To.Month + "/" + y締日.ToString());
                    }
                    else
                    {
                        y出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.AddMonths(-1).Month.ToString() + "/" + (y締日 + 1).ToString());
                        y出金日付To = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + y締日.ToString());
                    }
                }
                else
                {
                    y出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + 01);
                    y出金日付To = y出金日付From.AddMonths(1).AddDays(-1);
                }



                var query2 = (from t04 in context.T04_NYUK
                              where t04.取引先KEY == i得意先KEY && (t04.入出金日付 >= y出金日付From && t04.入出金日付 <= y出金日付To) && t04.明細区分 == 3 &&　t04.明細番号 != p明細番号
                              group t04 by t04.入出金金額 into Group
                              select new DLY09010_NData
                              {
                                  既出金額 = Group.Sum(c => c.入出金金額) == null ? 0 : Group.Sum(c => c.入出金金額)
                              });

                 #endregion

                #region 日付計算(入金相殺)

                //***日付計算(入金相殺)***//
                int z締日 = qm01.Ｓ締日;
                int z日付 = AppCommon.IntParse(p入出金日付.Day.ToString());
                DateTime z出金日付From, z出金日付To;
                if (y締日 < 31)
                {
                    if (y締日 < y日付)
                    {
                        z出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + (z締日 + 1).ToString());
                        z出金日付To = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + 01);
                        z出金日付To = z出金日付To.AddMonths(+1);
                        z出金日付To = Convert.ToDateTime(z出金日付To.Year.ToString() + "/" + z出金日付To.Month + "/" + z締日.ToString());
                    }
                    else
                    {
                        z出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.AddMonths(-1).Month.ToString() + "/" + (z締日 + 1).ToString());
                        z出金日付To = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + z締日.ToString());
                    }
                }
                else
                {
                    z出金日付From = Convert.ToDateTime(p入出金日付.Year.ToString() + "/" + p入出金日付.Month.ToString() + "/" + 01);
                    z出金日付To = z出金日付From.AddMonths(1).AddDays(-1);
                }



                var query3 = (from t04 in context.T04_NYUK
                              where t04.取引先KEY == i得意先KEY && (t04.入出金日付 >= z出金日付From && t04.入出金日付 <= z出金日付To) && t04.明細区分 == 2 && t04.入出金区分 == 5
                              group t04 by t04.入出金金額 into Group
                              select new DLY09010_NData
                              {
                                  入金相殺 = Group.Sum(c => c.入出金金額) == null ? 0 : Group.Sum(c => c.入出金金額)
                              });

                #endregion

                //+++リスト化***//
                List<DLY09010_NData> queryList = new List<DLY09010_NData>();
                //初期値をセット
                queryList.Add(new DLY09010_NData() { 入金相殺 = 0, 既出金額 = 0, 予定出金金額 = 0 });
                List<DLY09010_NData> queryLIST1 = query.ToList();
                List<DLY09010_NData> queryLIST2 = query2.ToList();
                List<DLY09010_NData> queryLIST3 = query3.ToList();
                int x = 0 , y = 0 , z = 0;

                //上記で求めたqueryの件数をひとつの合計金額に
                foreach (var row in queryLIST1)
                {
                    queryList[0].予定出金金額 += queryLIST1[x].予定出金金額;
                    x += 1;
                }

                foreach (var row in queryLIST2)
                {

                    queryList[0].既出金額 += queryLIST2[y].既出金額;
                    y += 1;
                }

                foreach (var row in queryLIST3)
                {
                    queryList[0].入金相殺 += queryLIST3[z].入金相殺;
                    z += 1;
                }

                return queryList;
            }
        }

        #endregion

        #region 顧客別残高リスト取得

        /// <summary>
        /// 顧客別残高リスト取得
        /// </summary>
        /// <param name="p得意先ID"></param>
        /// <param name="p入出金日付"></param>
        /// <returns></returns>
        public List<DLY09010_OData> DLY09010_GETOData(int p得意先ID, DateTime p入出金日付 , int pサイト)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                //【qList】は帳票の最初のデータになる
                //【Query】で出力する1個前のデータを取得する
                //もし1個前のデータが無ければM01_TOKにある期首残を取得する為のクエリ
                var qList = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
                             join s02 in context.S02_YOSS.Where(c => c.回数 == 1) on m01.得意先KEY equals s02.支払先KEY into Group
                             from s02Group in Group
                             group new { m01, s02Group } by new { m01.得意先ID, m01.得意先名１, s02Group.集計年月, m01.Ｓ締日期首残 } into grGroup
                             where grGroup.Key.得意先ID == p得意先ID
                             select new DLY09010_OData
                             {
                                 年月 = 0,
                                 出金金額 = 0,
                                 調整他 = 0,
                                 出金差額 = 0,
                                 支払金額 = 0,
                                 立替通行料 = 0,
                                 消費税 = 0,
                                 支払合計額 = 0,
                                 繰越金額 = grGroup.Key.Ｓ締日期首残 == null ? 0 : grGroup.Key.Ｓ締日期首残,
                             }).Distinct().AsQueryable();
                p入出金日付 = p入出金日付.AddMonths(-pサイト);
                int cnt = 0;
                int i集計年月 = p入出金日付.Month.ToString().Length == 1 ? Convert.ToInt32(p入出金日付.Year.ToString() + "0" + p入出金日付.Month.ToString()) :  Convert.ToInt32(p入出金日付.Year.ToString() + p入出金日付.Month.ToString());
                //Spreadに出力する残高データ
                var query = (from m01 in context.M01_TOK.Where(c => c.削除日付 == null)
                             join s02 in context.S02_YOSS.Where(c => c.回数 ==1) on m01.得意先KEY equals s02.支払先KEY into Group
                             from s02Group in Group
                             group new { m01, s02Group } by new { m01.得意先ID, m01.得意先名１, s02Group.集計年月, m01.Ｓ締日期首残 } into grGroup
                             where grGroup.Key.得意先ID == p得意先ID
                             select new DLY09010_OData
                             {
                                 年月 = grGroup.Key.集計年月,
                                 出金金額 = grGroup.Sum(c => c.s02Group.締日入金現金 + c.s02Group.締日入金手形) == null ? 0 : grGroup.Sum(c => c.s02Group.締日入金現金 + c.s02Group.締日入金手形),
                                 調整他 = grGroup.Sum(c => c.s02Group.締日入金その他) == null ? 0 : grGroup.Sum(c => c.s02Group.締日入金その他),
                                 支払金額 = grGroup.Sum(c => c.s02Group.締日売上金額) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額),
                                 消費税 = grGroup.Sum(c => c.s02Group.締日消費税) == null ? 0 : grGroup.Sum(c => c.s02Group.締日消費税),
                                 立替通行料 = grGroup.Sum(c => c.s02Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s02Group.締日通行料),
                                 支払合計額 = grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料) == null ? 0 : grGroup.Sum(c => c.s02Group.締日売上金額 + c.s02Group.締日消費税 + c.s02Group.締日通行料),
                                 前月残高 = grGroup.Where(c => c.s02Group.回数 == 1).Sum(c => c.s02Group.締日前月残高) == null ? 0 : grGroup.Where(c => c.s02Group.回数 == 1).Sum(c => c.s02Group.締日前月残高),
                             }).AsQueryable();

                //クエリをリスト化【Query】
                List<DLY09010_OData> queryList = query.ToList();
                //前回繰越を元に差引金額と現在の繰越残高を算出
                if (queryList.Count > 0)
                {
                    queryList[0].繰越金額 = (queryList[0].支払金額 + queryList[0].消費税 + queryList[0].立替通行料) - (queryList[0].出金金額 + queryList[0].調整他) + queryList[0].前月残高;
                    for (int i = 1; i < queryList.Count(); i++)
                    {
                        queryList[i].出金差額 = queryList[i].前月残高 - queryList[i].出金金額 - queryList[i].調整他;
                        queryList[i].繰越金額 = queryList[i].出金差額 + queryList[i].支払金額 + queryList[i].消費税 + queryList[i].立替通行料;
                        if (queryList[i].繰越金額 == 0 && cnt == 0 && queryList.Count() < 2)
                        {
                            queryList[i].繰越金額 = queryList[i + 1].前月残高;
                            cnt = 1;
                        }
                    }
                }
                else
                {
                    return queryList;
                }
                //集計年月で絞込み
                queryList = queryList.Where(c => c.年月 <= i集計年月).ToList();

                //先頭クエリをリスト化【qList】
                List<DLY09010_OData> queryList2 = qList.ToList();
                //【Query】で算出したデータを取得

                if (queryList.Count() == 0)
                {
                    return queryList2;
                }

                queryList2[0].繰越金額 = queryList[0].出金差額;

                //取得したデータが0の場合はM01_TOKにある期首残を取得
                if (queryList2[0].繰越金額 == decimal.Zero)
                {
                    queryList2[0].繰越金額 = queryList[0].前月残高;
                    queryList[0].出金差額 = queryList2[0].繰越金額;
                }

                //【Query】と【qList】をフュージョンして1つのリストにまとめる
                //(querylist2)は(querylist)の下にくっつく使用になっている
                queryList.AddRange(queryList2);

                //【Query】を年月を元に並び替え
                queryList = queryList.OrderBy(c => c.年月).ToList();

                //【Query】の件数を取得
                int queryCount = queryList.Count();

                //【Query】の件数を元にLoopさせる
                for (int i = 1; i < queryCount; i++)
                {
                    //LINQ内ではConvert.ToStringが使用できない為、手動で年月をInt型 → String型に変更
                        int Days = queryList[i].年月;
                        queryList[i].請求年月 = Days.ToString().Substring(0, 4) + "/" + Days.ToString().Substring(4, 2);
                }
                return queryList;
            }
        }

        #endregion

        #region データ登録・変更

        /// <summary>
        /// データ登録・変更
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="入金入力一覧表"></param>
        public int DLY09010_UPDATE(int pno, List<DLY09010_Member> 入金入力一覧表)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                //Pno(明細番号)がマイナス1では無かったら編集モード
                if (pno != -1)
                {
                    // トランザクションのインスタンス化(開始)
                    using (var tran = new TransactionScope())
                    {
                        int i明細番号 = 入金入力一覧表[0].明細番号;
                        pno = i明細番号;
                        string sql = string.Empty;

                        //編集モードで明細番号が一致するものを一旦削除
                        sql = string.Format("DELETE T04_NYUK WHERE T04_NYUK.明細番号 = '" + i明細番号 + "' ");
                        int count = context.ExecuteStoreCommand(sql);
                        tran.Complete();

                    }
                        //編集モードで登録した全データをForeach文で回しながら登録
                        foreach (DLY09010_Member row in 入金入力一覧表)
                        {
                            DateTime result;
                            //CmbBoxの入金区分が選択されていなければデータとして見ない
                            if (row.出金区分 == 0)
                            {
                                //Continueで処理をせず次のデータを参照
                                continue;
                            }
                            int i取引先ID = 入金入力一覧表[0].得意先ID;
                            var query = from m01 in context.M01_TOK
                                        where m01.得意先ID == i取引先ID
                                        select m01;
                            var qm01 = query.FirstOrDefault();
                            T04_NYUK IT04 = new T04_NYUK();
                            IT04.更新日時 = DateTime.Now;
                            IT04.明細番号 = row.明細番号;
                            IT04.明細行 = row.明細行;
                            IT04.明細区分 = 3;
                            IT04.入出金日付 = row.入出金日付;
                            IT04.取引先KEY = qm01.得意先KEY;
                            IT04.入出金区分 = row.出金区分;
                            IT04.入出金金額 = row.出金金額;
                            IT04.摘要ID = row.摘要ID;
                            IT04.摘要名 = row.摘要;
                            IT04.手形日付 = DateTime.TryParse(row.Str手形期日, out result) == true ? result : (DateTime?)null;
                            IT04.入力者ID = row.入力者ID;
                            context.T04_NYUK.ApplyChanges(IT04);
                        }
                        context.SaveChanges();
                    
                }
                //Pno(明細番号)が-1だった場合は新規登録
                else
                {
                    // 新番号取得
                    var 明細番号M = (from n in context.M88_SEQ
                                 where n.明細番号ID == 1
                                 select n
                                    ).FirstOrDefault();
                    if (明細番号M == null)
                    {
                        return -1;
                    }
                    pno = 明細番号M.現在明細番号 + 1;
                    明細番号M.現在明細番号 = pno;
                    明細番号M.AcceptChanges();

                    //新規登録なのでデータは削除せずそのまま登録へ
                    foreach (DLY09010_Member row in 入金入力一覧表)
                    {
                        DateTime result;
                        int i取引先ID = 入金入力一覧表[0].得意先ID;

                        //CmbBoxの入金区分が選択されていなければデータとして見ない
                        if (row.出金区分 == 0)
                        {
                            //Continueで処理をせず次のデータを参照
                            continue;
                        }
                        //var T04 = query.FirstOrDefault();
                        var query =  from m01 in context.M01_TOK
                                     where m01.得意先ID == i取引先ID
                                     select m01;
                        var qm01 = query.FirstOrDefault();
                        T04_NYUK IT04 = new T04_NYUK();
                        IT04.更新日時 = DateTime.Now;
                        IT04.明細番号 = pno;
                        IT04.明細行 = row.明細行;
                        IT04.明細区分 = 3;
                        IT04.入出金日付 = row.入出金日付;
                        IT04.取引先KEY = qm01.得意先KEY;
                        IT04.入出金区分 = row.出金区分;
                        IT04.入出金金額 = row.出金金額;
                        IT04.摘要ID = row.摘要ID;
                        IT04.摘要名 = row.摘要;
                        IT04.手形日付 = DateTime.TryParse(row.Str手形期日, out result) == true ? result : (DateTime?)null;
                        IT04.入力者ID = row.入力者ID;
                        context.T04_NYUK.ApplyChanges(IT04);
                    }


                    context.SaveChanges();
                }
                return pno;
            }
        }

        #endregion

        #region データ削除

        /// <summary>
        /// データ削除
        /// </summary>
        /// <param name="p明細番号"></param>
        /// <param name="p得意先ID"></param>
        /// <param name="p入金日付"></param>
        /// <returns></returns>
        public void DLY09010_DELETE(int i明細番号)
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // トランザクションのインスタンス化(開始)
                using (var tran = new TransactionScope())
                {
                    string sql = string.Empty;

                    //編集モードで明細番号が一致するものを一旦削除
                    sql = string.Format("DELETE T04_NYUK WHERE T04_NYUK.明細番号 = '" + i明細番号 + "' ");
                    int count = context.ExecuteStoreCommand(sql);
                    tran.Complete();

                }
            }
        }

        #endregion

        #region 自動採番

        /// <summary>
        /// 自動採番
        /// </summary>
        /// <returns></returns>
        public int DLY09010_GetNEXTData()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                // 新番号取得
                var query = (from x in context.M88_SEQ
                             where x.明細番号ID == 1
                             select x
                                ).FirstOrDefault();
                if (query == null)
                {
                    return -1;
                }
                return query.現在明細番号 + 1;
            }
        }

        #endregion

        #region Spread内摘要名表示用

        /// <summary>
        /// SPREAD内摘要名表示用
        /// </summary>
        /// <param name="pShrID"></param>
        /// <param name="pRow"></param>
        /// <returns></returns>
        public List<T04_TEKIYO_NAME> GetTekiyoName(int pTekiyoID, int pRow)
        {
            List<T04_TEKIYO_NAME> result = new List<T04_TEKIYO_NAME>();
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var tekiyonm = (from k in context.M11_TEK
                                where k.摘要ID == pTekiyoID
                                   && k.削除日付 == null
                                select k.摘要名).FirstOrDefault();
                result.Add(
                    new T04_TEKIYO_NAME()
                    {
                        row = pRow,
                        摘要ID = pTekiyoID,
                        摘要 = tekiyonm,
                    }
                    );
            }

            return result;
        }

        #endregion

    }
}
