using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDalolatnoma.MVC.Models
{
    public class Dalolatnoma:BaseModel
    {
        public int Id { get; set; }
        public int SellerCompanyId { get; set; }//Сотувчи (етказиб берувчи) СТИРи 
        [ForeignKey("SellerCompanyId")]
        public Company SellerCompany { get; set; }//Харидор СТИРи 
        public int BuyerCompanyId { get; set; }//Харидор СТИРи

        [ForeignKey("BuyerCompanyId")]
        public Company BuyerCompany { get; set; }//Харидор СТИРи
        public DateTime ContractDate { get; set; }//Шартнома санаси
        public string ContractNumber { get; set; }//Шартнома рақами
        public double GasAmount { get; set; }//Етказиб берилган газ миқдари
        public DateTime ActDate { get; set; }//Далолатнома санаси 
        public string ActNumber { get; set; }//Далолатнома рақами
        public string MeterNumber { get; set; }//Ўрнатилган газ ҳисоблагич рақами
        public string GasMeterNetwork { get; set; }//Газ ўлчаш тармоғи номи
        public DateTime BeginDate { get; set; }//Табиий газ етказиб бериш бошланган сана
        public DateTime EndDate { get; set; }//Табиий газ етказиб бериш тугаш санаси
        public DalolatnomaStatus Status { get; set; } = DalolatnomaStatus.Created;//Газ ўлчаш тармоғи номи 
        public string ResponseMessage { get; set; } 
        public DateTime? ResponseDatetime { get; set; } 
    }
    public enum DalolatnomaStatus
    {
        Created,
        SendSuccessfully, 
        Rejected,
        ErrorSending
    }
}
