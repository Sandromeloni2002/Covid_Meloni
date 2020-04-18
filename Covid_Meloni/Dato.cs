using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid_Meloni
{
    public class Dato
    {
        public DateTime Data { get; set; }
        public string Stato { get; set; }
        public int RicoveratiConSintomi { get; set; }
        public int TerapiaIntensiva { get; set; }
        public int TotaleOspedalizzati { get; set; }
        public int IsolamentoDomiciliare { get; set; }
        public int TotalePositivi { get; set; }
        public int VariazioneTotalePositivi { get; set; }
        public int NuoviPositivi { get; set; }
        public int DimessiGuariti { get; set; }
        public int Deceduti { get; set; }
        public int TotaleCasi { get; set; }
        public int Tamponi { get; set; }

        public override string ToString()
        {
            return $"{Stato}    {Data.ToShortDateString()}    Casi: {TotaleCasi}";
        }
    }
}
