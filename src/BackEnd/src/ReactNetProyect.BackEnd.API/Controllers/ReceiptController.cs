﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactNetProyect.BackEnd.API.Utils;
using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.BackEnd.Service;
using ReactNetProyect.Shared.DTO;
using System.Linq;

namespace ReactNetProyect.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReceiptController : Controller
    {
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;

        public ReceiptController(IReceiptService receiptService, IMapper mapper)
        {
            _receiptService = receiptService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDTO>>> GetAllReceiptsAsync([FromQuery] PagerDTO pagerDTO)
        {
            var query = _receiptService.GetAllReceipts();
            await HttpContext.InsertPagerParamsInHeader(query);
            var receipts = await query.OrderBy(x => x.Date).Pager(pagerDTO).ToListAsync();
            return Ok(_mapper.Map<List<ReceiptDTO>>(receipts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceiptByIdAsync(int id)
        {
            var receipt = await _receiptService.GetReceiptByIdAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            return Ok(receipt);
        }

        [HttpPost]
        public async Task<ActionResult<Receipt>> AddReceiptAsync(CreateReceiptDTO createReceiptDTO)
        {
            var receipt = _mapper.Map<Receipt>(createReceiptDTO);
            await _receiptService.AddReceiptAsync(receipt);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReceiptAsync(int id, Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return BadRequest();
            }
            await _receiptService.UpdateReceiptAsync(receipt);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReceiptAsync(int id)
        {
            var receipt = await _receiptService.GetReceiptByIdAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            await _receiptService.DeleteReceiptAsync(receipt.Id);
            return NoContent();
        }
    }
}
