using CoreApp;
using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        //Siempre vamos a retornar 2 respuestas
        //200 --> OK
        //500 --> Internal server Error
        //Los retrieve trabajan con el verbo get de http
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new BranchManager();
                var branchList = um.RetrieveAll();
                return Ok(branchList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int brachId)
        {
            try
            {
                var um = new BranchManager();
                var branch = um.RetrieveById(brachId);

                if (branch != null)
                {
                    return Ok(branch);
                }
                else
                {
                    return NotFound("Branch not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Create(branch);
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Update(branch);
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }



        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Delete(branch);
                return Ok(branch);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddServiceToBranch")]
        public ActionResult AddServiceToBranch(int branchId, int serviceId)
        {
            try
            {
                var um = new BranchManager();
                var branch = um.RetrieveById(branchId);

                if (branch == null)
                {
                    return NotFound("Branch not found");
                }

                // Agregar el ID del servicio al branch
                branch.Services.Add(serviceId);

                // Actualizar el branch en el repositorio de datos
                um.AddServices(branch);

                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }

}


    

