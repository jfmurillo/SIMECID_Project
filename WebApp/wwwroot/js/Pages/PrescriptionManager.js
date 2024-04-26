function PrescriptionController() {
    this.ViewName = "Prescription";
    this.ApiService = "Prescription";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("Prescription view init");


        // Bind del click del botón create con la función correspondiente
        $("#btnCreate").click(function () {
            var vc = new PrescriptionController();
            vc.Create();
        });

        $("#btnUpdate").click(function () {
            var vc = new PrescriptionController();
            vc.Update();
        });
        $("#btnDelete").click(function () {
            var vc = new PrescriptionController();
            vc.Delete();
        });

        this.loadTable();

    };

    this.Create = function () {
        try {
            // Crear un dto
            let prescription = {};

            prescription.patientId = $("#patientId").val();
            prescription.doctorId = $("#doctorId").val();
            prescription.prescriptionName = $("#prescriptionName").val();
            prescription.medicationName = $("#medicationName").val();
            prescription.prescriptionDate = $("#prescriptionDate").val();
            prescription.recommendations = $("#recommendations").val();

            let profileImageInput = $("#profileImage")[0];
            if (!profileImageInput) {
                console.error("Profile image input not found");
                return;
            }

            let files = profileImageInput.files;
            if (!files || files.length === 0) {
                console.error("No files selected");
                return;
            }

            prescription.UploadedFile = files[0].name;

            // Invocar al API
            var ca = new ControlActions();
            var serviceRoute = this.ApiService + "/Create";

            ca.PostToAPI(serviceRoute, prescription, function () {
                console.log("Prescription Created --->" + JSON.stringify(prescription));
            });
        } catch (error) {
            console.error("Error occurred while creating prescription:", error);
        }
    };



    this.Update = function () {

        let prescription = {};

        prescription.id = $("#txtId").val();
        prescription.patientId = $("#patientId").val();
        prescription.doctorId = $("#doctorId").val();
        prescription.prescriptionName = $("#prescriptionName").val();
        prescription.medicationName = $("#medicationName").val();
        prescription.prescriptionDate = $("#prescriptionDate").val();
        prescription.recommendations = $("#recommendations").val();

        let profileImageInput = $("#profileImage")[0];
        if (!profileImageInput) {
            console.error("Profile image input not found");
            return;
        }

        let files = profileImageInput.files;
        if (!files || files.length === 0) {
            console.error("No files selected");
            return;
        }

        prescription.UploadedFile = files[0].name;

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Update";

        ca.PutToAPI(serviceRoute, prescription, function () {
            console.log("Prescription Updated --->" + JSON.stringify(prescription));
        })
    }

    this.Delete = function () {

        let prescription = {};

        prescription.id = $("#txtId").val();
        prescription.patientId = 0;
        prescription.doctorId = 0;
        prescription.prescriptionName = "default";
        prescription.medicationName = "default";
        prescription.prescriptionDate = "2024-04-26";
        prescription.recommendations = "default";

        prescription.UploadedFile = "default";

        // invocar al api

        var ca = new ControlActions();
        var serviceRoute = this.ApiService + "/Delete";

        ca.DeleteToAPI(serviceRoute, prescription, function () {
            console.log("Prescription Deleted --->" + JSON.stringify(prescription));
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
        columns[2] = { 'data': 'doctorId' }
        columns[3] = { 'data': 'prescriptionName' }
        columns[4] = { 'data': 'medicationName' }
        columns[5] = { 'data': 'prescriptionDate' }
        columns[6] = { 'data': 'recommendations' }
        columns[7] = { 'data': 'uploadedFile' }

        // Inicializar la tabla como un data table
        $("#tblPrescriptions").dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": columns
        });

        $('#tblPrescriptions tbody').on('click', 'tr', function () {

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
           

        });
    }
}

function formatDate(dateString) {
    var date = new Date(dateString);
    var year = date.getFullYear();
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var day = ('0' + date.getDate()).slice(-2);
    return year + '-' + month + '-' + day;
}

$(document).ready(function () {
    var vc = new PrescriptionController();
    vc.InitView();
});