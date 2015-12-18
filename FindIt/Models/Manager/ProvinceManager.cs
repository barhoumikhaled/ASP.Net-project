using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindIt.Models.Entities;

namespace FindIt.Models.Manager
{
    public class ProvinceManager
    {
        public static void Add(Province province)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Province.Add(province);
                db.SaveChanges();
            }
        }

        public static Province GetById(int id, ApplicationDbContext db = null)
        {
            Province province = null;
            if (db != null)
            {
                province = db.Province.Include("Country").Where(v => v.Id == id).FirstOrDefault();
            }
            else
            {
                using (db = new ApplicationDbContext())
                {
                    province = db.Province.Include("Country").Where(v => v.Id == id).FirstOrDefault();
                }
            }
            return province;
        }

        public static void Delete(int provinceId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Province province = GetById(provinceId, db);

                if (province != null)
                {
                    db.Province.Remove(province);
                }
                db.SaveChanges();
            }
        }

        public static void Modify(Province newProvince)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                Province province = GetById(newProvince.Id, db);

                province.Name = newProvince.Name;

                db.SaveChanges();
            }
        }

        public static List<Province> GetAll()
        {
            List<Province> listProvince = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listProvince = db.Province.Include("Country").OrderBy(c => c.Id).ToList();
            }
            return listProvince;
        }

        public static List<Province> GetAllByCountryId(int id)
        {
            List<Province> listProvince = null;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                listProvince = db.Province.Include("Country").Where(v => v.CountryId == id).OrderBy(c => c.Name).ToList();
            }
            return listProvince;
        }

        public static IEnumerable<SelectListItem> GetSelectList(int? id)
        {
            int selectedValue = id.HasValue ? id.Value : -1;
            IEnumerable<Province> lst = GetAllByCountryId((int)id).OrderBy(p => p.Name);

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