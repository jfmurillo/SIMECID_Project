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