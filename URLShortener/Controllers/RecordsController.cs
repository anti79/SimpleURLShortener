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
		public GetRecordsResponse GetMultiple(int count, int offset=0)
		{

			using (var db = new ShortenerDBContext())
			{
				var records = db.Records.OrderByDescending(r => r.URLRecordId).Skip(offset).Take(count);
				if (records.Any() == false)
				{
					return new GetRecordsResponse() { Records = new List<URLRecord>(), Status = Status.success, Total=0 };
				}
				else
				{
					return new GetRecordsResponse() { Records = records.ToList(), Status = Status.success, Total=db.Records.Count() };
				}
			}
			
		}

		// GET api/<RecordsController>/5
		[HttpGet("{id}")]
		public URLRecord Get(int id)
		{
			using (var db = new ShortenerDBContext())
			{
				var record = db.Records.Where(r => r.URLRecordId == id).FirstOrDefault();
				if(record!=null)
				{
					return record;

				}
				else
				{
					Response.StatusCode = 404;
					return null;
				}
			}
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
