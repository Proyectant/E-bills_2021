using BillsNamespace;
using ERacuniNovi.Shared.Models;
using Google.Protobuf.WellKnownTypes;
using ERacuniNovi.Shared.DTO;
using System;

namespace ERacuniNovi.Shared.Konvertor
{
    public class ConvertGRPC
    {
        #region Konvertori za ceo racun

        public BillMsg ConvertCeoRacun(Bill racun)
        {
            BillMsg racunproto = new BillMsg
            {
                Barcode = racun.Barcode,
                Naziv = racun.Naziv,
                DatumSlanja = DateTime.SpecifyKind(racun.DatumSlanja, DateTimeKind.Utc).ToTimestamp(),
                DatumPrimanja = DateTime.SpecifyKind(racun.DatumPrimanja, DateTimeKind.Utc).ToTimestamp(),
                NabavnaCena = racun.NabavnaCena,
                ProdajnaCena = racun.ProdajnaCena,
                Postarina = racun.Postarina,
                Rashod = racun.Rashod,
                AdresaMusterije = racun.AdresaMusterije,
                PresekStanja = racun.PresekStanja,
                StatusRacuna = racun.StatusRacuna,
                WayOfSelling = (int)racun.WayOfSellingEnum,
                ID = racun.ID
            };
            return racunproto;
        }

        public Bill ConvertCeoRacun(BillMsg racunproto)
        {
            Bill racun = new Bill
            {
                Barcode = racunproto.Barcode,
                Naziv = racunproto.Naziv,
                DatumSlanja = racunproto.DatumSlanja.ToDateTime(),
                DatumPrimanja = racunproto.DatumPrimanja.ToDateTime(),
                NabavnaCena = racunproto.NabavnaCena,
                ProdajnaCena = racunproto.ProdajnaCena,
                Postarina = racunproto.Postarina,
                Rashod = racunproto.Rashod,
                AdresaMusterije = racunproto.AdresaMusterije,
                PresekStanja = racunproto.PresekStanja,
                StatusRacuna = racunproto.StatusRacuna,
                WayOfSellingEnum = (Bill.WayOfSelling)racunproto.WayOfSelling,
                ID=racunproto.ID
            };
            return racun;
        }

        #endregion Konvertori za ceo racun

        #region Konvertori za Postu

        public PostaBillMsg ConvertPosta(Bill racunposta)
        {
            PostaBillMsg racunpostaproto = new PostaBillMsg
            {
                Barcode = racunposta.Barcode,
                Naziv = racunposta.Naziv,
                NabavnaCena = racunposta.NabavnaCena,
                ProdajnaCena = racunposta.ProdajnaCena,
                Postarina = racunposta.Postarina,
                Rashod = racunposta.Rashod,
                StatusRacuna = racunposta.StatusRacuna,
                AdresaMusterije = racunposta.AdresaMusterije,
                DatumSlanja = DateTime.SpecifyKind(racunposta.DatumSlanja, DateTimeKind.Utc).ToTimestamp(),
                DatumPrimanja = DateTime.SpecifyKind(racunposta.DatumPrimanja, DateTimeKind.Utc).ToTimestamp()
            };
            return racunpostaproto;
        }

        public Bill ConvertPosta(PostaBillMsg racunpostaproto)
        {
            Bill racunposta = new Bill
            {
                Barcode = racunpostaproto.Barcode,
                Naziv = racunpostaproto.Naziv,
                NabavnaCena = racunpostaproto.NabavnaCena,
                ProdajnaCena = racunpostaproto.ProdajnaCena,
                Postarina = racunpostaproto.Postarina,
                Rashod = racunpostaproto.Rashod,
                StatusRacuna = racunpostaproto.StatusRacuna,
                AdresaMusterije = racunpostaproto.AdresaMusterije,
                DatumSlanja=racunpostaproto.DatumSlanja.ToDateTime(),
                DatumPrimanja=racunpostaproto.DatumPrimanja.ToDateTime()
            };
            return racunposta;
        }

        #endregion Konvertori za Postu

        #region Konvertori za Rashod
        public RashodBillMsg ConvertRashod(Bill racunrashod)
        {
            RashodBillMsg racunrashodproto = new RashodBillMsg
            { Naziv= racunrashod.Naziv,
            DatumSlanja= DateTime.SpecifyKind(racunrashod.DatumSlanja, DateTimeKind.Utc).ToTimestamp(),
            Rashod=racunrashod.Rashod,
            ID=racunrashod.ID
            };
            return racunrashodproto;
        }

        public Bill ConvertRashod(RashodBillMsg racunrashodproto)
        {
            Bill racunrashod = new Bill
            {
                Naziv=racunrashodproto.Naziv,
                DatumSlanja=racunrashodproto.DatumSlanja.ToDateTime(),
                Rashod=racunrashodproto.Rashod,
                ID=racunrashodproto.ID
            };
            return racunrashod;
        }
        #endregion

        #region Konvertori za Banku

            public BankaBillMsg ConvertBanka(Bill racunBanka) //saljem na server
        {
            BankaBillMsg racunBankaProto = new BankaBillMsg
            {
                Naziv = racunBanka.Naziv, //Naziv artikla sta salje
                AdresaMusterije = racunBanka.AdresaMusterije,
                NabavnaCena = racunBanka.NabavnaCena, //Ono sto mu je uplaceno
                ProdajnaCena = racunBanka.ProdajnaCena, //Ono sto je podigao
                PresekStanja = racunBanka.PresekStanja, //KOliko mu je ostalo
                DatumSlanja = DateTime.SpecifyKind(racunBanka.DatumSlanja, DateTimeKind.Utc).ToTimestamp(),//kad je prodao
                DatumPrimanja = DateTime.SpecifyKind(racunBanka.DatumPrimanja, DateTimeKind.Utc).ToTimestamp(),//kad je podigao novac

        };
            return racunBankaProto;
        }


        public Bill ConvertBanka(BankaBillMsg racunBankaProto) //saljem klijentu
        {
            Bill racunBanka = new Bill
            {
                Naziv = racunBankaProto.Naziv, //Naziv artikla sta salje
                AdresaMusterije = racunBankaProto.AdresaMusterije,
                NabavnaCena = racunBankaProto.NabavnaCena, //Ono sto mu je uplaceno
                ProdajnaCena = racunBankaProto.ProdajnaCena, //Ono sto je podigao
                PresekStanja = racunBankaProto.PresekStanja, //KOliko mu je ostalo
                DatumSlanja = racunBankaProto.DatumSlanja.ToDateTime(),//kad je prodao
                DatumPrimanja = racunBankaProto.DatumPrimanja.ToDateTime(),//kad je podigao novac
            };
            return racunBanka;
        }



        #endregion Konvertori za Banku

        #region Konvertori za Kucu

        public KucaBillMsg ConvertKuca(Bill racunKuca) //saljem na server
        {
            KucaBillMsg racunKucaProto = new KucaBillMsg
            {
                ID = racunKuca.ID, // ID racuna
                Naziv = racunKuca.Naziv, //Naziv artikla sta prodaje
                AdresaMusterije = racunKuca.AdresaMusterije, // Adresa musterije kome prodaje
                DatumSlanja = DateTime.SpecifyKind(racunKuca.DatumSlanja, DateTimeKind.Utc).ToTimestamp(),//kad je poslao
                NabavnaCena = racunKuca.NabavnaCena, //Koliko je platio artikal
                ProdajnaCena = racunKuca.ProdajnaCena, //Koliko je prodao artkal
                Rashod = racunKuca.Rashod //Koliko je imao troska ili mu umanjio cenu
            };
            return racunKucaProto;
        }


        public Bill ConvertKuca(KucaBillMsg racunKucaProto) //saljem klijentu
        {
            Bill racunKuca = new Bill
            {
                ID = racunKucaProto.ID, // ID racuna
                Naziv = racunKucaProto.Naziv, //Naziv artikla sta salje
                AdresaMusterije = racunKucaProto.AdresaMusterije, // Adresa musterije kome prodaje
                DatumSlanja = racunKucaProto.DatumSlanja.ToDateTime(),//kad je prodao
                NabavnaCena = racunKucaProto.NabavnaCena, //Ono sto mu je uplaceno
                ProdajnaCena = racunKucaProto.ProdajnaCena, //Ono sto je podigao
                Rashod = racunKucaProto.Rashod //Koliko je imao troska ili mu umanjio cenu
            };
            return racunKuca;
        }



        #endregion Konvertori za Kucu

        #region Konvertori za Izvestaj
        public IzvestajBillMsg ConvertIzvestaj(KlasaIzvestaj izvestaj)
        {
            IzvestajBillMsg izvestajproto = new IzvestajBillMsg
            {
                Start = DateTime.SpecifyKind(izvestaj.Start, DateTimeKind.Utc).ToTimestamp(),
                End = DateTime.SpecifyKind(izvestaj.End, DateTimeKind.Utc).ToTimestamp(),
                NacinSlanja = izvestaj.NacinSlanja,
                TipDatuma=izvestaj.TipDatuma

            };
            return izvestajproto;
        }

        public KlasaIzvestaj ConvertIzvestaj(IzvestajBillMsg izvestajproto)
        {
            KlasaIzvestaj izvestaj = new KlasaIzvestaj
            {
                Start = izvestajproto.Start.ToDateTime(),
                End = izvestajproto.End.ToDateTime(),
                NacinSlanja = izvestajproto.NacinSlanja,
                TipDatuma = izvestajproto.TipDatuma
            };
            return izvestaj;
        }

        #endregion
    }
}