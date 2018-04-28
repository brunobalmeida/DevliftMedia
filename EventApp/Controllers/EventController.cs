﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventApp.Models;

namespace EventApp.Controllers
{
    public class EventController : Controller
    {
        private readonly EvenAppContext _context;

        public EventController(EvenAppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Event Controller index: Filter the events and return them to the view 
        /// </summary>
        /// <returns>Returns a List of events from Db</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        /// <summary>
        /// Event Controller details: Fetch the details from a single event by id
        /// </summary>
        /// <param name="id">int: Event Id</param>
        /// <returns>Returns the single event details from Db</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        /// <summary>
        /// Get method for the Create view
        /// </summary>
        /// <returns>Returns the create view</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Event Controller Create POST method: Check the validation from ModelState and create the new Event
        /// </summary>
        /// <param name="events">event object</param>
        /// <returns>Return the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventTitle,EventDate,Picture,PictureType,EventDescription,EventContact,EventLink,EventAddress")] Events events)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(events);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The event has been successfully added to the Database.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Create error:{e.GetBaseException().Message}");
            }
            return View(events);
        }

        /// <summary>
        /// Edit get method: fetch the data from a single event to be changed
        /// </summary>
        /// <param name="id">int: Event id </param>
        /// <returns>Returns the view </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        /// <summary>
        /// Event Controller Edit POST method: Check the validation from ModelState and update the selected Event
        /// </summary>
        /// <param name="id">int: Event id</param>
        /// <param name="events">Event object</param>
        /// <returns>Returns the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventTitle,EventDate,Picture,PictureType,EventDescription,EventContact,EventLink,EventAddress")] Events events)
        {
            if (id != events.EventId)
            {
                ModelState.AddModelError("", "Invalid ID, please try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "The event has been successfully updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.EventId))
                    {
                        ModelState.AddModelError("", "The event does not exist, try again");
                    }
                    else
                    {
                        ModelState.AddModelError("", "This Event has already been updated");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", $"Error on Edit: {e.GetBaseException().Message}");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(events);
        }

        /// <summary>
        /// Get method for delete event
        /// </summary>
        /// <param name="id">int: Event id</param>
        /// <returns>Return the view</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events
                .SingleOrDefaultAsync(m => m.EventId == id);
            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        /// <summary>
        /// POST method for delete event. Deletes the selected event from Db
        /// </summary>
        /// <param name="id">int: Id</param>
        /// <returns>Returns the view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var events = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
                _context.Events.Remove(events);
                await _context.SaveChangesAsync();
                TempData["message"] = "The event has been successfully Deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["message"] = $"Delete error: {e.GetBaseException().Message}";
            }
            return Redirect($"/event/delete/{id}");
        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}