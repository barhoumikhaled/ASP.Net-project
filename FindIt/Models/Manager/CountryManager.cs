using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class CountryManager
    {
        public static void Add(Country country)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Country.Add(country);
                db.SaveChanges();
            }
        }

        public static Country GetById(int id, ApplicationDbContext db = null)
        {
            Country country = null;
            if (db != null)
            {
                country = db.Country.Include("Provinces").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    country = db.Country.Include("Provinces").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return country;
        }

        public static void Delete(int countryId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Country country = GetById(countryId, db);

                if (country != null)
                {
                    db.Country.Remove(country);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Country newAddress)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Country country = GetById(newAddress.Id, db);

                country.Name = newAddress.Name;

                db.SaveChanges();
            }
        }

        public static List<Country> GetAll()
        {
            List<Country> listCountry = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listCountry = db.Country.Include("Provinces").OrderBy(c => c.Id).ToList();
            }
            return listCountry;
        }

        public static IEnumerable<SelectListItem> GetSelectList(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Country> lst = GetAll().OrderBy(p => p.Name);

            IEnumerable<SelectListItem> selectList = lst.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = (selectedValue == p.Id)
            });

            return selectList;
        }
    }
}