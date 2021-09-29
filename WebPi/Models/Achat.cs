using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPi.Models
{
    public class Achat
    {
		[JsonProperty("id")]
		public int id { get; set; }
		[JsonProperty("titre")]
		public String titre { get; set; }
		[JsonProperty("description")]
		public String description { get; set; }
		[JsonProperty("date")]
		public DateTime date { get; set; }
		[JsonProperty("bathrooms")]
		public int bathrooms { get; set; }
		[JsonProperty("bedrooms")]
		public int bedrooms { get; set; }
		[JsonProperty("elevator")]
		public bool elevator { get; set; }
		[JsonProperty("garage")]
		public int garage { get; set; }
		[JsonProperty("type")]
		public string type { get; set; }
		[JsonProperty("areas")]
		public int areas { get; set; }
		[JsonProperty("pool")]
		public bool pool { get; set; }
		[JsonProperty("price")]
		public double price { get; set; }
		[JsonProperty("photo")]
		public string photo { get; set; }
	}
}