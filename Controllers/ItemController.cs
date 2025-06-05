using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace ApiTienda_GruopMeet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        //Creamos un objeto de la interfaz
        private readonly IItemService _itemService;

        //Generamos el constructor del controlador y le pasamos como parametro el objeto de la interfaz
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        //Generamos el metódo para mostrar los datos
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        //Generamos el método para guardar un nuevo registro de item
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ItemCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _itemService.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetAllItems), new { id = createdUser.id }, createdUser);
        }


        //Generamos el método para actializar un item
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemUpdateDto dto)
        {
            if (id != dto.id)
                return BadRequest("El id del item no se encontró.");

            var updatedItem = await _itemService.UpdateItemAsync(dto);

            if (updatedItem == null)
                return NotFound();

            return Ok(updatedItem);
        }

        //Generamos el método para eliminar un item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var result = await _itemService.DeleteStoreAsync(id);

            if (!result)
                return NotFound(new { message = $"No se encontró el item con id: {id}" });

            return Ok(new { message = "El item se eliminó correctamente." });

        }
    }
}
