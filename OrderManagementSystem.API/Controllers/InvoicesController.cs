using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Services.Dtos;

namespace OrderManagementSystem.API.Controllers
{

        [Authorize(Roles ="Admin")]
    public class InvoicesController : BaseController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InvoicesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>> > GetAllInvoices()
        {
            var Invoices=await _unitOfWork.Repository<Invoice>().GetAllAsync();
            return Ok( _mapper.Map<IReadOnlyList<InvoiceDto> >(Invoices));
            
        }
        [HttpGet("{InvoiceId}")]
        public async Task<ActionResult<InvoiceDto>> GetById( int InvoiceId)
        {
            var Invoices = await _unitOfWork.Repository<Invoice>().GetById(InvoiceId);
            if (Invoices == null) return NotFound(new ErrorApiResponse(404));
            return Ok(_mapper.Map<InvoiceDto>(Invoices));
        }
    }
}
