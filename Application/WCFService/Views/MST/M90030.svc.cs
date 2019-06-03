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
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "DataDriveLogService" を変更できます。
    // 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで DataDriveLogService.svc または DataDriveLogService.svc.cs を選択し、デバッグを開始してください。
    public class M90030 : IM90030
    {

        public List<M01_TOK> GETCSVDATA01()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m01 in context.M01_TOK
                            where m01.削除日付 == null
                            select m01;
                return query.ToList();
            }
        }

        public List<M10_UHK> GETCSVDATA02()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m10 in context.M10_UHK
                            where m10.削除日付 == null
                            select m10;
                return query.ToList();
            }
        }
                            
        public List<M08_TIK> GETCSVDATA03()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m08 in context.M08_TIK
                            where m08.削除日付 == null
                            select m08;
                return query.ToList();
            }
        }
        public List<M05_CAR> GETCSVDATA04()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m05 in context.M05_CAR
                           where m05.削除日付 == null
                           select m05;                    
                return query.ToList();
            }
        }

        public List<M04_DRV> GETCSVDATA05()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m04 in context.M04_DRV
                             where m04.削除日付 == null
                             select m04;
                return query.ToList();
            }
        }

        public List<M06_SYA> GETCSVDATA06()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m06 in context.M06_SYA
                            where m06.削除日付 == null
                            select m06;
                return query.ToList();
            }
        }

        public List<M09_HIN> GETCSVDATA07()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();

                var query = from m09 in context.M09_HIN
                             where m09.削除日付 == null
                             select m09;
                return query.ToList();
            }
        }

        public List<M11_TEK> GETCSVDATA08()
        {
            using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
            {
                context.Connection.Open();
                var query = from m11 in context.M11_TEK
                             where m11.削除日付 == null
                             select m11;
                return query.ToList();
            }
        }

        //#region 05 乗務員マスタ

        //public List<CSVDATA05> GETCSVDATA05()
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();

        //        var query = (from x in context.M04_DRV
        //                     where x.削除日付 == null
        //                     select new CSVDATA05
        //                     {
        //                         乗務員ID = x.乗務員ID,
        //                         登録日時 = x.登録日時,
        //                         更新日時 = x.更新日時,
        //                         乗務員名 = x.乗務員名,
        //                         自傭区分 = x.自傭区分,
        //                         就労区分 = x.就労区分,
        //                         かな読み = x.かな読み,
        //                         生年月日 = x.生年月日,
        //                         入社日 = x.入社日,
        //                         自社部門ID = x.自社部門ID,
        //                         歩合率 = x.歩合率,
        //                         デジタコCD = x.デジタコCD,
        //                         性別区分 = x.性別区分,
        //                         郵便番号 = x.郵便番号,
        //                         住所１ = x.住所１,
        //                         住所２ = x.住所２,
        //                         電話番号 = x.電話番号,
        //                         携帯電話 = x.携帯電話,
        //                         業務種類 = x.業務種類,
        //                         選任年月日 = x.選任年月日,
        //                         血液型 = x.血液型,
        //                         免許証番号 = x.免許証番号,
        //                         免許証取得年月日 = x.免許証取得年月日,
        //                         免許種類1 = x.免許種類1,
        //                         免許種類2 = x.免許種類2,
        //                         免許種類3 = x.免許種類3,
        //                         免許種類4 = x.免許種類4,
        //                         免許種類5 = x.免許種類5,
        //                         免許種類6 = x.免許種類6,
        //                         免許種類7 = x.免許種類7,
        //                         免許種類8 = x.免許種類8,
        //                         免許種類9 = x.免許種類9,
        //                         免許種類10 = x.免許種類10,
        //                         免許証条件 = x.免許証条件,
        //                         職種分類区分 = x.職種分類区分,
        //                         職種分類 = x.職種分類,
        //                         作成番号 = x.作成番号,
        //                         撮影年 = x.撮影年,
        //                         撮影月 = x.撮影月,
        //                         免許有効年月日1 = x.免許有効年月日1,
        //                         免許有効番号1 = x.免許有効番号1,
        //                         免許有効年月日2 = x.免許有効年月日2,
        //                         免許有効番号2 = x.免許有効番号2,
        //                         免許有効年月日3 = x.免許有効年月日3,
        //                         免許有効番号3 = x.免許有効番号3,
        //                         免許有効年月日4 = x.免許有効年月日4,
        //                         免許有効番号4 = x.免許有効番号4,
        //                         履歴年月日1 = x.履歴年月日1,
        //                         履歴1 = x.履歴1,
        //                         履歴年月日2 = x.履歴年月日2,
        //                         履歴2 = x.履歴2,
        //                         履歴年月日3 = x.履歴年月日3,
        //                         履歴3 = x.履歴3,
        //                         履歴年月日4 = x.履歴年月日4,
        //                         履歴4 = x.履歴4,
        //                         履歴年月日5 = x.履歴年月日5,
        //                         履歴5 = x.履歴5,
        //                         履歴年月日6 = x.履歴年月日6,
        //                         履歴6 = x.履歴6,
        //                         履歴年月日7 = x.履歴年月日7,
        //                         履歴7 = x.履歴7,
        //                         経験種類1 = x.経験種類1,
        //                         経験定員1 = x.経験定員1,
        //                         経験積載量1 = x.経験積載量1,
        //                         経験年1 = x.経験年1,
        //                         経験月1 = x.経験月1,
        //                         経験事業所1 = x.経験事業所1,
        //                         経験種類2 = x.経験種類2,
        //                         経験定員2 = x.経験定員2,
        //                         経験積載量2 = x.経験積載量2,
        //                         経験年2 = x.経験年2,
        //                         経験月2 = x.経験月2,
        //                         経験事業所2 = x.経験事業所2,
        //                         経験種類3 = x.経験種類3,
        //                         経験定員3 = x.経験定員3,
        //                         経験積載量3 = x.経験積載量3,
        //                         経験年3 = x.経験年3,
        //                         経験月3 = x.経験月3,
        //                         経験事業所3 = x.経験事業所3,
        //                         資格賞罰年月日1 = x.資格賞罰年月日1,
        //                         資格賞罰名1 = x.資格賞罰名1,
        //                         資格賞罰内容1 = x.資格賞罰内容1,
        //                         資格賞罰年月日2 = x.資格賞罰年月日2,
        //                         資格賞罰名2 = x.資格賞罰名2,
        //                         資格賞罰内容2 = x.資格賞罰内容2,
        //                         資格賞罰年月日3 = x.資格賞罰年月日3,
        //                         資格賞罰名3 = x.資格賞罰名3,
        //                         資格賞罰内容3 = x.資格賞罰内容3,
        //                         資格賞罰年月日4 = x.資格賞罰年月日4,
        //                         資格賞罰名4 = x.資格賞罰名4,
        //                         資格賞罰内容4 = x.資格賞罰内容4,
        //                         資格賞罰年月日5 = x.資格賞罰年月日5,
        //                         資格賞罰名5 = x.資格賞罰名5,
        //                         資格賞罰内容5 = x.資格賞罰内容5,
        //                         事業者コード = x.事業者コード,
        //                         健康保険加入日 = x.健康保険加入日,
        //                         健康保険番号 = x.健康保険番号,
        //                         厚生年金加入日 = x.厚生年金加入日,
        //                         厚生年金番号 = x.厚生年金番号,
        //                         雇用保険加入日 = x.雇用保険加入日,
        //                         雇用保険番号 = x.雇用保険番号,
        //                         労災保険加入日 = x.労災保険加入日,
        //                         労災保険番号 = x.労災保険番号,
        //                         厚生年金基金加入日 = x.厚生年金基金加入日,
        //                         厚生年金基金番号 = x.厚生年金基金番号,
        //                         通勤時間 = x.通勤時間,
        //                         通勤分 = x.通勤分,
        //                         家族連絡 = x.家族連絡,
        //                         住居の種類 = x.住居の種類,
        //                         通勤方法 = x.通勤方法,
        //                         家族氏名1 = x.家族氏名1,
        //                         家族生年月日1 = x.家族生年月日1,
        //                         家族続柄1 = x.家族続柄1,
        //                         家族血液型1 = x.家族血液型1,
        //                         家族その他1 = x.家族その他1,
        //                         家族氏名2 = x.家族氏名2,
        //                         家族生年月日2 = x.家族生年月日2,
        //                         家族続柄2 = x.家族続柄2,
        //                         家族血液型2 = x.家族血液型2,
        //                         家族その他2 = x.家族その他2,
        //                         家族氏名3 = x.家族氏名3,
        //                         家族生年月日3 = x.家族生年月日3,
        //                         家族続柄3 = x.家族続柄3,
        //                         家族血液型3 = x.家族血液型3,
        //                         家族その他3 = x.家族その他3,
        //                         家族氏名4 = x.家族氏名4,
        //                         家族生年月日4 = x.家族生年月日4,
        //                         家族続柄4 = x.家族続柄4,
        //                         家族血液型4 = x.家族血液型4,
        //                         家族その他4 = x.家族その他4,
        //                         家族氏名5 = x.家族氏名5,
        //                         家族生年月日5 = x.家族生年月日5,
        //                         家族続柄5 = x.家族続柄5,
        //                         家族血液型5 = x.家族血液型5,
        //                         家族その他5 = x.家族その他5,
        //                         退職年月日 = x.退職年月日,
        //                         退職理由 = x.退職理由,
        //                         特記事項1 = x.特記事項1,
        //                         特記事項2 = x.特記事項2,
        //                         特記事項3 = x.特記事項3,
        //                         特記事項4 = x.特記事項4,
        //                         特記事項5 = x.特記事項5,
        //                         健康診断年月日1 = x.健康診断年月日1,
        //                         健康診断年月日2 = x.健康診断年月日2,
        //                         健康診断年月日3 = x.健康診断年月日3,
        //                         健康診断年月日4 = x.健康診断年月日4,
        //                         健康診断年月日5 = x.健康診断年月日5,
        //                         健康状態 = x.健康状態,
        //                         水揚連動区分 = x.水揚連動区分,
        //                         個人ナンバー = x.個人ナンバー,
        //                         削除日付 = x.削除日付,
        //                         乗務員KEY = x.乗務員KEY,
        //                         固定給与 = x.固定給与,
        //                         固定賞与積立金 = x.固定賞与積立金,
        //                         固定退職引当金 = x.固定退職引当金,
        //                         固定福利厚生費 = x.固定福利厚生費,
        //                         固定法定福利費 = x.固定法定福利費,
        //                         固定労働保険 = x.固定労働保険,
        //                     }).AsQueryable();
        //        return query.ToList();
        //    }

        //}


        //#endregion

        //#region 06 車種マスタ

        //public List<CSVDATA06> GETCSVDATA06()
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();
        //        var query = (from m06 in context.M06_SYA
        //                     where m06.削除日付 == null
        //                     select new CSVDATA06
        //                     {
        //                         車種ID = m06.車種ID,
        //                         登録日時 = m06.登録日時,
        //                         更新日時 = m06.更新日時,
        //                         車種名 = m06.車種名,
        //                         積載重量 = m06.積載重量,
        //                         削除日付 = m06.削除日付,
        //                     }).AsQueryable();
        //        return query.ToList();
        //    }
        //}

        //#endregion

        //#region 07 商品マスタ

        //public List<CSVDATA07> GETCSVDATA07()
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();


        //        //全件表示
        //        var query = (from m09 in context.M09_HIN
        //                     where m09.削除日付 == null
        //                     select new CSVDATA07
        //                     {
        //                         商品ID = m09.商品ID,
        //                         登録日時 = m09.登録日時,
        //                         更新日時 = m09.更新日時,
        //                         商品名 = m09.商品名,
        //                         かな読み = m09.かな読み,
        //                         単位 = m09.単位,
        //                         商品重量 = m09.商品重量,
        //                         商品才数 = m09.商品才数,
        //                         削除日付 = m09.削除日付,
        //                     }).AsQueryable();
        //        return query.ToList();
        //    }
        //}
        //#endregion

        //#region 08 摘要マスタ

        //public List<CSVDATA08> GETCSVDATA08()
        //{
        //    using (TRAC3Entities context = new TRAC3Entities(CommonData.TRAC3_GetConnectionString()))
        //    {
        //        context.Connection.Open();
        //        var query = (from m08 in context.M11_TEK
        //                   where m08.削除日付 == null
        //                   select new CSVDATA08
        //                   {
        //                       摘要ID = m08.摘要ID,
        //                       登録日時 = m08.登録日時,
        //                       更新日時 = m08.更新日時,
        //                       摘要名 = m08.摘要名,
        //                       かな読み = m08.かな読み,
        //                   }).AsQueryable();
        //        return query.ToList();
        //    }
        //}

        //#endregion
    }
}
