using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models.Entities;
using System.Data.Entity.Infrastructure;

namespace FindIt.Models.Manager
{
    public class AddressManager
    {
        public static void Add(Address address)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Address.Add(address);
                db.SaveChanges();
            }
        }

        public static Address GetById(int id, ApplicationDbContext db = null)
        {
            Address address = null;
            if (db != null)
            {
                address = db.Address.Include("Province").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    address = db.Address.Include("Province").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return address;
        }

        //Retourne une adresse avec la province (et le pays dans l'entité province)
        public static Address GetByIdJoinCountry(int id, ApplicationDbContext db) {
            return db.Address.Include("Province").Join(db.Country, a => a.Province.CountryId, c => c.Id,
                delegate(Address a, Country c) {
                    a.Province.Country = c;
                    return a;
                }).SingleOrDefault(a => a.Id == id);
        }

        //Surcharge
        public static Address GetByIdJoinCountry(int id) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                return GetByIdJoinCountry(id, db);
            }
        }

        public static void Delete(int addressId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Address address = GetById(addressId, db);

                if (address != null)
                {
                    db.Address.Remove(address);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Address newAddress)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Address address = GetById(newAddress.Id, db);

                address.No = newAddress.No;
                address.PostalCode = newAddress.PostalCode;
                address.ProvinceId = newAddress.ProvinceId;
                address.Province = newAddress.Province;
                address.Street = newAddress.Street;

                db.SaveChanges();
            }
        }

        public static List<Address> GetAll()
        {
            List<Address> listAddress = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listAddress = db.Address.Include("Province").OrderBy(c => c.Id).ToList();
            }
            return listAddress;
        }

        public static bool ExistInDb(Address address) {
            bool exist = false;
            List<Address> listAddress = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listAddress = db.Address.Include("Province").Where(v => v.No == address.No).ToList();
                foreach (Address a in listAddress) {
                    if (a.ProvinceId.Equals(address.ProvinceId) && a.PostalCode.Equals(address.PostalCode) && a.Street.Equals(address.Street) && a.City.Equals(address.City))
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }

        public static Address GetDBAddressByData(Address address) {
            using (ApplicationDbContext db = new ApplicationDbContext()) {
                return db.Address.Include("Province").SingleOrDefault(v => v.No == address.No && v.PostalCode == address.PostalCode && v.Street == address.Street && v.City == address.City && v.ProvinceId == address.ProvinceId);
            }
        }

        public static int GetIDByData(Address address){
            Address oldId = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                oldId = db.Address.Include("Province").Where(v => v.No == address.No && v.PostalCode == address.PostalCode && v.Street == address.Street && v.City == address.City && v.ProvinceId == address.ProvinceId).FirstOrDefault();
            }
            return oldId.Id;
        }

        public static Address GetByCommandeID(int id)
        {
         // chercher l addresse d un supplier
            Address add = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //chercher la commande supplier
                CommandeSupplier commSupplier = db.CommandeSupplier.Where(cs => cs.Id == id).FirstOrDefault();
                //chercher le supplier de la commande
                Supplier supplier = db.Supplier.Where(s => s.Id == commSupplier.SupplierId).FirstOrDefault();
                // trouver l addresse de supplier
                add = db.Address.Where(a => a.Id == supplier.AddressId).FirstOrDefault();
            }

            return add;
        }
    }
}