function AppointmentController() {
    this.ViewName = "Appointment";
    this.ApiService = "Appointment";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("appointment view init");


        // Bind del click del botón create con la función correspondiente
        $("#btnCreate").click(function () {
            var vc = new AppointmentController();
            vc.Create();
        });

        $("#btnUpdate").click(function () {
            var vc = new AppointmentController();
            vc.Update();
        });
        $("#btnDelete").click(function () {
            var vc = new AppointmentController();
            vc.Delete();
        });

        this.loadTable();

    };

    this.Create = function () {

        // crear un dto
        var appointment = {};
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = $("#txtStartTime").val();
        appointment.text = $("#txtText").val();
        appointment.status = $("#txtStatus").val();


        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Create";

        ca.PostToAPI(serviceRoute, appointment, function () {
            console.log("Appointment Created --->" + JSON.stringify(appointment));
        })
    };

    this.Update = function () {

        // crear un dto
        var appointment = {};
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = $("#txtStartTime").val();
        appointment.text = $("#txtText").val();
        appointment.status = $("#txtStatus").val();

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, appointment, function () {
            console.log("Appointment Updated --->" + JSON.stringify(appointment));
        })
    }

    this.Delete = function () {

        // crear un dto
        var appointment = {};
        appointment.id = $("#txtId").val();


        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, appointment, function () {
            console.log("Appointment Deleted --->" + JSON.stringify(appointment));
        })
    }

    this.loadTable = function () {
        var ca = new ControlActions();

        // Ruta del api a consumir servicio

        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll")

        // Definir las columnas a extraer del json de  respuesta
        var columns = [];
        columns[0] = { 'data': 'patientName' }
        columns[1] = { 'data': 'patientLastName' }
        columns[2] = { 'data': 'serviceName' }
        columns[3] = { 'data': 'branchName' }
        columns[4] = { 'data': 'startTime' }
        columns[5] = { 'data': 'text' }
        columns[6] = { 'data': 'status' }

        // Inicializar la tabla como un data table
        $("#tblAppointments").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblAppointments tbody').on('click', 'tr', function () {

            // extraer fila a la que le dio click
            var row = $(this).closest('tr')

            // extraer la data del registro contenido en la fila

            var appointment = $('#tblAppointments').DataTable().row(row).data();

            // mapeo de los valores del objeto data con el  formulario
            $("#txtId").val(apppointment.Id);
            $("#txtPatientName").val(appointment.patientName);
            $("#txtPatientLastName").val(appointment.patientLastName);
            $("#txtServiceName").val(appointment.serviceName);
            $("#txtBranchName").val(appointment.branchName);
            $("#txtStartTime").val(apppointment.startTime);
            $("#txtText").val(appointment.text);
            $("#txtStatus").val(appointment.status);



        })
    }

}

function AllBranchInfoController() {
    // Propiedades de la clase
    this.ViewName = "Branch"; //como se llama la pagina
    this.ApiService = "Branch";

    // Método para inicializar la vista de todas las sucursales
    this.InitViewAllBranch = function () {

        // Asignar evento click al botón de agregar
        $("#btnAdd").click(function () {
            var vc = new AllBranchInfoController();
            vc.Add();
        });

        // Cargar la tabla de información de todas las sucursales
        this.LoadTableAllInfoBranch();

        this.LoadTableBranch();

    }

    // Cargar branchs al form
    this.LoadTableAllInfoBranch = function () {
        var bi = new ControlActions();

        //Ruta del api
        var urlService = bi.GetUrlApiService(this.ApiService + "/RetrieveAll")

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "address" }
        columns[3] = { 'data': "description" }

        // Crear la tabla utilizando DataTables
        $("#tblAllBranches").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblAllBranches tbody').on('click', 'tr', function () {

            //Extrae la fila a la que le dio click
            var row = $(this).closest('tr');

            //Extraer la data del registro contenido en la fila
            var branch = $('#tblAllBranches').DataTable().row(row).data();

            //Mapeo de los valores del objeto data con el formulario
            $("#BranchId").val(branch.id);
            $("#TxtName").val(branch.name);


        });
    }

    // Cargar servicios al form
    this.LoadTableBranch = function () {
        var ba = new ControlActions();

        //Ruta del api
        var urlService = ba.GetUrlApiService(this.ApiService + "/RetrieveAllBranchServices")
        console.log(urlService);

        // Definir las columnas de la tabla
        var columns = [];
        columns[0] = { 'data': "id" }
        columns[1] = { 'data': "name" }
        columns[2] = { 'data': "serviceId" }
        columns[3] = { 'data': "serviceName" }
        columns[4] = { 'data': "servicePrice" }
        columns[5] = { 'data': "serviceTax" }
        console.log(columns);

        // Crear la tabla utilizando DataTables
        $("#tblBranch").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        // Asignar evento al hacer clic en una fila de la tabla
        $('#tblBranch tbody').on('click', 'tr', function () {
            // Extraer la fila en la que se hizo clic
            var row = $(this).closest('tr');
            // Extraer los datos del servicio de la fila
            var service = $('#tblBranch').DataTable().row(row).data();
            // Mapear los valores del objeto de datos con los campos del formulario
            $("#ServiceId").val(service.Id);
            $("#textServiceName").val(service.name);
        });
    }
}

// Instanciamiento de la clase 
$(document).ready(function () {
    var vc = new AppointmentController();
    vc.InitView();
    var ba = new AllBranchInfoController();
    ba.InitViewAllBranch();
});
