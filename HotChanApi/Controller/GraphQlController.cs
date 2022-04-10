using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChanApi.Controller
{
	[Route("api/service/[controller]")]
	public class GraphQlController : ControllerBase
	{
	
		//[HttpPost]
		//public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
		//{
		//	if (query == null)
		//	{
		//		throw new ArgumentNullException(nameof(query));
		//	}

		//	var inputs = query.Variables.ToInputs();

		//	var execusionOptions = new ExecutionOptions
		//	{
		//		Schema = _schema,
		//		Query = query.Query,
		//		Inputs = inputs
		//	};

		//	var result = await _documentExecuter.ExecuteAsync(execusionOptions);

		//	if (result.Errors?.Count > 0)
		//	{
		//		return BadRequest(result);
		//	}

		//	return Ok(result);
		//}
	}

	
}
