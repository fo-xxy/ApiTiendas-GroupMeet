using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace ApiTienda_GruopMeet.Controllers
{
    [Route("api/Store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        //Creamos un objeto de la interfaz
        private readonly IStoreService _storeService;

        //Generamos el constructor del controlador y le pasamos como parametro el objeto de la interfaz
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        //Generamos el metódo para mostrar los datos
        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }


        //Generamos el metódo para mostrar los datos de una tienda por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        //Generamos el metódo para crear la tienda
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdStore = await _storeService.CreateStoreAsync(dto);
            return CreatedAtAction(nameof(GetStoreById), new { id = createdStore.id }, createdStore);
        }

        //Generamos el método para eliminar una tienda
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _storeService.DeleteStoreAsync(id);

            if (!result)
                return NotFound(new { message = $"No se encontró la tienda con id: {id}" });

            return Ok(new { message = "La tienda se eliminó correctamente." });
        }
    }
}
