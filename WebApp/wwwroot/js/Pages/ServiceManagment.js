//Clase Javascript que es el controlador de la vista
//Nos referimos a controlador partiendo del supuesto que esta clase controla el compartamiento de la
//vista o pagina

//Definmos la clase 
function BranchController() {
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";

    this.LoadTableBranch();

    this.InitView = function () {
        console.log("Services view init");
        this.LoadTableBranch();

    }

    this.LoadTableBranch = function () { //Metodo para la carga de la tabla de datos
        var ca = new ControlActions();

        //Ruta del api
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll")



        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "address" }
        columns[3] = { 'data': "description" }
        columns[4] = { 'data': "serviceId" }

        $("#tblBranch").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        //Asignacion de evento al click de la fila de la tabla
        //$('#tblBranch tbody').on('click', 'tr', function () {

        //    //Extrae la fila a la que le dio click
        //    var row = $(this).closest('tr');

        //    //Extraer la data del registro contenido en la fila
        //    var branch = $('#tblBranch').DataTable().row(row).data();

        //    //Mapeo de los valores del objeto data con el formulario
        //    $("#BranchId").val(branch.id);
        //    $("#textName").val(branch.name);
        //    $("#textAddress").val(branch.address);
        //    $("#textDescription").val(branch.description);
        //    $("#textServiceId").val(branch.serviceId);


        //});
    }
}
function ServiceController() {
    this.ViewName = "Services"; //como se llama la pagina
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
            console.log("Service Created --->" + JSON.stringify(service));
            $('#tblServices').DataTable().ajax.reload();

        });

    }

    this.Update = function () {
        var service = {};
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
                console.log("Service Deleted --->" + serviceId);
                $('#tblServices').DataTable().ajax.reload();

            } else if (response.statusCode == 500) {
                console.log("El servicio con el Id " + serviceId + " no existe.");

            } else {
                console.log("Error al eliminar el servicio. Código de estado: " + response.statusCode);

            }
            $('#tblServices').DataTable().ajax.reload();
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
})

$(document).ready(function () {
    var bc = new BranchController();
    bc.InitView();
})