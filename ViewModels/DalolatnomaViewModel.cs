using System;

namespace EDalolatnoma.MVC.ViewModels
{
    public class DalolatnomaViewModel
    {
        public int Id { get; set; } 
        public string SellerCompany { get; set; }
        public string BuyerCompany { get; set; }  
        public string ContractNumberDate { get; set; }
        public string ActNumberDate { get; set; } 
        public string GasMeterNetwork { get; set; } 
        public double GasAmount { get; set; }
        public string BeginDateEndDate { get; set; }
        public string SendStatus { get; set; }


    }
}
