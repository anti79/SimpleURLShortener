using Microsoft.AspNetCore.Mvc;
using URLShortener.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace URLShortener.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecordsController : ControllerBase
	{
		// GET: api/<RecordsController>
		[HttpGet]
		public GetRecordsResponse GetMultiple(int count)
		{

			using (var db = new ShortenerDBContext())
			{
				var records = db.Records.OrderByDescending(r => r.URLRecordId).Take(count);
				if (records.Any() == false)
				{
					return new GetRecordsResponse() { Records = new List<URLRecord>(), Status = Status.success };
				}
				else
				{
					return new GetRecordsResponse() { Records = records.ToList(), Status = Status.success };
				}
			}
			
		}

		// GET api/<RecordsController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<RecordsController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<RecordsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<RecordsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
