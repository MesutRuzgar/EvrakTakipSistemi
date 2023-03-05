using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Entities:IEntities
    {
        public int Id { get; set; }
        public string VKN { get; set; }
        public string FirmaAd { get; set; }
        public int VergiLevhasiYili { get; set; }
        public DateTime FaaliyetBelgesiTarihi { get; set; }
        public DateTime ImzaSirküsüTarihi { get; set; }
        public string FirmaYetkilileri { get; set; }


    }
}
