using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebApplication1.Helper
{
    public static class HelpCreation
    {
        public static void CreateStorageItem()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if(db.Storages.ToList().Count == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Storage sg = new Storage { Id = Guid.NewGuid(), Name = "TestStorage" + i };
                        db.Storages.Add(sg);
                    }
                    db.SaveChanges();
                }
                
            }
        }

        public static void CreateNomenclatureItem()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if(db.Nomenclatures.ToList().Count == 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        Nomenclature nl = new Nomenclature { Id = Guid.NewGuid(), Name = "TestNomenclature" + i };
                        db.Nomenclatures.Add(nl);
                    }
                    db.SaveChanges();
                }
               
            }
        }
    }
}
