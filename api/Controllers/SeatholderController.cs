using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/seatholders")]
    [ApiController]
    public class SeatholderController : ControllerBase
    {
        //In memory db, lazy init
        private List<Seatholder> seatholders = null;

        public SeatholderController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Seatholder> Get()
        {
            if(seatholders == null)
            {
                LoadSeatHolders();
            }

            return seatholders;
        }

        private void LoadSeatHolders()
        {
            seatholders = new List<Seatholder>();

            using (var reader = new StreamReader("assets/abonnees_genk.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                seatholders = csv.GetRecords<Seatholder>().ToList();
            }
        }
    }
}
