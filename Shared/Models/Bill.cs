using System;

namespace ERacuniNovi.Shared.Models
{
    public class Bill
    {

        public int ID { get; set; } //ID u tabeli 
        public string Barcode { get; set; }  //Barcode za slanje postom
        public string Naziv { get; set; } //Koristimo za kucu , banku , rashod, postu
        public DateTime DatumSlanja { get; set; }   //Koristimo za kucu , banku , rashod, postu //kod poste je datetime.now
        public DateTime DatumPrimanja { get; set; } //Koristimo za kucu , banku , rashod, postu
        public double NabavnaCena { get; set; } //Koristimo za kucu, banku i postu
        public double ProdajnaCena { get; set; } //Koristimo za kucu, banku i postu
        public double Postarina { get; set; } //Koristimo za kucu, banku i postu
        public double Rashod { get; set; } // //Koristimo za kucu, banku i postu i rashod
        public string AdresaMusterije { get; set; } //Koristimo za postu
        public double PresekStanja { get; set; } //Koristimo samo za banku
        public enum WayOfSelling { Posta, Banka, Kuca, Rashod }
        public WayOfSelling WayOfSellingEnum { get; set; } //Koristimo kao filter
        public bool StatusRacuna { get; set; } //Koristimo za postu 
        //public double Zarada  { get=> NabavnaCena - ProdajnaCena - Postarina -Rashod; } //zarada kod poste se koristi, verovatno i kod banke i kuce
        //public enum StatusRacuna { Poslato, Vraceno, Isporuceno }
        //public StatusRacuna StatusRacunaEnum { get; set; }

        public Bill ShallowCopy()
        {
            return (Bill)this.MemberwiseClone();
        }


    }
}