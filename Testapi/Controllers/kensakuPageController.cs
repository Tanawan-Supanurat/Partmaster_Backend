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
                string sql = "";
                sql += "select PM.PART_NO,PM.PART_REV_NO,PM.PART_NAME_LOC1,PM.UPD_WHO,JNV.USER_NAME UPD_NAME,PM.UPD_WHEN,PM.ENT_WHO,JNV_ENT.USER_NAME ENT_NAME,PM.ENT_WHEN " +
                       ",PM.REV_START_DATE,PM.REV_STOP_DATE,PM.M_START_DATE,PM.M_STOP_DATE,PM.CUR_TYPE,PM.APP_CUR_TYPE " + " From PPPMMS PM ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV on PM.UPD_WHO = JNV.USER_ID ";
                sql += " FULL JOIN JNV_JNSHAIN_01 JNV_ENT on PM.ENT_WHO = JNV_ENT.USER_ID ";
                sql += " where PM.PART_NO = '" + Table_Id + "'";
                var result = DbContext.Database.SqlQuery<HeaderID>(sql).ToList();
                return result;
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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