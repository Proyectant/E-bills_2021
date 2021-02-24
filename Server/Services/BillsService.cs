using ERacuniNovi.Server.Context;
using ERacuniNovi.Shared.Konvertor;
using ERacuniNovi.Shared.Models;
using BillsNamespace;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;

namespace ERacuniNovi.Server.Services
{
    public class BillsService : BillProtoServis.BillProtoServisBase
    {
        private readonly ILogger<BillsService> _logger;
        private readonly BillDBContext _context;
        private readonly ConvertGRPC _convert;

        public BillsService(ILogger<BillsService> log, BillDBContext context, ConvertGRPC convert)
        {
            _logger = log;
            _context = context;
            _convert = convert;
        }

        #region Posta Taskovi
        public override async Task<BillPostResponse> AddBillPosta(PostaBillMsg request, ServerCallContext context)
        {

            _logger.LogInformation("Usao u dodavanje racuna koji je poslat postom");
            await _context.Bills.ToListAsync();
            Bill racun = _convert.ConvertPosta(request);
            Console.WriteLine("ajdi racuna");
            Console.WriteLine(racun.ID);
            racun.WayOfSellingEnum = Bill.WayOfSelling.Posta;
            racun.DatumSlanja = DateTime.Now;
            Console.WriteLine(racun.AdresaMusterije);
            try
            {
                if (_context.Bills.Where(b => b.Barcode.ToLower() == racun.Barcode.ToLower()).FirstOrDefault() is not null) //ako ima ovakav u bazi
                {
                    _logger.LogInformation("Nije Dodao Racun sa Barcodom" + racun.Barcode + " jer postoji u bazi!");
                    return new BillPostResponse { IsUpisan = false };
                }
                else //ako nema ovakav u bazi
                {
                    _context.Bills.Add(racun);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Dodao Racun sa Barcodom" + racun.Barcode);
                    return new BillPostResponse { IsUpisan = true, BillResponse = _convert.ConvertCeoRacun(racun) };
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("usao u catch");
                _logger.LogError(ex.Message);
                return new BillPostResponse { IsUpisan = false };
            }
        }


        public override async Task<ChangeBillPostaResponse> ChangeBillPosta(PostaBillMsg request, ServerCallContext context)
        {
            var comparativeBill = _context.Bills.Where(b => b.Barcode.ToLower() == request.Barcode.ToLower()).FirstOrDefault(); //nasao ga u bazi
            Console.WriteLine(request.DatumSlanja);
            Console.WriteLine(request.DatumPrimanja);

            _logger.LogInformation($"Nadjen je racun sa tim barcodom u bazi i barcode je {comparativeBill.Barcode}");

            if (comparativeBill is not null)
            {
                _logger.LogInformation("usao sam u if comparativeBill is not null");
                _logger.LogInformation("ovaj Bill sto sam poslao sam smestio u comparativeBill");
                try
                {
                    _logger.LogInformation("Usao sam u try da sacuvam to u bazu");
                    //comparativeBill = _convert.ConvertPosta(request);
                    //barcode se ne menja jer ga po tome trazi u bazi i ako ga promeni
                    //nikada ga nece naci
                    comparativeBill.Naziv = _convert.ConvertPosta(request).Naziv;
                    comparativeBill.AdresaMusterije = _convert.ConvertPosta(request).AdresaMusterije;
                    comparativeBill.NabavnaCena = _convert.ConvertPosta(request).NabavnaCena;
                    comparativeBill.ProdajnaCena = _convert.ConvertPosta(request).ProdajnaCena;
                    comparativeBill.Postarina = _convert.ConvertPosta(request).Postarina;
                    comparativeBill.DatumSlanja = _convert.ConvertPosta(request).DatumSlanja;
                    comparativeBill.DatumPrimanja = _convert.ConvertPosta(request).DatumPrimanja;
                    comparativeBill.Rashod = _convert.ConvertPosta(request).Rashod;
                    await _context.SaveChangesAsync();

                    return new ChangeBillPostaResponse { BillResponse = _convert.ConvertCeoRacun(comparativeBill), IsChanged = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError("Nije nasao racun u bazi da ga izmeni");
                    return new ChangeBillPostaResponse { IsChanged = false };
                }
            }
            else
            {
                _logger.LogInformation("comparativeBill je null, nema takvog u bazi, ovo ne treba da se dogadja");
                return new ChangeBillPostaResponse { IsChanged = false };
            }
        }


        public override async Task<DeleteBillPostaResponse> DeleteBillPosta(DeleteBillPostaRequest request, ServerCallContext context)
        {
            await _context.Bills.ToListAsync();
            if ((await _context.Bills.Where(b => b.Barcode.ToLower() == request.Barcode.ToLower()).FirstOrDefaultAsync()) is not null)
            {
                _logger.LogInformation($"Brisemo racun sa sledecim Barcodom   {(await _context.Bills.Where(b => b.Barcode.ToLower() == request.Barcode.ToLower()).FirstOrDefaultAsync()).Barcode}");
                _context.Bills.Remove(await _context.Bills.Where(b => b.Barcode.ToLower() == request.Barcode.ToLower()).FirstOrDefaultAsync());

                try
                {
                    await _context.SaveChangesAsync();
                    return new DeleteBillPostaResponse { Status = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new DeleteBillPostaResponse { Status = false };
                }
            }
            else
            {
                return new DeleteBillPostaResponse { Status = false };
            }
        }

        public override async Task GetAllPostaBills(EmptyMsg request, IServerStreamWriter<BillMsg> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("[Usli smo u GetAllBills]");
            //var RacuniPosta = _context.Bills.Where(b => b.WayOfSellingEnum == Bill.WayOfSelling.Posta);
            try
            {
                foreach (var item in _context.Bills.Where(b => b.WayOfSellingEnum == Bill.WayOfSelling.Posta))
                {
                    _logger.LogInformation("[Poceo je stream]");
                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            _logger.LogInformation("[zzzzzzzavrsio sam strimovanje]");
        }
        #endregion


        #region Rashod Taskovi
        public override async Task<BillPostResponse> AddBillRashod(RashodBillMsg request, ServerCallContext context)
        {
            DateTime defaultDateTime = new DateTime(0001, 1, 1, 0, 0, 0);
            
            _logger.LogInformation("Usao u dodavanje racuna koji je poslat postom");

            try
            {
                await _context.Bills.ToListAsync();
                Bill racun = _convert.ConvertRashod(request);
                racun.WayOfSellingEnum = Bill.WayOfSelling.Rashod;
                racun.Naziv ??= string.Empty;
                racun.Barcode ??= string.Empty;
                racun.AdresaMusterije ??= string.Empty;
                
                if (DateTime.Compare(racun.DatumSlanja, defaultDateTime) == 0)
                {
                    racun.DatumSlanja = DateTime.Now;
                }
                racun.DatumPrimanja = racun.DatumSlanja;
                await _context.Bills.AddAsync(racun);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Dodao Racun sa Ajdijem" + racun.ID);
                    return new BillPostResponse { IsUpisan = true, BillResponse = _convert.ConvertCeoRacun(racun) };    
            }
            catch (Exception ex)
            {
                _logger.LogInformation("usao u catch");
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                return new BillPostResponse { IsUpisan = false };
            }

        }

        public override async Task<ChangeBillRashodResponse> ChangeBillRashod(RashodBillMsg request, ServerCallContext context)
        {
            var comparativeBill = _context.Bills.Find(request.ID); //nasao ga u bazi


            _logger.LogInformation($"Nadjen je racun sa tim barcodom u bazi i barcode je {comparativeBill.ID}");

            if (comparativeBill is not null)
            {
                _logger.LogInformation("usao sam u if comparativeBill is not null");
                _logger.LogInformation("ovaj Bill sto sam poslao sam smestio u comparativeBill");
                try
                {
                    _logger.LogInformation("Usao sam u try da sacuvam to u bazu");

                    comparativeBill.Naziv = _convert.ConvertRashod(request).Naziv;
                    comparativeBill.DatumSlanja = _convert.ConvertRashod(request).DatumSlanja;
                    comparativeBill.Rashod = _convert.ConvertRashod(request).Rashod;
                    await _context.SaveChangesAsync();

                    return new ChangeBillRashodResponse { BillResponse = _convert.ConvertCeoRacun(comparativeBill), IsChanged = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError("Nije nasao racun u bazi da ga izmeni");
                    return new ChangeBillRashodResponse { IsChanged = false };
                }
            }
            else
            {
                _logger.LogInformation("comparativeBill je null, nema takvog u bazi, ovo ne treba da se dogadja");
                return new ChangeBillRashodResponse { IsChanged = false };
            }
        }

        public override async Task<DeleteBillRashodResponse> DeleteBillRashod(DeleteBillRequest request, ServerCallContext context)
        {
            await _context.Bills.ToListAsync();

            if (await _context.Bills.FindAsync(request.ID) is not null)
            {
                _logger.LogInformation($"Brisemo racun sa sledecim Ajdijem   {_context.Bills.Find(request.ID).ID}");
                _context.Bills.Remove(await _context.Bills.FindAsync(request.ID));

                try
                {
                    await _context.SaveChangesAsync();
                    return new DeleteBillRashodResponse { Status = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new DeleteBillRashodResponse { Status = false };
                }
            }
            else
            {
                return new DeleteBillRashodResponse { Status = false };
            }
        }

        public override async Task GetAllRashodBill(EmptyMsg request, IServerStreamWriter<BillMsg> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("[Usli smo u GetAllBills]");
            try
            {
                foreach (var item in _context.Bills.Where(b => b.WayOfSellingEnum == Bill.WayOfSelling.Rashod))
                {
                    _logger.LogInformation("[Poceo je stream]");
                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            _logger.LogInformation("[zzzzzzzavrsio sam strimovanje]");
        }

        #endregion

        #region Banka Taskovi

        public override async Task<BillPostResponse> AddBillBanka(BankaBillMsg request, ServerCallContext context)
        {

            _logger.LogInformation("Usao u dodavanje racuna koji je poslat bankom");
            await _context.Bills.ToListAsync();
            Bill racunBanka = _convert.ConvertBanka(request);

            racunBanka.WayOfSellingEnum = Bill.WayOfSelling.Banka;
            racunBanka.Barcode ??= string.Empty;
            try
            {
                _context.Bills.Add(racunBanka);
                await _context.SaveChangesAsync();
                return new BillPostResponse { IsUpisan = true, BillResponse = _convert.ConvertCeoRacun(racunBanka) };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Nije upisao!");
                _logger.LogError(ex.Message);
                return new BillPostResponse { IsUpisan = false };

            }
        }
        public override async Task GetAllBankaBills(EmptyMsg request, IServerStreamWriter<BillMsg> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Usli smo u GetAllBankaBills");
            var racuniBanka = _context.Bills.Where(a => a.WayOfSellingEnum == Bill.WayOfSelling.Banka).ToList();
            try
            {
                foreach (var item in racuniBanka)
                {
                    _logger.LogInformation("poceo stream");
                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(item));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        #endregion

        #region Kuća Taskovi

        public override async Task<BillPostResponse> AddBillKuca(KucaBillMsg request, ServerCallContext context)
        {


            _logger.LogInformation("Usao u dodavanje racuna od kuce");

            try
            {
                DateTime defaultDateTime = new DateTime(0001, 1, 1, 0, 0, 0);
                await _context.Bills.ToListAsync();
                Bill racun = _convert.ConvertKuca(request);
                racun.WayOfSellingEnum = Bill.WayOfSelling.Kuca;
                racun.Naziv ??= string.Empty;
                racun.Barcode ??= string.Empty;
                racun.AdresaMusterije ??= string.Empty;
                if (DateTime.Compare(racun.DatumPrimanja, defaultDateTime) == 0)
                {
                    racun.DatumSlanja = DateTime.Now;
                    
                }
                racun.DatumPrimanja = racun.DatumSlanja;
                await _context.Bills.AddAsync(racun);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Dodao Racun sa Ajdijem" + racun.ID);
                return new BillPostResponse { IsUpisan = true, BillResponse = _convert.ConvertCeoRacun(racun) };
            }
            catch (Exception ex)
            {
                _logger.LogInformation("usao u catch");
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.InnerException.ToString());
                return new BillPostResponse { IsUpisan = false };
            }
        }
        public override async Task<BillPostResponse> ChangeBillKuca(KucaBillMsg request, ServerCallContext context)
        {
            var comparativeBill = _context.Bills.Find(request.ID); //nasao ga u bazi


            _logger.LogInformation($"Nadjen je racun sa tim barcodom u bazi i ID je {comparativeBill.ID}");

            if (comparativeBill is not null)
            {
                _logger.LogInformation("usao sam u if comparativeBill is not null");
                _logger.LogInformation("ovaj Bill sto sam poslao sam smestio u comparativeBill");
                try
                {
                    _logger.LogInformation("Usao sam u try da sacuvam to u bazu");

                    comparativeBill.Naziv = _convert.ConvertKuca(request).Naziv;
                    comparativeBill.AdresaMusterije = _convert.ConvertKuca(request).AdresaMusterije;
                    comparativeBill.DatumSlanja = _convert.ConvertKuca(request).DatumSlanja;
                    comparativeBill.NabavnaCena = _convert.ConvertKuca(request).NabavnaCena;
                    comparativeBill.ProdajnaCena = _convert.ConvertKuca(request).ProdajnaCena;
                    comparativeBill.Rashod = _convert.ConvertKuca(request).Rashod;
                    await _context.SaveChangesAsync();

                    return new BillPostResponse { BillResponse = _convert.ConvertCeoRacun(comparativeBill), IsUpisan = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError("Nije nasao racun u bazi da ga izmeni");
                    return new BillPostResponse { IsUpisan = false };
                }
            }
            else
            {
                _logger.LogInformation("comparativeBill je null, nema takvog u bazi, ovo ne treba da se dogadja");
                return new BillPostResponse { IsUpisan = false };
            }
        }
        public override async Task<DeleteBillResponse> DeleteBill(DeleteBillRequest request, ServerCallContext context)
        {

            if (await _context.Bills.FindAsync(request.ID) is not null)
            {
                _logger.LogInformation($"Brisemo racun sa sledecim Ajdijem   {_context.Bills.Find(request.ID).ID}");
                _context.Bills.Remove(await _context.Bills.FindAsync(request.ID));

                try
                {
                    await _context.SaveChangesAsync();
                    return new DeleteBillResponse { Status = true };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new DeleteBillResponse { Status = false };
                }
            }
            else
            {
                return new DeleteBillResponse { Status = false };
            }
        }
        public override async Task GetAllKucaBill(EmptyMsg request, IServerStreamWriter<BillMsg> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Usli smo u GetAllKucaBill ");
            try
            {
                foreach (var item in _context.Bills.Where(b => b.WayOfSellingEnum == Bill.WayOfSelling.Kuca))
                {
                    _logger.LogInformation("Poceo je stream");
                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(item));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            _logger.LogInformation("zavrsio sam strimovanje");
        }
        #endregion

        #region Izvestaj Taskovi

        public override async Task ListaZaIzvestaj(IzvestajBillMsg request, IServerStreamWriter<BillMsg> responseStream, ServerCallContext context)
        {
            Console.WriteLine(request.Start);
            Console.WriteLine(request.End);
            Console.WriteLine(request.NacinSlanja);
            Console.WriteLine(request.TipDatuma);
            DateTime Start = _convert.ConvertIzvestaj(request).Start;
            DateTime End = _convert.ConvertIzvestaj(request).End;
            string NacinSlanja = _convert.ConvertIzvestaj(request).NacinSlanja;
            string TipDatuma = _convert.ConvertIzvestaj(request).TipDatuma;
            DateTime defaultDateTime = new DateTime(0001, 1, 1, 0, 0, 0);
            Bill.WayOfSelling result;

            switch (TipDatuma)
            {
                case "Poslati":
                    {
                        if (DateTime.Compare(Start, defaultDateTime) == 0)
                        {
                            //nije selektovao opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {

                                _logger.LogInformation("nije selektovao opseg niti nacin slanja a prikazati samo poslate");
                                var bills = await _context.Bills.Where(b => DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0).ToListAsync();
                                foreach (var bill in bills)
                                {
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }
                            }
                            else
                            {

                                _logger.LogInformation("nije selektovao opseg, selektovao je nacin slanja  a prikazati samo poslate");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0 && b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }

                                }
                            }
                        }
                        else
                        {
                            //selektovao je opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {
                                _logger.LogInformation("selektovao je opseg, nije nacin slanja a prikazati samo poslate");
                                var bills = await _context.Bills.Where(b => DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0 && b.DatumSlanja >= Start && b.DatumSlanja <= End ).ToListAsync();
                                foreach (var bill in bills)
                                {
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }
                            }
                            else
                            {
                                _logger.LogInformation("selektovao opseg i nacin slanja a prikazati samo poslate");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0 && b.DatumSlanja >= Start && b.DatumSlanja<=End  && b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Primljeni":
                    {
                        if (DateTime.Compare(Start, defaultDateTime) == 0)
                        {
                            //nije selektovao opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {

                                _logger.LogInformation("nije selektovao opseg niti nacin slanja a prikazati samo primljene");
                                var bills = await _context.Bills.Where(b => !(DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0)).ToListAsync();
                                foreach (var bill in bills)
                                {
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }

                            }
                            else
                            {

                                _logger.LogInformation("nije selektovao opseg, selektovao je nacin slanja a prikazati samo primljene");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => !(DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0) && b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }
                                }
                            }
                        }
                        else
                        {
                            //selektovao je opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {
                                _logger.LogInformation("selektovao je opseg, nije nacin slanja a prikazati samo primljene");
                                var bills = await _context.Bills.Where(b => !(DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0) && b.DatumSlanja >= Start && b.DatumSlanja <= End && b.DatumPrimanja >= Start && b.DatumPrimanja <= End).ToListAsync();
                                foreach (var bill in bills)
                                {
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }
                            }
                            else
                            {
                                _logger.LogInformation("selektovao opseg i nacin slanja a prikazati samo primljene");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => !(DateTime.Compare(b.DatumPrimanja, defaultDateTime) == 0) && b.DatumSlanja >= Start && b.DatumSlanja <= End && b.DatumPrimanja >= Start && b.DatumPrimanja <= End && b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }
                                }

                            }
                        }
                    }
                    break;
                case "Oboje":
                    {
                        if (DateTime.Compare(Start, defaultDateTime) == 0)
                        {
                            //nije selektovao opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {

                                _logger.LogInformation("nije selektovao opseg niti nacin slanja a prikazati i poslate i primljene");
                                var bills = await _context.Bills.ToListAsync(); //ovo su ustvari svi racuni
                                foreach (var bill in bills)
                                {
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }
                            }
                            else
                            {

                                _logger.LogInformation("nije selektovao opseg, selektovao je nacin slanja a prikazati i poslate i primljene");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }
                                }
                            }
                        }
                        else
                        {
                            //selektovao je opseg
                            if (string.IsNullOrEmpty(NacinSlanja))
                            {
                                _logger.LogInformation("selektovao je opseg, nije nacin slanja a prikazati i poslate i primljene");
                                var bills = await _context.Bills.Where(b => b.DatumSlanja >= Start && b.DatumSlanja <= End).ToListAsync();
                                foreach (var bill in bills)
                                {
                                    Console.WriteLine(bills.Count);
                                    _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                    await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                }

                            }
                            else
                            {
                                _logger.LogInformation("selektovao opseg i nacin slanja a prikazati i poslate i primljene");
                                if (Enum.TryParse<Bill.WayOfSelling>(NacinSlanja, out result))
                                {
                                    Console.WriteLine(result);
                                    var bills = await _context.Bills.Where(b => b.DatumSlanja >= Start && b.DatumSlanja <= End && b.WayOfSellingEnum.Equals(result)).ToListAsync();
                                    foreach (var bill in bills)
                                    {
                                        Console.WriteLine(bill.Naziv);
                                        Console.WriteLine(bill.Barcode);
                                        _logger.LogInformation("[Poceo je stream osoba prema nacinu slanja]");
                                        await responseStream.WriteAsync(_convert.ConvertCeoRacun(bill));
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

        }
        #endregion
    }
}
