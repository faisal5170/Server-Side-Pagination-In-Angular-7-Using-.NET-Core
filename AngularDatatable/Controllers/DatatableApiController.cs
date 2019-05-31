using System.Linq;
using AngularDatatable.Entity;
using AngularDatatable.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularDatatable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatatableApiController : ControllerBase
    {
        private readonly Context _context;
        public DatatableApiController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
        [HttpPost]
        public IActionResult Get(int id)
        {
            var recordSkip = id == 1 ? 0 : (id - 1) * 10;
            var users = _context.Users.OrderBy(emp => emp.ID).Skip(recordSkip).Take(10).ToList();
            return Ok(users);
        }
        [HttpPut]
        public IActionResult Post([FromBody]PagingRequest paging)
        {
            var pagingResponse = new PagingResponse()
            {
                Draw = paging.Draw
            };

            if (!paging.SearchCriteria.IsPageLoad)
            {
                IQueryable<Users> query = null;

                if (!string.IsNullOrEmpty(paging.SearchCriteria.Filter))
                {
                    query = _context.Users.Where(emp => emp.Name.Contains(paging.SearchCriteria.Filter));
                }
                else
                {
                    query = _context.Users;
                }

                var recordsTotal = query.Count();

                var colOrder = paging.Order[0];

                switch (colOrder.Column)
                {
                    case 0:
                        query = colOrder.Dir == "asc" ? query.OrderBy(emp => emp.ID) : query.OrderByDescending(emp => emp.ID);
                        break;
                    case 1:
                        query = colOrder.Dir == "asc" ? query.OrderBy(emp => emp.Name) : query.OrderByDescending(emp => emp.Name);
                        break;
                    case 2:
                        query = colOrder.Dir == "asc" ? query.OrderBy(emp => emp.Email) : query.OrderByDescending(emp => emp.Email);
                        break;
                    case 3:
                        query = colOrder.Dir == "asc" ? query.OrderBy(emp => emp.Company) : query.OrderByDescending(emp => emp.Company);
                        break;
                }

                pagingResponse.Users = query.Skip(paging.Start).Take(paging.Length).ToArray();
                pagingResponse.RecordsTotal = recordsTotal;
                pagingResponse.RecordsFiltered = recordsTotal;
            }

            return Ok(pagingResponse);
        }
    }
}
