using Data.Model;
using Ext.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace Data
{
    /// <summary>
    /// Summary description for CustomService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CustomService : System.Web.Services.WebService
    {

        
        #region actors
        List<Actor> list = new List<Actor>
            {
                new Actor { 
                    Id = 1,
                    FirstName="Brad",
                    LastName= "Pitt",
                    DateOfBirth = DateTime.ParseExact("18/12/1963","dd/MM/yyyy",null),
                    Image = "brad-pitt",
                    IsGoodActor = false,
                    FamousFor = "Fight Club"
                    },
                    new Actor { 
                    Id = 2,
                    FirstName="Christian",
                    LastName= "Bale",
                    DateOfBirth = DateTime.ParseExact("30/01/1974","dd/MM/yyyy",null),
                    Image = "Christian-Bale",
                    IsGoodActor = true,
                    FamousFor = "Batman"
                    },
                    new Actor { 
                    Id = 3,
                    FirstName="Emma",
                    LastName= "Roberts",
                    DateOfBirth = DateTime.ParseExact("10/02/1991","dd/MM/yyyy",null),
                    Image = "Emma-Roberts",
                    IsGoodActor = false,
                    FamousFor = "We 're the Millers"
                    }
                    ,
                    new Actor { 
                    Id = 4,
                    FirstName="James",
                    LastName= "Franco",
                    DateOfBirth = DateTime.ParseExact("19/04/1978","dd/MM/yyyy",null),
                    Image = "james-franco",
                    IsGoodActor = true,
                    FamousFor = "127 Hours"
                    }
                    ,
                    new Actor { 
                    Id = 5,
                    FirstName="Jennifer",
                    LastName= "Anniston",
                    DateOfBirth = DateTime.ParseExact("11/02/1969","dd/MM/yyyy",null),
                    Image = "Jennifer-Aniston",
                    IsGoodActor = true,
                    FamousFor = "Friends"
                    },
                    new Actor { 
                    Id = 6,
                    FirstName="Kate",
                    LastName= "Beckinsale",
                    DateOfBirth = DateTime.ParseExact("26/06/1973","dd/MM/yyyy",null),
                    Image = "Kate-Beckinsale",
                    IsGoodActor = true,
                    FamousFor = "Underworld"
                    },
                    new Actor { 
                    Id = 7,
                    FirstName="Kristen",
                    LastName= "Stewart",
                    DateOfBirth = DateTime.ParseExact("09/04/1990","dd/MM/yyyy",null),
                    Image = "Kristen-Stewart",
                    IsGoodActor = false,
                    FamousFor = "Twilight"
                    },
                    new Actor { 
                    Id = 8,
                    FirstName="Mark",
                    LastName= "Wahlberg",
                    DateOfBirth = DateTime.ParseExact("05/06/1971","dd/MM/yyyy",null),
                    Image = "Mark-Wahlberg",
                    IsGoodActor = true,
                    FamousFor = "The Fighter"
                    },
                    new Actor { 
                    Id = 9,
                    FirstName="Michael",
                    LastName= "Hall",
                    DateOfBirth = DateTime.ParseExact("01/02/1971","dd/MM/yyyy",null),
                    Image = "Michael-Hall",
                    IsGoodActor = true,
                    FamousFor = "Dexter"
                    },
                    new Actor { 
                    Id = 10,
                    FirstName="Monica",
                    LastName= "Bellucci",
                    DateOfBirth = DateTime.ParseExact("30/09/1964","dd/MM/yyyy",null),
                    Image = "Monica-Bellucci",
                    IsGoodActor = false,
                    FamousFor = "Malena"
                    },
                    new Actor { 
                    Id = 11,
                    FirstName="Natalie",
                    LastName= "Portman",
                    DateOfBirth = DateTime.ParseExact("09/06/1981","dd/MM/yyyy",null),
                    Image = "Natalie-Portman",
                    IsGoodActor = true,
                    FamousFor = "V for Venetta"
                    },
                    new Actor { 
                    Id = 12,
                    FirstName="Penelope",
                    LastName= "Cruz",
                    DateOfBirth = DateTime.ParseExact("28/04/1974","dd/MM/yyyy",null),
                    Image = "Penelope-Cruz",
                    IsGoodActor = false,
                    FamousFor = "Blow"
                    },
                    new Actor { 
                    Id = 13,
                    FirstName="Scarlett",
                    LastName= "Johansson",
                    DateOfBirth = DateTime.ParseExact("22/09/1984","dd/MM/yyyy",null),
                    Image = "Scarlett-Johansson",
                    IsGoodActor = false,
                    FamousFor = "Lost in Translation"
                    },
                    new Actor { 
                    Id = 14,
                    FirstName="Tom",
                    LastName= "Cruise",
                    DateOfBirth = DateTime.ParseExact("03/07/1962","dd/MM/yyyy",null),
                    Image = "Tom-Cruise",
                    IsGoodActor = false,
                    FamousFor = "Top Gun"
                    }
            };
        #endregion

        [WebMethod]
        public void GetActors()
        {
            Thread.Sleep(500);
            HttpContext.Current.Response.ContentType = "application/json";
            var requestParams = new StoreRequestParameters(HttpContext.Current);
            int start = requestParams.Start;
            int limit = requestParams.Limit;
            string filter = requestParams.Filter.Length > 0 ? requestParams.Filter[requestParams.Filter.Length-1].Value.ToLower() : "";
            
            string sort = string.IsNullOrEmpty(requestParams.SimpleSort) ? "Id" : requestParams.SimpleSort;
            string sortDirection = requestParams.SimpleSortDirection.ToString() != "Default"  ? requestParams.SimpleSortDirection.ToString() : "ASC";
            Paging<Actor> actors = GetData(start, limit, sort, sortDirection, filter);
            HttpContext.Current.Response.Write(JSON.Serialize(actors));
        }

        public Paging<Actor> GetData(int start, int limit,string sortedBy, string dir, string filter)
        {
            if(!string.IsNullOrEmpty(filter))
            {
                return new Paging<Actor>(list.Where(e=>e.FirstName.ToLower().Contains(filter)).Skip(start).Take(limit)
                    .OrderBy(e => e.Id), list.Count(e => e.FirstName.ToLower().Contains(filter)));
            }
            if (sortedBy == "LastName")
            {
                return new Paging<Actor>(list.Skip(start).Take(limit)
                  .OrderByDescending(e => e.LastName), list.Count);
            }
            else
            {
                return new Paging<Actor>(list.Skip(start).Take(limit)
                    .OrderBy(e => e.Id), list.Count);
            }           
        }
    }
}
