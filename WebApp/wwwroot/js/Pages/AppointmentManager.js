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
        
        // Definir las columnas a extraer del json de  respuesta
        var columns = [
            { 'data': 'patientName' },
            { 'data': 'patientLastName' },
            { 'data': 'serviceName' },
            { 'data': 'branchName' }
            { 'data': 'startTime' }
            { 'data': 'text' }
            { 'data': 'status' }
        ];

        // Inicializar la tabla como un data table
        $("#tblAppointments").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });
    }
}

// Instanciamiento de la clase 
$(document).ready(function () {
    var vc = new AppointmentController();
    vc.InitView();
});
