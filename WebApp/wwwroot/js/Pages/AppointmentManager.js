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
        try {
            // Crear un dto
            var appointment = {};

            appointment.patientId = 15;
            appointment.patientName = "Default";
            appointment.patientLastName = "Default";
            appointment.doctorId = 15;
            appointment.doctorName = "Default";
            appointment.doctorLastName = "Default";
            appointment.serviceId = $("#ServiceId").val();
            appointment.serviceName = "Default";
            appointment.branchId = $("#BranchId").val();
            appointment.branchName = "Default";
            appointment.startTime = formatDate($("#startTime").val()); // Format date here
            appointment.endTime = formatDate($("#startTime").val());
            appointment.text = $("#txtReason").val();
            appointment.status = $("#txtStatus").val();

            // Invocar al API
            var ca = new ControlActions();
            var serviceRoute = this.ApiService + "/Create";

            ca.PostToAPI(serviceRoute, appointment, function () {
                console.log("Appointment Created --->" + JSON.stringify(appointment));
            });
        } catch (error) {
            console.error("Error occurred while creating appointment:", error);
        }
    };


    this.Update = function () {

        // crear un dto
        var appointment = {};
        appointment.id = $("#txtId").val();
        appointment.patientId = $("#patientId").val();
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.doctorId = $("#doctorId").val();
        appointment.doctorName = $("#doctorName").val();
        appointment.doctorLastName = $("#doctorLastName").val();
        appointment.serviceId = $("#serviceId").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchId = $("#branchId").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = formatDate($("#startTime").val()); // Format date here
        appointment.endTime = formatDate($("#startTime").val());
        appointment.text = $("#text").val();
        appointment.status = $("#status").val();

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
        appointment.patientId = $("#patientId").val();
        appointment.patientName = $("#txtPatientName").val();
        appointment.patientLastName = $("#txtPatientLastName").val();
        appointment.doctorId = $("#doctorId").val();
        appointment.doctorName = $("#doctorName").val();
        appointment.doctorLastName = $("#doctorLastName").val();
        appointment.serviceId = $("#serviceId").val();
        appointment.serviceName = $("#txtServiceName").val();
        appointment.branchId = $("#branchId").val();
        appointment.branchName = $("#txtBranchName").val();
        appointment.startTime = formatDate($("#startTime").val()); // Format date here
        appointment.endTime = formatDate($("#startTime").val());
        appointment.text = $("#text").val();
        appointment.status = $("#status").val();


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
        columns[0] = { 'data': 'id' }
        columns[1] = { 'data': 'patientId' }
        columns[2] = { 'data': 'patientName' }
        columns[3] = { 'data': 'patientLastName' }
        columns[4] = { 'data': 'doctorId' }
        columns[5] = { 'data': 'doctorName' }
        columns[6] = { 'data': 'doctorLastName' }
        columns[7] = { 'data': 'serviceId' }
        columns[8] = { 'data': 'serviceName' }
        columns[9] = { 'data': 'branchId' }
        columns[10] = { 'data': 'branchName' }
        columns[11] = { 'data': 'startTime' }
        columns[12] = { 'data': 'endTime' }
        columns[13] = { 'data': 'text' }
        columns[14] = { 'data': 'status' }

        // Inicializar la tabla como un data table
        $("#tblAppointments").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns,
            columnDefs: [
                {
                    target: 1,
                    visible: false
                },
                {
                    target: 4,
                    visible: false
                },
                {
                    target: 5,
                    visible: false
                },
                {
                    target: 6,
                    visible: false
                },
                {
                    target: 7,
                    visible: false
                },
                {
                    target: 8,
                    visible: false
                },
                {
                    target: 9,
                    visible: false
                },
                {
                    target: 12,
                    visible: false
                }
            ]
        });

        $('#tblAppointments tbody').on('click', 'tr', function () {

            // extraer fila a la que le dio click
            var row = $(this).closest('tr')

            // extraer la data del registro contenido en la fila

            var appointment = $('#tblAppointments').DataTable().row(row).data();

            // mapeo de los valores del objeto data con el  formulario
            $("#txtId").val(appointment.id);
            $("#patientId").val(appointment.patientId);
            $("#txtPatientName").val(appointment.patientName);
            $("#txtPatientLastName").val(appointment.patientLastName);
            $("#doctorId").val(appointment.doctorId);
            $("#doctorName").val(appointment.doctorName);
            $("#doctorLastName").val(appointment.doctorLastName);
            $("#serviceId").val(appointment.serviceId);
            $("#txtServiceName").val(appointment.serviceName);
            $("#branchId").val(appointment.branchId);
            $("#txtBranchName").val(appointment.branchName);
            $("#text").val(appointment.text);
            $("#status").val(appointment.status);

        })
    }

    function formatDate(dateString) {
        var date = new Date(dateString);
        var year = date.getFullYear();
        var month = ('0' + (date.getMonth() + 1)).slice(-2);
        var day = ('0' + date.getDate()).slice(-2);
        return year + '-' + month + '-' + day;
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
            var rowData = $('#tblBranch').DataTable().row(row).data();
            // Mapear los valores del objeto de datos con los campos del formulario
            $("#ServiceId").val(rowData.serviceId);
            $("#textServiceName").val(rowData.serviceName);
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