using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Api.Controllers.ViewModels;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using BusinessLogic.Common;
using System;

namespace WebTodos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskServiceController : ControllerBase
    {

        protected readonly IMapper _mapper;
        protected readonly IUserService _userService;
        protected readonly ITaskService _taskService;
        protected readonly ITaskValidationService _taskValidationService;
        protected readonly ITaskToggleStatusValidationService _taskToggleStatusValidationService;

        public TaskServiceController(IMapper mapper,
                                    IUserService userService,
                                    ITaskService taskService,
                                    ITaskValidationService taskValidationService,
                                    ITaskToggleStatusValidationService taskToggleStatusValidationService)
        {
            _mapper = mapper;
            _userService = userService;
            _taskService = taskService;
            _taskValidationService = taskValidationService;
            _taskToggleStatusValidationService = taskToggleStatusValidationService;
        }

        public string CurrentUser
        {
            get
            {
                return "Anonymous";
            }
        }

        public IResult ValidatePermission(EnumPermission permission)
        {

            var User = CurrentUser;

            if (!_userService.Validate(User, permission))
            {
                return new Result(EnumResultBL.ERROR_PERMISSION_VALIDATIONS);
            }

            return Result.Ok;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TaskViewModel>> Get()
        {
            try
            {
                var resVal = ValidatePermission(EnumPermission.READ_TASK);
                if (!resVal.ComputeResult().IsOk())
                {
                    return BadRequest(resVal);
                }

                var result = _taskService.GetAll();
                return Ok(_mapper.Map<IEnumerable<TaskViewModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(EnumResultBL.ERROR_UNEXPECTED_EXCEPTION, ex.Message));
            }


        }

        [HttpGet("{code}", Name = "Get[controller]")]
        public ActionResult<TaskViewModel> Get(string code)
        {
            try
            {

                var resVal = ValidatePermission(EnumPermission.READ_TASK);
                if (!resVal.ComputeResult().IsOk())
                {
                    return BadRequest(resVal);
                }

                var Code = code;

                var result = _taskService.GetAll(Code);
                return Ok(_mapper.Map<IEnumerable<TaskViewModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(EnumResultBL.ERROR_UNEXPECTED_EXCEPTION, ex.Message));
            }
        }


        [HttpPost]
        public ActionResult<TaskViewModel> Post([FromBody] TaskViewModel value)
        {
            try
            {
                var Item = _mapper.Map<ITask>(value);

                var resVal = ValidatePermission(EnumPermission.CREATE_TASK);
                if (!resVal.ComputeResult().IsOk())
                {
                    return BadRequest(resVal);
                }

                if (Item.Status != EnumStatusTask.Pending)
                {
                    return BadRequest(new Result(EnumResultBL.ERROR_STATUS_NOT_ALLOWED_ON_CREATION, Item.Status));
                }

                var resultValidation = _taskValidationService.Validation(Item);
                if (!resultValidation.ComputeResult().IsOk())
                {
                    return BadRequest(resultValidation);
                }

                var result = _taskService.Add(Item);

                if (result.ComputeResult().IsOk())
                {
                    return Ok(_mapper.Map<TaskViewModel>(Item));
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(EnumResultBL.ERROR_UNEXPECTED_EXCEPTION, ex.Message));
            }

        }

        [HttpPut("{id}")]
        public ActionResult<TaskViewModel> Put(int id, [FromBody] TaskViewModel value)
        {
            try
            {
                var Item = _mapper.Map<ITask>(value);
                Item.Id = id;


                var resVal = ValidatePermission(EnumPermission.UPDATE_TASK);
                if (!resVal.ComputeResult().IsOk())
                {
                    return BadRequest(resVal);
                }

                var resultValidation = _taskValidationService.Validation(Item);
                if (!resultValidation.ComputeResult().IsOk())
                {
                    return BadRequest(resultValidation);
                }

                resultValidation = _taskToggleStatusValidationService.Validation(Item);
                if (!resultValidation.ComputeResult().IsOk())
                {
                    return BadRequest(resultValidation);
                }

                var result = _taskService.Edit(Item);

                if (result.ComputeResult().IsOk())
                {
                    return Ok(_mapper.Map<TaskViewModel>(Item));
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new Result(EnumResultBL.ERROR_UNEXPECTED_EXCEPTION, ex.Message));
            }
        }

    }
}