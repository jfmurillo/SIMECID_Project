//Clase Javascript que es el controlador de la vista
//Nos referimos a controlador partiendo del supuesto que esta clase controla el compartamiento de la
//vista o pagina

/*Definmos la clase */
function AllBranchInfoController() {
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";


    this.InitViewAllBranch = function () {
        console.log("All Branch view init");
        $("#btnAdd").click(function () {
            var vc = new AllBranchInfoController();
            vc.Add();

        })
        this.LoadTableAllInfoBranch();

        this.LoadBranches();
        /*this.LoadServices();*/
    }

    this.LoadBranches = function () {
        var ba = new ControlActions();
        var urlService = this.ApiService + "/RetrieveAll";

        ba.GetToApi(urlService, function (response) {
            response.forEach(function (branch) {
                $("#branchSelect").append('<option value="' + branch.id + '">' + branch.id + ' - ' + branch.name + '</option>');
            });
        });
    }

    

    this.Add = function () {
        //Crear un Dto de User
        var Branchservice = {};
        Branchservice.branchId = $("#BranchId").val();
        Branchservice.serviceId = $("#ServiceId").val();

        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/AddServiceToBranch";

        ca.PostToAPI(serviceRoute, Branchservice, function () {
            console.log("Service Added to branch --->" + JSON.stringify(Branchservice));
            $('#tblBranch').DataTable().ajax.reload();
        });
    }

    this.LoadTableAllInfoBranch = function () { //Metodo para la carga de la tabla de datos
        var bi = new ControlActions();

        //Ruta del api
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll")

        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "address" }
        columns[3] = { 'data': "description" }
        //columns[4] = { 'data': "serviceId" }
        //columns[5] = { 'data': "serviceName" }
        //columns[6] = { 'data': "servicePrice" }
        //columns[7] = { 'data': "serviceTax" }

        $("#tblAllBranches").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });
    }
}

//CONTROLLER DE BRANCH 
function BranchController() {
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";



    this.InitViewBranch = function () {
        console.log("Branch view init");
        this.LoadTableBranch();

    }

    this.LoadTableBranch = function () { //Metodo para la carga de la tabla de datos
        var ba = new ControlActions();

        //Ruta del api
        var urlService = ba.GetUrlApiService(this.ApiService + "/RetrieveAllBranchServices")
        console.log(urlService);


        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "serviceId" }
        columns[3] = { 'data': "serviceName" }
        columns[4] = { 'data': "servicePrice" }
        columns[5] = { 'data': "serviceTax" }
        console.log(columns);


        $("#tblBranch").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        console.log(urlService);

    }
}

//CONTROLLER DE SERVICIOS 
function ServiceController() {
    this.ViewName = "Service"; //como se llama la pagina
    this.ApiService = "Service"  //Servicio de Api

    //Metodo a ejecutar al inicio de la vista

    this.InitView = function () {

        console.log("Services view init");

        //Bind del click del boton del create con la funcion correspondiente //# para que busque por Id
        $("#btnCreate").click(function () {
            var vc = new ServiceController();
            vc.Create();

        })

        $("#btnUpdate").click(function () {
            var vc = new ServiceController();
            vc.Update();

        })

        $("#btnDelete").click(function () {
            var vc = new ServiceController();
            vc.Delete();

        })
        this.LoadTableService();
        
        this.LoadAllServices();

    }
    /*
    {
      "id": 0,
      "created": "2024-04-03T01:12:29.654Z",
      "name": "string",
      "description": "string",
      "price": 0
    }
    */

    this.LoadAllServices = function (callback) {
        var ca = new ControlActions();
        var urlService = this.ApiService + "/RetrieveAll";

        ca.GetToApi(urlService, function (response) {

            response.forEach(function (service) {
                $("#serviceSelect").append('<option value="' + service.id + '">' + service.id + ' - ' + service.name + '</option>');
            });

            if (typeof callback === 'function') {
                callback();
            }
        });
    }

    this.Create = function () {

        //Crear un Dto de User
        var service = {};
        service.name = $("#textName").val();
        service.description = $("#textDescription").val();
        service.price = $("#textPrice").val();
        service.tax = $("#textTax").val();

        
        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, service, function () {
            let loadsrv = new ServiceController();
            console.log("Service Created --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();
            loadsrv.LoadAllServices();
            

        });
       

    }

    this.Update = function () {
        var service = {};
        service.Id = $("#ServiceId").val();
        service.name = $("#textName").val();
        service.description = $("#textDescription").val();
        service.price = $("#textPrice").val();
        service.tax = $("#textTax").val();

        //Invocar Api
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, service, function () {

            console.log("Service Updated --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();
            
        });
    }



    this.Delete = function () {
        var serviceId = $("#ServiceId").val();

        // Construir un objeto user con solo el ID del usuario
        var service = {

            Id: serviceId,
            name: "Default",
            description: "Default",
            price: 0,
            tax: 0


        };

        // Invocar la API DELETE con el objeto user
        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, service, function (response) {
            
            if (response.statusCode == 200) {
                let loadsrv = new ServiceController();
                console.log("Service Deleted --->" + serviceId);
                $('#tblServices').DataTable().ajax.reload();
                $('#tblBranch').DataTable().ajax.reload(); //Esto para que si se elimina un service se actualice el branch que lo tenia y se elimine
                loadsrv.LoadAllServices();
                
            } else if (response.statusCode == 500) {
                console.log("El servicio con el Id " + serviceId + " no existe.");

            } else {
                console.log("Error al eliminar el servicio. CÃ³digo de estado: " + response.statusCode);

            }
            $('#tblServices').DataTable().ajax.reload();
            $('#tblBranch').DataTable().ajax.reload();
                        
            
        });
    }

    this.LoadTableService = function () { //Metodo para la carga de la tabla de datos
        var ca = new ControlActions();

        //Ruta del api
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll")



        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "description" }
        columns[3] = { 'data': "price" }
        columns[4] = { 'data': "tax" }
        console.log(columns)
        

        $("#tblServices").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        //Asignacion de evento al click de la fila de la tabla
        $('#tblServices tbody').on('click', 'tr', function () {

            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //Extraer la data del registro contenido en la fila
            var service = $('#tblServices').DataTable().row(row).data();

            //Mapeo de los valores del objeto data con el formulario
            $("#ServiceId").val(service.id);
            $("#textName").val(service.name);
            $("#textDescription").val(service.description);
            $("#textPrice").val(service.price);
            $("#textTax").val(service.tax);


        });
    }




}

//Instanciamineto de la clase 
$(document).ready(function () {
    var vc = new ServiceController();
    vc.InitView();
    var bc = new BranchController();
    bc.InitViewBranch();
    var ba = new AllBranchInfoController();
    ba.InitViewAllBranch();
    
})


