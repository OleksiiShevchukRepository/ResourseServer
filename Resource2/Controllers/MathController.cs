using System.Web.Http;
using System;
using System.Data;

namespace Resource2.Controllers
{
    [RoutePrefix("Math")]
    public class MathController : ApiController
    {
        //[Authorize]
        [HttpPost]
        [Route("Calc")]
        public IHttpActionResult CalculateExpression([FromBody]string expression)
        {
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return Ok(double.Parse((string)row["expression"]));
        }
    }
}
