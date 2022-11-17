using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Testapi.Models;
using Testapi.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Testapi.Controllers
{
    public class kensakuPageController : ApiController
    {
       
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        // GET api/<controller>/5
        public List<PMKensaku> KensakuBtn1(
            string PART_NO,                                         //部品コード
            string PART_NAME_LOC1,                                  //部品名
            int FIND_OPTION                                         //部品名検索条件
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                PART_NO = DbContext.FixedSQLi(PART_NO);
                PART_NAME_LOC1 = DbContext.FixedSQLi(PART_NAME_LOC1);
                string sql = SqlTable.getSQLTableBase();
                sql += SqlTable.getSQLBuhimei(PART_NO, PART_NAME_LOC1, FIND_OPTION);
                sql += " order by PM.PART_NO ";
                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;

            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<PMKensaku> KensakuBtn2(
            string PRODUCT_TYPE,                                //製品種別
            string ISSUE_DATE_1,                                //標準図発行日１
            string ISSUE_DATE_2,                                //標準図発行日２
            string DWG_CHG_DATE_1,                              //標準図切替日１
            string DWG_CHG_DATE_2,                              //標準図切替日２
            string CHG_NO,                                      //切替通知書No
            string ISSUE_NO                                     //発行通知書No
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                PRODUCT_TYPE = DbContext.FixedSQLi(PRODUCT_TYPE);
                ISSUE_DATE_1 = DbContext.FixedSQLi(ISSUE_DATE_1);
                ISSUE_DATE_2 = DbContext.FixedSQLi(ISSUE_DATE_2);
                DWG_CHG_DATE_1 = DbContext.FixedSQLi(DWG_CHG_DATE_1);
                DWG_CHG_DATE_2 = DbContext.FixedSQLi(DWG_CHG_DATE_2);
                CHG_NO = DbContext.FixedSQLi(CHG_NO);
                ISSUE_NO = DbContext.FixedSQLi(ISSUE_NO);

                string sql = SqlTable.getSQLTableBase();
                if (PRODUCT_TYPE !="-") {sql += "  and DWG.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' "; }           //製品種別
                sql += SqlTable.getSQLHyoujuuhakko(ISSUE_DATE_1, ISSUE_DATE_2,false);                           //標準図発行日
                sql += SqlTable.getSQLHyoujuuKirikae(DWG_CHG_DATE_1, DWG_CHG_DATE_2, CHG_NO,ISSUE_NO);          //標準図切替日 発行通知書No 切替通知書No
                sql += " order by PM.PART_NO ";

                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;

            }
        }
   
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<PMKensaku> ShousaiKensaku(
            string PART_NO,                                     //部品コード
            int PART_NO_OPTION,                                 //部品コード検索方法
            string PART_NAME_LOC1,                              //部品名
            int PART_NAME_LOC1_OPTION ,                         //部品名検索方法
            string MAINT_PART_NAME,                             //保守部品名
            int MAINT_PART_NAME_OPTION ,                        //保守部品名検索方法
            string DWG_NO,                                      //図番
            int DWG_NO_OPTION,                                  //図番検索方法
            string ISSUE_DATE_1,                                //標準図発行日1
            string ISSUE_DATE_2,                                //標準図発行日２
            string PRODUCT_CODE,                                //製品分類コード
            string PART_TYPE,                                   //部品区分
            string PDM_TYPE,                                    //PDM判定
            string MACHINE_TYPE,                                //機種
            string SELLING_PRICE_TYPE,                          //価格設定
            string MAKER_PART_NO,                               //メーカー型番
            string PLANT_NO,                                    //工場区分
            string MFG_TYPE,                                    //内外作区分
            string STOCK_TYPE,                                  //貯蔵区分
            string ARR_BRANCH_CODE,                             //管理店所
            string ARR_WHO,                                     //在庫担当
            string PO_BRANCH_CODE,                              //発注店所
            string PO_WHO,                                      //発注担当
            string BUCKET,                                      //バケット
            string ORDER_TYPE ,                                 //管理基準
            string ABC_TYPE,                                    //ABC区分
            string STOCK_CODE,                                  //在庫管理コード
            string ROUTING_CODE,                                //工程コード
            string VENDOR_CODE,                                 //取引先コード
            string SG_CODE,                                     //作業コード
            bool ckUselWHCode_Checked,                          //倉庫コードチェックボックス
            [FromUri]string[] pWhCode,                                      //倉庫コード   //ERROR
            string LOCATION,                                    //置場/棚番
            string SOKO_TANTO,                                  //倉庫担当
            string PS_FLAG,                                     //P/S展開区分
            string AUTO_PURCHASE_REQ,                           //自動購入指示
            bool ckMoreZero_Checked,                            //在庫数チェックボックス
            string CURRENT_BALANCE_1,                           //在庫数１
            string CURRENT_BALANCE_2,                           //在庫数２
            string eStockAmount_1,                              //在庫金額１
            string eStockAmount_2,                              //在庫金額２
            string YOTEI_TANKA_1,                                //標準単価１
            string YOTEI_TANKA_2,                               //標準単価２
            bool ckNoReceipt_Checked,                           //最終入庫日チェックボックス
            string LAST_RECEIPT_DATE_1,                         //最終入庫日１
            string LAST_RECEIPT_DATE_2,                         //最終入庫日２
            bool ckNoIssue_Checked,                             //最終出庫日チェックボックス
            string LAST_ISSUE_DATE_1,                           //最終出庫日１
            string LAST_ISSUE_DATE_2,                           //最終出庫日２
            string STOCK_START_DATE_1,                          //貯蔵開始日１
            string STOCK_START_DATE_2,                          //貯蔵開始日２
            string STOCK_STOP_FlAG,                             //貯蔵中止予定
            string STOCK_STOP_DATE,                             //貯蔵止め
            string PART_LOCATION,                               //部位
            string MAINT_TYPE,                                  //保守判定
            bool ckPPPMMAINTMS2_notEdit_Checked,                //図面発行後、二次判定が一度も設定されていないものチェックボックス
            bool ckPCEntandSPNoEnt_Checked,                     //製造原価登録済且つ販売価格未登録部品チェックボックス
            bool ckRepairRepEnt_Checked,                        //修理提案書利用チェックボックス
            bool ckPartDescAndRepReason_Checked,                //部品説明or取替理由が未登録チェックボックス
            bool ckNoPhoto_Checked                              //写真未登録チェックボックス
            )
        {
            using (var DbContext = new TablesDbContext())
            {
                //SQL　インジェクション対策 
                PART_NO             = DbContext.FixedSQLi(PART_NO            );                        
                PART_NAME_LOC1      = DbContext.FixedSQLi(PART_NAME_LOC1     );
                MAINT_PART_NAME     = DbContext.FixedSQLi(MAINT_PART_NAME    );
                DWG_NO              = DbContext.FixedSQLi(DWG_NO             );
                ISSUE_DATE_1        = DbContext.FixedSQLi(ISSUE_DATE_1       );
                ISSUE_DATE_2        = DbContext.FixedSQLi(ISSUE_DATE_2       );
                PRODUCT_CODE        = DbContext.FixedSQLi(PRODUCT_CODE       );
                PART_TYPE           = DbContext.FixedSQLi(PART_TYPE          );
                PDM_TYPE            = DbContext.FixedSQLi(PDM_TYPE           );
                MACHINE_TYPE        = DbContext.FixedSQLi(MACHINE_TYPE       );
                SELLING_PRICE_TYPE  = DbContext.FixedSQLi(SELLING_PRICE_TYPE );
                MAKER_PART_NO       = DbContext.FixedSQLi(MAKER_PART_NO      );
                PLANT_NO            = DbContext.FixedSQLi(PLANT_NO           );
                MFG_TYPE            = DbContext.FixedSQLi(MFG_TYPE           );
                STOCK_TYPE          = DbContext.FixedSQLi(STOCK_TYPE         );
                ARR_BRANCH_CODE     = DbContext.FixedSQLi(ARR_BRANCH_CODE    );
                ARR_WHO             = DbContext.FixedSQLi(ARR_WHO            );
                PO_BRANCH_CODE      = DbContext.FixedSQLi(PO_BRANCH_CODE     );
                PO_WHO              = DbContext.FixedSQLi(PO_WHO             );
                BUCKET              = DbContext.FixedSQLi(BUCKET             );
                ORDER_TYPE          = DbContext.FixedSQLi(ORDER_TYPE         );
                ABC_TYPE            = DbContext.FixedSQLi(ABC_TYPE           );
                STOCK_CODE          = DbContext.FixedSQLi(STOCK_CODE         );
                ROUTING_CODE        = DbContext.FixedSQLi(ROUTING_CODE       );
                VENDOR_CODE         = DbContext.FixedSQLi(VENDOR_CODE        );
                SG_CODE             = DbContext.FixedSQLi(SG_CODE            );
                //pWhCode             = DbContext.FixedSQLi(pWhCode            );
                LOCATION            = DbContext.FixedSQLi(LOCATION           );
                SOKO_TANTO          = DbContext.FixedSQLi(SOKO_TANTO         );
                PS_FLAG             = DbContext.FixedSQLi(PS_FLAG            );
                AUTO_PURCHASE_REQ   = DbContext.FixedSQLi(AUTO_PURCHASE_REQ  );
                CURRENT_BALANCE_1   = DbContext.FixedSQLi(CURRENT_BALANCE_1  );
                CURRENT_BALANCE_2   = DbContext.FixedSQLi(CURRENT_BALANCE_2  );
                eStockAmount_1      = DbContext.FixedSQLi(eStockAmount_1     );
                eStockAmount_2      = DbContext.FixedSQLi(eStockAmount_2     );
                YOTEI_TANKA_1       = DbContext.FixedSQLi(YOTEI_TANKA_1      );
                YOTEI_TANKA_2       = DbContext.FixedSQLi(YOTEI_TANKA_2      );
                LAST_RECEIPT_DATE_1 = DbContext.FixedSQLi(LAST_RECEIPT_DATE_1);
                LAST_RECEIPT_DATE_2 = DbContext.FixedSQLi(LAST_RECEIPT_DATE_2);
                LAST_ISSUE_DATE_1   = DbContext.FixedSQLi(LAST_ISSUE_DATE_1  );
                LAST_ISSUE_DATE_2   = DbContext.FixedSQLi(LAST_ISSUE_DATE_2  );
                STOCK_START_DATE_1  = DbContext.FixedSQLi(STOCK_START_DATE_1 );
                STOCK_START_DATE_2  = DbContext.FixedSQLi(STOCK_START_DATE_2 );
                STOCK_STOP_FlAG     = DbContext.FixedSQLi(STOCK_STOP_FlAG    );
                STOCK_STOP_DATE     = DbContext.FixedSQLi(STOCK_STOP_DATE    );
                PART_LOCATION       = DbContext.FixedSQLi(PART_LOCATION      );
                MAINT_TYPE          = DbContext.FixedSQLi(MAINT_TYPE         );

                //データのテーブル取得
                string sql = SqlTable.getSQLTableBase();

                //検索条件
                //PMマスタ系
                sql += SqlTable.getSQLBuhimei(PART_NO, PART_NAME_LOC1, PART_NO_OPTION);                                 //部品コード 部品名
                sql += SqlTable.getSQLHoshuZumei(MAINT_PART_NAME, MAINT_PART_NAME_OPTION,                               //保守部品名
                                                 DWG_NO,DWG_NO_OPTION);                                                 //図番
                
                sql += SqlTable.getSQLHyoujuuhakko(ISSUE_DATE_1, ISSUE_DATE_2,true);                                    //標準図発行日
                
                sql += SqlTable.getSQLPM_Mask(PRODUCT_CODE, PART_TYPE, PDM_TYPE,                                        //製品分類コード 部品区分 PDMタイプ
                                              MACHINE_TYPE, SELLING_PRICE_TYPE, MAKER_PART_NO);                         //機種 価格設定 メーカー型番
                
                sql += SqlTable.getSQLPCEntandSONotEnt(ckPCEntandSPNoEnt_Checked);                                      //PC登録済 かつ SP未登録
               
                //手配マスタ系
                sql += SqlTable.getSQLPPPMORDER_Mask(PLANT_NO, MFG_TYPE, STOCK_TYPE,                                    //工場区分 内外作区分 貯蔵区分
                                                    ARR_BRANCH_CODE, ARR_WHO,PO_BRANCH_CODE,                            //管理店所 在庫担当 発注店所
                                                    PO_WHO, BUCKET, ORDER_TYPE, ABC_TYPE,                               //発注担当 バケット 管理基準 ABC区分
                                                    STOCK_TYPE, ROUTING_CODE);                                          //在庫管理コード 工程コード
                
                //注文マスタ系
                sql += SqlTable.getSQLCHMSA_Mask(VENDOR_CODE, SG_CODE);                                                 //取引先コード 作業コード
                                                                                                                        //在庫マスタ系　【未完】倉庫情報未追加

                                                                                                                        
                sql += SqlTable.getSQLZKMS_Mask(ckUselWHCode_Checked , pWhCode,                                         //倉庫コードチェックボックス 倉庫【追加予定】
                                                LOCATION, SOKO_TANTO, PS_FLAG,                                          //置場/棚番　倉庫担当　P/S展開区分
                                                AUTO_PURCHASE_REQ, ckMoreZero_Checked, CURRENT_BALANCE_1,               //自動購入指示　在庫数チェックボックス　在庫数１
                                                CURRENT_BALANCE_2,eStockAmount_1, eStockAmount_2,                       //在庫数2　在庫金額1　在庫金額2
                                                YOTEI_TANKA_1, YOTEI_TANKA_2, ckNoReceipt_Checked,                      //標準単価1 標準単価2 最終入庫日チェックボックス
                                                LAST_RECEIPT_DATE_1,LAST_RECEIPT_DATE_2, ckNoIssue_Checked,             //最終入庫日１　最終入庫日2　最終出庫日チェックボックス
                                                LAST_ISSUE_DATE_1, LAST_ISSUE_DATE_2,STOCK_START_DATE_1,                //最終出庫日1 最終出庫日2 貯蔵開始日1
                                                STOCK_START_DATE_2, STOCK_STOP_FlAG, STOCK_STOP_DATE);                  // 貯蔵開始日2 貯蔵中止予定 貯蔵止め

                //保守マスタ系
                sql += SqlTable.getSQLPPPMMAINTMS_Mask(PART_LOCATION, MAINT_TYPE);
                //検索条件を追加
                sql += SqlTable.getSQLOption(ckPPPMMAINTMS2_notEdit_Checked, ckRepairRepEnt_Checked,
                                    ckPartDescAndRepReason_Checked, ckNoPhoto_Checked);
                sql += " order by PM.PART_NO ";
                //検索開始
                var result = DbContext.Database.SqlQuery<PMKensaku>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<DialogKoumoku> getDialogs(string CM_KOUNO, string START_DATE,string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);

                string sql = SqlTable.getSQLDialogKoumoku(CM_KOUNO, START_DATE, STOP_DATE);
                var result = DbContext.Database.SqlQuery<DialogKoumoku>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CMCODE> getCM_CODE(string CM_KOUNO,string START_DATE,string STOP_DATE,bool CM_CODE_ONLY)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);

                if (CM_CODE_ONLY)
                {
                    string sql = SqlTable.getSQLDialogKoumoku(CM_KOUNO, START_DATE, STOP_DATE);
                    var result = DbContext.Database.SqlQuery<CMCODE>(sql).ToList();
                    return result;
                }
            }
            return null;
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CM_KOUNOLIST> getSokoType(string CM_KOUNO)
        {
            using (var DbContext = new TablesDbContext())
            {
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);

                string sql = "SELECT CM_CODE,CM_CODE_SETUMEI from cmmsb where cm_kouno = '"+ CM_KOUNO +"' order by CM_CODE ";
                var result = DbContext.Database.SqlQuery<CM_KOUNOLIST>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CM_KOUNOLIST> getSokoInfo(string CM_KOUNO,string data3)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "";
                CM_KOUNO = DbContext.FixedSQLi(CM_KOUNO);
                data3 = DbContext.FixedSQLi(data3);
                if (data3 == "-")
                {
                    sql += "select CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO = '310' order by CM_CODE";
                }
                else
                {
                    sql += "select CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO = '" + CM_KOUNO + "'";
                    sql += " and data3 = '" + data3 + "' order by CM_CODE";
                }
                var result = DbContext.Database.SqlQuery<CM_KOUNOLIST>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<KouteiCode> getKouteiInfo(string KT_START_DATE, string KT_STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "";
                sql += "select * from ktktms where START_DATE <= '" + KT_START_DATE + "' and stop_date >= '" + KT_STOP_DATE + "' order by 1";
                var result = DbContext.Database.SqlQuery<KouteiCode>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<HeaderID> getHeaderInfo(string Table_Id)
        {
            using (var DbContext = new TablesDbContext())
            {
                Table_Id = DbContext.FixedSQLi(Table_Id);
                string sql = "";
                sql += "select PM.PART_NO,PM.PART_REV_NO,PM.PART_NAME_LOC1,PM.UPD_WHO,JNV.USER_NAME UPD_NAME,PM.UPD_WHEN,PM.ENT_WHO,JNV_ENT.USER_NAME ENT_NAME,PM.ENT_WHEN " +
                       ",PM.REV_START_DATE,PM.REV_STOP_DATE,PM.M_START_DATE,PM.M_STOP_DATE,PM.CUR_TYPE,PM.APP_CUR_TYPE " + " From PPPMMS PM ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV on PM.UPD_WHO = JNV.USER_ID ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV_ENT on PM.ENT_WHO = JNV_ENT.USER_ID ";
                sql += " where PM.PART_NO = '" + Table_Id + "' ";
                sql += " and PM.PART_REV_NO <= (select PART_REV_NO from pppmms where part_no ='" + Table_Id + "' and APP_CUR_TYPE =1)";
                var result = DbContext.Database.SqlQuery<HeaderID>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> getEditInfo(string Edit_PART_NO,string Edit_REV_NO,string USER_ID)
        {
            using (var DbContext = new TablesDbContext())
            {
                Edit_PART_NO = DbContext.FixedSQLi(Edit_PART_NO);
                Edit_REV_NO = DbContext.FixedSQLi(Edit_REV_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);
                //個人設定を確認
                string sql_2 = "";
                string sql_3 = "";
                bool CheckIndiviSet = false;

                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='PPPMMS' order by USER_ID";
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                foreach (var user in resultIndiviSet)
                {
                    //個人設定を確認できたら個人並び順に変更
                    if (USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }
                string MS_TABLE_PPPMMSSQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = 'PPPMMS' and MS_TABLE != '0'";
                var reusult_ms_table_pppmms = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLE_PPPMMSSQL).ToList();
                string sql_ms_table_pppmms = "";
                foreach(var str in reusult_ms_table_pppmms)
                {
                    if(sql_ms_table_pppmms =="" )
                    {
                        sql_ms_table_pppmms += "'" + str.MS_TABLE_Find + "'";
                    }
                    else
                    {
                        sql_ms_table_pppmms += ",'" + str.MS_TABLE_Find + "'";
                    }
                }

                string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                foreach (var str in result)
                {
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                foreach (var str in result_num)
                {
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }
                }

                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = 'PPPMMS' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";
                sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                sql_3 = sql_2;

                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '" + Edit_PART_NO;
                sql_2 += "' and PART_REV_NO = " + Edit_REV_NO + ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " left join (select CM_KOUNO,CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO in (select MS_ITEM_NO from  PPPMTABLEHDRMNG where table_name = 'PPPMMS' and MS_TABLE in ("
                            + sql_ms_table_pppmms + "))) MS_1 on MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and  MS_1.CM_CODE = NEWTABLE.FIELD_VALUE";

                sql_2 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME from SYDBGRID where DBGRID_NAME = 'PPPMMS' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";

                sql_3 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '" + Edit_PART_NO;
                sql_3 += "' and PART_REV_NO = " + Edit_REV_NO + ") UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_3 += " left join (select CM_KOUNO,CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO in (select MS_ITEM_NO from  PPPMTABLEHDRMNG where table_name = 'PPPMMS' and MS_TABLE in ("
                            + sql_ms_table_pppmms + "))) MS_1 on MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and  MS_1.CM_CODE = NEWTABLE.FIELD_VALUE";

                sql_3 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME from SYDBGRID where DBGRID_NAME = 'PPPMMS' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                result_2.AddRange(result_3);
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }


        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> getEditInfo2(string Edit_PART_NO, string USER_ID, string PLANT_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                Edit_PART_NO = DbContext.FixedSQLi(Edit_PART_NO);
                USER_ID = DbContext.FixedSQLi(USER_ID);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                //個人設定を確認
                string sql_2 = "";
                string sql_3 = "";
                bool CheckIndiviSet = false;
                string IndivSetSQL = "select DISTINCT USER_ID from SYDBGRID where DBGRID_NAME='PPPMORDER' order by USER_ID";
                var resultIndiviSet = DbContext.Database.SqlQuery<IndividualSettting>(IndivSetSQL).ToList();
                foreach (var user in resultIndiviSet)
                {
                    //個人設定を確認できたら個人並び順に変更
                    if (USER_ID == user.USER_ID)
                    {
                        CheckIndiviSet = true;
                    }
                }
                string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMORDER' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMORDER' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                foreach (var str in result)
                {
                    if (In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME;
                    }
                }
                foreach (var str in result_num)
                {
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME + "'";
                    }

                }

                string MS_TABLE_PPPMORDERSSQL = "select distinct MS_TABLE MS_TABLE_Find from PPPMTABLEHDRMNG where table_name = 'PPPMORDER' and MS_TABLE != '0'";
                var reusult_ms_table_pppmorder = DbContext.Database.SqlQuery<MS_TABLE>(MS_TABLE_PPPMORDERSSQL).ToList();
                string sql_ms_table_pppmorder = "";
                foreach (var str in reusult_ms_table_pppmorder)
                {
                    if (sql_ms_table_pppmorder == "")
                    {
                        sql_ms_table_pppmorder += "'" + str.MS_TABLE_Find + "'";
                    }
                    else
                    {
                        sql_ms_table_pppmorder += ",'" + str.MS_TABLE_Find + "'";
                    }
                }

                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE,MS_1.CM_CODE_SETUMEI FIELD_EXPLAIN from( ";
                sql_2 += "select TABLE_NAME, FIELD_NAME, MAX(AUTH_TYPE) AUTH_TYPE from PPPMTABLEAUTHMNG where ";
                sql_2 += "MNG_NO IN (select ROLE_ID from CPUMGSSO_USER_ROLE_MST where USER_ID = '" + USER_ID + "' ";
                sql_2 += "and ROLE_ID in ('1','2','3','4','5','6','7','8','9','10','99')) and TABLE_NAME = 'PPPMORDER' group by FIELD_NAME,TABLE_NAME";
                sql_2 += " ) PMTH ";
                sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME and PMHD.FIELD_NAME = PMTH.FIELD_NAME";

                sql_3 = sql_2;

                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMORDER where PART_NO =  '" + Edit_PART_NO;
                sql_2 += "' and PLANT_NO = '"+ PLANT_NO  + "' ) UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " left join (select CM_KOUNO,CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO in (select MS_ITEM_NO from  PPPMTABLEHDRMNG where table_name = 'PPPMORDER' and MS_TABLE in ("
                            + sql_ms_table_pppmorder + "))) MS_1 on MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and  MS_1.CM_CODE = NEWTABLE.FIELD_VALUE";

                sql_2 += " where PMTH.TABLE_NAME = 'PPPMORDER' ";
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where + ")";
                sql_2 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_2 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME from SYDBGRID where DBGRID_NAME = 'PPPMORDER' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  order by SYD.SEQ_NO"
                    : sql_2 + " order by PMHD.FIELD_SEQ_NO ";


                sql_3 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMORDER where PART_NO =  '" + Edit_PART_NO;
                sql_3 += "'  and PLANT_NO = '"+ PLANT_NO + "') UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_3 += " left join (select CM_KOUNO,CM_CODE,CM_CODE_SETUMEI from cmmsb where CM_KOUNO in (select MS_ITEM_NO from  PPPMTABLEHDRMNG where table_name = 'PPPMORDER' and MS_TABLE in ("
                            + sql_ms_table_pppmorder + "))) MS_1 on MS_1.CM_KOUNO = PMHD.MS_ITEM_NO and  MS_1.CM_CODE = NEWTABLE.FIELD_VALUE";

                sql_3 += " where PMTH.TABLE_NAME = 'PPPMORDER' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                sql_3 = CheckIndiviSet ?
                    "select SYD.SEQ_NO FIELD_SEQ_NO ,BASE.* from (" + sql_3 + ") BASE left join(select SEQ_NO, DBGRID_NAME, FIELD_NAME from SYDBGRID where DBGRID_NAME = 'PPPMORDER' and USER_ID = '" + USER_ID +
                    "') SYD on BASE.FIELD_NAME = SYD.FIELD_NAME where SYD.FIELD_NAME IS NOT NULL  order by SYD.SEQ_NO"
                    : sql_3 + " order by PMHD.FIELD_SEQ_NO ";


                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                result_2.AddRange(result_3);
                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));
                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<EditInfo> GetTestdata(string Test)
        {
            using (var DbContext = new TablesDbContext())
            {
                string sql = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE != 'NUMBER' order by INTERNAL_COLUMN_ID";
                string sql_num = "SELECT COLUMN_NAME FROM all_tab_cols where TABLE_NAME = 'PPPMMS' and DATA_TYPE = 'NUMBER' order by INTERNAL_COLUMN_ID";
                string In_Con = "";
                string In_Con_Num = "";
                string In_Con_Num_where = "";
                var result = DbContext.Database.SqlQuery<TESTCLASS>(sql).ToList();
                var result_num = DbContext.Database.SqlQuery<TESTCLASS>(sql_num).ToList();
                foreach (var str in result)
                {
                    if(In_Con == "")
                    {
                        In_Con = str.COLUMN_NAME;
                    }
                    else
                    {
                        In_Con += "," + str.COLUMN_NAME ;
                    }
                }
                foreach (var str in result_num)
                {
                    if (In_Con_Num == "")
                    {
                        In_Con_Num = str.COLUMN_NAME;
                        In_Con_Num_where = "'" + str.COLUMN_NAME + "'";
                    }
                    else
                    {
                        In_Con_Num += "," + str.COLUMN_NAME;
                        In_Con_Num_where += ",'" + str.COLUMN_NAME +"'";
                    }
                }
                
                string sql_2 = "";

                sql_2 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE from PPPMTABLEAUTHMNG PMTH ";
                sql_2 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME ";
                sql_2 += " and PMHD.FIELD_NAME = PMTH.FIELD_NAME ";

                sql_2 += "  LEFT JOIN ( select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '5310GAF001' and PART_REV_NO ='000' )";
                sql_2 += " UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_2 += " IN(" + In_Con + " ))) ";
                sql_2 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";

                sql_2 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_2 += " and PMTH.MNG_NO = '1' ";
                sql_2 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME NOT IN (" + In_Con_Num_where +")";
                sql_2 += " order by PMTH.FIELD_SEQ_NO ";
                var result_2 = DbContext.Database.SqlQuery<EditInfo>(sql_2).ToList();

                string sql_3 = "";

                sql_3 += "select PMTH.AUTH_TYPE,PMHD.*,NEWTABLE.FIELD_VALUE from PPPMTABLEAUTHMNG PMTH ";
                sql_3 += " full join PPPMTABLEhdrMNG PMHD on PMHD.TABLE_NAME = PMTH.TABLE_NAME ";
                sql_3 += " and PMHD.FIELD_NAME = PMTH.FIELD_NAME ";

                sql_3 += " LEFT JOIN (select FIELD_NAME,FIELD_VALUE From (select * FROM PPPMMS where PART_NO =  '5310GAF001' and PART_REV_NO ='000' )";
                sql_3 += " UNPIVOT  INCLUDE NULLS(FIELD_VALUE FOR FIELD_NAME ";
                sql_3 += " IN(" + In_Con_Num + " ))) ";
                sql_3 += " NEWTABLE on PMTH.FIELD_NAME = NEWTABLE.FIELD_NAME ";
                
                sql_3 += " where PMTH.TABLE_NAME = 'PPPMMS' ";
                sql_3 += " and PMTH.MNG_NO = '1' ";
                sql_3 += " and AUTH_TYPE <> 0 and PMHD.FIELD_NAME IN (" + In_Con_Num_where + ")";
                sql_3 += " order by PMTH.FIELD_SEQ_NO ";
                var result_3 = DbContext.Database.SqlQuery<EditInfo>(sql_3).ToList();
                result_2.AddRange(result_3);

                result_2.Sort((a, b) => Int32.Parse(a.FIELD_SEQ_NO) - Int32.Parse(b.FIELD_SEQ_NO));

                return result_2;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<CHOUMON> GetChoumon(string CH_KOUNO,string START_DATE,string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                CH_KOUNO = DbContext.FixedSQLi(CH_KOUNO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);
                string sql = "select CH_CODE,CH_CODE_SETUMEI_1,START_DATE,STOP_DATE from CHCDMS where CH_KOUNO ='"+ CH_KOUNO + "'AND START_DATE <= '" +START_DATE +"' AND STOP_DATE >= '"+STOP_DATE +"' ORDER BY CH_CODE";
                var result = DbContext.Database.SqlQuery<CHOUMON>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<TANTOU> GetTantou(string TANTO_KUBUN, string PLANT_NO, string START_DATE,string STOP_DATE)
        {
            using (var DbContext = new TablesDbContext())
            {
                TANTO_KUBUN = DbContext.FixedSQLi(TANTO_KUBUN);
                PLANT_NO = DbContext.FixedSQLi(PLANT_NO);
                START_DATE = DbContext.FixedSQLi(START_DATE);
                STOP_DATE = DbContext.FixedSQLi(STOP_DATE);
                string sql = "select TM.TANTO_CODE,TM.USER_ID,JNV.USER_NAME,TM.START_DATE,TM.STOP_DATE from CMTANTOMS TM ";
                sql += " left join JNV_JNSHAIN_01 JNV on TM.USER_ID = JNV.USER_ID ";
                sql += " where START_DATE <= '" + START_DATE + "' AND STOP_DATE >= '" + STOP_DATE + "' ";
                sql += " AND TANTO_KUBUN = '" + TANTO_KUBUN + "' AND PLANT_NO = '" + PLANT_NO + "' ";
                sql += " ORDER BY TANTO_CODE";
                var result = DbContext.Database.SqlQuery<TANTOU>(sql).ToList();
                return result;
            }
        }
        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<TANTAI>GetTantai(string LANGU)
        {
            using (var DbContext = new TablesDbContext())
            {
                LANGU = DbContext.FixedSQLi(LANGU);
                string sql = "select * from NRTANIMS where  LANGUAGE  = '" +LANGU +"'";
                var result = DbContext.Database.SqlQuery<TANTAI>(sql).ToList();
                return result;
            }
        }

        [HttpGet]
        [Route("api/KensakuBtnGet")]
        public List<Pic>GetPic(string PIC_PART_NO)
        {
            using (var DbContext = new TablesDbContext())
            {
                PIC_PART_NO = DbContext.FixedSQLi(PIC_PART_NO);
                string sql = "select DOC_FILE_NAME from pppmdocms where PART_NO = '" + PIC_PART_NO+"'";
                var result = DbContext.Database.SqlQuery<Pic>(sql).ToList();
                return result;
            }
        }

        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMMS")]
        // POST api/<controller>
        public void Post_PPPMMs(POST_PPPMMS PM)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = PM.GetType().GetProperties();
                foreach (var item in PM.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if(item.GetValue(PM) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PM).ToString());
                    }
                }
                int index = -1;
                foreach(var item in list_value)
                {
                    index += 1;
                    if(item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PART_REV_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] +" = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }
                    
                }
                
                string sql = "UPDATE PPPMMS "+ Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(PM.PART_NO) + "' and PART_REV_NO = '" + DbContext.FixedSQLi(PM.PART_REV_NO) + "'";

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }
        [HttpPost]
        [Route("api/KensakuBtnPost/PPPMORDER")]
        // POST api/<controller>
        public void Post_PPPMORDER(POST_PPPMORDER PO)
        {
            using (var DbContext = new TablesDbContext())
            {
                string Set_sql = "";
                var list_name = new List<string>() { };
                var list_value = new List<string>() { };
                var typePM = PO.GetType().GetProperties();
                foreach (var item in PO.GetType().GetProperties())
                {
                    list_name.Add(item.Name);
                    if (item.GetValue(PO) == null)
                    {
                        list_value.Add("null");
                    }
                    else
                    {
                        list_value.Add(item.GetValue(PO).ToString());
                    }
                }
                int index = -1;
                foreach (var item in list_value)
                {
                    index += 1;
                    if (item != "null" && list_name[index] != "PART_NO" && list_name[index] != "PLANT_NO")
                    {
                        if (Set_sql == "")
                        {
                            Set_sql += "SET " + list_name[index] + " = '" + item + "' ";
                        }
                        else
                        {
                            Set_sql += ", " + list_name[index] + " = '" + item + "' ";
                        }
                    }

                }
                string PLANT_NO_List = "";
                foreach (var item in PO.PLANT_NO)
                {
                    var item_1 ="";
                    item_1 = DbContext.FixedSQLi(item);
                    if (PLANT_NO_List == "")
                    {
                        PLANT_NO_List += "'" + item_1 + "'";
                    }
                    else
                    {
                        PLANT_NO_List += ",'" + item_1 + "'";
                    }
                }
                string sql = "UPDATE PPPMORDER " + Set_sql + " where PART_NO = '" + DbContext.FixedSQLi(PO.PART_NO) + "' and PLANT_NO in ( " + PLANT_NO_List + ")";

                DbContext.Database.ExecuteSqlCommand(sql);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        // SQLインジェクション対策
    }
}