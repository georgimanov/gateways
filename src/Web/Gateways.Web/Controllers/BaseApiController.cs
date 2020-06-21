namespace Gateways.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [IgnoreAntiforgeryToken]
    public class BaseApiController : BaseController
    {
    }
}
