document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridYear,dayGridMonth,timeGridWeek'
        },
        initialView: 'dayGridYear',
        initialDate: '2023-01-12',
        editable: true,
        selectable: true,
        dayMaxEvents: true,
        events: 'api/appointment/RetrieveAll' // Endpoint para obtener eventos desde el servidor
    });

    calendar.render();
});

function AppointmentController() {
    this.ViewName = "Appointment";
    this.ApiService = "Appointment";

    // Método para ejecutar al inicio de la vista
    this.InitView = function () {
        console.log("appointment view init");

        this.loadTable();


    };

    this.loadTable = function () {
        var ca = new ControlActions();

        // Ruta del api a consumir servicio
        var urlService = ca.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Definir las columnas a extraer del json de  respuesta
        var columns = [
            { 'data': 'appointmentDate' },
            { 'data': 'motive' },
            { 'data': 'diagnostic' },
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
