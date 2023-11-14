using System.ComponentModel.DataAnnotations;
using System;

namespace EDalolatnoma.MVC.Requests
{
    public class DalolatnomaAddRequest
    {
        public string SellerTin { get; set; }//Сотувчи (етказиб берувчи) СТИРи 
        public string SellerName { get; set; }//Ташкилот нами
        public string BuyerTin { get; set; }//Харидор СТИРи
        public string BuyerName { get; set; }//Харидор нами
        public DateTime ContractDate { get; set; }//Шартнома санаси
        public string ContractNumber { get; set; }//Шартнома рақами
        public double GasAmount { get; set; }//Етказиб берилган газ миқдари
        public DateTime ActDate { get; set; }//Далолатнома санаси 
        public string ActNumber { get; set; }//Далолатнома рақами
        public string MeterNumber { get; set; }//Ўрнатилган газ ҳисоблагич рақами
        public string GasMeterNetwork { get; set; }//Газ ўлчаш тармоғи номи
        public DateTime BeginDate { get; set; }//Табиий газ етказиб бериш бошланган сана
        public DateTime EndDate { get; set; }//Табиий газ етказиб бериш тугаш санаси
    }
}
