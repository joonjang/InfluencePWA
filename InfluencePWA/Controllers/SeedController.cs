using InfluencePWA.Data;
using InfluencePWA.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InfluencePWA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(
            ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            var path = Path.Combine(
                _env.ContentRootPath,
                String.Format("Data/Source/influenceDataExcel.xlsx"));

            using (var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read))
            {
                using (var ep = new ExcelPackage(stream))
                {
                    // get the first worksheet

                    var ws = ep.Workbook.Worksheets[0];

                    // initialize the record counters
                    var nPrincipleTypes = 0;
                    var nPrinciples = 0;

                    #region Import all PrincipleTypes
                    // create a list containing all the PrincipleTypes already existing
                    // into the Database (it will be empty on first run).
                    var lstPrincipleTypes = _context.PrincipleTypes.ToList();

                    // iterates through all rows, skipping the first one
                    for (int nRow = 2; nRow <= ws.Dimension.End.Row; nRow++)
                    {
                        var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];
                        row.Style.WrapText = true;
                        row.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                        var name = row[nRow, 4].GetValue<string>();

                        // Did we already create a PrincipleType with that name?
                        if (lstPrincipleTypes.Where(c => c.Name == name).Count() == 0)
                        {
                            // create the PrincipleType entity and fill it with xlsx data
                            var principleType = new PrincipleType();
                            principleType.Name = name;

                            // save it into the Database
                            _context.PrincipleTypes.Add(principleType);
                            await _context.SaveChangesAsync();

                            // store the PrincipleType to retrieve its Id later on
                            lstPrincipleTypes.Add(principleType);

                            // increment the counter
                            nPrincipleTypes++;
                        }
                    }

                    #endregion

                    #region Import all Principles
                    // iterates through all rows, skipping the first one
                    for (int nRow = 2;
                        nRow <= ws.Dimension.End.Row;
                        nRow++)
                    {
                        var row = ws.Cells[nRow, 1, nRow, ws.Dimension.End.Column];

                        // create the Principle entity and fill it with xlsx data
                        var principle = new Principle();
                        principle.Law = row[nRow, 1].GetValue<string>();
                        principle.Title = row[nRow, 2].GetValue<string>();
                        principle.Description = row[nRow, 3].GetValue<string>();

                        // retrieve PrincipleTypeId
                        var principleTypeName = row[nRow, 4].GetValue<string>();
                        var principleType = lstPrincipleTypes.Where(c => c.Name == principleTypeName)
                            .FirstOrDefault();
                        principle.PrincipleTypeId = principleType.Id;

                        // save the Principle into the Database
                        _context.Principles.Add(principle);
                        await _context.SaveChangesAsync();

                        // increment the counter
                        nPrinciples++;
                    }
                    #endregion

                    return new JsonResult(new
                    {
                        Principles = nPrinciples,
                        PrincipleTypes = nPrincipleTypes
                    });
                }
            }
        }
    }
}
