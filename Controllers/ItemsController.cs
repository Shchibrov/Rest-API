    using Microsoft.AspNetCore.Mvc;
    using Rest_API.Models;
    using System.Collections.Generic;
    
    namespace Rest_API.Controllers // Замените на актуальное пространство имен вашего проекта
{
        [ApiController]
        [Route("[controller]")]
        public class ItemsController : ControllerBase
        {
            private static readonly List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1" },
            new Item { Id = 2, Name = "Item 2" },
            // Предварительно заполненные элементы
        };

            // GET: /Items
            [HttpGet]
            public IEnumerable<Item> Get()
            {
                return items;
            }

            // GET: /Items/{id}
            [HttpGet("{id}")]
            public ActionResult<Item> GetItem(int id)
            {
                var item = items.Find(i => i.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                return item;
            }

        // POST: /Items
        [HttpPost]
        public ActionResult<Item> PostItem(Item newItem)
        {
            // Проверяем, существует ли элемент с таким же ID.
            if (items.Any(i => i.Id == newItem.Id))
            {
                // Возвращаем статусный код 409 Conflict, если элемент с таким ID уже существует.
                return Conflict(new { message = $"An item with ID {newItem.Id} already exists." });
            }

            // Добавляем новый элемент, если ID уникальный.
            items.Add(newItem);
            return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
        }


        // PUT: /Items/{id}
        [HttpPut("{id}")]
            public IActionResult PutItem(int id, Item updatedItem)
            {
                var item = items.Find(i => i.Id == id);
                if (item == null)
                {
                    return NotFound();
                }

                item.Name = updatedItem.Name;
                // Обновите остальные свойства, если они есть

                return NoContent();
            }

            // DELETE: /Items/{id}
            [HttpDelete("{id}")]
            public IActionResult DeleteItem(int id)
            {
                var itemIndex = items.FindIndex(i => i.Id == id);
                if (itemIndex == -1)
                {
                    return NotFound();
                }

                items.RemoveAt(itemIndex);
                return NoContent();
            }
        }
    }
