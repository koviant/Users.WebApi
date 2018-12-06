using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Swagger.Net.Annotations;

using Users.BLL.Base;
using Users.BLL.Models;
using Users.WebApi.Filters;

namespace Users.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    [CustomExceptionFilter]
    public class UserController : ApiController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of users.")]
        public async Task<IHttpActionResult> GetUsers()
        {
            var users = await this._service.GetAllUsersAsync();

            return this.Ok(users);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Returns a new user and it's location.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid model state of a passed user.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "User with same LoginName already exists.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> CreateUser([FromBody]UserRequest createRequest)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = await this._service.CreateUserAsync(createRequest);
            var userLocation = $"api/users/{user.Id}";

            return this.Created(userLocation, user);
        }

        [HttpGet]
        [Route("{userId:int:min(1)}/addresses")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns list of addresses of user with passed id.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetUserAddresses([FromUri]int userId)
        {
            var addresses = await this._service.GetUserAddressesAsync(userId);

            return this.Ok(addresses);
        }

        [HttpGet]
        [Route("{userId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns user with passed id.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetUser([FromUri]int userId)
        {
            var user = await this._service.GetUserById(userId);

            return this.Ok(user);
        }

        [HttpGet]
        [Route("{userId:int:min(1)}/addresses/{addressId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns user's address.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist or user don't have address with passed id.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetAddress([FromUri]int userId, [FromUri]int addressId)
        {
            var address = await this._service.GetUsersAddressByIdAsync(userId, addressId);

            return this.Ok(address);
        }

        [HttpPut]
        [Route("{userId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates user with passed id.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid model state of a passed user.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "User with same LoginName already exists.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateUser([FromUri]int userId, [FromBody]UserRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this._service.UpdateUserAsync(userId, request);

            var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
            return this.ResponseMessage(response);
        }

        [HttpPatch]
        [Route("{userId:int:min(1)}/lastName")]
        [SwaggerExample("lastName", "\"new last name\"")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates last name of the user with passed id.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Last name was null or empty.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateUserLastName([FromUri]int userId, [FromBody]string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                return this.BadRequest($"{nameof(lastName)} must not be null or empty.");
            }

            await this._service.UpdateUserLastNameAsync(userId, lastName);

            var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
            return this.ResponseMessage(response);
        }

        [HttpPatch]
        [Route("{userId:int:min(1)}/addresses/{addressId:int:min(1)}")]
        [SwaggerExample("lastName", "\"new last name\"")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Adds new address to the user with passed id.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Not valid address model state.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "Address with same Description already exists.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist or it does not have address with passed id.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateUserAddress([FromUri]int userId, [FromUri]int addressId, [FromBody]AddressRequest addressRequest)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            await this._service.UpdateUserAddressAsync(userId, addressId, addressRequest);

            var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
            return this.ResponseMessage(response);
        }

        [HttpPost]
        [Route("{userId:int:min(1)}/addresses")]
        [SwaggerExample("lastName", "\"new last name\"")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Returns new address and it's location.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Not valid address model state.")]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "Address with same Description already exists.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddNewAddress([FromUri]int userId, [FromBody]AddressRequest addressRequest)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var address = await this._service.AddNewAddressToUserAsync(userId, addressRequest);
            var addressLocation = $"api/users/{userId}/addresses/{address.Id}";

            return this.Created(addressLocation, address);
        }

        [HttpDelete]
        [Route("{userId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes user with passed id.")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with passed id does not exist.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteUser([FromUri]int userId)
        {
            await this._service.DeleteUserByIdAsync(userId);

            var response = this.Request.CreateResponse(HttpStatusCode.NoContent);
            return this.ResponseMessage(response);
        }
    }
}
